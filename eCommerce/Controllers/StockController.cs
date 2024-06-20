using eCommerce.Data.IRepository;
using eCommerce.Entitys.Response;
using eCommerce.Entitys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using eCommerce.Entitys.Request;

namespace eCommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _StockRepository;

        public StockController(IStockRepository StockRepository)
        {
            _StockRepository = StockRepository;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _StockRepository.GetById(id));
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var articulos = await _StockRepository.GetAll();

            return Ok(articulos);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("articulo/{idArticulo}")]
        public async Task<IActionResult> GetImagenes(int idArticulo)
        {
            var images = await _StockRepository.GetByIdArticulo(idArticulo);

            return Ok(images);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post(stockRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequestModelState();
            }

            await _StockRepository.CreateStock(request);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Stock stock)
        {
            if (id != stock.id)
            {
                return BadRequest();
            }

            await _StockRepository.Update(stock);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _StockRepository.Delete(id) == 0)
                return BadRequest("El Articulo no existe");
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
