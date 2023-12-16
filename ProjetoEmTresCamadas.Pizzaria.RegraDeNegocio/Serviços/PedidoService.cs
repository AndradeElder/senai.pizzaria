using ProjetoEmTresCamadas.Pizzaria.DAO.Regras;
using ProjetoEmTresCamadas.Pizzaria.DAO.ValueObjects;
using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Entidades;
using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Regras;

namespace ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Serviços;

public class PedidoService : IPedidoService
{

    private readonly IPedidoClienteDao _pedidoClienteDao;
    private readonly IPedidoDao _pedidoDao;

    public PedidoService(IPedidoClienteDao pedidoClienteDao, IPedidoDao pedidoDao )
    {
        _pedidoClienteDao = pedidoClienteDao;
        _pedidoDao = pedidoDao;
    }

    public Pedido Adicionar(Pedido objeto)
    {
        var pedidoClienteVo =  _pedidoClienteDao.CriarRegistro(objeto.ToPedidoClienteVo());
        objeto.Id = pedidoClienteVo;

        foreach (var pizza in objeto.Pizzas)
        {
            var pedidoVo = new PedidoVo()
            {
                PedidoClienteId = pedidoClienteVo,
                PizzaId = pizza.Id,
            };
            pedidoVo.Id = _pedidoDao.CriarRegistro(pedidoVo);
        }
        
        return objeto;
    }

    public Task<Pedido> AtualizarAsync(Pedido objeto)
    {
        throw new NotImplementedException();
    }

    public Task Deletar(int ID)
    {
        throw new NotImplementedException();
    }

    public Pedido FazerPedido(Cliente cliente, Pizza pizza)
    {
        var pedido = new Pedido
        {
            Cliente = cliente
        };
        pedido.Pizzas.Add(pizza);
        return Adicionar(pedido);
    }
    public Pedido FazerPedido(Cliente cliente, Pizza[] pizzas)
    {
        var pedido = new Pedido
        {
            Cliente = cliente
        };
        pedido.Pizzas.AddRange(pizzas);
        return Adicionar(pedido);
    }
    public string ObterInformacoesPedidos()
    {
        throw new NotImplementedException();
    }

    public Task<List<Pedido>> ObterTodos()
    {
        throw new NotImplementedException();
    }
}

