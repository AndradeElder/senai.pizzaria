using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Entidades;
using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Regras;

namespace ProjetoEmTresCamadas.Pizzaria.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        public readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<Cliente[]> BuscarClientes()
        {
            return (await _clienteService.ObterTodos()).ToArray();
        }

        [HttpPost]
        public async Task<Cliente> Adicionar(Cliente cliente)
        {
            return _clienteService.Adicionar(cliente);
        }

        [HttpPut]
        public async Task<Cliente> Atualizar(Cliente cliente)
        {
            try
            {

                return await _clienteService.AtualizarAsync(cliente);

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete]
        public async Task Deletar(int ID)
        {
            try
            {
                await _clienteService.Deletar(ID);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
