//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using soldaline_back.DTOs;
//using soldaline_back.Models;

//namespace soldaline_back.Controllers
//{
//	[Route("api/[controller]")]
//	[ApiController]
//	public class VentaController : Controller
//	{
//		private readonly SoldalineBdContext _context;

//		public VentaController(SoldalineBdContext context)
//		{
//			_context = context;
//		}


//		[HttpGet("GetVenta")]
//		public async Task<IActionResult> GetVenta()
//		{
//			var ventas = await _context.Venta
//				.Include(v => v.Usuario) // Incluyes la entidad Usuario si existe una relación con ventas
//				.Select(v => new
//				{
//					v.Id,           // id de la venta
//					v.Fecha,        // fecha de la venta
//					v.Folio,        // folio de la venta
//					Usuario = v.Usuario.Nombre,  // Incluyes el nombre del usuario o cualquier otro dato necesario
//					v.UsuarioId     // id del usuario asociado a la venta
//				})
//				.ToListAsync();

//			return Ok(ventas);
//		}


//		/*[HttpPost("PostVenta")]
//		public async Task<IActionResult> PostVenta([FromBody] VentaDTO ventaDTO)
//		{
//			var nuevaVenta = new VentaDTO
//			{
//				Fecha = ventaDTO.Fecha,
//				Folio = ventaDTO.Folio,
//				UsuarioId = ventaDTO.UsuarioId
//			};
//			_context.Venta.Add(nuevaVenta);
//			await _context.SaveChangesAsync();

//			return CreatedAtAction(nameof(GetVenta), new { id = nuevaVenta.Folio }, nuevaVenta); 
//		}*/
//		[HttpPost("PostVenta")]
//		public async Task<IActionResult> PostVenta([FromBody] VentaDTO request)
//		{
//			// Validar el modelo
//			if (!ModelState.IsValid)
//			{
//				return BadRequest(ModelState);
//			}

//			// Crear una nueva instancia de Ventum
//			var nuevaVenta = new Ventum
//			{
//				Fecha = request.Fecha,
//				Folio = request.Folio,
//				UsuarioId = request.UsuarioId
//			};

//			// Agregar los detalles de la venta
//			/*foreach (var detalle in request.Detalleventa)
//			{
//				var nuevoDetalle = new Detalleventum
//				{
//					Cantidad = detalle.Cantidad,
//					PrecioUnitario = detalle.PrecioUnitario,
					 
//				};

//				nuevaVenta.Detalleventa.Add(nuevoDetalle);
//			}*/

//			// Agregar la nueva venta al contexto
//			_context.Venta.Add(nuevaVenta);

//			// Guardar cambios en la base de datos
//			await _context.SaveChangesAsync();

//			// Retornar la venta creada
//			return CreatedAtAction(nameof(GetVenta), new { id = nuevaVenta.Id }, nuevaVenta);
//		}


//	}
//}
