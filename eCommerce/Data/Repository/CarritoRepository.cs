using eCommerce.Data.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using eCommerce.Entitys;

namespace eCommerce.Data.Repository
{
    public class CarritoRepository : ICarritoRepository
    {
        private readonly DbEContext _context;
        public CarritoRepository(DbEContext context) 
        {
            _context = context;
        }
        public async Task<Carrito> Create(int idCliente)
        {
            Carrito carrito = new Carrito()
            {
                idCliente = idCliente
            };
            await _context.carritos.AddAsync(carrito);
            await Save();
            return carrito;
        }

        public async Task<int> Delete(int id)
        {
            var carrito = await _context.carritos.FindAsync(id);

            if (carrito == null)
                return 0;

            _context.Entry(carrito).State = EntityState.Deleted;

            try
            {
                await Save();
                return carrito.id;
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }
        }

        public async Task<Carrito> GetById(int id)
        {
            return await _context.carritos.Include(c=>c.items).FirstOrDefaultAsync(c=>c.id==id);
        }

        public async Task<Carrito> GetByIdCliente(int idCliente)
        {
            return await _context.carritos.FirstOrDefaultAsync(c=>c.idCliente==idCliente);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Carrito> Update(Carrito carrito)
        {
            _context.Entry(carrito).State = EntityState.Modified;

            try
            {
                await Save();
                return carrito;
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
        }
    }
}
