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

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ClienteVo>(entity =>
            {
                // Define a tabela para a entidade ClienteVo
                entity.ToTable("TB_Cliente");

                // Define a chave primária
                entity.HasKey(c => c.Id);

                // Define as propriedades
                entity.Property(c => c.Nome).IsRequired().HasMaxLength(100);
                //entity.Property(c => c.UserId).IsRequired();

                // Exemplo de configuração de relacionamento (se houver)
                // entity.HasOne(c => c.AlgumaOutraEntidade)
                //       .WithMany(o => o.Clientes)
                //       .HasForeignKey(c => c.AlgumaOutraEntidadeId);
            });
        }

    }
    public class ClienteVoConfiguration : IEntityTypeConfiguration<ClienteVo>
    {
        public void Configure(EntityTypeBuilder<ClienteVo> builder)
        {
            // Define a tabela para a entidade ClienteVo
            builder.ToTable("TB_Cliente");

            // Define a chave primária
            builder.HasKey(c => c.Id);

            // Define as propriedades
            builder.Property(c => c.Nome).IsRequired().HasMaxLength(100);
           // builder.Property(c => c.UserId).IsRequired();

            // Exemplo de configuração de relacionamento (se houver)
            // builder.HasOne(c => c.AlgumaOutraEntidade)
            //        .WithMany(o => o.Clientes)
            //        .HasForeignKey(c => c.AlgumaOutraEntidadeId);
        }
    }
}
