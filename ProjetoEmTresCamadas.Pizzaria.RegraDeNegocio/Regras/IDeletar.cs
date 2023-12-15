namespace ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Regras;

public interface IDeletar<T>
{
    Task Deletar(int ID);
}
