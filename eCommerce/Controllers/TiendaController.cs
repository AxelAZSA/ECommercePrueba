using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.Data.IRepository;
using eCommerce.Entitys.Response;
using eCommerce.Entitys;

namespace eCommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiendaController : ControllerBase
    {
        private readonly ITiendaRepository _TiendaRepository;

        public TiendaController(ITiendaRepository TiendaRepository)
        {
            _TiendaRepository = TiendaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _TiendaRepository.GetAll());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post(Tienda tienda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequestModelState();
            }

            await _TiendaRepository.Create(tienda);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Tienda tienda)
        {
            if (id != tienda.id)
            {
                return BadRequest();
            }

            await _TiendaRepository.Update(tienda);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("articuloTienda/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _TiendaRepository.Delete(id) == 0)
                return BadRequest("El articulo no existe");
            else
                return Ok("Registro eliminado con éxito");
        }

        private IActionResult BadRequestModelState()
        {
            IEnumerable<string> errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
            return BadRequest(new ErrorResponse(errors));
        }
    }
}
