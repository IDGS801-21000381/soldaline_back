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
	public class PagosController : Controller
	{
		private readonly SoldalineBdContext _context;

		public PagosController(SoldalineBdContext context)
		{
			_context = context;
		}

		// Método para registrar un nuevo pago
		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] PagoRegisterDTO pagoDTO)
		{
			if (pagoDTO == null)
			{
				return BadRequest("Datos inválidos.");
			}

			// Validar que las referencias existan
			var cuenta = await _context.CuentasPorPagars.FirstOrDefaultAsync(c => c.Id == pagoDTO.CuentaId);
			if (cuenta == null)
			{
				return BadRequest("La cuenta por pagar especificada no existe.");
			}

			var clienteExiste = await _context.ClientePotencials.AnyAsync(c => c.ClienteId == pagoDTO.ClientePotencialId);
			if (!clienteExiste)
			{
				return BadRequest("El cliente especificado no existe.");
			}

			var usuarioExiste = await _context.Usuarios.AnyAsync(u => u.Id == pagoDTO.UsuarioId);
			if (!usuarioExiste)
			{
				return BadRequest("El usuario especificado no existe.");
			}

			// Validar que el monto no sea mayor al saldo pendiente
			if (pagoDTO.Monto > cuenta.SaldoPendiente)
			{
				return BadRequest("El monto del pago excede el saldo pendiente.");
			}

			// Registrar el nuevo pago
			var pago = new Pago
			{
				CuentaId = pagoDTO.CuentaId,
				Monto = pagoDTO.Monto,
				FechaPago = pagoDTO.FechaPago,
				MetodoPago = pagoDTO.MetodoPago,
				ClientePotencialId = pagoDTO.ClientePotencialId,
				UsuarioId = pagoDTO.UsuarioId
			};

			_context.Pagos.Add(pago);

			// Actualizar el saldo pendiente de la cuenta
			cuenta.SaldoPendiente -= pagoDTO.Monto;

			// Si el saldo pendiente llega a 0, cambiar el estatus a 'Pagada'
			if (cuenta.SaldoPendiente <= 0)
			{
				cuenta.Estatus = 1; // '1' representa 'Pagada'
			}

			_context.CuentasPorPagars.Update(cuenta);
			await _context.SaveChangesAsync();

			return Ok(new
			{
				Message = "Pago registrado exitosamente.",
				SaldoPendiente = cuenta.SaldoPendiente,
				Estatus = cuenta.Estatus == 1 ? "Pagada" : "Abierta"
			});
		}


		// Método para obtener un pago y sus detalles
		[HttpGet("{id}")]
		public async Task<IActionResult> GetPago(int id)
		{
			var pago = await _context.Pagos
				.FirstOrDefaultAsync(p => p.Id == id);

			if (pago == null)
			{
				return NotFound("Pago no encontrado.");
			}

			// Crear un DTO para la respuesta
			var response = new PagoResponseDTO
			{
				Id = pago.Id,
				CuentaId = pago.CuentaId,
				Monto = pago.Monto,
				FechaPago = pago.FechaPago,
				MetodoPago = pago.MetodoPago,
				ClientePotencialId = pago.ClientePotencialId,
				UsuarioId = pago.UsuarioId
			};

			return Ok(response);
		}

		// Método para obtener todos los pagos
		[HttpGet]
		public async Task<IActionResult> GetAllPagos()
		{
			var pagos = await _context.Pagos.ToListAsync();

			var response = pagos.Select(pago => new PagoResponseDTO
			{
				Id = pago.Id,
				CuentaId = pago.CuentaId,
				Monto = pago.Monto,
				FechaPago = pago.FechaPago,
				MetodoPago = pago.MetodoPago,
				ClientePotencialId = pago.ClientePotencialId,
				UsuarioId = pago.UsuarioId
			}).ToList();

			return Ok(response);
		}
	}
}
