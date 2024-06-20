 using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using eCommerce.Data.IRepository;
using eCommerce.Entitys;
using eCommerce.Entitys.Response;
using eCommerce.Bussiness.AuthenticationService;
using eCommerce.Bussiness.TokenService;
using eCommerce.Bussiness.Service.password;
using eCommerce.Entitys.Request;
using eCommerce.Entitys.Tokens;
using eCommerce.Entitys.DTOs;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel.DataAnnotations;

namespace eCommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private const string role = "Cliente";
        private readonly IPasswordHasher _passwordHasher;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly RefreshTokenValidator _refreshTokenValidator;
        private readonly Authenticator _authenticator;
        private readonly IClienteRepository _ClienteRepository;

        public ClienteController(IClienteRepository ClienteRepository, IPasswordHasher passwordHasher, IRefreshTokenRepository refreshTokenRepository, Authenticator authenticator, RefreshTokenValidator refreshTokenValidator)
        {
            _ClienteRepository = ClienteRepository;
            _passwordHasher = passwordHasher;
            _authenticator = authenticator;
            _refreshTokenValidator = refreshTokenValidator;
            _refreshTokenRepository = refreshTokenRepository;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequestModelState();
            }

            var existingUser = await _ClienteRepository.GetByCorreo(login.correo);

            if (existingUser == null)
            {
                return Unauthorized();
            }

            return Ok(await _authenticator.Authentication(existingUser));
        }

        [Authorize]
        [HttpDelete("logout")]
        public async Task<IActionResult> Logout()
        {
            string rawUserId = HttpContext.User.FindFirstValue("id");
            if (!int.TryParse(rawUserId, out int userId))
            {
                return Unauthorized();
            }
            await _refreshTokenRepository.DeleteAll(userId, role);
            return NoContent();
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequestModelState();
            }
            bool validateToken = _refreshTokenValidator.Validate(request.RefreshToken);
            if (!validateToken)
            {
                return BadRequest(new ErrorResponse("Invalid Refresh Token"));
            }
            RefreshToken refreshTokenDTO = await _refreshTokenRepository.GetByToken(request.RefreshToken, role);
            if (refreshTokenDTO == null)
            {
                return NotFound(new ErrorResponse("Invalid refresh token"));
            }
            await _refreshTokenRepository.DeleteRefreshToken(refreshTokenDTO.id);
            Cliente sesion = await _ClienteRepository.GetById(refreshTokenDTO.idSesion);
            if (sesion == null)
            {
                return NotFound(new ErrorResponse("User doesn´t exist"));
            }

            return Ok(await _authenticator.Authentication(sesion));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string rawUserId = HttpContext.User.FindFirstValue("id");
            if (!int.TryParse(rawUserId, out int clienteId))
            {
                return Unauthorized();
            }
            var cliente = await _ClienteRepository.GetById(clienteId);
            ClienteDTO dto = new ClienteDTO()
            {
                nombre = cliente.nombre,
                apellido = cliente.apellido,
                direccion = cliente.direccion,
                correo = cliente.correo
            };

            return Ok(dto);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Post(ClienteRequest cliente)
        {
            if (!ModelState.IsValid)
            {
                 return BadRequestModelState();
            }

            if (cliente.password != cliente.confirmPassword)
            {
                return BadRequest(new ErrorResponse("password no coincide con confirmPassword"));
            }

            var existingUser = await _ClienteRepository.GetByCorreo(cliente.correo);

            if (existingUser != null)
            {
                return Conflict(new ErrorResponse("Ya hay una cuenta con ese correo"));
            }

            Login login = new Login()
            {
                correo = cliente.correo,
                password = cliente.password
            };

            cliente.password = _passwordHasher.Hash(cliente.password);

            Cliente clienteC = new Cliente()
            {
                nombre = cliente.nombre,
                apellido = cliente.apellido,
                direccion = cliente.direccion,
                correo = cliente.correo,
                password = cliente.confirmPassword
            };

            await _ClienteRepository.Create(clienteC);
            return await Login(login);
        }

        [Authorize]
        [HttpPut("{id}")]
            public async Task<IActionResult> Put(int id, Cliente cliente)
            {
                if (id != cliente.id)
                {
                    return BadRequest();
                }

                await _ClienteRepository.Update(cliente);

                return Ok();
            }

        [Authorize]
        [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                if (await _ClienteRepository.Delete(id) == 0)
                    return BadRequest("El cliente no existe");
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

