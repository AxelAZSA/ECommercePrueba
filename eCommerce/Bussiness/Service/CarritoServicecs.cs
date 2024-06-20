using eCommerce.Data.IRepository;
using eCommerce.Entitys;

namespace eCommerce.Bussiness.Service
{
    public class CarritoServicecs
    {
        private readonly ICarritoRepository _carritoRepository;
        private readonly ICarritoItemRepository _carritoItemRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IArticuloRepository _articuloRepository;
        public CarritoServicecs(ICarritoRepository carritoRepository, ICarritoItemRepository carritoItemRepository,
         IClienteRepository clienteRepository, IArticuloRepository articuloRepository)
        {
            _carritoRepository = carritoRepository;
            _carritoItemRepository = carritoItemRepository;
            _clienteRepository = clienteRepository;
            _articuloRepository = articuloRepository;
        }

        public async Task AgregarItemHome(int idArticulo, int idCliente)
        {
            var cliente = await _clienteRepository.GetById(idCliente);
            var carrito = await GetCarrito(idCliente);
            var carritoItem = carrito.items.FirstOrDefault(x => x.idArticulo == idArticulo);


            if (carritoItem !=null)
            {
                await ActualizarCant(carritoItem, carritoItem.cantidad+1);
            }
            else
            {
                CarritoItem item = new CarritoItem()
                {
                    idCarrito = carrito.id,
                    idArticulo = idArticulo,
                    cantidad = 1
                };

                await _carritoItemRepository.Create(item);
            }
        }

        public async Task AgregarItemCarrito(int idCarritoItem, int cantidad)
        {
            var carritoItem = await _carritoItemRepository.GetById(idCarritoItem);

            if (carritoItem != null)
            {
                await ActualizarCant(carritoItem, cantidad);
            }
        }

        public async Task ActualizarCant(CarritoItem item, int cantidad)
        {
            if (cantidad == 0)
            {
                await _carritoItemRepository.Delete(item.id);
            }
            else
            {
                item.cantidad = cantidad;
                await _carritoItemRepository.Update(item); 
            }

        }

        public async Task<Carrito> GetCarrito(int idCliente)
        {
            var carrito = await _carritoRepository.GetByIdCliente(idCliente);

            carrito.items = await _carritoItemRepository.GetByIdCarrito(carrito.id);
            return carrito;
        }

        public async Task PostCarrito(int idCliente)
        {
            await _carritoRepository.Create(idCliente);

        }
    }
}
