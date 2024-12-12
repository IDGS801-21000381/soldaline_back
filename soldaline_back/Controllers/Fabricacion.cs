using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using soldaline_back.DTOs;
using soldaline_back.Models;
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

        [HttpPost("register")]
        public async Task<IActionResult> RegisterProducto([FromForm] ProductoRegisterDTO productoDTO, IFormFile imagenProducto)
        {
            if (productoDTO == null || string.IsNullOrEmpty(productoDTO.NombreProducto))
            {
                return BadRequest("Datos inválidos.");
            }

            string rutaImagen = null;

            // Verifica si el archivo de imagen está presente
            if (imagenProducto != null)
            {
                // Define la ruta para guardar la imagen
                var carpetaImagenes = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagenes");
                if (!Directory.Exists(carpetaImagenes))
                {
                    Directory.CreateDirectory(carpetaImagenes);
                }

                var nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(imagenProducto.FileName);
                var rutaCompleta = Path.Combine(carpetaImagenes, nombreArchivo);

                // Guarda la imagen en el servidor
                using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                {
                    await imagenProducto.CopyToAsync(stream);
                }

                rutaImagen = $"http://localhost:5055/imagenes/{nombreArchivo}"; // Ruta accesible desde la aplicación
            }

            // Crea el objeto del producto
            var nuevoProducto = new Fabricacion
            {
                NombreProducto = productoDTO.NombreProducto,
                ImagenProducto = rutaImagen,
                Estatus = productoDTO.Estatus ?? 1, // Activo por defecto
                Categoria = productoDTO.Categoria
            };

            _context.Fabricacions.Add(nuevoProducto);
            await _context.SaveChangesAsync(); // Guarda el producto para obtener el ID

            var nuevoinventario = new InventarioProducto
            {
                Cantidad = 0,
                Precio = productoDTO.Precio,
                FechaCreacion = DateOnly.MinValue,
                Lote = "Lote1",
                FabricacionId = nuevoProducto.Id, // Asigna el ID generado
                ProduccionId = 4,
            };

            _context.InventarioProductos.Add(nuevoinventario);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Producto registrado exitosamente.", ImagenUrl = rutaImagen });
        }

        // Obtener un producto por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducto(int id)
        {
            var producto = await _context.Fabricacions.FindAsync(id);

            if (producto == null)
            {
                return NotFound("Producto no encontrado.");
            }

            var productoResponse = new ProductoResponseDTO
            {
                Id = producto.Id,
                NombreProducto = producto.NombreProducto,
                ImagenProducto = producto.ImagenProducto,
                Estatus = producto.Estatus,
                categoria = producto.Categoria
            };

            return Ok(productoResponse);
        }

        // Obtener todos los productos
        [HttpGet("all")]
        public async Task<IActionResult> GetAllProductos()
        {
            var productos = await _context.Fabricacions.ToListAsync();

            if (productos == null || productos.Count == 0)
            {
                return NotFound("No hay productos registrados.");
            }

            var productosResponse = productos.Select(p => new ProductoResponseDTO
            {
                Id = p.Id,
                NombreProducto = p.NombreProducto,
                ImagenProducto = p.ImagenProducto,
                Estatus = p.Estatus,
                categoria = p.Categoria
            }).ToList();

            return Ok(productosResponse);
        }

        // Actualizar un producto
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProducto(int id, [FromBody] ProductoUpdateDTO productoDTO)
        {
            var producto = await _context.Fabricacions.FindAsync(id);

            if (producto == null)
            {
                return NotFound("Producto no encontrado.");
            }

            producto.NombreProducto = productoDTO.NombreProducto ?? producto.NombreProducto;
            producto.ImagenProducto = productoDTO.ImagenProducto ?? producto.ImagenProducto;
            producto.Estatus = productoDTO.Estatus ?? producto.Estatus;
            producto.Categoria = productoDTO.categoria ?? producto.Categoria;

            await _context.SaveChangesAsync();

            return Ok(new { Message = "Producto actualizado exitosamente." });
        }

        // Eliminar un producto (cambio de estado lógico)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _context.Fabricacions.FindAsync(id);

            if (producto == null)
            {
                return NotFound("Producto no encontrado.");
            }

            producto.Estatus = 0; // Inactivo
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Producto eliminado lógicamente." });
        }
    }
}
