using eCommerce.Data.IRepository;
using eCommerce.Entitys;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Data.Repository
{
    public class CompraItemRepository : ICompraItemRepository
    {
        private readonly DbEContext _context;

        public CompraItemRepository(DbEContext dbWContext)
        {
            _context = dbWContext;
        }
        public async Task<int> Create(CompraItem item)
        {
            await _context.compraItems.AddAsync(item);
            await Save();
            return item.id;
        }

        public async Task<int> Delete(int id)
        {
            var item = await _context.compraItems.FindAsync(id);
            _context.Entry(item).State = EntityState.Deleted;

            try
            {
                await Save();
                return item.id;
            }
            catch (DbUpdateConcurrencyException)
            {
                return -1;
            }
        }

        public async Task<List<CompraItem>> GetAll()
        {
            return await _context.compraItems.ToListAsync();
        }

        public async Task<CompraItem> GetById(int id)
        {
            return await _context.compraItems.FindAsync(id);
        }

        public async Task<List<CompraItem>> GetByIdArticulo(int idArticulo)
        {
            return await _context.compraItems.Where(ci => ci.idArticulo == idArticulo).ToListAsync();
        }

        public async Task<List<CompraItem>> GetByIdCompra(int idCompra)
        {
            return await _context.compraItems.Where(ci => ci.idCompra == idCompra).ToListAsync();
        }

        public async Task<CompraItem> Update(CompraItem item)
        {
            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await Save();
                return item;
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
