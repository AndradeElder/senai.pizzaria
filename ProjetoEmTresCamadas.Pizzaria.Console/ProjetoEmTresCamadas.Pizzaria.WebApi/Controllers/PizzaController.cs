using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Entidades;
using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Serviços;

namespace ProjetoEmTresCamadas.Pizzaria.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly PizzaService _pizzaService;

        public PizzaController(PizzaService pizzaService)
        {
            _pizzaService = pizzaService;
        }

        [HttpGet]
        public Pizza[] GetPizzas()
        {
            List<Pizza> pizzas = _pizzaService.ObterTodos();

            return pizzas.ToArray();
        }

        [HttpPost]
        public Pizza CriarPizza(Pizza pizza)
        {
            pizza = _pizzaService.Adicionar(pizza);
            return pizza;
        }

        [HttpPut]
        public Pizza AtualizarPizza(Pizza pizza)
        {
            pizza = _pizzaService.AtualizarAsync(pizza).Result;

            return pizza;
        }

    }
}
