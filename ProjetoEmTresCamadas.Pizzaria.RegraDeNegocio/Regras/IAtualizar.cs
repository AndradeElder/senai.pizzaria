namespace ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Regras;

public interface IAtualizar<T>
{
    Task<T> AtualizarAsync(T objeto);
}
