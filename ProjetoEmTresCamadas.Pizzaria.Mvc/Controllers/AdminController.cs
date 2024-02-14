using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Entidades;

namespace ProjetoEmTresCamadas.Pizzaria.Mvc.Controllers
{
    [Authorize(Roles = "manager")]
    public class AdminController : Controller
    {
        public IConfiguration Configuration { get; }

        private readonly HttpClient _httpClient;
        private readonly string PizzaApiEndpoint;
        private readonly string ClienteApiEndpoint;

        public AdminController(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            Configuration = configuration;
            PizzaApiEndpoint = Configuration["PizzaApiEndpoint"] + "/api/pizza";
            ClienteApiEndpoint = configuration["PizzaApiEndpoint"] + "/api/cliente";
        }


        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Clientes()
        {
            var clientes = await _httpClient.GetFromJsonAsync<Cliente[]>(ClienteApiEndpoint);

            return View(clientes);
        }

        public async Task<IActionResult> Pizzas()
        {
            var pizzas = await _httpClient.GetFromJsonAsync<Pizza[]>(PizzaApiEndpoint);

            return View(pizzas);
        }

    }
}
