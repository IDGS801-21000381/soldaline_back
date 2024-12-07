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
	public class ComentariosClienteController : ControllerBase
	{
		private readonly SoldalineBdContext _context;

		public ComentariosClienteController(SoldalineBdContext context)
		{
			_context = context;
		}

		// Registrar un nuevo comentario
		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] ComentarioClienteRegisterDTO comentarioDTO)
		{
			if (comentarioDTO == null)
			{
				return BadRequest("Datos inválidos.");
			}

			// Validar que el usuario y el cliente potencial existan
			var usuarioExiste = await _context.Usuarios.AnyAsync(u => u.Id == comentarioDTO.UsuarioId);
			if (!usuarioExiste)
			{
				return BadRequest("Usuario no encontrado.");
			}

			var clienteExiste = await _context.ClientePotencials.AnyAsync(c => c.ClienteId == comentarioDTO.ClienteId);
			if (!clienteExiste)
			{
				return BadRequest("Cliente no encontrado.");
			}

			// Crear el comentario
			var comentario = new ComentariosCliente
			{
				UsuarioId = comentarioDTO.UsuarioId,
				ClienteId = comentarioDTO.ClienteId,
				Fecha = comentarioDTO.Fecha,
				Tipo = comentarioDTO.Tipo,
				Descripcion = comentarioDTO.Descripcion,
				Estatus = comentarioDTO.Estatus,
				Calificacion = comentarioDTO.Calificacion
			};

			_context.ComentariosClientes.Add(comentario);
			await _context.SaveChangesAsync();

			return Ok("Comentario registrado exitosamente.");
		}

		// Obtener todos los comentarios
		[HttpGet("getAll")]
		public async Task<IActionResult> GetAll()
		{
			var comentarios = await _context.ComentariosClientes.ToListAsync();
			return Ok(comentarios);
		}

		// Obtener un comentario por ID
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var comentario = await _context.ComentariosClientes.FirstOrDefaultAsync(c => c.Id == id);
			if (comentario == null)
			{
				return NotFound("Comentario no encontrado.");
			}
			return Ok(comentario);
		}

		// Actualizar el estatus de un comentario
		[HttpPut("updateStatus/{id}")]
		public async Task<IActionResult> UpdateStatus(int id, [FromBody] int estatus)
		{
			var comentario = await _context.ComentariosClientes.FirstOrDefaultAsync(c => c.Id == id);
			if (comentario == null)
			{
				return NotFound("Comentario no encontrado.");
			}

			comentario.Estatus = estatus;
			_context.ComentariosClientes.Update(comentario);
			await _context.SaveChangesAsync();

			return Ok("Estatus actualizado exitosamente.");
		}


	}
}
