using api_reserva.Models;
using api_reserva.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.Configure<ReservaDeSalasUsbDatabaseSettings>(
    builder.Configuration.GetSection("ReservaDeSalasUsbDatabase"));

builder.Services.AddSingleton<PeriodoService>();
builder.Services.AddSingleton<ReservaService>();
builder.Services.AddSingleton<SalaService>();
builder.Services.AddSingleton<UsuarioService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();