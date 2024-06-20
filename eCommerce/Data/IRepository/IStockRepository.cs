using eCommerce.Entitys;
using eCommerce.Entitys.Request;

namespace eCommerce.Data.IRepository
{
    public interface IStockRepository
    {
        Task CreateStock(stockRequest request);
        Task<int> Create(Stock stock);
        Task<ICollection<Stock>> GetAll();
        Task<ICollection<Stock>> GetByIdArticulo(int idArticulo);
        Task<ICollection<Stock>> GetByIdTienda(int idTienda);
        Task<Stock> GetById(int id);
        Task<Stock> Update(Stock stock);
        Task<int> Delete(int id);
    }
}
