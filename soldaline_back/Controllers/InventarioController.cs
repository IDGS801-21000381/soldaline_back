using Microsoft.AspNetCore.Mvc;
using soldaline_back.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using soldaline_back.DTOs;

namespace soldaline_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioController : Controller
    {
        private readonly SoldalineBdContext _context;

        public InventarioController(SoldalineBdContext context)
        {
            _context = context;
        }

        #region Inventario de Materiales

        // Método para registrar un nuevo material en el inventario
        [HttpPost("material/register")]
        public async Task<IActionResult> RegisterMaterial([FromBody] InventarioMaterialRegisterDTO materialDTO)
        {
            if (materialDTO == null)
            {
                return BadRequest("Datos inválidos.");
            }

            // Verificar que el proveedor y el material existan
            var proveedorExiste = await _context.Proveedors.AnyAsync(p => p.Id == materialDTO.ProveedorId);
            var materialExiste = await _context.Materials.AnyAsync(m => m.Id == materialDTO.MaterialId);

            if (!proveedorExiste)
            {
                return BadRequest("Proveedor no encontrado.");
            }
            if (!materialExiste)
            {
                return BadRequest("Material no encontrado.");
            }

            // Crear un nuevo registro en el inventario de materiales
            var inventarioMaterial = new Inventariomateriale
            {
                Cantidad = materialDTO.Cantidad,
                ProveedorId = materialDTO.ProveedorId,
                MaterialId = materialDTO.MaterialId,
                DetallecompraId = materialDTO.DetallecompraId
            };

            _context.Inventariomateriales.Add(inventarioMaterial);
            await _context.SaveChangesAsync();

            return Ok("Material registrado exitosamente en el inventario.");
        }

        // Método para obtener los detalles de un material en el inventario
        [HttpGet("material/{id}")]
        public async Task<IActionResult> GetMaterial(int id)
        {
            var inventarioMaterial = await _context.Inventariomateriales
                .Include(i => i.Proveedor)
                .Include(i => i.Material)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (inventarioMaterial == null)
            {
                return NotFound("Material no encontrado.");
            }

            // Crear un DTO para la respuesta
            var response = new InventarioMaterialResponseDTO
            {
                Id = inventarioMaterial.Id,
                Cantidad = inventarioMaterial.Cantidad,
                ProveedorId = inventarioMaterial.ProveedorId,
                MaterialId = inventarioMaterial.MaterialId,
                DetallecompraId = inventarioMaterial.DetallecompraId,
                NombreProveedor = inventarioMaterial.Proveedor.NombreEmpresa,
                NombreMaterial = inventarioMaterial.Material.Nombre
            };

            return Ok(response);
        }

        #endregion

        #region Inventario de Productos

        // Método para registrar un nuevo producto en el inventario
        [HttpPost("producto/register")]
        public async Task<IActionResult> RegisterProducto([FromBody] InventarioProductoRegisterDTO productoDTO)
        {
            if (productoDTO == null)
            {
                return BadRequest("Datos inválidos.");
            }

            // Verificar que las relaciones de fabricación y producción existan
            var fabricacionExiste = await _context.Fabricacions.AnyAsync(f => f.Id == productoDTO.FabricacionId);
            var produccionExiste = await _context.Produccions.AnyAsync(p => p.Id == productoDTO.ProduccionId);

            if (!fabricacionExiste)
            {
                return BadRequest("Fabricación no encontrada.");
            }
            if (!produccionExiste)
            {
                return BadRequest("Producción no encontrada.");
            }

            // Crear un nuevo registro en el inventario de productos
            var inventarioProducto = new InventarioProducto
            {
                Cantidad = productoDTO.Cantidad,
                Precio = productoDTO.Precio,
                FechaCreacion = productoDTO.FechaCreacion,
                Lote = productoDTO.Lote,
                FabricacionId = productoDTO.FabricacionId,
                ProduccionId = productoDTO.ProduccionId,
                NivelMinimoStock = productoDTO.NivelMinimoStock
            };

            _context.InventarioProductos.Add(inventarioProducto);
            await _context.SaveChangesAsync();

            return Ok("Producto registrado exitosamente en el inventario.");
        }

        // Método para obtener los detalles de un producto en el inventario
        [HttpGet("producto/{id}")]
        public async Task<IActionResult> GetProducto(int id)
        {
            var inventarioProducto = await _context.InventarioProductos
                .Include(i => i.Fabricacion)
                .Include(i => i.Produccion)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (inventarioProducto == null)
            {
                return NotFound("Producto no encontrado.");
            }


            // Crear un DTO para la respuesta
            var response = new InventarioProductoResponseDTO
            {
                Id = inventarioProducto.Id,
                Cantidad = inventarioProducto.Cantidad,
                Precio = inventarioProducto.Precio,
                FechaCreacion = inventarioProducto.FechaCreacion,
                Lote = inventarioProducto.Lote,
                FabricacionId = inventarioProducto.FabricacionId,
                ProduccionId = inventarioProducto.ProduccionId,
                NivelMinimoStock = inventarioProducto.NivelMinimoStock,
                NombreFabricacion = inventarioProducto.Fabricacion.NombreProducto,
            };

            return Ok(response);
        }

        #endregion
    }
}
