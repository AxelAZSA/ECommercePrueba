using eCommerce.Data.IRepository;
using eCommerce.Entitys;
using eCommerce.Entitys.Request;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Data.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly DbEContext _context;

        public StockRepository(DbEContext dbWContext)
        {
            _context = dbWContext;
        }

        public async Task<int> Create(Stock stock)
        {
            await _context.stocks.AddAsync(stock);
            await Save();
            return stock.id;
        }

        public async Task CreateStock(stockRequest request)
        {
            for(int i = 0; i < request.cantidad; i++)
            {
                Stock stock = new Stock()
                {
                    idArticulo = request.idArticulo,
                    idTienda = request.idTienda,
                    fechaRegistro = DateTime.Now
                };

                await Create(stock);
            }
        }

        public async Task<int> Delete(int id)
        {
            var stock = await _context.stocks.FindAsync(id);

            if (stock == null)
                return 0;

            _context.Entry(stock).State = EntityState.Deleted;

            try
            {
                await Save();
                return stock.id;
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }
        }

        public async Task<ICollection<Stock>> GetAll()
        {
            return await _context.stocks.ToListAsync();
        }

        public async Task<Stock> GetById(int id)
        {
            return await _context.stocks.FindAsync(id);
        }

        public async Task<ICollection<Stock>> GetByIdArticulo(int idArticulo)
        {
            return await _context.stocks.Where(s => s.idArticulo == idArticulo).ToListAsync();
        }

        public async Task<ICollection<Stock>> GetByIdTienda(int idTienda)
        {
            return await _context.stocks.Where(s => s.idTienda == idTienda).ToListAsync();
        }

        public async Task<Stock> Update(Stock stock)
        {
            _context.Entry(stock).State = EntityState.Modified;

            try
            {
                await Save();
                return stock;
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
