using eCommerce.Data;
using eCommerce.Data.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using eCommerce.Entitys;

namespace eCommerce.Data.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly DbEContext _context;
        public AdminRepository(DbEContext context)
        {
            _context = context;
        }

        public async Task<Admin> Create(Admin admin)
        {
            await _context.admins.AddAsync(admin);
            await Save();
            return admin;
        }

        public async Task<int> Delete(int id)
        {
            var admin = await _context.admins.FindAsync(id);

            if (admin == null)
                return 0;

            _context.Entry(admin).State = EntityState.Deleted;

            try
            {
                await Save();
                return admin.id;
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }
        }

        public async Task<Admin> GetById(int id)
        {
            return await _context.admins.FindAsync(id);
        }

        public async Task<Admin> GetByCorreo(string correo)
        {
            return await _context.admins.FirstOrDefaultAsync(s => s.correo == correo);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Admin> Update(Admin sesion)
        {
            _context.Entry(sesion).State = EntityState.Modified;

            try
            {
                await Save();
                return sesion;
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
        }

    }
}
