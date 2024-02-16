using ProjetoEmTresCamadas.Pizzaria.DAO.ValueObjects;

namespace ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Regras;

public interface IObter<T>
{
    Task<List<T>> ObterTodos();
    Task<List<T>> ObterTodos(int[] id);
    Task<T> Obter(int id);
}
