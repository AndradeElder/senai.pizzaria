using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoEmTresCamadas.Pizzaria.Mvc.Models;
using ProjetoEmTresCamadas.Pizzaria.Mvc.Services;
using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Entidades;

namespace ProjetoEmTresCamadas.Pizzaria.Mvc.Controllers
{

    public class PizzasController : Controller
    {
        public PizzasViewModel PizzasViewModel { get; set; }
        public PizzasApiService PizzaApiService { get; set; }

        public PizzasController(PizzasApiService pizzaApiService)
        {
            PizzaApiService = pizzaApiService;
        }

        [Authorize(Roles = "simples")]
        public async Task<IActionResult> IndexAsync()
        {
            PizzasViewModel pizzaViewModel = new PizzasViewModel();
            pizzaViewModel.Pizzas.AddRange(await PizzaApiService.Get());
            return View(pizzaViewModel);
        }

        [HttpGet]
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> Cadastro()
        {
            return View(new Pizza());
        }


        [HttpPost]
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> Cadastro
            ([Bind("Sabor,TamanhoDePizza,Descricao,Valor")] Pizza pizza)
        {
            if (ModelState.IsValid is false)
            {
                return View(pizza);
            }
            string returnUrl = Request.Headers["Referer"].ToString();

            object result = await PizzaApiService.Create(pizza);
            if (string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return View(pizza);
        }

    }
}
