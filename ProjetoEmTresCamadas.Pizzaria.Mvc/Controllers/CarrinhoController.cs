using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ProjetoEmTresCamadas.Pizzaria.Mvc.Controllers
{
    public class CarrinhoController : Controller
    {
        public CarrinhoController()
        {

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
