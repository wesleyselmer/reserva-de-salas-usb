using api_reserva.Models;
using api_reserva.Services;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.Configure<ReservaDeSalasUsbDatabaseSettings>(
    builder.Configuration.GetSection("ReservaDeSalasUsbDatabase"));

builder.Services.AddSingleton<PeriodoService>();
builder.Services.AddSingleton<ReservaService>();
builder.Services.AddSingleton<SalaService>();
builder.Services.AddSingleton<UsuarioService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API Reserva de Salas - USB",
        Description = "Uma solução ASP.NET Core Web API para gerenciar o Banco de Dados MongoDB da aplicação de Reserva de Salas - USB",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Contato",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });

        // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(
        //options =>
            //{
               //options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
               //options.RoutePrefix = string.Empty;
    //}
    );
}

app.UseHttpsRedirection();


app.Run();