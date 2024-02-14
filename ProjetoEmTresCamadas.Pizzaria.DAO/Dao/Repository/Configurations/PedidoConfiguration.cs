using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoEmTresCamadas.Pizzaria.DAO.ValueObjects;

namespace ProjetoEmTresCamadas.Pizzaria.DAO.Dao.Repository.Configurations
{
    public class PedidoConfiguration : IEntityTypeConfiguration<PedidoVo>
    {
        public void Configure(EntityTypeBuilder<PedidoVo> builder)
        {
            builder.ToTable("Pedidos");
            builder.HasKey(x => x.Id);

            builder.HasMany(a => a.Pizzas)
                .WithMany(a => a.Pedidos)
                .UsingEntity(j => j.ToTable("PedidoPizza"));
        }
    }
}
