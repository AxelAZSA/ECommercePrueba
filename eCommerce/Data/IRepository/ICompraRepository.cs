using eCommerce.Entitys;

namespace eCommerce.Data.IRepository
{
    public interface ICompraRepository
    {
        Task<int> CreateCompra(Compra compra);
        Task UpdateCompra(Compra compra);
        Task DeleteCompra(int id); 
        Task<Compra> GetCompraById(int id);
        Task<List<Compra>> GetAllCompras();
        Task<List<Compra>> GetComprasByEstado(string estado);
        Task<List<Compra>> GetComprasByIdCliente(int idCliente);
        Task<List<Compra>> GetComprasByIdTienda(int idTienda);
    }
}
