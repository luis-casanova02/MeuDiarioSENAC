using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<RegistroDAO>();
builder.Services.AddScoped<RegistroService>();
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
var jwtSettings = builder.Configuration.GetSection("JWTSettings");
var chaveSecreta = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "JwtBearer";
    options.DefaultChallengeScheme = "JwtBearer";
}).AddJwtBearer("JwtBearer", options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(chaveSecreta),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapPost("/login/auth", ([FromBody] Usuario usuario) =>
{
    TokenService authService = new TokenService(builder.Configuration);
    return authService.GenerateToken(usuario);
});

var registrosGroup = app.MapGroup("/registros").RequireAuthorization();

registrosGroup.MapPost("/", ([FromBody] Registro registro, RegistroService registroService) =>
{
    registroService.InserirRegistro(registro);
    return Results.Created($"/registros/{registro.Id}", registro);
});

app.MapGet("/registros", (RegistroService registroService) =>
{
    List<Registro> registros = registroService.ListarTodos();
    return registros;
});

app.Run();
