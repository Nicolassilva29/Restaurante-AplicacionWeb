using Application.Interfaces.IComanda;
using Application.Interfaces.IComandaMercaderia;
using Application.Interfaces.IFormaEntrega;
using Application.Interfaces.IMercaderia;
using Application.Interfaces.ITipoMercaderia;
using Application.UseCase;
using Infraestructura;
using Infraestructura.Command;
using Infraestructura.Querys;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//custom

var connectionString = builder.Configuration["ConnectionString"];
builder.Services.AddDbContext<RestauranteContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<ITipoMercaderiaService, TipoMercaderiaService>();
builder.Services.AddScoped<ITipoMercaderiaCommand, TipoMercaderiaCommand>();
builder.Services.AddScoped<ITipoMercaderiaQuery, TipoMercaderiaQuery>();

builder.Services.AddScoped<IMercaderiaService, MercaderiaService>();
builder.Services.AddScoped<IMercaderiaCommand, MercaderiaCommand>();
builder.Services.AddScoped<IMercaderiaQuery, MercaderiaQuery>();

builder.Services.AddScoped<IComandaMercaderiaService, ComandaMercaderiaService>();
builder.Services.AddScoped<IComandaMercaderiaCommand, ComandaMercaderiaCommand>();
builder.Services.AddScoped<IComandaMercaderiaQuery, ComandaMercaderiaQuery>();

builder.Services.AddScoped<IComandaService, ComandaService>();
builder.Services.AddScoped<IComandaCommand, ComandaCommand>();
builder.Services.AddScoped<IComandaQuery, ComandaQuery>();

builder.Services.AddScoped<IFormaEntregaService, FormaEntregaService>();
builder.Services.AddScoped<IFormaEntregaCommand, FormaEntregaCommand>();
builder.Services.AddScoped<IFormaEntregaQuery, FormaEntregaQuery>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowAll");

app.MapControllers();

app.Run();
