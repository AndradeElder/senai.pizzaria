using Microsoft.EntityFrameworkCore;
using ProjetoEmTresCamadas.Pizzaria.DAO.Dao.Repository;
using ProjetoEmTresCamadas.Pizzaria.DAO.Regras;
using ProjetoEmTresCamadas.Pizzaria.DAO.ValueObjects;
using System.Linq.Expressions;

namespace ProjetoEmTresCamadas.Pizzaria.DAO.Dao.Ef
{
    public class PedidoRepository : IPedidoDao
    {
        public AppDbContext AppDbContext { get; }
        private Repository<PedidoVo> Repository { get; }

        public PedidoRepository(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
            Repository = new Repository<PedidoVo>(appDbContext);
            SeedData();
        }

        private void SeedData()
        {
            PedidoVo vo = new PedidoVo();
            vo.Cliente = new ClienteVo { Nome = "John Doe", UserId = Guid.NewGuid() };
            Repository.AddAsync(vo).Wait();

            PedidosPizza pedidosPizza = new PedidosPizza();
            pedidosPizza.Pedido = vo;
            pedidosPizza.Pizza = new PizzaVo
            {
                Sabor = "Calabresa",
                Descricao = "massa tradicional, com molho, calabresa",
                TamanhoDePizza = 1,
                Valor = 50.00
            };
            vo.PedidosPizza.Add(pedidosPizza);
            Repository.Update(vo);
        }

        public IQueryable<PedidoVo> GetAll()
        {
            return Repository.GetAll()
                .Include(x => x.Cliente)
                .Include(x => x.PedidosPizza)
                    .ThenInclude(x => x.Pizza);
        }

        public IQueryable<PedidoVo> Get(Expression<Func<PedidoVo, bool>> predicate)
        {
            return Repository.Get(predicate)
                .Include(x => x.PedidosPizza)
                .Include(x => x.Cliente);
        }

        public Task<PedidoVo> GetByIdAsync(int id)
        {
            return Repository.Get(x => x.Id.Equals(id))
                .Include(x => x.PedidosPizza)
                .Include(x => x.Cliente)
                .FirstOrDefaultAsync();
        }

        public Task AddAsync(PedidoVo entity)
        {
            return Repository.AddAsync(entity);
        }

        public void Update(PedidoVo entity)
        {
            Repository.Update(entity);
        }

        public void Delete(PedidoVo entity)
        {
            Repository.Delete(entity);
        }
    }
}
