using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Entidades;
using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Regras;
using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Serviços;

namespace ProjetoEmTresCamadas.Pizzaria.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly IPizzaService _pizzaService;

        public PizzaController(IPizzaService pizzaService)
        {
            _pizzaService = pizzaService;
        }

        [HttpGet]
        public async Task<Pizza[]> GetPizzas()
        {
            List<Pizza> pizzas = await _pizzaService.ObterTodos();

            return pizzas.ToArray();
        }

        [HttpPost]
        public Pizza CriarPizza(Pizza pizza)
        {
            pizza = _pizzaService.Adicionar(pizza);
            return pizza;
        }

        [HttpPut]
        public async Task<Pizza> AtualizarPizza(Pizza pizza)
        {
            return await _pizzaService.AtualizarAsync(pizza); 
        }

        [HttpDelete]
        public async Task DeletePizza(int ID)
        {
            await _pizzaService.Deletar(ID);
        }
    
    }
}
