using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoEmTresCamadas.Pizzaria.DAO.ValueObjects;


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
           this.Database.EnsureCreated();
        }
    }
   
}
