using Microsoft.AspNetCore.Mvc;
using soldaline_back.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using soldaline_back.DTOs;

namespace soldaline_back.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class proveedorcontroller : Controller
    {
        private readonly SoldalineBdContext _context;

        public proveedorcontroller(SoldalineBdContext context)
        {
            _context = context;
        }

        // método para registrar un nuevo proveedor
        [HttpPost("register")]
        public async Task<IActionResult> register([FromBody] ProveedorRegisterDTO proveedordto)
        {
            if (proveedordto == null)
            {
                return BadRequest("datos inválidos.");
            }

            // verificar si el proveedor ya está registrado por el nombre de la empresa o teléfono de contacto
            var existeproveedor = await _context.Proveedors
                .AnyAsync(p => p.NombreEmpresa == proveedordto.NombreEmpresa || p.TelefonoContacto == proveedordto.TelefonoContacto);

            if (existeproveedor)
            {
                return BadRequest("proveedor ya registrado.");
            }

            // crear el objeto proveedor a partir del dto
            var proveedor = new Proveedor
            {
                NombreEmpresa = proveedordto.NombreEmpresa,
                Direccion = proveedordto.Direccion,
                TelefonoContacto = proveedordto.TelefonoContacto,
                NombreContacto = proveedordto.NombreContacto,
                ApellidoM = proveedordto.ApellidoM,
                ApellidoP = proveedordto.ApellidoP,
                Estatus = 1 // proveedor activo por defecto
            };

            // guardar el proveedor en la base de datos
            _context.Proveedors.Add(proveedor);
            await _context.SaveChangesAsync();

            return Ok("proveedor registrado exitosamente.");
        }

        // método para obtener los detalles de un proveedor
        [HttpGet("{id}")]
        public async Task<IActionResult> getproveedor(int id)
        {
            var proveedor = await _context.Proveedors.FirstOrDefaultAsync(p => p.Id == id);

            if (proveedor == null)
            {
                return NotFound("proveedor no encontrado.");
            }


            // crear un dto para la respuesta
            var proveedorresponse = new ProveedorResponseDTO
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

            return Ok(proveedorresponse);
        }
    }
}
