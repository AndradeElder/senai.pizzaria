using Microsoft.Extensions.Logging;
using ProjetoEmTresCamadas.Pizzaria.DAO.Dao;
using ProjetoEmTresCamadas.Pizzaria.DAO.ValueObjects;
using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Entidades;
using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Regras;

namespace ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Serviços;
public class PizzaService : IPizzaService

{
    private readonly IPizzaDao PizzaDao;

    public readonly ILogger<PizzaService> Logger;

    public PizzaService(
        IPizzaDao pizzaDao,
        ILogger<PizzaService> logger
        )
    {
        PizzaDao = pizzaDao;
        Logger = logger;
    }

    public Pizza Adicionar(Pizza objeto)
    {

        PizzaVo pizzaVo = objeto.ToPizzaVo();
        objeto.Id = PizzaDao.CriarRegistro(pizzaVo);
        Logger.LogInformation("Pizza adicionada com sucesso");
        return objeto;
    }

    public async Task<List<Pizza>> ObterTodos()
    {
        List<Pizza> pizzas = new ();
        List<PizzaVo> pizzasBanco = PizzaDao.ObterRegistros();
        Logger.LogInformation("Pizzas do banco");
        foreach (PizzaVo pizzaVo in pizzasBanco)
        {
            Pizza pizza = new Pizza()
            {
                Descricao = pizzaVo.Descricao,
                Sabor = pizzaVo.Sabor,
                TamanhoDePizza = (TamanhoDePizza)pizzaVo.TamanhoDePizza,
                Valor = pizzaVo.Valor,
                Id = pizzaVo.Id,
            };
            pizzas.Add(pizza);
        }
        return pizzas;
    }

    public async Task<Pizza> AtualizarAsync(Pizza objeto)
    {
        PizzaVo pizzaVo = objeto.ToPizzaVo();
        await PizzaDao.AtualizarRegistro(pizzaVo);

        objeto = (await Obter(objeto.Id));

        Logger.LogInformation("Pizza Atualizada com sucesso");
        return objeto;
    }

    public async Task Deletar(int ID)
    {
        await PizzaDao.DeletarRegistro(ID);
    }

    public async Task<List<Pizza>> ObterTodos(int[] id)
    {
        return
            (
                await ObterTodos())
                    .FindAll(x => id.Contains(x.Id)
            );
    }

    public async Task<Pizza> Obter(int id)
    {
        PizzaVo pizzaVo = await PizzaDao.ObterRegistro(id);
        Pizza pizza = new ()
        {
            Descricao = pizzaVo.Descricao,
            Sabor = pizzaVo.Sabor,
            TamanhoDePizza = (TamanhoDePizza)pizzaVo.TamanhoDePizza,
            Valor = pizzaVo.Valor,
            Id = pizzaVo.Id,
        };
        return pizza;
    }
}
