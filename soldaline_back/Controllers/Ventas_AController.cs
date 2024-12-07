using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using soldaline_back.DTOs;
using soldaline_back.Models;

namespace soldaline_back.Controllers
{
	[Route("api/")]
	[ApiController]
	public class VentaAController : Controller
	{
		private readonly SoldalineBdContext _context;

		public VentaAController(SoldalineBdContext context)
		{
			_context = context;
		}


		[HttpGet("GetVenta")]
		public async Task<IActionResult> GetVenta()
		{
			var ventas = await _context.Venta
				.Include(v => v.Usuario)
				.Select(v => new
				{
					v.Id,
					v.Fecha,
					v.Folio,
					Usuario = v.Usuario.Nombre,
					v.UsuarioId
				})
				.ToListAsync();

			return Ok(ventas);
		}

		[HttpGet("GetDashboard")]
		public async Task<IActionResult> GetDashboard()
		{
			var ventas = await _context.Detalleventa

				.Select(v => new
				{
					v.Id,
					v.Cantidad,
					v.Venta,
					v.PrecioUnitario
				})
				.ToListAsync();

			return Ok(ventas);
		}

	}
}
