//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using soldaline_back.Models;
//using soldaline_back.DTOs;

//namespace soldaline_back.Controllers;

//[Route("api/[controller]")]
//[ApiController]
//public class ProyectoController : ControllerBase
//{
//    private readonly SoldalineBdContext _context;

//    public ProyectoController(SoldalineBdContext context)
//    {
//        _context = context;
//    }

//    // Crear Proyecto
//    [HttpPost("create")]
//    public async Task<IActionResult> Create([FromBody] ProyectoRegisterDTO dto)
//    {
//        var proyecto = new Proyecto
//        {
//            ClienteId = dto.ClienteId,
//            EmpresaId = dto.EmpresaId,
//            NombreProyecto = dto.NombreProyecto,
//            Descripcion = dto.Descripcion,
//            Monto = dto.Monto,
//            Estatus = 1,
//            FechaInicio = dto.FechaInicio,
//            FechaFin = dto.FechaFin
//        };

//        _context.Proyectos.Add(proyecto);
//        await _context.SaveChangesAsync();
//        return Ok("Proyecto creado correctamente.");
//    }

//    // Cambiar estatus de Proyecto a 5
//    [HttpPut("change-status/{id}")]
//    public async Task<IActionResult> ChangeStatus(int id)
//    {
//        var proyecto = await _context.Proyectos.FirstOrDefaultAsync(p => p.ProyectoId == id);
//        if (proyecto == null) return NotFound("Proyecto no encontrado.");

//        proyecto.Estatus = 5;
//        await _context.SaveChangesAsync();
//        return Ok("Estatus cambiado a 5.");
//    }

//    // Obtener Proyecto por ID
//    [HttpGet("{id}")]
//    public async Task<IActionResult> GetById(int id)
//    {
//        var proyecto = await _context.Proyectos
//            .Include(p => p.Cliente)
//            .Include(p => p.Empresa)
//            .FirstOrDefaultAsync(p => p.ProyectoId == id);

//        if (proyecto == null) return NotFound("Proyecto no encontrado.");

//        return Ok(proyecto);
//    }
//}
