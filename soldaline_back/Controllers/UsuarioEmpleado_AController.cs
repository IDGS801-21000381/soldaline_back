using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using soldaline_back.Models;
using System.Data;
using Microsoft.Data.SqlClient;
using soldaline_back.DTOs;
using System.Threading.Tasks;
using System.Security.Cryptography;
using BCrypt.Net;
namespace soldaline_back.Controllers
{
    [ApiController]
    [Route("api/")]
    public class UsuarioEmpleado_AController : ControllerBase
    {
        private readonly SoldalineBdContext _context;

        public UsuarioEmpleado_AController(SoldalineBdContext context)
        {
            _context = context;
        }

        [HttpPost("CrearUsuario")]
        public async Task<ActionResult> CrearUsuario([FromBody] UsuarioRegisterDTO request)

        {

            // Validar que la contraseña no sea nula o vacía
            if (string.IsNullOrWhiteSpace(request.Contrasenia))
            {
                return BadRequest("La contraseña es requerida.");
            }

            // Encriptar la contraseña usando BCrypt
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Contrasenia);


            // Define los parámetros del procedimiento almacenado
            var parameters = new[]
            {
                new SqlParameter("@nombres", request.Nombres ?? (object)DBNull.Value),
                new SqlParameter("@apellidoM", request.ApellidoM ?? (object)DBNull.Value),
                new SqlParameter("@apellidoP", request.ApellidoP ?? (object)DBNull.Value),
                new SqlParameter("@correo", request.Correo ?? (object)DBNull.Value),
                new SqlParameter("@estatusDetalle", request.EstatusDetalle),
                new SqlParameter("@nombre", request.Nombre ?? (object)DBNull.Value),
                new SqlParameter("@contrasenia", hashedPassword),
                new SqlParameter("@rol", request.Rol ?? (object)DBNull.Value),
                new SqlParameter("@estatusUsuario", request.EstatusUsuario),
                new SqlParameter("@urlImage", request.UrlImage ?? (object)DBNull.Value),
                new SqlParameter("@direccion", request.Direccion ?? (object)DBNull.Value),
                new SqlParameter("@tarjeta", request.Tarjeta ?? (object)DBNull.Value),
            };

            // Ejecuta el procedimiento almacenado
            var resultado = await _context.Database.ExecuteSqlRawAsync("EXEC InsertarUsuarioYDetalles @nombres, @apellidoM, @apellidoP, @correo, @estatusDetalle, @nombre, @contrasenia, @rol, @estatusUsuario, @urlImage, @direccion, @tarjeta", parameters);

            if (resultado > 0)
            {
                return Ok(new { Message = "Usuario creado exitosamente" });
            }
            else
            {
                return BadRequest(new { Message = "Error al crear usuario" });
            }
        }

        [HttpGet("ObtenerUsuario/{id}")]
        public async Task<ActionResult<UsuarioResponseDTO>> ObtenerUsuario(int id)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.DetallesUsuario)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            // Crear un DTO para la respuesta
            var usuarioResponse = new UsuarioResponseDTO
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Rol = usuario.Rol,
                Estatus = usuario.Estatus,
                UrlImage = usuario.UrlImage,
                Direccion = usuario.Direccion,
                ClientePotencial = usuario.ClientePotencial
            };

            return Ok(usuarioResponse);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDTO loginDTO)
        {
            if (loginDTO == null)
            {
                return BadRequest("Datos inválidos.");
            }

            // Buscar usuario por correo
            var usuario = await _context.Usuarios
                .Include(u => u.DetallesUsuario)
                .FirstOrDefaultAsync(u => u.DetallesUsuario.Correo == loginDTO.Correo);

            if (usuario == null)
            {
                return Unauthorized("Usuario no encontrado.");
            }

            // Verificar la contraseña
            bool contraseniaValida = BCrypt.Net.BCrypt.Verify(loginDTO.Contrasenia, usuario.Contrasenia);
            if (!contraseniaValida)
            {
                return Unauthorized("Contraseña incorrecta.");
            }

            // Aquí puedes generar un token de autenticación (JWT)
            var token = "GENERAR_TOKEN_AQUI"; // Implementar un sistema de JWT en lugar de un string

            usuario.Token = token; // Suponiendo que tu modelo de usuario tiene una propiedad Token
            await _context.SaveChangesAsync();

            // Crear un DTO para la respuesta
            var usuarioResponse = new UsuarioResponseDTO
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Rol = usuario.Rol,
                Estatus = usuario.Estatus,
                UrlImage = usuario.UrlImage,
                Direccion = usuario.Direccion,
                ClientePotencial = usuario.ClientePotencial
            };

            return Ok(usuarioResponse);
        }
    }
}
