using ProjetoEmTresCamadas.Clienteria.DAO.Dao;
using ProjetoEmTresCamadas.Pizzaria.DAO.Dao;
using ProjetoEmTresCamadas.Pizzaria.DAO.Regras;
using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Regras;
using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Serviços;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Criação objetos acesso a dados
builder.Services.AddScoped<IPizzaDao, PizzaDao>();
builder.Services.AddScoped<IClienteDao, ClienteDao>();
builder.Services.AddScoped<IPedidoClienteDao, PedidoClienteDao>();
builder.Services.AddScoped<IPedidoDao, PedidoDao>();

//Criação objetos de serviço
builder.Services.AddScoped<IPizzaService,PizzaService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
