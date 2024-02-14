using ProjetoEmTresCamadas.Pizzaria.DAO.Regras;
using ProjetoEmTresCamadas.Pizzaria.DAO.ValueObjects;
using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Entidades;
using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Regras;

namespace ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Serviços;

public class ClienteService : IClienteService

{
    public readonly IClienteDao _clienteDao;

    public ClienteService(IClienteDao clienteDao)
    {
        _clienteDao = clienteDao;
    }
    public async Task<Cliente> AdicionarAsync(Cliente objeto)
    {
        var clienteVo = objeto.ToVo();
        await _clienteDao.AddAsync(clienteVo);
        objeto.Id = clienteVo.Id;
        return objeto;
    }

    public async Task<Cliente> AtualizarAsync(Cliente objeto)
    {
        _clienteDao.Update(objeto.ToVo());
        objeto = await Obter(objeto.Id);
        return objeto;
    }

    public async Task Deletar(int ID)
    {
        ClienteVo clienteVo = await _clienteDao.GetByIdAsync(ID);
        _clienteDao.Delete(clienteVo);
    }

    public async Task<Cliente> Obter(int id)
    {
        ClienteVo clienteVo = await _clienteDao.GetByIdAsync(id);

        Cliente cliente = MapVoToCliente(clienteVo);
        return cliente;
    }

    public async Task<List<Cliente>> ObterTodos()
    {
        List<Cliente> clientes = new();
        var clientesVo = _clienteDao.GetAll().ToList();
        MapVoToClientes(clientes, clientesVo);
        return clientes;
    }

    private static void MapVoToClientes(List<Cliente> clientes, List<ClienteVo> clientesVo)
    {
        foreach (ClienteVo clienteVo in clientesVo)
        {
            Cliente cliente = MapVoToCliente(clienteVo);
            clientes.Add(cliente);
        }
    }

    public static Cliente MapVoToCliente(ClienteVo o, Cliente cliente = null)
    {
        if (cliente == null)
            cliente = new Cliente();

        cliente.Nome = o.Nome;
        cliente.Id = o.Id;
        cliente.UserId = o.UserId;

        return cliente;

    }

    public async Task<List<Cliente>> ObterTodos(int[] id)
    {
        var clientesVo = _clienteDao.GetAll().Where(cliente => id.Contains(cliente.Id)).ToList();
        List<Cliente> clientes = new();
        MapVoToClientes(clientes, clientesVo);
        return clientes;
    }

}
