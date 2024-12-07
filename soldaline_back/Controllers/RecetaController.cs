using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using soldaline_back.DTOs;
using soldaline_back.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace soldaline_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class RecetaController : ControllerBase
    {

        private readonly SoldalineBdContext _context;

        public RecetaController(SoldalineBdContext context)
        {
            _context = context;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllRecetas()
        {
            try
            {
                // Obtener todas las recetas con la información relacionada de fabricacion
                var recetas = await _context.RecetasProtecciones
                    .Join(
                        _context.Fabricacions,  // La tabla fabricacion que contiene los productos
                        r => r.IdFabricacion,  // Relacionar el campo IdFabricacion en RecetasProtecciones
                        f => f.Id,             // Relacionar con el campo id de la tabla Fabricacion
                        (r, f) => new RecetaResponseDTO
                        {
                            IdReceta = r.IdReceta,
                            ProductoNombre = f.NombreProducto,  // Mostramos el nombre del producto
                            CategoriaNombre = r.TipoProteccion ?? "Sin Categoría", // Valor predeterminado si es null
                            Material = r.Material,
                            Cantidad = r.Cantidad,
                            Descripcion = r.Descripcion,
                            FechaCreacion = r.FechaCreacion,
                            Estatus = r.Estatus
                        })
                    .ToListAsync();

                if (recetas == null || !recetas.Any())
                {
                    return NotFound("No se encontraron recetas.");
                }

                return Ok(recetas);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }



        [HttpPost("create")]
        public async Task<IActionResult> CreateReceta([FromBody] RecetaDTO recetaDTO)
        {
            try
            {
                if (recetaDTO == null)
                {
                    return BadRequest("La receta no puede ser nula.");
                }

                if (recetaDTO.Materiales == null || recetaDTO.Materiales.Count == 0)
                {
                    return BadRequest("Debe proporcionar al menos un material.");
                }

                // Obtener la fabricación y categoría asociada
                var fabricacion = await _context.Fabricacions
                    .Where(f => f.Id == recetaDTO.IdFabricacion)
                    .Select(f => new { f.NombreProducto, f.Categoria })
                    .FirstOrDefaultAsync();

                // Verificar si fabricación tiene un valor
                if (fabricacion == null)
                {
                    return BadRequest("No se encontró la fabricación para el ID proporcionado.");
                }

                // Iterar sobre los materiales para agregarlos a la base de datos
                foreach (var materialDTO in recetaDTO.Materiales)
                {
                    if (string.IsNullOrEmpty(materialDTO.Material))
                    {
                        return BadRequest("El material y la cantidad son obligatorios para cada material.");
                    }

                    // Mapear cada material al modelo de base de datos
                    var receta = new RecetasProteccione
                    {
                        IdFabricacion = recetaDTO.IdFabricacion,
                        Material = materialDTO.Material,
               
                        Descripcion = recetaDTO.Descripcion,
                        FechaCreacion = recetaDTO.FechaCreacion ?? DateTime.UtcNow,
                        Estatus = recetaDTO.Estatus,
                        TipoProteccion = fabricacion.Categoria ?? "DefaultProtection" // Si la categoría es nula, asignar "DefaultProtection"
                    };

                    // Agregar cada receta a la base de datos
                    _context.RecetasProtecciones.Add(receta);
                }

                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();

                // Responder con el recurso recién creado
                return CreatedAtAction(nameof(GetAllRecetas), new { id = recetaDTO.IdFabricacion }, recetaDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }










    }
}
