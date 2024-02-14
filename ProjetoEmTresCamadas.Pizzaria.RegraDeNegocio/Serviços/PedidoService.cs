using ProjetoEmTresCamadas.Pizzaria.DAO.Regras;
using ProjetoEmTresCamadas.Pizzaria.DAO.ValueObjects;
using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Entidades;
using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Regras;

namespace ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Serviços;

public class PedidoService : IPedidoService
{
    private readonly IPedidoDao _pedidoDao;
    private readonly IPizzaService _pizzaService;
    private readonly IClienteService _clienteService;

    public PedidoService(
        IPedidoDao pedidoDao, IPizzaService pizzaService, IClienteService clienteService)
    {
        _pedidoDao = pedidoDao;
        _pizzaService = pizzaService;
        _clienteService = clienteService;
    }

    public async Task<Pedido> AdicionarAsync(Pedido pedido)
    {
        var cliente = await _clienteService.Obter(pedido.Cliente.Id);
        var ids = new List<int>();
        pedido.Pizzas.ForEach(pizza => ids.Add(pizza.Id));
        var pizzas = await _pizzaService.ObterTodos(ids.ToArray());

        var pedidoVo = pedido.ToPedidoClienteVo();
        await _pedidoDao.AddAsync(pedidoVo);

        pedido.Id = pedidoVo.Id;
        return pedido;
    }

    public async Task<Pedido> AtualizarAsync(Pedido objeto)
    {
        var pedidoClienteVo = objeto.ToPedidoClienteVo();
        pedidoClienteVo.Id = objeto.Id;
        _pedidoDao.Update(pedidoClienteVo);
        return objeto;

    }

    public async Task Deletar(int ID)
    {
        var pedido = await _pedidoDao.GetByIdAsync(ID);
        _pedidoDao.Delete(pedido);

    }

    public async Task<List<Pedido>> ObterTodos()
    {
        var pedidosVo = _pedidoDao.GetAll().ToList();

        var pedidos = new List<Pedido>();
        foreach (var pedidoVo in pedidosVo)
        {
            await MapVoToModel(pedidoVo);
        }
        return pedidos;
    }

    private async Task MapVoToModel(PedidoVo pedidoVo)
    {
        var cliente = ClienteService.MapVoToCliente(pedidoVo.Cliente);
        var pizzas = new List<Pizza>();
        foreach (var pedidoDePizza in pedidoVo.Pizzas)
        {
            var pizza = PizzaService.MapPizzaVo(pedidoDePizza);
            pizzas.Add(pizza);
        }
        var pedido = new Pedido()
        {
            Cliente = cliente,
            DataFinalizacaoEntrega = pedidoVo.DataFinalizacaoEntrega,
            DataPreparacao = pedidoVo.DataPreparacao,
            DataSaidaEntrega = pedidoVo.DataSaidaEntrega,
            DataSolicitacao = pedidoVo.DataSolicitacao,
            Pizzas = pizzas
        };
    }

    public async Task<List<Pedido>> ObterTodos(int[] id)
    {
        var pedidosVo = _pedidoDao.GetAll().Where(pedido => id.Contains(pedido.Id));

        var pedidos = new List<Pedido>();
        foreach (var pedidoVo in pedidosVo)
        {
            await MapVoToModel(pedidoVo);
        }
        return pedidos;
    }

    public async Task<Pedido> Obter(int id)
    {
        var pedidoVo = await _pedidoDao.GetByIdAsync(id);
        var pedido = new Pedido();
        await MapVoToModel(pedidoVo);
        return pedido;
    }
}

