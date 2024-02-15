using ProjetoEmTresCamadas.Pizzaria.DAO.Dao;
using ProjetoEmTresCamadas.Pizzaria.DAO.Regras;
using ProjetoEmTresCamadas.Pizzaria.DAO.ValueObjects;
using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Entidades;
using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Regras;

namespace ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Serviços;

public class PedidoService : IPedidoService
{
    private readonly IPedidoDao _pedidoDao;
    private readonly IClienteService _clienteService;
    private readonly IPizzaDao _pizzaDao;

    public PedidoService(
        IPedidoDao pedidoDao,
        IClienteService clienteService,
        IPizzaDao pizzaDao)
    {
        _pedidoDao = pedidoDao;
        _clienteService = clienteService;
        _pizzaDao = pizzaDao;
    }

    public async Task<Pedido> AdicionarAsync(Pedido pedido)
    {
        var cliente = await _clienteService.Obter(pedido.Cliente.Id);
        var ids = new List<int>();
        pedido.Pizzas.ForEach(pizza => ids.Add(pizza.Id));

        var pizzas = _pizzaDao.GetAll().Where(x => ids.Contains(x.Id)).ToList();


        var pedidoVo = pedido.ToPedidoClienteVo();
        pedidoVo.ClienteId = cliente.Id;

        await _pedidoDao.AddAsync(pedidoVo);

        foreach (var pizza in pizzas)
        {
            var pedidoPizza = new PedidosPizza()
            {
                PedidoId = pedidoVo.Id,
                PizzaId = pizza.Id,
            };
            pedidoVo.PedidosPizza.Add(pedidoPizza);
        }
        _pedidoDao.Update(pedidoVo);

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
            pedidos.Add(await MapVoToModel(pedidoVo));
        }
        return pedidos;
    }

    private async Task<Pedido> MapVoToModel(PedidoVo pedidoVo)
    {
        var cliente = ClienteService.MapVoToCliente(pedidoVo.Cliente);
        var pizzas = new List<Pizza>();
        foreach (var pedidoDePizza in pedidoVo.PedidosPizza)
        {
            var pizza = PizzaService.MapPizzaVo(pedidoDePizza.Pizza);
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
        return pedido;
    }

    public async Task<List<Pedido>> ObterTodos(int[] id)
    {
        var pedidosVo = _pedidoDao.GetAll().Where(pedido => id.Contains(pedido.Id));

        var pedidos = new List<Pedido>();
        foreach (var pedidoVo in pedidosVo)
        {
            pedidos.Add(await MapVoToModel(pedidoVo));
        }
        return pedidos;
    }

    public async Task<Pedido> Obter(int id)
    {
        var pedidoVo = await _pedidoDao.GetByIdAsync(id);
        var pedido = await MapVoToModel(pedidoVo);
        return pedido;
    }
}

