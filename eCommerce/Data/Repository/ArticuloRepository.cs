using eCommerce.Data.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using eCommerce.Entitys;

namespace eCommerce.Data.Repository
{
    public class ArticuloRepository : IArticuloRepository
    {
        private readonly DbEContext _context;

        public ArticuloRepository(DbEContext dbWContext)
        {
            _context = dbWContext;
        }

        public async Task<int> Create(Articulo articulo)
        {
            await _context.articulos.AddAsync(articulo);
            await Save();
            return articulo.id;
        }

        public async Task<int> Delete(int id)
        {
            var articulo = await _context.articulos.FindAsync(id);

            if (articulo == null)
                return 0;

            _context.Entry(articulo).State = EntityState.Deleted;

            try
            {
                await Save();
                return articulo.id;
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }
        }

        public async Task<List<Articulo>> GetAll()
        {
            return await _context.articulos.ToListAsync();
        }

        public async Task<Articulo> GetById(int id)
        {
            return await _context.articulos.FindAsync(id);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Articulo> Update(Articulo articulo)
        {
            _context.Entry(articulo).State = EntityState.Modified;

            try
            {
                await Save();
                return articulo;
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
        }
    }
}
