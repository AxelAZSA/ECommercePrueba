using eCommerce.Data.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using eCommerce.Entitys;

namespace eCommerce.Data.Repository
{
    public class TiendaRepository : ITiendaRepository
    {

        private readonly DbEContext _context;

        public TiendaRepository(DbEContext dbWContext)
        {
            _context = dbWContext;
        }

        public async Task<Tienda> Create(Tienda tienda)
        {
            await _context.tiendas.AddAsync(tienda);
            await Save();
            return tienda;
        }

        public async Task<int> Delete(int id)
        {
            var tienda = await _context.tiendas.FindAsync(id);

            if (tienda == null)
                return 0;

            _context.Entry(tienda).State = EntityState.Deleted;

            try
            {
                await Save();
                return tienda.id;
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }
        }

        public async Task<List<Tienda>> GetAll()
        {
            return await _context.tiendas.ToListAsync(); ;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Tienda> Update(Tienda tienda)
        {
            _context.Entry(tienda).State = EntityState.Modified;

            try
            {
                await Save();
                return tienda;
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
        }
    }
}
