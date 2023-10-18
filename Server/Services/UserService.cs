using ECommerceWeb.DataAccess;
using ECommerceWeb.Shared.Configuracion;
using ECommerceWeb.Shared.Request;
using ECommerceWeb.Shared.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security;
using System.Security.Claims;
using System.Text;
using ECommerceWeb.Entities;
using ECommerceWeb.Repositories.Interfaces;
using ECommerceWeb.Shared;

namespace ECommerceWeb.Server.Services;

public class UserService : IUserService
{
    private readonly UserManager<IdentityUserECommerce> _userManager;
    private readonly ILogger<UserService> _logger;
    private readonly IOptions<AppSettings> _options;
    private readonly IClienteRepository _clienteRepository;

    public UserService(UserManager<IdentityUserECommerce> userManager, 
        ILogger<UserService> logger, 
        IOptions<AppSettings> options,
        IClienteRepository clienteRepository)
    {
        _userManager = userManager;
        _logger = logger;
        _options = options;
        _clienteRepository = clienteRepository;
    }

    public async Task<LoginDtoResponse> LoginAsync(LoginDtoRequest request)
    {
        var response = new LoginDtoResponse();

        try
        {
            var identity = await _userManager.FindByNameAsync(request.Usuario);

            if (identity is null)
                throw new SecurityException("Usuario no existe");

            if (!await _userManager.CheckPasswordAsync(identity, request.Password))
            {
                throw new SecurityException($"Clave incorrecta para el usuario {identity.UserName}");
            }

            var roles = await _userManager.GetRolesAsync(identity);

            var fechaExpiracion = DateTime.Now.AddDays(1);

            // Vamos a crear los claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, identity.NombreCompleto),
                new Claim(ClaimTypes.Email, identity.Email!),
                new Claim(ClaimTypes.Expiration, fechaExpiracion.ToLongDateString()),
            };

            claims.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)));
            response.Roles = roles;

            // Creamos el JWT
            var llaveSimetrica = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Jwt.SecretKey));
            var credenciales = new SigningCredentials(llaveSimetrica, SecurityAlgorithms.HmacSha256);

            var header = new JwtHeader(credenciales);

            var payload = new JwtPayload(
                issuer: _options.Value.Jwt.Emisor,
                audience: _options.Value.Jwt.Audiencia,
                notBefore: DateTime.Now,
                claims: claims,
                expires: fechaExpiracion);

            var token = new JwtSecurityToken(header, payload);
            response.Token = new JwtSecurityTokenHandler().WriteToken(token);
            response.NombreCompleto = identity.NombreCompleto;
            response.Exito = true;

            _logger.LogInformation("Se creó el JWT de forma satisfactoria");
        }
        catch (SecurityException ex)
        {
            response.MensajeError = ex.Message;
            _logger.LogWarning(ex, "Error de seguridad {Message}", ex.Message);
        }
        catch (Exception ex)
        {
            response.MensajeError = "Error de autenticacion";
            _logger.LogCritical(ex, "Error al autenticar {Message}", ex.Message);
        }

        return response;
    }

    public async Task<BaseResponse> RegisterAsync(RegistrarUsuarioDto request)
    {
        var response = new BaseResponse();
        try
        {
            var user = new IdentityUserECommerce()
            {
                NombreCompleto = request.NombreCompleto,
                FechaNacimiento = request.FechaNacimiento,
                Direccion = request.Direccion,
                UserName = request.NombreUsuario,
                Email = request.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Constantes.RolCliente);

                var cliente = new Cliente
                {
                    Nombres = request.NombreCompleto.Split(" ", StringSplitOptions.RemoveEmptyEntries).First(),
                    Apellidos = request.NombreCompleto.Split(" ", StringSplitOptions.RemoveEmptyEntries).Last(),
                    Email = request.Email,
                    FechaNacimiento = request.FechaNacimiento,
                    TipoClienteId = 1
                };

                await _clienteRepository.AddAsync(cliente);
            }
            else
            {
                var sb = new StringBuilder();
                foreach (var identityError in result.Errors)
                {
                    sb.AppendFormat("{0} ", identityError.Description);
                }

                response.MensajeError = sb.ToString();
                sb.Clear();
            }

            response.Exito = result.Succeeded;
        }
        catch (Exception ex)
        {
            response.MensajeError = "Error al registrar";
            _logger.LogCritical(ex, "{MensajeError} {Message}", response.MensajeError, ex.Message);
        }
        return response;
    }
}