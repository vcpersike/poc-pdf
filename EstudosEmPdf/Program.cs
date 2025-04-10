using EstudosEmPdf.Services;

var builder = WebApplication.CreateBuilder(args);

// Adicionar controllers
builder.Services.AddControllers();

// Registrar nossos serviços
builder.Services.AddSingleton<HtmlService>();

// Adicionar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "EstudosEmPdf API", Version = "v1" });
});

var app = builder.Build();



// Configurar pipeline de requisição HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EstudosEmPdf API v1"));
}
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();