using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Entidades;

namespace ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Regras;

public interface IPizzaService :
    IAdicionar<Pizza>,
    IObter<Pizza>,
    IAtualizar<Pizza>,
    IDeletar<Pizza>
{

}
