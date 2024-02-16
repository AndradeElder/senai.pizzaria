using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Entidades;

namespace ProjetoEmTresCamadas.Pizzaria.Mvc.Models
{
    public class PizzasViewModel
    {
        public PizzasViewModel()
        {
            Pizzas = new List<Pizza>();
        }
        public List<Pizza> Pizzas { get; set; }
    }
}
