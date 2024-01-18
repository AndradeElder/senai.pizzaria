using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using ProjetoEmTresCamadas.Pizzaria.DAO.Regras;
using ProjetoEmTresCamadas.Pizzaria.DAO.Settings;
using ProjetoEmTresCamadas.Pizzaria.DAO.ValueObjects;

namespace ProjetoEmTresCamadas.Pizzaria.DAO.Dao;

public class PedidoDao : BaseDao<PedidoVo>, IPedidoDao
{
    private const string TABELA_Pedido_NOME = "TB_Pedido";

    private const string TABELA_Pedido = @$"CREATE TABLE IF NOT EXISTS {TABELA_Pedido_NOME}
                (
                    ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    PedidoClienteId INTEGER NOT NULL,
                    PizzaId INTEGER NOT NULL
                )";

    private const string INSERIR_Pedido = @$"
                INSERT INTO {TABELA_Pedido_NOME} (PedidoClienteId, PizzaId)
                VALUES (@PedidoClienteId, @PizzaId)";

    private const string UPDATE_Pedido = @$"
    UPDATE {TABELA_Pedido_NOME}
    SET
        PedidoClienteId = @PedidoClienteId,
        PizzaId = @PizzaId
    WHERE
        ID = @Id";

    private const string DELETE_Pedido = $@"
    DELETE FROM {TABELA_Pedido_NOME} 
    WHERE ID = @ID";

    private const string SELECT_Pedido = @$"SELECT * FROM {TABELA_Pedido_NOME}";
    private const string SELECT_PEDIDO_BY_ID = @$"SELECT * FROM {TABELA_Pedido_NOME} WHERE ID = @ID";

    public PedidoDao(IOptions<ConnectionStrings> connectionStringOptions) : base(
        TABELA_Pedido,
        SELECT_Pedido,
        INSERIR_Pedido,
        TABELA_Pedido_NOME,
        UPDATE_Pedido,
        DELETE_Pedido,
        SELECT_PEDIDO_BY_ID,
        connectionStringOptions)
    { }

    protected override PedidoVo CriarInstancia(SqliteDataReader sqliteDataReader)
    {
        return new PedidoVo
        {
            Id = Convert.ToInt32(sqliteDataReader["ID"]),
            PedidoClienteId = Convert.ToInt32(sqliteDataReader["PedidoClienteId"]),
            PizzaId = Convert.ToInt32(sqliteDataReader["PizzaId"])
        };
    }
}
