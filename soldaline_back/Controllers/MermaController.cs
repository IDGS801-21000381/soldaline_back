using Microsoft.AspNetCore.Mvc;
using soldaline_back.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using soldaline_back.DTOs;

namespace soldaline_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MermaController : Controller
    {
        private readonly SoldalineBdContext _context;

        public MermaController(SoldalineBdContext context)
        {
            _context = context;
        }

        // Método para registrar una nueva merma
        [HttpPost("calcular-merma")]
        public async Task<IActionResult> CalcularMerma(int idFabricacion, int cantidad)
        {
            if (idFabricacion <= 0 || cantidad <= 0)
            {
                return BadRequest("El idFabricacion y la cantidad deben ser mayores a cero.");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Obtener los materiales relacionados con la fabricación
                var materiales = await _context.Materialfabricacions
                    .Where(m => m.FabricacionId == idFabricacion)
                    .Select(m => new
                    {
                        m.MaterialId,
                        m.Cantidad
                    })
                    .ToListAsync();

                if (materiales == null || !materiales.Any())
                {
                    return NotFound("No se encontraron materiales asociados a la fabricación.");
                }

                // Iterar sobre los materiales para calcular la merma y actualizar el inventario
                foreach (var material in materiales)
                {
                    // Calcular la cantidad requerida para la merma
                    int? cantidadMerma = material.Cantidad * cantidad;

                    // Obtener el inventario del material
                    var inventarioMaterial = await _context.Inventariomateriales
                        .FirstOrDefaultAsync(i => i.MaterialId == material.MaterialId);

                    if (inventarioMaterial == null || inventarioMaterial.Cantidad < cantidadMerma)
                    {
                        return BadRequest($"No hay suficiente inventario para el material con ID {material.MaterialId}.");
                    }

                    // Restar la cantidad merma del inventario
                    inventarioMaterial.Cantidad -= cantidadMerma;

                    // Registrar la merma
                    var nuevaMerma = new Merma
                    {
                        Cantidad = cantidadMerma,
                        Descripcion = $"Merma generada para fabricación ID {idFabricacion}.",
                        Fecha = DateTime.Now,
                        UsuarioId = 1, // Cambiar por el ID real del usuario
                        InventariomaterialesId = inventarioMaterial.Id
                    };

                    _context.Mermas.Add(nuevaMerma);
                }

                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();

                // Confirmar la transacción
                await transaction.CommitAsync();

                return Ok("Merma calculada y registrada exitosamente.");
            }
            catch (Exception ex)
            {
                // Revertir la transacción en caso de error
                await transaction.RollbackAsync();
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }

        // Método para obtener los detalles de una merma por su ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMerma(int id)
        {
            var merma = await _context.Mermas
                .Include(m => m.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (merma == null)
            {
                return NotFound("Merma no encontrada.");
            }

            // Crear un DTO para la respuesta
            var mermaResponse = new MermaResponseDTO
            {
                Id = merma.Id,
                Cantidad = merma.Cantidad,
                Descripcion = merma.Descripcion,
                Fecha = merma.Fecha,
                UsuarioId = merma.UsuarioId,
                InventarioProductoId = merma.InventarioProductoId,
                InventariomaterialesId = merma.InventariomaterialesId,
                UsuarioNombre = merma.Usuario.Nombre
            };

            return Ok(mermaResponse);
        }

        // Método para obtener todas las mermas asociadas a productos
        [HttpGet("productos")]
        public async Task<IActionResult> GetAllMermasByProducto()
        {
            var mermas = await _context.Mermas
                .Where(m => m.InventarioProductoId != null)
                .Include(m => m.Usuario)
                .ToListAsync();

            if (mermas == null || mermas.Count == 0)
            {
                return NotFound("No hay mermas asociadas a productos.");
            }

            // Crear una lista de respuesta de DTOs
            var mermasResponse = mermas.Select(merma => new MermaResponseDTO
            {
                Id = merma.Id,
                Cantidad = merma.Cantidad,
                Descripcion = merma.Descripcion,
                Fecha = merma.Fecha,
                UsuarioId = merma.UsuarioId,
                InventarioProductoId = merma.InventarioProductoId,
                UsuarioNombre = merma.Usuario.Nombre
            }).ToList();

            return Ok(mermasResponse);
        }

        // Método para obtener todas las mermas asociadas a materiales
        [HttpGet("materiales")]
        public async Task<IActionResult> GetAllMermasByMateriales()
        {
            var mermas = await _context.Mermas
                .Where(m => m.InventariomaterialesId != null)
                .Include(m => m.Usuario)
                .ToListAsync();

            if (mermas == null || mermas.Count == 0)
            {
                return NotFound("No hay mermas asociadas a materiales.");
            }

            // Crear una lista de respuesta de DTOs
            var mermasResponse = mermas.Select(merma => new MermaResponseDTO
            {
                Id = merma.Id,
                Cantidad = merma.Cantidad,
                Descripcion = merma.Descripcion,
                Fecha = merma.Fecha,
                UsuarioId = merma.UsuarioId,
                InventariomaterialesId = merma.InventariomaterialesId,
                UsuarioNombre = merma.Usuario.Nombre
            }).ToList();

            return Ok(mermasResponse);
        }
    }
}
