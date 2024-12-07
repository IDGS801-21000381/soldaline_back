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
    public class FabricacionController : ControllerBase
    {
        private readonly SoldalineBdContext _context;
        public FabricacionController(SoldalineBdContext context)
        {
            _context = context;
        }
        // API para agregar un nuevo producto
        [HttpPost("agregar")]
        public async Task<IActionResult> AgregarProducto([FromBody] ProductoDTO productoDTO)
        {
            if (productoDTO == null || string.IsNullOrEmpty(productoDTO.NombreProducto) || productoDTO.PrecioProducto <= 0)
            {
                return BadRequest("Datos inválidos.");
            }

            // Crear el nuevo producto
            var nuevoProducto = new Fabricacion
            {
                NombreProducto = productoDTO.NombreProducto,
                ImagenProducto = productoDTO.ImagenProducto,
                PrecioProducto = productoDTO.PrecioProducto,
                Estatus = productoDTO.Estatus,
                Categoria = productoDTO.Categoria
            };

            // Agregar el producto a la base de datos
            _context.Fabricacions.Add(nuevoProducto);
            await _context.SaveChangesAsync();
            // Relacionar los materiales con el nuevo producto
            if (productoDTO.Materiales != null && productoDTO.Materiales.Any())
            {
                foreach (var material in productoDTO.Materiales)
                {
                    var nuevoMaterialFabricacion = new Materialfabricacion
                    {
                        FabricacionId = nuevoProducto.Id,
                        MaterialId = material.MaterialId,
                        Cantidad = material.Cantidad,
                        Estatus = 1 // Asumimos que los materiales están activos al agregarlos
                    };
                    _context.Materialfabricacions.Add(nuevoMaterialFabricacion);
                }
                await _context.SaveChangesAsync();
            }
            return Ok(new { Mensaje = "Producto agregado exitosamente." });
        }

        // API para modificar un producto existente
        [HttpPut("modificar/{id}")]
        public async Task<IActionResult> ModificarProducto(int id, [FromBody] ProductoDTO productoDTO)
        {
            var producto = await _context.Fabricacions.FindAsync(id);
            if (producto == null)
            {
                return NotFound("Producto no encontrado.");
            }

            // Actualizar los datos del producto
            producto.NombreProducto = productoDTO.NombreProducto ?? producto.NombreProducto;
            producto.ImagenProducto = productoDTO.ImagenProducto ?? producto.ImagenProducto;
            producto.PrecioProducto = productoDTO.PrecioProducto > 0 ? productoDTO.PrecioProducto : producto.PrecioProducto;
            producto.Estatus = productoDTO.Estatus ?? producto.Estatus;
            producto.Categoria = productoDTO.Categoria ?? producto.Categoria;

            _context.Fabricacions.Update(producto);
            await _context.SaveChangesAsync();
            // Eliminar los materiales antiguos y agregar los nuevos
            var materialesExistentes = await _context.Materialfabricacions
                .Where(mf => mf.FabricacionId == id)
                .ToListAsync();

            _context.Materialfabricacions.RemoveRange(materialesExistentes);
            await _context.SaveChangesAsync();
            // Agregar los nuevos materiales
            if (productoDTO.Materiales != null && productoDTO.Materiales.Any())
            {
                foreach (var material in productoDTO.Materiales)
                {
                    var nuevoMaterialFabricacion = new Materialfabricacion
                    {
                        FabricacionId = id,
                        MaterialId = material.MaterialId,
                        Cantidad = material.Cantidad,
                        Estatus = 1 // Asumimos que los materiales están activos al modificarlos
                    };
                    _context.Materialfabricacions.Add(nuevoMaterialFabricacion);
                }
                await _context.SaveChangesAsync();
            }

            return Ok(new { Mensaje = "Producto y materiales modificados exitosamente." });
        }

        // Otros métodos de la API para obtener productos o listar todos los productos, si es necesario.
        // ...
    }
}