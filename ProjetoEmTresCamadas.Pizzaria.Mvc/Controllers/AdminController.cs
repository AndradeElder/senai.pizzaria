using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Entidades;
using System.Net.Http;

namespace ProjetoEmTresCamadas.Pizzaria.Mvc.Controllers
{
    [Authorize(Roles = "manager")]
    public class AdminController : Controller
    {
        public IConfiguration Configuration { get; }

        private readonly HttpClient _httpClient;
        private readonly string PizzaApiEndpoint;

        public AdminController(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            Configuration = configuration;
            PizzaApiEndpoint = Configuration["PizzaApiEndpoint"] + "/api/pizza";
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastro
            ([Bind("Sabor,TamanhoDePizza,Descricao,Valor")] Pizza pizza)
        {
            using (var response = await _httpClient.PostAsJsonAsync<Pizza>(PizzaApiEndpoint, pizza))
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
