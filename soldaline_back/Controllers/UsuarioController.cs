﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using soldaline_back.DTOs;
using soldaline_back.Models;
using System.Threading.Tasks;
using System.Security.Cryptography;
using BCrypt.Net;

namespace soldaline_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly SoldalineBd2Context _context;

        public UsuarioController(SoldalineBd2Context context)
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

            // Verificar si el correo ya está registrado
            var existeCorreo = await _context.DetallesUsuarios
                .AnyAsync(u => u.Correo == usuarioDTO.Correo);

            if (existeCorreo)
            {
                return BadRequest("Correo ya registrado.");
            }

            // Encriptar la contraseña del usuario
            var contraseniaEncriptada = BCrypt.Net.BCrypt.HashPassword(usuarioDTO.Contrasenia);

            // Crear el objeto DetallesUsuario
            var detallesUsuario = new DetallesUsuario
            {
                Nombres = usuarioDTO.Nombres,
                ApellidoP = usuarioDTO.ApellidoP,
                ApellidoM = usuarioDTO.ApellidoM,
                Correo = usuarioDTO.Correo
            };

            // Guardar DetallesUsuario en la base de datos
            _context.DetallesUsuarios.Add(detallesUsuario);
            await _context.SaveChangesAsync(); // Necesario para obtener el ID generado

            // Crear el objeto Usuario asociado
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

            // Guardar Usuario en la base de datos
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return Ok("Usuario registrado exitosamente.");
        }


        // Método para login de usuario
        [HttpPost("login")]
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

            usuario.Token = token;
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

        // Método para actualizar los datos del usuario (excepto contraseña)
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

        // Método para actualizar la contraseña del usuario
        [HttpPut("change-password/{id}")]
        public async Task<IActionResult> UpdatePassword(int id, [FromBody] UsuarioUpdatePasswordDTO passwordDTO)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            // Verificar la contraseña actual
            bool contraseniaValida = BCrypt.Net.BCrypt.Verify(passwordDTO.ContraseniaActual, usuario.Contrasenia);
            if (!contraseniaValida)
            {
                return Unauthorized("La contraseña actual no es válida.");
            }

            // Encriptar la nueva contraseña
            usuario.Contrasenia = BCrypt.Net.BCrypt.HashPassword(passwordDTO.NuevaContrasenia);
            await _context.SaveChangesAsync();

            return Ok("Contraseña actualizada exitosamente.");
        }

        // Método para eliminar usuario lógicamente (cambiar el estado)
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

        // Método para obtener los detalles del usuario
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

        //[HttpGet("all")]
        //public async Task<IActionResult> GetAllUsuarios()
        //{
        //    var usuarios = await _context.Usuarios
        //        .Include(u => u.DetallesUsuario)
        //        .ToListAsync();

        //    if (usuarios == null || usuarios.Count == 0)
        //    {
        //        return NotFound("No hay usuarios registrados.");
        //    }

        //    // Mapeo de usuarios a DTO
        //    var usuariosResponse = usuarios.Select(usuario => new UsuarioResponseDTO
        //    {
        //        Id = usuario.Id,
        //        Nombre = usuario.Nombre,
        //        Rol = usuario.Rol,
        //        Estatus = usuario.Estatus,
        //        UrlImage = usuario.UrlImage,
        //        Direccion = usuario.Direccion,
        //    }).ToList();

        //    return Ok(usuariosResponse);
        //}
        // Método para obtener todos los clientes (rol = 6)
        [HttpGet("getAllClientes")]
        public async Task<IActionResult> GetAllClientes()
        {
            var clientes = await _context.Usuarios
                .Where(u => u.Rol == "6")
                .Include(u => u.DetallesUsuario)
                .ToListAsync();

            if (clientes == null || clientes.Count == 0)
            {
                return NotFound("No hay clientes registrados.");
            }

            // Mapeo de clientes a DTO
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

        // Método para obtener todos los empleados (rol != 6)
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

            // Mapeo de empleados a DTO, eliminando el campo ClientePotencial
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

        // Método para obtener todos los usuarios (sin filtro)
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
                ClientePotencial = usuario.ClientePotencial
            }).ToList();

            return Ok(usuariosResponse);
        }

    }


}
