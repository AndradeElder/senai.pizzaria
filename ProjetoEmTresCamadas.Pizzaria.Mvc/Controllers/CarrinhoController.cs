using Microsoft.AspNetCore.Mvc;

namespace ProjetoEmTresCamadas.Pizzaria.Mvc.Controllers
{
    public class CarrinhoController : Controller
    {
        public CarrinhoController()
        {
            
        }
        [HttpPost]
        public IActionResult AdicionarAoCarrinho(int pizzaId, int quantidade)
        {
            return View();
        }
    }
}
