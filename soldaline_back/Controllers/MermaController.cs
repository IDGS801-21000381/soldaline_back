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
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] MermaRegisterDTO mermaDTO)
        {
            if (mermaDTO == null)
            {
                return BadRequest("Datos inválidos.");
            }

            // Verificar que el usuario exista
            var usuarioExiste = await _context.Usuarios
                .AnyAsync(u => u.Id == mermaDTO.UsuarioId);

            if (!usuarioExiste)
            {
                return BadRequest("Usuario no encontrado.");
            }

            // Crear la nueva merma a partir del DTO
            var merma = new Merma
            {
                Cantidad = mermaDTO.Cantidad,
                Descripcion = mermaDTO.Descripcion,
                Fecha = mermaDTO.Fecha,
                UsuarioId = mermaDTO.UsuarioId,
                InventarioProductoId = mermaDTO.InventarioProductoId,
                InventariomaterialesId = mermaDTO.InventariomaterialesId
            };

            // Guardar la nueva merma en la base de datos
            _context.Mermas.Add(merma);
            await _context.SaveChangesAsync();

            return Ok("Merma registrada exitosamente.");
        }

        // Método para obtener los detalles de una merma
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
    }
}
