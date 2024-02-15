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

            // Configurar relacionamentos
            builder.HasMany(p => p.PedidosPizza)
                   .WithOne(pp => pp.Pedido)
                   .HasForeignKey(pp => pp.PedidoId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }

    public class PedidosPizzaConfiguration : IEntityTypeConfiguration<PedidosPizza>
    {
        public void Configure(EntityTypeBuilder<PedidosPizza> builder)
        {
            builder.ToTable("PedidosPizza");
            builder.HasKey(x => x.Id);
        }
    }
}
