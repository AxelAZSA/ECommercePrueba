using System.Collections.Generic;
using System.Threading.Tasks;
using eCommerce.Entitys;

namespace eCommerce.Data.IRepository
{
    public interface ICarritoItemRepository
    {
        Task<int> Create(CarritoItem item);
        Task<CarritoItem> GetById(int id);
        Task<CarritoItem> GetByIdArticulo(int idArticulo, int idCarrito);
        Task<List<CarritoItem>> GetByIdCarrito(int idCarrito);
        Task Update(CarritoItem item);
        Task PatchCantidad(int id);
        Task<int> Delete(int id);
        Task DeleteByCarrito(int idCarrito);
    }
}
