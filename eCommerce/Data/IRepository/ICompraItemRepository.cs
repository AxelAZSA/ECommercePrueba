using eCommerce.Entitys;

namespace eCommerce.Data.IRepository
{
    public interface ICompraItemRepository
    {
        Task<int> Create(CompraItem item);
        Task<List<CompraItem>> GetAll();
        Task<List<CompraItem>> GetByIdArticulo(int idArticulo);
        Task<List<CompraItem>> GetByIdCompra(int idCompra);
        Task<CompraItem> GetById(int id);
        Task<CompraItem> Update(CompraItem item);
        Task<int> Delete(int id);
    }
}
