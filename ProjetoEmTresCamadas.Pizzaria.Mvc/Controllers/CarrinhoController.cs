using Microsoft.AspNetCore.Mvc;
using ProjetoEmTresCamadas.Pizzaria.Mvc.Models;
using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Entidades;
using System.Text.Json;

namespace ProjetoEmTresCamadas.Pizzaria.Mvc.Controllers
{
    public class CarrinhoController : Controller
    {
        public IConfiguration Configuration { get; }

        private readonly HttpClient _httpClient;
        private readonly string PizzaApiEndpoint;

        public CarrinhoController(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            Configuration = configuration;
            PizzaApiEndpoint = Configuration["PizzaApiEndpoint"] + "/api/pizza";
        }

        [HttpPost]
        public IActionResult AdicionarAoCarrinho(int pizzaId)
        {
            string returnUrl = Request.Headers["Referer"].ToString();
            List<int> pizzas = GetPizzas(HttpContext);
            pizzas.Add(pizzaId);
            
            HttpContext.Session.SetString("Pedidos", JsonSerializer.Serialize(pizzas.ToArray()));
            return Redirect(returnUrl);
        }

        public async Task<IActionResult> Index()
        {
            CarrinhoViewModel carrinhoViewModel = new CarrinhoViewModel();
            var ids = GetPizzas(HttpContext);

            var idParameter = string.Join(",", ids); 
            var endpointWithIds = $@"{PizzaApiEndpoint}?ids={idParameter}";

            var pizzas = await _httpClient.GetFromJsonAsync<Pizza[]>(endpointWithIds);
            carrinhoViewModel.ConvertPizzas(pizzas);

            return View(carrinhoViewModel);
        }

        public static List<int> GetPizzas(HttpContext context)
        {
            List<int> pizzas = new List<int>();
            var data = context.Session.GetString("Pedidos");

            if (!string.IsNullOrEmpty(data))
            {
                pizzas.AddRange(JsonSerializer.Deserialize<int[]>(data));
            }
            return pizzas;
        }
    }
}
