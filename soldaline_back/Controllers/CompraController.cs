using Microsoft.AspNetCore.Mvc;
using soldaline_back.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using soldaline_back.DTOs;

namespace soldaline_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraController : Controller
    {
        private readonly SoldalineBdContext _context;

        public CompraController(SoldalineBdContext context)
        {
            _context = context;
        }

        // Método para registrar una nueva compra
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CompraRegisterDTO compraDTO)
        {
            if (compraDTO == null || compraDTO.DetalleCompras.Count == 0)
            {
                return BadRequest("Datos inválidos.");
            }

            // Verificar que el usuario exista
            var usuarioExiste = await _context.Usuarios.AnyAsync(u => u.Id == compraDTO.UsuarioId);
            if (!usuarioExiste)
            {
                return BadRequest("Usuario no encontrado.");
            }

            // Crear una nueva compra
            var compra = new Compra
            {
                Fecha = compraDTO.Fecha,
                UsuarioId = compraDTO.UsuarioId
            };

            _context.Compras.Add(compra);
            await _context.SaveChangesAsync();

            // Agregar detalles de compra
            foreach (var detalleDTO in compraDTO.DetalleCompras)
            {
                var detalleCompra = new Detallecompra
                {
                    Cantidad = detalleDTO.Cantidad,
                    Folio = detalleDTO.Folio,
                    Descripcion = detalleDTO.Descripcion,
                    Costo = detalleDTO.Costo,
                    CompraId = compra.Id // Asociar el detalle a la compra creada
                };

                _context.Detallecompras.Add(detalleCompra);
            }

            await _context.SaveChangesAsync();

            return Ok("Compra registrada exitosamente.");
        }

        // Método para obtener una compra y sus detalles
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompra(int id)
        {
            var compra = await _context.Compras
                .Include(c => c.Detallecompras)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (compra == null)
            {
                return NotFound("Compra no encontrada.");
            }

            // Crear un DTO para la respuesta
            var response = new CompraResponseDTO
            {
                Id = compra.Id,
                Fecha = compra.Fecha,
                UsuarioId = compra.UsuarioId,
                DetalleCompras = compra.Detallecompras.Select(dc => new DetalleCompraResponseDTO
                {
                    Id = dc.Id,
                    Cantidad = dc.Cantidad,
                    Folio = dc.Folio,
                    Descripcion = dc.Descripcion,
                    Costo = dc.Costo
                }).ToList()
            };

            return Ok(response);
        }
    }
}
