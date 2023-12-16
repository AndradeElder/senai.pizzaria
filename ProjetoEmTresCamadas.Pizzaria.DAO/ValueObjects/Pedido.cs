using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoEmTresCamadas.Pizzaria.DAO.ValueObjects
{
    public  class PedidoVo : EntidadeBaseVo
    {
        public PedidoVo() { }

        public int PedidoClienteId { get; set; }
        public int PizzaId { get; set; }

    }
}
