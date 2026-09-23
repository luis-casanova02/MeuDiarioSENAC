var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<RegistroService>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/registros", (RegistroService registroService) =>
{
    List<Registro> registros = registroService.ListarTodos();
    return registros;
});

app.Run();
