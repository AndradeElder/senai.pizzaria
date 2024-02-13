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
    public class PizzaRepository : Repository<PizzaVo>, IPizzaDao
    {

        public PizzaRepository(AppDbContext context) : base(context)
        {
            SeedDataAsync().Wait();
        }

        private async Task SeedDataAsync()
        {
            // Check if any students exist
            if (GetAll().Count() > 0)
            {
                return; // Data has already been seeded
            }

            // Seed some students
            await AddAsync(new PizzaVo
            {
                Sabor = "Calabresa",
                Descricao = "massa tradicional, com molho, calabresa",
                TamanhoDePizza = 1,
                Valor = 50.00
            });

            await AddAsync(new PizzaVo
            {
                Sabor = "Calabresa com cebola",
                Descricao = "massa tradicional, com molho, calabresa e cebola",
                TamanhoDePizza = 1,
                Valor = 50.00
            });

            // Save changes
            await _context.SaveChangesAsync();
        }
    }
}
