using eCommerce.Entitys.Request;
using eCommerce.Entitys.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using eCommerce.Bussiness.Service;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eCommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraController : ControllerBase
    {
        private readonly CompraService _compraService;
        public CompraController(CompraService compraService) 
        {
            _compraService = compraService;
        }
        [Authorize]
        [HttpGet("cliente")]
        public async Task<IActionResult> GetByIdCliente()
        {
            string rawClienteId = HttpContext.User.FindFirstValue("id");
            if (!int.TryParse(rawClienteId, out int idCliente))
            {
                return Unauthorized();
            }
            return Ok(await _compraService.ComprasByCliente(idCliente));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("tienda/{idTienda}")]
        public async Task<IActionResult> GetByIdTienda(int idTienda)
        {
            return Ok(await _compraService.ComprasByTienda(idTienda));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var compras = await _compraService.AllCompras();

            return Ok(compras);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("pendiente/pago")]
        public async Task<IActionResult> GetComprasPendientePago()
        {
            var compras = await _compraService.ComprasPendientePago();

            return Ok(compras);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("pendiente/envio")]
        public async Task<IActionResult> GetComprasPendienteEnvio()
        {
            var compras = await _compraService.ComprasPendienteEnvio();

            return Ok(compras);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var compra = await _compraService.GetCompra(id);

            return Ok(compra);
        }

        [Authorize]
        [HttpGet("detalles/{id}")]
        public async Task<IActionResult> GetItemsById(int id)
        {
            var compra = await _compraService.GetItemsCompra(id);

            return Ok(compra);
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(CompraRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequestModelState();
            }
            string rawClienteId = HttpContext.User.FindFirstValue("id");
            if (!int.TryParse(rawClienteId, out int idCliente))
            {
                return Unauthorized();
            }

            int idCompra = await _compraService.CompraProceso(idCliente,request.idTienda);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id,EstadoRequest request)
        {
            if (id != request.idCompra)
                return BadRequest();

            await _compraService.ActualizarEstado(request.idCompra,request.estado);

            return Ok();
        }

        private IActionResult BadRequestModelState()
        {
            IEnumerable<string> errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
            return BadRequest(new ErrorResponse(errors));
        }

    }
}
