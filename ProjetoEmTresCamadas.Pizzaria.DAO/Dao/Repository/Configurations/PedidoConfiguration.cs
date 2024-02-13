using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ProjetoEmTresCamadas.Pizzaria.DAO.ValueObjects;

namespace ProjetoEmTresCamadas.Pizzaria.DAO.Dao.Repository.Configurations
{
    public class PedidoConfiguration : IEntityTypeConfiguration<PedidoVo>
    {
        public void Configure(EntityTypeBuilder<PedidoVo> builder)
        {
            builder.ToTable("Pedidos");
            builder.HasKey(x => x.Id);


            builder.HasOne(pedido => pedido.Cliente);
            builder
            .HasMany(c => c.Pizzas)
            .WithMany(s => s.Pedidos)
            .UsingEntity(j => j.ToTable("PedidosPizzas"));
        }
    }
}
