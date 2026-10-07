using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<RegistroDAO>();
builder.Services.AddScoped<RegistroService>();
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

app.MapPost("/login/auth", () =>
{
    TokenService authService = new TokenService(builder.Configuration);
    return authService.GenerateToken(null);
});

var registrosGroup = app.MapGroup("/registros");

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
