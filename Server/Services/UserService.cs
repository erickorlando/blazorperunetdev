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

namespace ECommerceWeb.Server.Services;

public class UserService : IUserService
{
    private readonly UserManager<IdentityUserECommerce> _userManager;
    private readonly ILogger<UserService> _logger;
    private readonly IOptions<AppSettings> _options;

    public UserService(UserManager<IdentityUserECommerce> userManager, 
        ILogger<UserService> logger, 
        IOptions<AppSettings> options)
    {
        _userManager = userManager;
        _logger = logger;
        _options = options;
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
}