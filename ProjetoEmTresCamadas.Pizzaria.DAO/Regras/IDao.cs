namespace ProjetoEmTresCamadas.Pizzaria.DAO.Regras;

public interface IDao<T>
{
    Task<T> ObterRegistro(int ID);
    List<T> ObterRegistrosAsync();
    List<T> ObterRegistros(int ID);
    int CriarRegistroAsync(T objetoVo);

    Task AtualizarRegistro(T objetoParaAtualizar);

    Task DeletarRegistro(int ID);
}
