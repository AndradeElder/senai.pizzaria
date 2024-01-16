using ProjetoEmTresCamadas.Pizzaria.DAO.Dao;
using ProjetoEmTresCamadas.Pizzaria.DAO.Regras;
using ProjetoEmTresCamadas.Pizzaria.DAO.ValueObjects;
using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Entidades;
using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Regras;

namespace ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Serviços;

public class PedidoService : IPedidoService
{
    
    private readonly IPedidoClienteDao _pedidoClienteDao;
    private readonly IPedidoDao _pedidoDao;
    private readonly IClienteService _clienteService;
    private readonly IPizzaService _pizzaService;

    public PedidoService(
        IPedidoClienteDao pedidoClienteDao,
        IPedidoDao pedidoDao,
        IClienteService clienteService,
        IPizzaService pizzaService)
    {
        _pedidoClienteDao = pedidoClienteDao;
        _pedidoDao = pedidoDao;
        _clienteService = clienteService;
        _pizzaService = pizzaService;
    }

    public Pedido Adicionar(Pedido pedido)
    {
        var pedidoClienteVo =  _pedidoClienteDao.CriarRegistro(pedido.ToPedidoClienteVo());
        pedido.Id = pedidoClienteVo;

        foreach (var pizza in pedido.Pizzas)
        {
            var pedidoVo = new PedidoVo()
            {
                PedidoClienteId = pedido.Cliente.Id,
                PizzaId = pizza.Id,
            };
            pedidoVo.Id = _pedidoDao.CriarRegistro(pedidoVo);
        }
        
        return pedido;
    }

    public Task<Pedido> AtualizarAsync(Pedido objeto)
    {
        throw new NotImplementedException();
    }

    public async Task Deletar(int ID)
    {
        await _pedidoDao.DeletarRegistro(ID);
        await _pedidoClienteDao.DeletarRegistro(ID);

    }
    public string ObterInformacoesPedidos()
    {
        throw new NotImplementedException();
    }

    public async Task<List<Pedido>> ObterTodos()
    {
        var pedidosVo = _pedidoClienteDao.ObterRegistros();

        var pedidos = new List<Pedido>();
        foreach (var pedidoVo in pedidosVo)
        {
            var cliente = await _clienteService.Obter(pedidoVo.CLienteID);

            var pedidosDePizza = _pedidoDao.ObterRegistros(pedidoVo.Id);

            var pizzas = new List<Pizza>();

            foreach (var pedidoDePizza in pedidosDePizza)
            {
                var pizza = await _pizzaService.Obter(pedidoDePizza.PizzaId);
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
        return pedidos;
    }

    public Task<List<Pedido>> ObterTodos(int[] id)
    {
        throw new NotImplementedException();
    }

    public Task<Pedido> Obter(int id)
    {
        throw new NotImplementedException();
    }
}

