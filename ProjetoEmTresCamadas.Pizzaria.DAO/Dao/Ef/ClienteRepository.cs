using ProjetoEmTresCamadas.Pizzaria.DAO.Dao.Repository;
using ProjetoEmTresCamadas.Pizzaria.DAO.Regras;
using ProjetoEmTresCamadas.Pizzaria.DAO.ValueObjects;

namespace ProjetoEmTresCamadas.Pizzaria.DAO.Dao.Ef
{
    public class ClienteRepository : Repository<ClienteVo>, IClienteDao
    {

        public ClienteRepository(AppDbContext context) : base(context)
        {
            SeedDataAsync().Wait();
        }

        private async Task SeedDataAsync()
        {
            if (GetAll().Count() > 0)
            {
                return; // Data has already been seeded
            }

            // Seed some students
            await AddAsync(new ClienteVo { Nome = "John Doe", UserId = Guid.NewGuid() });
            await AddAsync(new ClienteVo { Nome = "Jane Smith", UserId = Guid.NewGuid() });

            // Save changes
            await _context.SaveChangesAsync();
        }
    }
}
