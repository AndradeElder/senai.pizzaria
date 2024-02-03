namespace ProjetoEmTresCamadas.Pizzaria.Mvc.Models
{
    public class PizzasViewModel
    {       
        public List<PizzaModel> Pizzas { get; set; }
    }
    public class PizzaModel
    {
        public int ID { get; set; }
        public string Sabor { get; set; }
        public string TamanhoDePizza { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
    }
}
