using eCommerce.Data.IRepository;
using eCommerce.Entitys;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Data.Repository
{
    public class ImagenesRepository : IImagenesRepository
    {
        private readonly DbEContext _context;

        public ImagenesRepository(DbEContext dbWContext)
        {
            _context = dbWContext;
        }
        public async Task<int> Create(imagenes imagenes)
        {
            await _context.imagenes.AddAsync(imagenes);
            await Save();
            return imagenes.id;
        }

        public async Task<int> Delete(int id)
        {
            var imagen = await _context.imagenes.FindAsync(id);

            if (imagen == null)
                return 0;

            _context.Entry(imagen).State = EntityState.Deleted;

            try
            {
                await Save();
                return imagen.id;
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }
        }

        public async Task<List<imagenes>> GetAll()
        {
            return await _context.imagenes.ToListAsync();
        }

        public async Task<imagenes> GetById(int id)
        {
            return await _context.imagenes.FindAsync(id);
        }

        public async Task<List<imagenes>> GetByIdArticulo(int idArticulo)
        {
            return await _context.imagenes.Where(i=>i.idArticulo==idArticulo).ToListAsync();
        }

        public async Task<imagenes> Update(imagenes imagenes)
        {
            _context.Entry(imagenes).State = EntityState.Modified;

            try
            {
                await Save();
                return imagenes;
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
