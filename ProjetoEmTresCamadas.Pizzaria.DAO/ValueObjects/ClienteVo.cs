namespace ProjetoEmTresCamadas.Pizzaria.DAO.ValueObjects
{
    public class ClienteVo : EntidadeBaseVo
    {
        public string Nome { get; set; }
        public Guid UserId { get; set; }

        public virtual ICollection<PedidoVo> Pedidos { get; set; }
    }
}
