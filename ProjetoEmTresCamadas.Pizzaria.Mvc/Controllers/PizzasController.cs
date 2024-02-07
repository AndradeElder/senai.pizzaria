using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoEmTresCamadas.Pizzaria.Mvc.Models;
using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Entidades;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProjetoEmTresCamadas.Pizzaria.Mvc.Controllers
{
    [Authorize]
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

            using (var response = await _httpClient.GetAsync(PizzaApiEndpoint))
            {
                if (response.IsSuccessStatusCode)
                {
                    var jsonPizzas = await response.Content.ReadAsStringAsync();
                    Pizza[] pizzas = JsonSerializer.Deserialize<Pizza[]>(jsonPizzas);
                    pizzaViewModel.Pizzas.AddRange(pizzas);
                }
            }
            

            return View(pizzaViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastro
            ([Bind("Sabor,TamanhoDePizza,Descricao,Valor")] Pizza pizza)
        {
            var requestBody = $"{{ \"id\": 0,\"Sabor\": \"{pizza.Sabor}\"," +
              $" \"TamanhoDePizza\": {(int)pizza.TamanhoDePizza}," +
              $" \"Descricao\": \"{pizza.Descricao}\"," +
              $" \"Valor\": {pizza.Valor}" +
              $"}}";
            var content2 = new StringContent("{\r\n  \"id\": 0,\r\n  \"sabor\": \"sadsa\",\r\n  \"tamanhoDePizza\": 0,\r\n  \"descricao\": \"dsadsa\",\r\n  \"valor\": 23\r\n}", null, "application/json");

            var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            using (var response = await _httpClient.PostAsync(PizzaApiEndpoint, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
    }
}
