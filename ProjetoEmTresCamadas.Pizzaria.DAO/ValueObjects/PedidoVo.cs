namespace ProjetoEmTresCamadas.Pizzaria.DAO.ValueObjects
{
    public class PedidoVo : EntidadeBaseVo
    {
        public DateTime DataSolicitacao { get; set; }
        public DateTime? DataPreparacao { get; set; }
        public DateTime? DataSaidaEntrega { get; set; }
        public DateTime? DataFinalizacaoEntrega { get; set; }

        public int ClienteId { get; set; }
        public virtual ClienteVo Cliente { get; set; }

        public virtual ICollection<PedidosPizza> PedidosPizza { get; set; } = new List<PedidosPizza>();
    }
}
