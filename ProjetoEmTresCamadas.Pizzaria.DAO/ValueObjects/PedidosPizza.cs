namespace ProjetoEmTresCamadas.Pizzaria.DAO.ValueObjects
{
    public class PedidosPizza : EntidadeBaseVo
    {
        public int PizzaId { get; set; }
        public int PedidoId { get; set; }
        public virtual PizzaVo Pizza { get; set; }
        public virtual PedidoVo Pedido { get; set; }
    }
}
