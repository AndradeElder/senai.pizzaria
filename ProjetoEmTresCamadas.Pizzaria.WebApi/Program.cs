using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProjetoEmTresCamadas.Clienteria.DAO.Dao;
using ProjetoEmTresCamadas.Pizzaria.DAO.Dao;
using ProjetoEmTresCamadas.Pizzaria.DAO.Dao.Ef;
using ProjetoEmTresCamadas.Pizzaria.DAO.Dao.Memory;
using ProjetoEmTresCamadas.Pizzaria.DAO.Regras;
using ProjetoEmTresCamadas.Pizzaria.DAO.Settings;
using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Regras;
using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Serviços;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var logger = new LoggerConfiguration()
    .ReadFrom.
    Configuration(builder.Configuration)
    .CreateLogger();

Log.Logger = logger;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));

var connectionStrings = builder.Configuration.GetSection("ConnectionStrings").GetValue<string>("Master");

Console.WriteLine(connectionStrings);

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionStrings));
// Criação objetos acesso a dados
builder.Services.AddScoped<IPizzaDao, PizzaDao>();
builder.Services.AddScoped<IClienteDao, ClienteDaoInMemory>();
builder.Services.AddScoped<IPedidoClienteDao, PedidoClienteDao>();
builder.Services.AddScoped<IPedidoDao, PedidoDao>();

//Criação objetos de serviço
builder.Services.AddScoped<IPizzaService,PizzaService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();

builder.Services.AddLogging(configure =>
{
    configure.AddConsole();
    configure.AddSerilog();
});


var app = builder.Build();

app.Logger.LogInformation("Ola eu sou informação");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
