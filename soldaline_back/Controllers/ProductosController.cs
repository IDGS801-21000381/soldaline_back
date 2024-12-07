//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using soldaline_back.DTOs;
//using soldaline_back.Models;
//using System.Threading.Tasks;
//using System.Collections.Generic;

//namespace soldaline_back.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ProductosController : ControllerBase
//    {
//        private readonly SoldalineBdContext _context;

//        public ProductosController(SoldalineBdContext context)
//        {
//            _context = context;
//        }

//        // Obtener todos los productos
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<ProductoResponseDTO>>> GetAllProductos()
//        {
//            var productos = await _context.Fabricacions.ToListAsync();

//            // Convertir a DTO
//            var productosDTO = productos.ConvertAll(producto => new ProductoResponseDTO
//            {
//                Id = producto.Id,
//                NombreProducto = producto.NombreProducto,
//                ImagenProducto = producto.ImagenProducto,
//                Estatus = producto.Estatus
//            });

//            return productosDTO;
//        }

//        // Obtener un producto por ID
//        [HttpGet("{id}")]
//        public async Task<ActionResult<ProductoResponseDTO>> GetProductoById(int id)
//        {
//            var producto = await _context.Fabricacions.FindAsync(id);

//            if (producto == null)
//            {
//                return NotFound("Producto no encontrado.");
//            }

//            // Convertir a DTO
//            var productoDTO = new ProductoResponseDTO
//            {
//                Id = producto.Id,
//                NombreProducto = producto.NombreProducto,
//                ImagenProducto = producto.ImagenProducto,
//                Estatus = producto.Estatus
//            };

//            return productoDTO;
//        }

//        // Crear un nuevo producto
//        [HttpPost]
//        public async Task<ActionResult<ProductoResponseDTO>> CreateProducto([FromBody] ProductoRegisterDTO productoDTO)
//        {
//            var producto = new Fabricacion
//            {
//                NombreProducto = productoDTO.NombreProducto,
//                ImagenProducto = productoDTO.ImagenProducto,
//                Estatus = productoDTO.Estatus
//            };

//            _context.Fabricacions.Add(producto);
//            await _context.SaveChangesAsync();

//            // Crear respuesta en formato DTO
//            var productoResponseDTO = new ProductoResponseDTO
//            {
//                Id = producto.Id,
//                NombreProducto = producto.NombreProducto,
//                ImagenProducto = producto.ImagenProducto,
//                Estatus = producto.Estatus
//            };

//            return CreatedAtAction(nameof(GetProductoById), new { id = producto.Id }, productoResponseDTO);
//        }

//        // Actualizar un producto existente
//        [HttpPut("{id}")]
//        public async Task<IActionResult> UpdateProducto(int id, [FromBody] ProductoUpdateDTO productoDTO)
//        {
//            var productoExistente = await _context.Fabricacions.FindAsync(id);

//            if (productoExistente == null)
//            {
//                return NotFound("Producto no encontrado.");
//            }

//            // Actualizar los campos
//            productoExistente.NombreProducto = productoDTO.NombreProducto;
//            productoExistente.ImagenProducto = productoDTO.ImagenProducto;
//            productoExistente.Estatus = productoDTO.Estatus;

//            _context.Fabricacions.Update(productoExistente);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        // Eliminar un producto
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteProducto(int id)
//        {
//            var producto = await _context.Fabricacions.FindAsync(id);
//            if (producto == null)
//            {
//                return NotFound("Producto no encontrado.");
//            }

//            _context.Fabricacions.Remove(producto);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }
//    }
//}
