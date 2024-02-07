using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoEmTresCamadas.Pizzaria.Mvc.Models;
using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Entidades;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProjetoEmTresCamadas.Pizzaria.Mvc.Controllers
{
    [Authorize(Roles = "simples")]
    public class PizzasController : Controller
    {
        public PizzasViewModel PizzasViewModel { get; set; }
        public IConfiguration Configuration { get; }

        private readonly HttpClient _httpClient;
        private readonly string PizzaApiEndpoint;

        public PizzasController(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            Configuration = configuration;
            PizzaApiEndpoint = Configuration["PizzaApiEndpoint"] + "/api/pizza";
        }

        //private List<PizzaModel> ObterPizzas()
        //{
        //    //// Lógica para obter dados de pizzas do seu sistema
        //    //// Pode ser de um banco de dados, serviço, etc.
        //    //// Aqui estou usando dados fictícios para ilustrar
        //    //return new List<Pizza>
        //    //{
        //    //    new Pizza { Id = 1, Sabor = "Margherita", TamanhoDePizza = "Média", Descricao = "Pizza clássica com molho de tomate, queijo e manjericão.", Valor = 20.99, ImageUrl = "/images/margherita.jpg", Quantity = 0 },
        //    //    new Pizza { ID = 2, Sabor = "Pepperoni", TamanhoDePizza = "Grande", Descricao = "Pizza com pepperoni, queijo e molho de tomate.", Valor = 23.99, ImageUrl = "/images/pepperoni.jpg", Quantity = 0 },
        //    //    // Adicione mais pizzas conforme necessário
        //    //};
        //}

        public async Task<IActionResult> IndexAsync()
        {
            PizzasViewModel pizzaViewModel = new PizzasViewModel();

            var pizzas = await _httpClient.GetFromJsonAsync<Pizza[]>(PizzaApiEndpoint);

            pizzaViewModel.Pizzas.AddRange(pizzas);
            return View(pizzaViewModel);
        }

    }
}
