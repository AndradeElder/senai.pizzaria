using Microsoft.EntityFrameworkCore;
using ProjetoEmTresCamadas.Pizzaria.DAO.Regras;
using ProjetoEmTresCamadas.Pizzaria.DAO.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoEmTresCamadas.Pizzaria.DAO.Dao.Ef
{
    public class ClienteDaoEf : IClienteDao
    {
        public ClienteDaoEf(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }

        public AppDbContext AppDbContext { get; }

        public Task AtualizarRegistro(ClienteVo objetoParaAtualizar)
        {
            AppDbContext.Clientes.Update(objetoParaAtualizar);
            return AppDbContext.SaveChangesAsync();
        }

        public  int CriarRegistroAsync(ClienteVo objetoVo)
        {
             AppDbContext.Clientes.Add(objetoVo);
            return AppDbContext.SaveChanges();
        }

        public Task DeletarRegistro(int ID)
        {
            var entity = AppDbContext.Clientes.Find(ID);
            AppDbContext.Clientes.Remove(entity);
            AppDbContext.SaveChanges();
            return Task.CompletedTask;
        }

        public async Task<ClienteVo> ObterRegistro(int ID)
        {
            return await AppDbContext.Clientes.FindAsync(ID);
        }

        public List<ClienteVo> ObterRegistrosAsync()
        {
            return AppDbContext.Clientes.ToList();
        }

        public List<ClienteVo> ObterRegistros(int ID)
        {
            return AppDbContext.Clientes.Where(x => x.Id.ToString().Contains(ID.ToString())).ToList();
        }
    }
}
