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
    public Cliente Adicionar(Cliente objeto)
    {
        objeto.Id = _clienteDao.CriarRegistro(objeto.ToVo());        
        return objeto;
    }

    public async Task<Cliente> AtualizarAsync(Cliente objeto)
    {
        await _clienteDao.AtualizarRegistro(objeto.ToVo());
        objeto = (await ObterTodos()).Find(cliente => cliente.Id.Equals(objeto.Id));

        return objeto;
    }

    public async Task Deletar(int ID)
    {
       await _clienteDao.DeletarRegistro(ID);
    }

    public async Task<List<Cliente>> ObterTodos()
    {
        List<Cliente> clientes = new List<Cliente>();
        List<ClienteVo> clientesVo = _clienteDao.ObterRegistros();

        foreach (ClienteVo o in clientesVo)
        {
            Cliente cliente = new Cliente()
            {
                Nome = o.Nome,
                Id = o.Id,
            };
            clientes.Add(cliente);
        }
        return clientes;
    }
}
