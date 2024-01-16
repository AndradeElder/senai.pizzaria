using Microsoft.Data.Sqlite;
using ProjetoEmTresCamadas.Pizzaria.DAO.Dao;
using ProjetoEmTresCamadas.Pizzaria.DAO.Regras;
using ProjetoEmTresCamadas.Pizzaria.DAO.ValueObjects;

namespace ProjetoEmTresCamadas.Clienteria.DAO.Dao;
public class ClienteDao : BaseDao<ClienteVo>, IClienteDao
{
    private const string TABELA_Cliente_NOME = "TB_Cliente";

    private const string TABELA_Cliente = @$"CREATE TABLE IF NOT EXISTS {TABELA_Cliente_NOME}
                (
                    ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Nome VARCHAR(50) not null
                )";

    private const string INSERIR_Cliente = @$"
                INSERT INTO {TABELA_Cliente_NOME} (Nome)
                VALUES (@Nome)";

    private const string UPDATE_Cliente = @$"
    UPDATE {TABELA_Cliente_NOME}
    SET
        Nome = @Nome        
    WHERE
        ID = @Id";

    private const string DELETE_Cliente = $@"
    DELETE FROM {TABELA_Cliente_NOME} 
    WHERE ID = @ID";

    private const string SELECT_Cliente = @$"SELECT * FROM {TABELA_Cliente_NOME}";

    private const string SELECT_Cliente_By_ID = @$"SELECT * FROM {TABELA_Cliente_NOME} WHERE ID = @ID";

    public ClienteDao() : base(
        TABELA_Cliente,
        SELECT_Cliente,
        INSERIR_Cliente,
        TABELA_Cliente_NOME,
        UPDATE_Cliente,
        DELETE_Cliente,
        SELECT_Cliente_By_ID)
    { }

    protected override ClienteVo CriarInstancia(SqliteDataReader sqliteDataReader)
    {
        return new ClienteVo
        {
            Id = Convert.ToInt32(sqliteDataReader["Id"]),
            Nome = sqliteDataReader["Nome"].ToString()
        };
    }
}
