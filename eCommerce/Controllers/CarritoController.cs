using eCommerce.Bussiness.Service;
using eCommerce.Data.Repository;
using eCommerce.Entitys;
using eCommerce.Entitys.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eCommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoController : ControllerBase
    {
        private readonly CarritoServicecs _carritoService;
        public CarritoController(CarritoServicecs carritoService)
        {
            _carritoService = carritoService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCarrito()
        {
            string rawClienteId = HttpContext.User.FindFirstValue("id");
            if (!int.TryParse(rawClienteId, out int idCliente))
            {
                return Unauthorized();
            }
            return Ok(await _carritoService.GetCarrito(idCliente));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostCarrito()
        {
            string rawClienteId = HttpContext.User.FindFirstValue("id");
            if (!int.TryParse(rawClienteId, out int idCliente))
            {
                return Unauthorized();
            }
            await _carritoService.PostCarrito(idCliente);
            return Ok();
        }

        [Authorize]
        [HttpPut("home")]
        public async Task<IActionResult> AgregarItemHome(CarritoRequest request)
        {
            string rawClienteId = HttpContext.User.FindFirstValue("id");
            if (!int.TryParse(rawClienteId, out int idCliente))
            {
                return Unauthorized();
            }
            await _carritoService.AgregarItemHome(request.idArticulo, idCliente);
            return Ok();
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> AgregarItemCarrito(CarritoItemoRequest request)
        {
            await _carritoService.AgregarItemCarrito(request.idCarritoItem,request.cantidad);
            return Ok();
        }
        
    }
}
