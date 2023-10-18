using System.Text;
using ECommerceWeb.DataAccess;
using ECommerceWeb.DataAccess.Data;
using ECommerceWeb.Server.DependencyInjection;
using ECommerceWeb.Shared.Configuracion;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// Leer el archivo de configuracion y lo traslada a un objeto fuertemente tipado
builder.Services.Configure<AppSettings>(builder.Configuration);

// Add services to the container.

//builder.Services.AddControllersWithViews(); // ASP.NET MVC
//builder.Services.AddRazorPages(); // ASP.NET RAZOR PAGES
builder.Services.AddDbContext<ECommerceDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ECommerceDb"));
    options.EnableSensitiveDataLogging();
});

// Configuramos ASP.NET Identity
builder.Services.AddIdentity<IdentityUserECommerce, IdentityRole>(policies =>
{
    policies.Password.RequireDigit = false;
    policies.Password.RequireLowercase = false;
    policies.Password.RequireUppercase = true;
    policies.Password.RequireNonAlphanumeric = true;
    policies.Password.RequiredLength = 8;

    policies.User.RequireUniqueEmail = true;

    // TODO: Politica de bloqueo de cuentas
})
    .AddEntityFrameworkStores<ECommerceDbContext>()
    .AddDefaultTokenProviders();

// Patron Builder
builder.Services.AddRepositories()
    .AddAutoMappers()
    .AddServices();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "ECommerce API REST",
        Version = "v1"
    });
});

// Configuramos el contexto de seguridad del API
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    var secretKey = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"] ??
                                           throw new InvalidOperationException("No se configuro el SecretKey"));

    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Emisor"],
        ValidAudience = builder.Configuration["Jwt:Audiencia"],
        IssuerSigningKey = new SymmetricSecurityKey(secretKey)
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ECommerce API v1");
        c.RoutePrefix = "swagger";
        c.DocumentTitle = "Documentacion de la API REST";
        c.DocExpansion(DocExpansion.List);
    });
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
// Autenticacion
app.UseAuthentication();
// Autorizacion (Permisos)
app.UseAuthorization();

//app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

using (var scope = app.Services.CreateScope())
{
    await UserDataSeeder.Seed(scope.ServiceProvider);
}

app.Run();
