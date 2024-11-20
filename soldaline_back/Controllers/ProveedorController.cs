using Microsoft.AspNetCore.Mvc;
using soldaline_back.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using soldaline_back.DTOs;

namespace soldaline_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : Controller
    {
        private readonly SoldalineBdContext _context;

        public ProveedorController(SoldalineBdContext context)
        {
            _context = context;
        }

        // Método para registrar un nuevo proveedor
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] ProveedorRegisterDTO proveedorDTO)
        {
            if (proveedorDTO == null)
            {
                return BadRequest("Datos inválidos.");
            }

            // Verificar si el proveedor ya está registrado por el nombre de la empresa o teléfono de contacto
            var existeProveedor = await _context.Proveedors
                .AnyAsync(p => p.NombreEmpresa == proveedorDTO.NombreEmpresa || p.TelefonoContacto == proveedorDTO.TelefonoContacto);

            if (existeProveedor)
            {
                return BadRequest("Proveedor ya registrado.");
            }

            // Crear el objeto Proveedor a partir del DTO
            var proveedor = new Proveedor
            {
                NombreEmpresa = proveedorDTO.NombreEmpresa,
                Direccion = proveedorDTO.Direccion,
                TelefonoContacto = proveedorDTO.TelefonoContacto,
                NombreContacto = proveedorDTO.NombreContacto,
                ApellidoM = proveedorDTO.ApellidoM,
                ApellidoP = proveedorDTO.ApellidoP,
                Estatus = 1 // Proveedor activo por defecto
            };

            // Guardar el proveedor en la base de datos
            _context.Proveedors.Add(proveedor);
            await _context.SaveChangesAsync();

            return Ok("Proveedor registrado exitosamente.");
        }

        // Método para obtener los detalles de un proveedor
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProveedor(int id)
        {
            var proveedor = await _context.Proveedors.FirstOrDefaultAsync(p => p.Id == id);

            if (proveedor == null)
            {
                return NotFound("Proveedor no encontrado.");
            }


            // Crear un DTO para la respuesta
            var proveedorResponse = new ProveedorResponseDTO
            {
                Id = proveedor.Id,
                NombreEmpresa = proveedor.NombreEmpresa,
                Direccion = proveedor.Direccion,
                TelefonoContacto = proveedor.TelefonoContacto,
                NombreContacto = proveedor.NombreContacto,
                ApellidoM = proveedor.ApellidoM,
                ApellidoP = proveedor.ApellidoP,
                Estatus = proveedor.Estatus
            };

            return Ok(proveedorResponse);
        }
    }
}
