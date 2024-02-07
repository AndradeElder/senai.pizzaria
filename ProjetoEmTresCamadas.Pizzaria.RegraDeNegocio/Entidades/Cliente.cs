using ProjetoEmTresCamadas.Pizzaria.DAO.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Entidades
{
    public class Cliente : EntidadeBase
    {
        public string Nome { get; set; }
        public Guid UserId { get; set; }

        public ClienteVo ToVo()
        {
            return new ClienteVo {  Nome = Nome, Id = Id, UserId = UserId };
        }
    }
}
