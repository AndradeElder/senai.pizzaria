namespace ProjetoEmTresCamadas.Pizzaria.DAO.Regras;

public interface IDao<T>
{
    List<T> ObterRegistros();
    int CriarRegistro(T objetoVo);

    Task AtualizarRegistro(T objetoParaAtualizar);
}
