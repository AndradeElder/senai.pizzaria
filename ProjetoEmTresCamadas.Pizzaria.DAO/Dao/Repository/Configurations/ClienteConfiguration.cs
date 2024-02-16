using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoEmTresCamadas.Pizzaria.DAO.ValueObjects;

namespace ProjetoEmTresCamadas.Pizzaria.DAO.Dao.Repository.Configurations;

public class ClienteConfiguration : IEntityTypeConfiguration<ClienteVo>
{
    public void Configure(EntityTypeBuilder<ClienteVo> builder)
    {
        builder.ToTable("Clientes");
        builder.HasKey(x => x.Id);

        builder.HasMany(cliente => cliente.Pedidos)
               .WithOne(pedido => pedido.Cliente)
               .HasForeignKey(pedido => pedido.ClienteId)
               .HasConstraintName("ForeignKey_Cliente_Pedido");
    }
}
