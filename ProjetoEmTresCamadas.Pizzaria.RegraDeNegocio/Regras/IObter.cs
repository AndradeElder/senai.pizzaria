namespace ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Regras;

public interface IObter<T>
{
    Task<List<T>> ObterTodos();
}
