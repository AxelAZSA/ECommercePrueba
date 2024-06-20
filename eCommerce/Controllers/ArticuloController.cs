using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using eCommerce.Data.IRepository;
using eCommerce.Entitys.Response;
using eCommerce.Entitys;
using System.IO;
using System.Collections;
using System;
using System.Text;

namespace eCommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticuloController : ControllerBase
    {
        private readonly IArticuloRepository _ArticuloRepository;
        private readonly IImagenesRepository _IImagenesRepository;

        public ArticuloController(IArticuloRepository ArticuloRepository, IImagenesRepository IImagenesRepository)
        {
            _ArticuloRepository = ArticuloRepository;
            _IImagenesRepository = IImagenesRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _ArticuloRepository.GetById(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var articulos = await _ArticuloRepository.GetAll();

            return Ok(articulos);
        }

        [HttpGet("imagen/{idArticulo}")]
        public async Task<IActionResult> GetImagenes(int idArticulo)
        {
            var images = await _IImagenesRepository.GetByIdArticulo(idArticulo);

            return Ok(images);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("imagen/{idArticulo}")]
        public async Task<IActionResult> PostImagen(int idArticulo,[FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No se ha proporcionado un archivo.");
            }

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                var imagen = new imagenes
                {
                    imagen = memoryStream.ToArray(),
                    ContentType = file.ContentType,
                    idArticulo = idArticulo
                };

                await _IImagenesRepository.Create(imagen);

                return Ok();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("imagen/{id}")]
        public async Task<IActionResult> DeleteImagen(int id)
        {
            if (await _IImagenesRepository.Delete(id) == 0)
                return BadRequest("La imagen no existe");
            else
                return Ok("Registro eliminado con éxito");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post(Articulo articulo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequestModelState();
            }

            await _ArticuloRepository.Create(articulo);
            
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Articulo Articulo)
        {
            if (id != Articulo.id)
            {
                return BadRequest();
            }

            await _ArticuloRepository.Update(Articulo);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _ArticuloRepository.Delete(id) == 0)
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
