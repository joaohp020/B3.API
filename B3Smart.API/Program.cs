using B3Smart.API.Interfaces;
using B3Smart.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Conexão com a aplicação FrontEnd
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policyBuilder =>
    {
        policyBuilder
            .WithOrigins("http://localhost:4200")  // Permitir o domínio específico da sua aplicação Angular
            .AllowAnyMethod()  // Permitir todos os métodos HTTP (GET, POST, etc.)
            .AllowAnyHeader()  // Permitir qualquer cabeçalho
            .AllowCredentials()  // Se estiver utilizando autenticação com cookies, ou se precisar permitir credenciais
            .SetIsOriginAllowed(origin => true)  // Permite requests de qualquer origem
            .WithExposedHeaders("Access-Control-Allow-Origin")  // Expõe cabeçalhos CORS necessários
            .Build();
    });
});

// Adicionar serviços ao container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICdbService, CdbService>();

var app = builder.Build();

// Configurar o pipeline de requisições HTTP
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
