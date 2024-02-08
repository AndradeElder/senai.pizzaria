using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoEmTresCamadas.Pizzaria.DAO.ValueObjects
{
    public class PedidoVo : EntidadeBaseVo
    {
        public PedidoVo() { }

        public int CLienteID { get; set; }

        public DateTime DataSolicitacao { get; set; }
        public DateTime? DataPreparacao { get; set; }
        public DateTime? DataSaidaEntrega { get; set; }
        public DateTime? DataFinalizacaoEntrega { get; set; }

        public int ClienteId { get; set; }
        public ClienteVo Cliente { get; set; }
        public ICollection<PizzaVo> Pizzas { get; set; }
    }
}
