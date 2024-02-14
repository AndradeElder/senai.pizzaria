using ProjetoEmTresCamadas.Pizzaria.DAO.ValueObjects;

namespace ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Entidades
{
    public class Cliente : EntidadeBase
    {
        public string Nome { get; set; }
        public Guid UserId { get; set; }

        public ClienteVo ToVo()
        {
            return new ClienteVo { Nome = Nome, UserId = UserId, Id = Id };//, UserId = UserId };
        }
    }
}
