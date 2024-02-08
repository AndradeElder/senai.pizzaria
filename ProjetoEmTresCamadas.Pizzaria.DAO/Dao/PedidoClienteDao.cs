using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using ProjetoEmTresCamadas.Pizzaria.DAO.Regras;
using ProjetoEmTresCamadas.Pizzaria.DAO.Settings;
using ProjetoEmTresCamadas.Pizzaria.DAO.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoEmTresCamadas.Pizzaria.DAO.Dao;
public class PedidoClienteDao : BaseDao<PedidoVo>, IPedidoClienteDao
{
    private const string TABELA_PedidoCliente_NOME = "TB_PedidoCliente";

    private const string TABELA_PedidoCliente = @$"CREATE TABLE IF NOT EXISTS {TABELA_PedidoCliente_NOME}
                (
                    ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    CLienteID INTEGER NOT NULL,
                    DataSolicitacao DATETIME NOT NULL,
                    DataPreparacao DATETIME,
                    DataSaidaEntrega DATETIME,
                    DataFinalizacaoEntrega DATETIME
                )";

    private const string INSERIR_PedidoCliente = @$"
                INSERT INTO {TABELA_PedidoCliente_NOME} (CLienteID, DataSolicitacao, DataPreparacao, DataSaidaEntrega, DataFinalizacaoEntrega)
                VALUES (@CLienteID, @DataSolicitacao, @DataPreparacao, @DataSaidaEntrega, @DataFinalizacaoEntrega)";

    private const string UPDATE_PedidoCliente = @$"
    UPDATE {TABELA_PedidoCliente_NOME}
    SET
        CLienteID = @CLienteID,
        DataSolicitacao = @DataSolicitacao,
        DataPreparacao = @DataPreparacao,
        DataSaidaEntrega = @DataSaidaEntrega,
        DataFinalizacaoEntrega = @DataFinalizacaoEntrega
    WHERE
        ID = @Id";

    private const string DELETE_PedidoCliente = $@"
    DELETE FROM {TABELA_PedidoCliente_NOME} 
    WHERE ID = @ID";

    private const string SELECT_PedidoCliente = @$"SELECT * FROM {TABELA_PedidoCliente_NOME}";
    private const string SELECT_PedidoCliente_By_Id = @$"SELECT * FROM {TABELA_PedidoCliente_NOME} WHERE ID = @ID";

    public PedidoClienteDao(IOptions<ConnectionStrings> connectionStringOptions) : base(
        TABELA_PedidoCliente,
        SELECT_PedidoCliente,
        INSERIR_PedidoCliente,
        TABELA_PedidoCliente_NOME,
        UPDATE_PedidoCliente,
        DELETE_PedidoCliente,
        SELECT_PedidoCliente_By_Id,
        connectionStringOptions)
    { }

    protected override PedidoVo CriarInstancia(SqliteDataReader sqliteDataReader)
    {
        return new PedidoVo
        {
            Id = Convert.ToInt32(sqliteDataReader["ID"]),
            CLienteID = Convert.ToInt32(sqliteDataReader["CLienteID"]),
            DataSolicitacao = Convert.ToDateTime(sqliteDataReader["DataSolicitacao"]),
            DataPreparacao = Convert.ToDateTime(sqliteDataReader["DataPreparacao"]),
            DataSaidaEntrega = Convert.ToDateTime(sqliteDataReader["DataSaidaEntrega"]),
            DataFinalizacaoEntrega = Convert.ToDateTime(sqliteDataReader["DataFinalizacaoEntrega"])
        };
    }
}
