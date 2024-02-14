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

        private const int pageSize = 10;

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

        public async Task<IActionResult> Clientes(int page = 1)
        {


            List<Cliente> clientes = [.. await _httpClient.GetFromJsonAsync<Cliente[]>(ClienteApiEndpoint)];

            int totalCount = clientes.Count;
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            if (totalCount < (pageSize * (page - 1)))
            {
                page = 1;
            }
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            var clientesDaPagina = clientes.Skip(pageSize * (page - 1)).Take(pageSize).ToArray();

            return View(clientesDaPagina);
        }

        public async Task<IActionResult> Pizzas()
        {
            var pizzas = await _httpClient.GetFromJsonAsync<Pizza[]>(PizzaApiEndpoint);

            return View(pizzas);
        }

    }
}
