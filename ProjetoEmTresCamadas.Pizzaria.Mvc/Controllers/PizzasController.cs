using Microsoft.AspNetCore.Mvc;
using ProjetoEmTresCamadas.Pizzaria.Mvc.Models;

namespace ProjetoEmTresCamadas.Pizzaria.Mvc.Controllers
{
    public class PizzasController : Controller
    {
        public PizzasViewModel PizzasViewModel { get; set; }
        public PizzasController()
        {
            PizzasViewModel = new PizzasViewModel();
            PizzasViewModel.Pizzas = ObterPizzas();

        }

        private List<PizzaModel> ObterPizzas()
        {
            // Lógica para obter dados de pizzas do seu sistema
            // Pode ser de um banco de dados, serviço, etc.
            // Aqui estou usando dados fictícios para ilustrar
            return new List<PizzaModel>
            {
                new PizzaModel { ID = 1, Sabor = "Margherita", TamanhoDePizza = "Média", Descricao = "Pizza clássica com molho de tomate, queijo e manjericão.", Valor = 20.99, ImageUrl = "/images/margherita.jpg", Quantity = 0 },
                new PizzaModel { ID = 2, Sabor = "Pepperoni", TamanhoDePizza = "Grande", Descricao = "Pizza com pepperoni, queijo e molho de tomate.", Valor = 23.99, ImageUrl = "/images/pepperoni.jpg", Quantity = 0 },
                // Adicione mais pizzas conforme necessário
            };
        }

        public IActionResult Index()
        {
            return View(PizzasViewModel);
        }
    }
}
