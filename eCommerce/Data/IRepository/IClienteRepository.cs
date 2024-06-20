using System.Collections.Generic;
using System.Threading.Tasks;
using eCommerce.Entitys;

namespace eCommerce.Data.IRepository
{
    public interface IClienteRepository
    {
        Task<int> Create(Cliente cliente);
        Task<Cliente> GetByCorreo(string correo);
        Task<Cliente> GetById(int id);
        Task<Cliente> Update(Cliente Cliente);
        Task<int> Delete(int id);
    }
}
