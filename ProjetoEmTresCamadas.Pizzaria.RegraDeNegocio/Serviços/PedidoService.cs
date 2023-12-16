using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Entidades;
using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Regras;

namespace ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Serviços;

public class PedidoService : IPedidoService
{
    public Pizza Adicionar(Pizza objeto)
    {
        throw new NotImplementedException();
    }

    public Task<Pizza> AtualizarAsync(Pizza objeto)
    {
        throw new NotImplementedException();
    }

    public Task Deletar(int ID)
    {
        throw new NotImplementedException();
    }

    public void FazerPedido(Cliente cliente, Pizza pizza)
    {
        throw new NotImplementedException();
    }

    public string ObterInformacoesPedidos()
    {
        throw new NotImplementedException();
    }

    public Task<List<Pizza>> ObterTodos()
    {
        throw new NotImplementedException();
    }
}

