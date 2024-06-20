using System.Collections.Generic;
using System.Threading.Tasks;
using eCommerce.Entitys;

namespace eCommerce.Data.IRepository
{
    public interface ITiendaRepository
    {
        Task<Tienda> Create(Tienda tienda);
        Task<List<Tienda>> GetAll();
        Task<Tienda> Update(Tienda tienda);
        Task<int> Delete(int id);
        Task Save();
    }
}
