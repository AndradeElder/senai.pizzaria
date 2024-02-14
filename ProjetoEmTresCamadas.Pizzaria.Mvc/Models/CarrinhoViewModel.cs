using Microsoft.AspNetCore.Http.Features;
using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Entidades;

namespace ProjetoEmTresCamadas.Pizzaria.Mvc.Models
{
    public class CarrinhoViewModel
    {
        public CarrinhoViewModel()
        {
            Items = new List<Item>();

        }
        public List<Item> Items { get; set; }
        public double Total { 
            get
            {
                return Items.Sum( x => x.ValorTotal);
            }
        }

        public void ConvertPizzas(Pizza[]? pizzas)
        {
            if (pizzas == null || pizzas.Length == 0)
            {
                // Handle case where there are no pizzas
                Items = new List<Item>();
                return;
            }

            // Group pizzas by their PizzaId
            var groupedPizzas = pizzas.GroupBy(p => p.Id);

            foreach (var group in groupedPizzas)
            {
                int totalQuantity = group.Count(); 
                Item item = new Item
                {
                    Id = group.Key,
                    Pizza = group.First(), // Assuming you want to take the first pizza in the group
                    Quantidade = totalQuantity
                };
                Items.Add(item);
            }
        }
    }
    public class Item
    {
        public int Id { get; set; }
        public Pizza Pizza { get; set; }
        public int Quantidade { get; set; }
        public double ValorTotal
        {
            get
            {
                return Pizza.Valor * Quantidade;

            }
        }
    }
}
