using eCommerce.Data.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using eCommerce.Entitys;

namespace eCommerce.Data.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly DbEContext _context;

        public ClienteRepository(DbEContext dbWContext)
        {
            _context = dbWContext;
        }
        public async Task<int> Create(Cliente cliente)
        {
            await _context.clientes.AddAsync(cliente);
            await Save();
            return cliente.id;
        }

        public async Task<int> Delete(int id)
        {
            var cliente = await _context.clientes.FindAsync(id);

            if (cliente == null)
                return 0;

            _context.Entry(cliente).State = EntityState.Deleted;

            try
            {
                await Save();
                return cliente.id;
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }
        }

        public async Task<Cliente> GetById(int id)
        {
            return await _context.clientes.FirstOrDefaultAsync(c=>c.id == id);
        }
        public async Task<Cliente> GetByCorreo(string correo)
        {
            return await _context.clientes.FirstOrDefaultAsync(c => c.correo == correo);
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Cliente> Update(Cliente cliente)
        {
            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await Save();
                return cliente;
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
        }
    }
}
