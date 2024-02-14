namespace ProjetoEmTresCamadas.Pizzaria.DAO.ValueObjects
{
    public class PedidoVo : EntidadeBaseVo
    {
        public PedidoVo() { }


        public DateTime DataSolicitacao { get; set; }
        public DateTime? DataPreparacao { get; set; }
        public DateTime? DataSaidaEntrega { get; set; }
        public DateTime? DataFinalizacaoEntrega { get; set; }

        public int ClienteId { get; set; }
        public virtual ClienteVo Cliente { get; set; }

        public ICollection<PizzaVo> Pizzas { get; set; }
    }
}
