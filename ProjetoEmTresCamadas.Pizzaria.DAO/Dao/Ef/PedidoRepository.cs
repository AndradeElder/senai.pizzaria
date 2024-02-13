using Microsoft.EntityFrameworkCore;
using ProjetoEmTresCamadas.Pizzaria.DAO.Dao.Repository;
using ProjetoEmTresCamadas.Pizzaria.DAO.Regras;
using ProjetoEmTresCamadas.Pizzaria.DAO.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoEmTresCamadas.Pizzaria.DAO.Dao.Ef
{
    public class PedidoRepository : Repository<PedidoVo>, IPedidoDao
    {
        public PedidoRepository(AppDbContext context) : base(context)
        {
        }
    }
}
