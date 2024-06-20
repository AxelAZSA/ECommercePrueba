using System.Threading.Tasks;
using eCommerce.Entitys;

namespace eCommerce.Data.IRepository
{
    public interface ICarritoRepository
    {
        Task<Carrito> Create(int idCliente);
        Task<Carrito> GetById(int id);
        Task<Carrito> GetByIdCliente(int idCliente);
        Task<Carrito> Update(Carrito carrito);
        Task<int> Delete(int id);
    }
}
