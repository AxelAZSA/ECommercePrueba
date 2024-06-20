using eCommerce.Data.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.Entitys;

namespace eCommerce.Data.Repository
{
    public class CarritoItemRepository : ICarritoItemRepository
    {
        private readonly DbEContext _context;
        public CarritoItemRepository(DbEContext context)
        {
            _context = context;
        }

        public async Task<int> Create(CarritoItem item)
        {
            await _context.carritoItems.AddAsync(item);
            await Save();
            return item.id;
        }

        public async Task<int> Delete(int id)
        {
            var item = await _context.carritoItems.FindAsync(id);

            if (item == null)
                return 0;

            _context.Entry(item).State = EntityState.Deleted;

            try
            {
                await Save();
                return item.id;
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }
        }

        public async Task DeleteByCarrito(int idCarrito)
        {
            var items = await GetByIdCarrito(idCarrito);
            foreach (var item in items) 
            {
                await Delete(item.id);
            }
        }

        public async Task<CarritoItem> GetById(int id)
        {
            return await _context.carritoItems.FirstOrDefaultAsync(ci=>ci.id==id);
        }

        public async Task<CarritoItem> GetByIdArticulo(int idArticulo, int idCarrito)
        {
            return await _context.carritoItems.FirstOrDefaultAsync(c => c.idCarrito == idCarrito && c.idArticulo == idArticulo);
        }

        public async Task<List<CarritoItem>> GetByIdCarrito(int idCarrito)
        {
            return await _context.carritoItems.Where(x => x.idCarrito == idCarrito).ToListAsync();
        }

        public async Task PatchCantidad(int id)
        {
            var item = await _context.carritoItems.FindAsync(id);
            item.cantidad += 1;
            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;
            }
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update(CarritoItem item)
        {
            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;
            }
        }
    }
}
