using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoEmTresCamadas.Pizzaria.Mvc.Services;
using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Entidades;

namespace ProjetoEmTresCamadas.Pizzaria.Mvc.Controllers
{
    [Authorize(Roles = "manager")]
    public class AdminController : Controller
    {
        public PizzasApiService PizzaApiService { get; }
        public IConfiguration Configuration { get; }

        private readonly HttpClient _httpClient;
        private readonly string PizzaApiEndpoint;
        private readonly string ClienteApiEndpoint;

        private const int pageSize = 10;

        public AdminController(PizzasApiService pizzaApiService, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            PizzaApiService = pizzaApiService;
            Configuration = configuration;
            PizzaApiEndpoint = Configuration["PizzaApiEndpoint"] + "/api/pizza";
            ClienteApiEndpoint = configuration["PizzaApiEndpoint"] + "/api/cliente";
        }


        public async Task<IActionResult> IndexAsync(int page = 1)
        {
            List<Pizza> pizzas = new List<Pizza>();
            pizzas.AddRange(await PizzaApiService.Get());

            int totalCount = pizzas.Count;
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            if (totalCount < (pageSize * (page - 1)))
            {
                page = 1;
            }
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            var pizzasDaPagina = pizzas.Skip(pageSize * (page - 1)).Take(pageSize).ToArray();

            return View(pizzasDaPagina);
        }

        public async Task<IActionResult> Clientes(int page = 1)
        {


            List<Cliente> clientes = [.. await _httpClient.GetFromJsonAsync<Cliente[]>(ClienteApiEndpoint)];

            for (int i = 0; i < 100; i++)
            {
                clientes.Add(new Cliente() { Id = i, Nome = $"Exemplo {i}" });
            }

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

    }
}
