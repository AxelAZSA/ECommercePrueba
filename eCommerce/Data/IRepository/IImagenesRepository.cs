using eCommerce.Entitys;

namespace eCommerce.Data.IRepository
{
    public interface IImagenesRepository
    {
        Task<int> Create(imagenes imagenes);
        Task<List<imagenes>> GetAll();
        Task<List<imagenes>> GetByIdArticulo(int idArticulo);
        Task<imagenes> GetById(int id);
        Task<imagenes> Update(imagenes imagenes);
        Task<int> Delete(int id);
    }
}
