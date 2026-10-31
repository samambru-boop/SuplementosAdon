using Microsoft.EntityFrameworkCore;
using SuplementosAdon.API.Data;
using SuplementosAdon.API.Interfaces;
using SuplementosAdon.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.WriteIndented = true;
    });

// Base de datos PostgreSQL
builder.Services.AddDbContext<SuplementosAdonContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("ConexionPostgreSQL")
    ));

// Servicios
builder.Services.AddScoped<ICompraService, CompraService>();
builder.Services.AddScoped<IVentaService, VentaService>();
builder.Services.AddScoped<IReporteService, ReporteService>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();