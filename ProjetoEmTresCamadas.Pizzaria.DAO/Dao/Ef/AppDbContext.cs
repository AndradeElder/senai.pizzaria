using Microsoft.EntityFrameworkCore;
using ProjetoEmTresCamadas.Pizzaria.DAO.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoEmTresCamadas.Pizzaria.DAO.Dao.Ef
{
    public class AppDbContext : DbContext
    {
        public DbSet<ClienteVo> Clientes { get; set; }
        public DbSet<PedidoVo> Pedidos { get; set; }
        public DbSet<PizzaVo> Pizzas { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
