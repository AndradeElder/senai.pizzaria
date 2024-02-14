using ProjetoEmTresCamadas.Pizzaria.DAO.ValueObjects;

namespace ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Entidades
{
    public class Pedido : EntidadeBase
    {
        public Pedido()
        {
            DataSolicitacao = DateTime.Now;
            Pizzas = new List<Pizza>();
        }
        public Cliente Cliente { get; set; }
        public List<Pizza> Pizzas { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public DateTime? DataPreparacao { get; set; }
        public DateTime? DataSaidaEntrega { get; set; }
        public DateTime? DataFinalizacaoEntrega { get; set; }

        public PedidoVo ToPedidoClienteVo()
        {
            return new PedidoVo()
            {
                DataSolicitacao = DataSolicitacao,
                DataFinalizacaoEntrega = DataFinalizacaoEntrega,
                DataPreparacao = DataPreparacao,
                Id = this.Id,
                DataSaidaEntrega = DataSaidaEntrega,
                ClienteId = Cliente.Id,
                //Cliente = Cliente.ToVo(),
                Pizzas = ToPizzasVo(Pizzas)
            };
        }

        private ICollection<PizzaVo> ToPizzasVo(List<Pizza> pizzas)
        {
            var pizzasVo = new HashSet<PizzaVo>();
            pizzas.ForEach(pizza => pizzasVo.Add(new PizzaVo() { Id = pizza.Id }));
            return pizzasVo;
        }
    }
}
