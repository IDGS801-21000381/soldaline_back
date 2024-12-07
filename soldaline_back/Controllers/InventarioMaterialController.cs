using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using soldaline_back.Models;
using soldaline_back.DTOs;
using System.Linq;
using System.Threading.Tasks;



namespace soldaline_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioMaterialesController : ControllerBase
    {
        private readonly SoldalineBdContext _context;

        public InventarioMaterialesController(SoldalineBdContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] InventarioMaterialesResponseDTO inventarioDTO)
        {
            if (inventarioDTO == null)
                return BadRequest("Datos inválidos.");
            inventarioDTO.DetalleCompraId = 1;

            // Crear una instancia del modelo a partir del DTO
            var inventario = new Inventariomateriale
            {
                Cantidad = inventarioDTO.Cantidad,
                ProveedorId = inventarioDTO.ProveedorId,
                MaterialId = inventarioDTO.MaterialId,
                DetallecompraId = 1
            };

            // Agregar el modelo al contexto
            _context.Inventariomateriales.Add(inventario);
            await _context.SaveChangesAsync();

            return Ok("Inventario registrado exitosamente.");
        }


        // Obtener detalles de un inventario por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInventario(int id)
        {
            var inventario = await _context.Inventariomateriales.FindAsync(id);

            if (inventario == null)
                return NotFound("Inventario no encontrado.");

            var inventarioResponse = new InventarioMaterialesResponseDTO
            {
                Id = inventario.Id,
                Cantidad = inventario.Cantidad ?? 0,
                ProveedorId = inventario.ProveedorId,
                MaterialId = inventario.MaterialId,
                DetalleCompraId = inventario.DetallecompraId
            };

            return Ok(inventarioResponse);
        }

        // Obtener todos los inventarios
        [HttpGet("all")]
        public async Task<IActionResult> GetAllInventarios()
        {
            // Carga los inventarios con las relaciones de proveedor y material
            var inventarios = await _context.Inventariomateriales
                .Include(i => i.Proveedor)  // Relación con Proveedor
                .Include(i => i.Material)   // Relación con Material
                .Select(i => new InventarioMaterialesResponseDTO
                {
                    Id = i.Id,
                    Cantidad = i.Cantidad ?? 0,
                    ProveedorId = i.ProveedorId,  // ID del proveedor
                    MaterialId = i.MaterialId,    // ID del material
                    DetalleCompraId = i.DetallecompraId,
                    NombreEmpresaProveedor = i.Proveedor.NombreEmpresa,  // Nombre de la empresa del proveedor
                    NombreMaterial = i.Material.Nombre // Nombre del material
                }).ToListAsync();

            if (inventarios == null || !inventarios.Any())
                return NotFound("No se encontraron inventarios.");

            return Ok(inventarios);
        }

        [HttpGet("material")]
        public async Task<IActionResult> GetAllMaterials()
        {
            // Carga todos los materiales
            var materiales = await _context.Materials
                .Select(m => new MaterialDTO
                {
                    Id = m.Id,
                    Nombre = m.Nombre
                })
                .ToListAsync();

            if (materiales == null || !materiales.Any())
                return NotFound("No se encontraron materiales.");

            return Ok(materiales);
        }




    }
}
