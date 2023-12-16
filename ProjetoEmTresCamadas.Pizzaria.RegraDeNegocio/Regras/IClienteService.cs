using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Entidades;

namespace ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Regras;

public interface IClienteService :
    IObter<Cliente>,
    IAdicionar<Cliente>,
    IAtualizar<Cliente>,
    IDeletar<Cliente>
{

}
