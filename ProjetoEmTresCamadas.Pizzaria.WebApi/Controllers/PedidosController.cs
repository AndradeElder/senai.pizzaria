using Microsoft.AspNetCore.Mvc;
using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Entidades;
using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Regras;

namespace ProjetoEmTresCamadas.Pizzaria.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidosController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpGet]
        public async Task<Pedido[]> GetPedidos()
        {
            List<Pedido> pedidos = await _pedidoService.ObterTodos();

            return pedidos.ToArray();
        }


        [HttpPost]
        public async Task<Pedido> FazerPedidoAsync(Pedido pedido)
        {
            return await _pedidoService.AdicionarAsync(pedido);
        }

        [HttpPut]
        public async Task AtualizarPedido(Pedido pedido)
        {
            await _pedidoService.AtualizarAsync(pedido);
        }

        [HttpDelete]
        public async Task Deletar(int ID)
        {
            try
            {
                await _pedidoService.Deletar(ID);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
