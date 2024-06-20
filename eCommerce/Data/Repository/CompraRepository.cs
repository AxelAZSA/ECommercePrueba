using eCommerce.Data.IRepository;
using eCommerce.Entitys;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Data.Repository
{
    public class CompraRepository : ICompraRepository
    {
        private readonly DbEContext _context;

        public CompraRepository(DbEContext dbWContext)
        {
            _context = dbWContext;
        }
        public async Task<int> CreateCompra(Compra compra)
        {
            await _context.compras.AddAsync(compra);
            await Save();
            return compra.id;
        }

        public async Task DeleteCompra(int id)
        {
            var compra = await _context.compras.FindAsync(id);

            _context.Entry(compra).State = EntityState.Deleted;

            try
            {
                await Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public async Task<List<Compra>> GetAllCompras()
        {
            return await _context.compras.Include(s => s.items).ToListAsync();
        }

        public async Task<Compra> GetCompraById(int id)
        {
            return await _context.compras.FirstOrDefaultAsync(s=>s.id == id);
        }

        public async Task<List<Compra>> GetComprasByIdCliente(int idCliente)
        {
            return await _context.compras.Where(s => s.idCliente == idCliente).ToListAsync();
        }

        public async Task<List<Compra>> GetComprasByIdTienda(int idTienda)
        {
            return await _context.compras.Where(s => s.idTienda == idTienda).ToListAsync();
        }

        public async Task UpdateCompra(Compra compra)
        {
            var trackedEntity = _context.compras.FirstOrDefault(c => c.id == compra.id);
            if (trackedEntity != null)
            {
                _context.Entry(trackedEntity).State = EntityState.Detached;
            }
            _context.Entry(compra).State = EntityState.Modified;

            try
            {
                await Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<Compra>> GetComprasByEstado(string estado)
        {
            return await _context.compras.Include(s => s.items).Where(s => s.estado == estado).ToListAsync();
        }
    }
}
