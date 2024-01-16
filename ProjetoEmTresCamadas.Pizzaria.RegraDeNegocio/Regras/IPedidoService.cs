using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Entidades;

namespace ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Regras;

public interface IPedidoService :
    IAdicionar<Pedido>,
    IObter<Pedido>,
    IAtualizar<Pedido>,
    IDeletar<Pedido>
{   
    string ObterInformacoesPedidos();
}

