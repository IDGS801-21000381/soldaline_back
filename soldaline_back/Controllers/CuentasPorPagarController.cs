using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using soldaline_back.Models;
using soldaline_back.DTOs;

namespace soldaline_back.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CuentasPorPagarController : Controller
	{
		private readonly SoldalineBdContext _context;

		public CuentasPorPagarController(SoldalineBdContext context)
		{
			_context = context;
		}

		// Método para registrar una nueva cuenta por pagar
		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] CuentasPorPagarRegisterDTO cuentaDTO)
		{
			if (cuentaDTO == null)
			{
				return BadRequest("Datos inválidos.");
			}

			// Validar que las referencias existan
			var ventaExiste = await _context.Venta.AnyAsync(v => v.Id == cuentaDTO.VentasId);
			if (!ventaExiste)
			{
				return BadRequest("La venta especificada no existe.");
			}

			var clienteExiste = await _context.ClientePotencials.AnyAsync(c => c.ClienteId == cuentaDTO.ClienteID);
			if (!clienteExiste)
			{
				return BadRequest("El cliente especificado no existe.");
			}

			var usuarioExiste = await _context.Usuarios.AnyAsync(u => u.Id == cuentaDTO.UsuarioId);
			if (!usuarioExiste)
			{
				return BadRequest("El usuario especificado no existe.");
			}

			// Crear una nueva cuenta por pagar
			var cuenta = new CuentasPorPagar
			{
				VentasId = cuentaDTO.VentasId,
				ClienteId = cuentaDTO.ClienteID,
				UsuarioId = cuentaDTO.UsuarioId,
				SaldoTotal = cuentaDTO.SaldoTotal,
				SaldoPendiente = cuentaDTO.SaldoPendiente,
				Fecha = cuentaDTO.Fecha,
				DiasPlazo = cuentaDTO.DiasPlazo,
				Estatus = cuentaDTO.Estatus
			};

			_context.CuentasPorPagars.Add(cuenta);
			await _context.SaveChangesAsync();

			return Ok("Cuenta por pagar registrada exitosamente.");
		}

		// Método para obtener una cuenta por pagar y sus detalles
		[HttpGet("{id}")]
		public async Task<IActionResult> GetCuentaPorPagar(int id)
		{
			var cuenta = await _context.CuentasPorPagars
				.FirstOrDefaultAsync(c => c.Id == id);

			if (cuenta == null)
			{
				return NotFound("Cuenta por pagar no encontrada.");
			}

			// Crear un DTO para la respuesta
			var response = new CuentasPorPagarResponseDTO
			{
				Id = cuenta.Id,
				VentasId = cuenta.VentasId,
				ClienteID = cuenta.ClienteId,
				UsuarioId = cuenta.UsuarioId,
				SaldoTotal = cuenta.SaldoTotal,
				SaldoPendiente = cuenta.SaldoPendiente,
				Fecha = cuenta.Fecha,
				DiasPlazo = cuenta.DiasPlazo,
				Estatus = cuenta.Estatus
			};

			return Ok(response);
		}

		// Método para obtener todas las cuentas por pagar
		[HttpGet]
		public async Task<IActionResult> GetAllCuentasPorPagar()
		{
			var cuentas = await _context.CuentasPorPagars
				.ToListAsync();

			var response = cuentas.Select(cuenta => new CuentasPorPagarResponseDTO
			{
				Id = cuenta.Id,
				VentasId = cuenta.VentasId,
				ClienteID = cuenta.ClienteId,
				UsuarioId = cuenta.UsuarioId,
				SaldoTotal = cuenta.SaldoTotal,
				SaldoPendiente = cuenta.SaldoPendiente,
				Fecha = cuenta.Fecha,
				DiasPlazo = cuenta.DiasPlazo,
				Estatus = cuenta.Estatus
			}).ToList();

			return Ok(response);
		}
	}
}
