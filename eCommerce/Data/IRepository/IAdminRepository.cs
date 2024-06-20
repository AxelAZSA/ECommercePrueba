using eCommerce.Entitys;
using System.Threading.Tasks;

namespace eCommerce.Data.IRepository
{
    public interface IAdminRepository
    {
        Task<Admin> Create(Admin admin);
        Task<Admin> GetByCorreo(string correo);
        Task<Admin> GetById(int id);
        Task<Admin> Update(Admin sesion);
        Task<int> Delete(int id);
    }
}
