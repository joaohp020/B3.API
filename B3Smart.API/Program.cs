using B3Smart.API.Interfaces;
using B3Smart.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Conex�o com a aplica��o FrontEnd
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policyBuilder =>
    {
        policyBuilder
            .WithOrigins("http://localhost:4200")  // Permitir o dom�nio espec�fico da sua aplica��o Angular
            .AllowAnyMethod()  // Permitir todos os m�todos HTTP (GET, POST, etc.)
            .AllowAnyHeader()  // Permitir qualquer cabe�alho
            .AllowCredentials()  // Se estiver utilizando autentica��o com cookies, ou se precisar permitir credenciais
            .SetIsOriginAllowed(origin => true)  // Permite requests de qualquer origem
            .WithExposedHeaders("Access-Control-Allow-Origin")  // Exp�e cabe�alhos CORS necess�rios
            .Build();
    });
});

// Adicionar servi�os ao container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICdbService, CdbService>();

var app = builder.Build();

// Configurar o pipeline de requisi��es HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Habilitar CORS
app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
