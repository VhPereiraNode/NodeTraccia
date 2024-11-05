using Microsoft.OpenApi.Models;
using NodeTraccia.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// `SwaggerDoc` aggiunge una specifica Swagger per la documentazione dell'API.
// Questo metodo configura il titolo, la versione e altre informazioni rilevanti,
// rendendo disponibile una pagina interattiva della documentazione dell'API.
// Utile per descrivere gli endpoint, i parametri e i codici di risposta.
builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "NodeTraccia", Version = "v1" });
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
    });

// `AddSingleton` registra un servizio in modalità singleton, il che significa che
// viene creata un'unica istanza del servizio per tutta la durata dell'applicazione.
// Ogni richiesta del servizio restituirà la stessa istanza.
// Utile per servizi che mantengono uno stato globale o risorse condivise.
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
