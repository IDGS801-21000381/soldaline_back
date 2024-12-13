using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using soldaline_back.DTOs;
using soldaline_back.Models;
using System.Threading.Tasks;
using System.Security.Cryptography;
using BCrypt.Net;
using Microsoft.Data.SqlClient;

namespace soldaline_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly SoldalineBdContext _context;

        public UsuarioController(SoldalineBdContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UsuarioRegisterDTO usuarioDTO)
        {
            if (usuarioDTO == null || string.IsNullOrEmpty(usuarioDTO.Correo) || string.IsNullOrEmpty(usuarioDTO.Contrasenia))
            {
                return BadRequest("Datos inválidos.");
            }


            var existeCorreo = await _context.DetallesUsuarios
                .AnyAsync(u => u.Correo == usuarioDTO.Correo);

            if (existeCorreo)
            {
                return BadRequest("Correo ya registrado.");
            }


            var contraseniaEncriptada = BCrypt.Net.BCrypt.HashPassword(usuarioDTO.Contrasenia);


            var detallesUsuario = new DetallesUsuario
            {
                Nombres = usuarioDTO.Nombres,
                ApellidoP = usuarioDTO.ApellidoP,
                ApellidoM = usuarioDTO.ApellidoM,
                Correo = usuarioDTO.Correo
            };


            _context.DetallesUsuarios.Add(detallesUsuario);
            await _context.SaveChangesAsync(); // Necesario para obtener el ID generado

            var usuario = new Usuario
            {
                Nombre = usuarioDTO.Nombre,
                Contrasenia = contraseniaEncriptada, // Guardamos la contraseña encriptada
                Rol = usuarioDTO.Rol,
                UrlImage = usuarioDTO.UrlImage,
                Direccion = usuarioDTO.Direccion,
                Tarjeta = usuarioDTO.Tarjeta,
                Estatus = 1, // Usuario activo
                DetallesUsuarioId = detallesUsuario.Id // Asignamos el ID de DetallesUsuario
            };


            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return Ok("Usuario registrado exitosamente.");
        }

        [HttpPost("CrearUsuario")]
        public async Task<ActionResult> CrearUsuario([FromBody] UsuarioRegisterDTO request)
        {
            // Validar la contraseña
            if (string.IsNullOrWhiteSpace(request.Contrasenia))
            {
                return BadRequest("La contraseña es requerida.");
            }

            try
            {
                // Encriptar la contraseña
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Contrasenia);

                // Asignar 1 como valor por defecto para estatusDetalle y estatusUsuario
                byte estatusDetalle = 1;
                byte estatusUsuario = 1;

                // Parámetros del procedimiento almacenado
                var parameters = new[]
                {
            new SqlParameter("@nombres", request.Nombres ?? (object)DBNull.Value),
            new SqlParameter("@apellidoM", request.ApellidoM ?? (object)DBNull.Value),
            new SqlParameter("@apellidoP", request.ApellidoP ?? (object)DBNull.Value),
            new SqlParameter("@correo", request.Correo ?? (object)DBNull.Value),
            new SqlParameter("@estatusDetalle", estatusDetalle),  // Aquí asignamos el valor 1 por defecto si es nulo
            new SqlParameter("@nombre", request.Nombre ?? (object)DBNull.Value),
            new SqlParameter("@contrasenia", hashedPassword),
            new SqlParameter("@rol", request.Rol ?? (object)DBNull.Value),
            new SqlParameter("@estatusUsuario", estatusUsuario),  // Aquí asignamos el valor 1 por defecto si es nulo
            new SqlParameter("@urlImage", request.UrlImage ?? (object)DBNull.Value),
            new SqlParameter("@direccion", request.Direccion ?? (object)DBNull.Value),
            new SqlParameter("@tarjeta", request.Tarjeta ?? (object)DBNull.Value),
        };

                // Ejecutar el procedimiento almacenado
                var resultado = await _context.Database.ExecuteSqlRawAsync(
                    "EXEC InsertarUsuarioYDetalles @nombres, @apellidoM, @apellidoP, @correo, @estatusDetalle, @nombre, @contrasenia, @rol, @estatusUsuario, @urlImage, @direccion, @tarjeta",
                    parameters);

                if (resultado > 0)
                {
                    return Ok(new { Message = "Usuario creado exitosamente" });
                }
                else
                {
                    return BadRequest(new { Message = "Error al crear usuario. Verifica los datos ingresados." });
                }
            }
            catch (SqlException ex)
            {
                // Loguear el error para depuración
                return StatusCode(500, new { Message = "Error en la base de datos.", Error = ex.Message });
            }
            catch (Exception ex)
            {
                // Loguear el error para depuración
                return StatusCode(500, new { Message = "Error inesperado.", Error = ex.Message });
            }
        }








        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDTO loginDTO)
        {
            if (loginDTO == null)
            {
                return Ok("Datos inválidos.");
            }

         
            var usuario = await _context.Usuarios
                .Include(u => u.DetallesUsuario)
                .FirstOrDefaultAsync(u => u.DetallesUsuario.Correo == loginDTO.Correo);

            if (usuario == null)
            {
                return Unauthorized("Usuario no encontrado.");
            }

 
            bool contraseniaValida = BCrypt.Net.BCrypt.Verify(loginDTO.Contrasenia, usuario.Contrasenia);
            if (!contraseniaValida)
            {
                return Unauthorized("Contraseña incorrecta.");
            }


            var token = "GENERAR_TOKEN_AQUI"; // Implementar un sistema de JWT en lugar de un string

            usuario.Token = token;
            await _context.SaveChangesAsync();

         
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


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] UsuarioUpdateDTO updateDTO)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            usuario.Nombre = updateDTO.Nombre ?? usuario.Nombre;
            usuario.Rol = updateDTO.Rol ?? usuario.Rol;
            usuario.UrlImage = updateDTO.UrlImage ?? usuario.UrlImage;
            usuario.Direccion = updateDTO.Direccion ?? usuario.Direccion;
            usuario.Tarjeta = updateDTO.Tarjeta ?? usuario.Tarjeta;

            await _context.SaveChangesAsync();

            return Ok("Usuario actualizado exitosamente.");
        }

       [HttpPut("change-password/{id}")]
        public async Task<IActionResult> UpdatePassword(int id, [FromBody] UsuarioUpdatePasswordDTO passwordDTO)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound("Usuario no encontrado.");
            }

       
            bool contraseniaValida = BCrypt.Net.BCrypt.Verify(passwordDTO.ContraseniaActual, usuario.Contrasenia);
            if (!contraseniaValida)
            {
                return Unauthorized("La contraseña actual no es válida.");
            }


            usuario.Contrasenia = BCrypt.Net.BCrypt.HashPassword(passwordDTO.NuevaContrasenia);
            await _context.SaveChangesAsync();

            return Ok("Contraseña actualizada exitosamente.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            usuario.Estatus = 0; // Establecer estado como inactivo
            await _context.SaveChangesAsync();

            return Ok("Usuario eliminado lógicamente.");
        }

 
       [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.DetallesUsuario)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null)
            {
                return NotFound("Usuario no encontrado.");
            }

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

        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsuarios()
        {
            var usuarios = await _context.Usuarios
                .Include(u => u.DetallesUsuario)
                .ToListAsync();

            if (usuarios == null || usuarios.Count == 0)
            {
                return NotFound("No hay usuarios registrados.");
            }

            // Mapeo de usuarios a DTO
            var usuariosResponse = usuarios.Select(usuario => new UsuarioResponseDTO
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Rol = usuario.Rol,
                Estatus = usuario.Estatus,
                UrlImage = usuario.UrlImage,
                Direccion = usuario.Direccion,
            }).ToList();

            return Ok(usuariosResponse);
        }

        [HttpGet("getAllClientes")]
        public async Task<IActionResult> GetAllClientes()
        {
            var clientes = await _context.Usuarios
                .Where(u => u.Rol == "cliente")
                .Include(u => u.DetallesUsuario)
                .ToListAsync();

            if (clientes == null || clientes.Count == 0)
            {
                return NotFound("No hay clientes registrados.");
            }


           var clientesResponse = clientes.Select(usuario => new UsuarioResponseDTO
           {
               Id = usuario.Id,
               Nombre = usuario.Nombre,
               Rol = usuario.Rol,
               Estatus = usuario.Estatus,
               UrlImage = usuario.UrlImage,
               Direccion = usuario.Direccion,
               ClientePotencial = usuario.ClientePotencial
           }).ToList();

            return Ok(clientesResponse);
        }

  
        [HttpGet("getAllEmpleados")]
        public async Task<IActionResult> GetAllEmpleados()
        {
            var empleados = await _context.Usuarios
                .Where(u => u.Rol != "6")
                .Include(u => u.DetallesUsuario)
                .ToListAsync();

            if (empleados == null || empleados.Count == 0)
            {
                return NotFound("No hay empleados registrados.");
            }

        
            var empleadosResponse = empleados.Select(usuario => new UsuarioResponseDTO
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Rol = usuario.Rol,
                Estatus = usuario.Estatus,
                UrlImage = usuario.UrlImage,
                Direccion = usuario.Direccion,
                ClientePotencial = null // Excluimos este campo para empleados
            }).ToList();

            return Ok(empleadosResponse);
        }

  

    }


}
