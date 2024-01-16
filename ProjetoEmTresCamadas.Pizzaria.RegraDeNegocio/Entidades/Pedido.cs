using ProjetoEmTresCamadas.Pizzaria.DAO.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public PedidoClienteVo ToPedidoClienteVo()
        {
            return new PedidoClienteVo()
            {
                CLienteID = Cliente.Id,
                DataSolicitacao = DataSolicitacao,
                DataFinalizacaoEntrega = DataFinalizacaoEntrega,
                DataPreparacao = DataPreparacao,
                Id = this.Id,
                DataSaidaEntrega = DataSaidaEntrega,
            };
        }
    }
}
