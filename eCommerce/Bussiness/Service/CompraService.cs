using eCommerce.Data.IRepository;
using eCommerce.Entitys;
using eCommerce.Entitys.Request;

namespace eCommerce.Bussiness.Service
{
    public class CompraService
    {
        private readonly ICarritoRepository _carritoRepository;
        private readonly ICarritoItemRepository _carritoItemRepository;
        private readonly ICompraRepository _compraRepository;
        private readonly ICompraItemRepository _compraItemRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IArticuloRepository _articuloRepository;
        public CompraService(ICarritoRepository carritoRepository, ICarritoItemRepository carritoItemRepository, 
         ICompraRepository compraRepository, ICompraItemRepository compraItemRepository, 
         IClienteRepository clienteRepository, IArticuloRepository articuloRepository) 
        { 
            _carritoRepository = carritoRepository;
            _carritoItemRepository = carritoItemRepository;
            _compraRepository = compraRepository;
            _compraItemRepository = compraItemRepository;
            _clienteRepository = clienteRepository;
            _articuloRepository = articuloRepository;
        }

        public async Task<int> CompraProceso(int idCliente, int idTienda) 
        {

            var cliente = await _clienteRepository.GetById(idCliente);
            //var carrito = await _carritoRepository.GetByIdCliente(idCliente);

            Compra compra = new Compra()
            {
                idCliente = idCliente,
                idTienda = idTienda,
                direccion = cliente.direccion,
                fecha = DateTime.Now,
                estado = "CompraProceso",
                total = 0
            };

            var carrito = await _carritoRepository.GetByIdCliente(cliente.id);
            int idCompra = await _compraRepository.CreateCompra(compra);

            List<CarritoItem> items = await _carritoItemRepository.GetByIdCarrito(carrito.id);
            decimal total = 0;
            foreach (var item in items) 
            {
                Articulo articulo = await _articuloRepository.GetById(item.idArticulo);
                decimal subtotal = item.cantidad * articulo.precio;
                total += subtotal;
                CompraItem compraItem = new CompraItem()
                {
                    idCompra = idCompra,
                    idArticulo = articulo.id,
                    cantidad = item.cantidad,
                    subtotal =  subtotal
                };

                await _compraItemRepository.Create(compraItem);
            }

            compra = new Compra()
            {
                id = idCompra,
                idCliente = idCliente,
                idTienda = idTienda,
                direccion = cliente.direccion,
                fecha = DateTime.Now,
                estado = "PendientePago",
                total = total
            };

            await _compraRepository.UpdateCompra(compra);

            await _carritoItemRepository.DeleteByCarrito(carrito.id);
            return idCompra;
        }

        public async Task<List<Compra>> ComprasPendientePago()
        {
            List<Compra> pendiente = await _compraRepository.GetComprasByEstado("PendientePago");
            return pendiente;
        }

        public async Task<List<Compra>> ComprasPendienteEnvio()
        {
            List<Compra> pendiente = await _compraRepository.GetComprasByEstado("PendienteEnvio");
            return pendiente;
        }

        public async Task ActualizarEstado(int idCompra, string estado)
        {
            var compra = await _compraRepository.GetCompraById(idCompra);

            compra.estado = estado;

            await _compraRepository.UpdateCompra(compra);
        }

        public async Task<List<Compra>> ComprasByCliente(int idCliente)
        {
            var compras = await _compraRepository.GetComprasByIdCliente(idCliente);
            return compras;
        }

        public async Task<List<Compra>> AllCompras()
        {
            return await _compraRepository.GetAllCompras();
        }

        public async Task<List<Compra>> ComprasByTienda(int idTienda)
        {
            return await _compraRepository.GetComprasByIdTienda(idTienda);
        }

        public async Task<List<CompraItem>> GetItemsCompra(int idCompra)
        {
            return await _compraItemRepository.GetByIdCompra(idCompra);
        }
        public async Task<Compra> GetCompra(int id)
        {
            return await _compraRepository.GetCompraById(id);

        }

    }
}
