using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Entidades;

namespace ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Regras;

public interface IPedidoService :
    IAdicionar<Pedido>,
    IObter<Pedido>,
    IAtualizar<Pedido>,
    IDeletar<Pedido>
{
    Pedido FazerPedido(Cliente cliente, Pizza pizza);
    Pedido FazerPedido(Cliente cliente, Pizza[] pizzas);
    string ObterInformacoesPedidos();
}

