using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using soldaline_back.DTOs;
using soldaline_back.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace soldaline_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialesController : ControllerBase
    {
        private readonly SoldalineBdContext _context;

        public MaterialesController(SoldalineBdContext context)
        {
            _context = context;
        }

        // Obtener todos los materiales
        [HttpGet("listar")]
        public async Task<IActionResult> ListarMateriales()
        {
            var materiales = await _context.Materials.ToListAsync();
            return Ok(materiales);
        }

        // Obtener un material por ID
        [HttpGet("obtener/{id}")]
        public async Task<IActionResult> ObtenerMaterial(int id)
        {
            var material = await _context.Materials.FindAsync(id);
            if (material == null)
            {
                return NotFound($"No se encontró el material con ID {id}.");
            }

            return Ok(material);
        }

        // Agregar un nuevo material
        [HttpPost("agregar")]
        public async Task<IActionResult> AgregarMaterial([FromBody] MaterialesDTO materialDto)
        {
            if (materialDto == null || string.IsNullOrEmpty(materialDto.Nombre))
            {
                return BadRequest("Datos inválidos.");
            }

            var nuevoMaterial = new Material
            {
                Nombre = materialDto.Nombre
            };

            _context.Materials.Add(nuevoMaterial);
            await _context.SaveChangesAsync();

            return Ok(new { Mensaje = "Material agregado exitosamente.", MaterialId = nuevoMaterial.Id });
        }

        // Modificar un material existente
        [HttpPut("modificar/{id}")]
        public async Task<IActionResult> ModificarMaterial(int id, [FromBody] MaterialesDTO materialDto)
        {
            var material = await _context.Materials.FindAsync(id);

            if (material == null)
            {
                return NotFound($"No se encontró el material con ID {id}.");
            }

            if (string.IsNullOrEmpty(materialDto.Nombre))
            {
                return BadRequest("El nombre del material no puede estar vacío.");
            }

            material.Nombre = materialDto.Nombre;
            await _context.SaveChangesAsync();

            return Ok("Material modificado exitosamente.");
        }

        // Eliminar un material
        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> EliminarMaterial(int id)
        {
            var material = await _context.Materials.FindAsync(id);

            if (material == null)
            {
                return NotFound($"No se encontró el material con ID {id}.");
            }

            _context.Materials.Remove(material);
            await _context.SaveChangesAsync();

            return Ok("Material eliminado exitosamente.");
        }
    }
}
