namespace ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Regras;

public interface IAdicionar<T>
{
    Task<T> AdicionarAsync(T objeto);
}
