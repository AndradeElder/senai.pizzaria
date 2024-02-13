using Microsoft.EntityFrameworkCore;
using ProjetoEmTresCamadas.Pizzaria.DAO.Dao;
using ProjetoEmTresCamadas.Pizzaria.DAO.Dao.Ef;
using ProjetoEmTresCamadas.Pizzaria.DAO.Regras;
using ProjetoEmTresCamadas.Pizzaria.DAO.Settings;
using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Regras;
using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Servi�os;
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
// Cria��o objetos acesso a dados
builder.Services.AddScoped<IPizzaDao, PizzaRepository>();
builder.Services.AddScoped<IClienteDao, ClienteRepository>();
builder.Services.AddScoped<IPedidoDao, PedidoRepository>();

//Cria��o objetos de servi�o
builder.Services.AddScoped<IPizzaService,PizzaService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();

builder.Services.AddLogging(configure =>
{
    configure.AddConsole();
    configure.AddSerilog();
});


var app = builder.Build();

app.Logger.LogInformation("Ola eu sou informa��o");


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
