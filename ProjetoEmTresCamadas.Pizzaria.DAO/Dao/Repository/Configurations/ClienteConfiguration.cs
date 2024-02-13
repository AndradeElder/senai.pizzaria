using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ProjetoEmTresCamadas.Pizzaria.DAO.ValueObjects;

namespace ProjetoEmTresCamadas.Pizzaria.DAO.Dao.Repository.Configurations;

public class ClienteConfiguration : IEntityTypeConfiguration<ClienteVo>
{
    public void Configure(EntityTypeBuilder<ClienteVo> builder)
    {
        builder.ToTable("Clientes");
        builder.HasKey(x => x.Id);
    }
}
