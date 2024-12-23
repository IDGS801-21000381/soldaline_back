﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using soldaline_back.Models;
using soldaline_back.DTOs;

namespace soldaline_back.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HistorialComunicacionController : ControllerBase
{
    private readonly SoldalineBd2Context _context;

    public HistorialComunicacionController(SoldalineBd2Context context)
    {
        _context = context;
    }

    // Crear Historial de Comunicación
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] HistorialComunicacionDTO dto)
    {
        var historial = new HistorialComunicacion
        {
            ClienteId = dto.ClienteId,
            UsuarioId = dto.UsuarioId,
            FechaComunicacion = dto.FechaComunicacion,
            TipoComunicacion = dto.TipoComunicacion,
            DetallesComunicado = dto.DetallesComunicado,
            FechaProximaCita = dto.FechaProximaCita,
            Solicitud = dto.Solicitud,
            Estatus = 1 // Estatus por defecto
        };

        _context.HistorialComunicacions.Add(historial);
        await _context.SaveChangesAsync();
        return Ok("Historial de comunicación creado correctamente.");
    }

    // Actualizar Historial de Comunicación
    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] HistorialComunicacionUpdateDTO dto)
    {
        var historial = await _context.HistorialComunicacions.FirstOrDefaultAsync(h => h.HistorialId == dto.HistorialId);
        if (historial == null) return NotFound("Historial no encontrado.");

        if (dto.TipoComunicacion != 0) historial.TipoComunicacion = dto.TipoComunicacion;
        if (!string.IsNullOrEmpty(dto.DetallesComunicado)) historial.DetallesComunicado = dto.DetallesComunicado;
        if (dto.FechaProximaCita.HasValue) historial.FechaProximaCita = dto.FechaProximaCita;
        if (!string.IsNullOrEmpty(dto.Solicitud)) historial.Solicitud = dto.Solicitud;

        await _context.SaveChangesAsync();
        return Ok("Historial de comunicación actualizado correctamente.");
    }

    // Cambiar estatus de Historial de Comunicación a 5
    [HttpPut("change-status/{id}")]
    public async Task<IActionResult> ChangeStatus(int id)
    {
        var historial = await _context.HistorialComunicacions.FirstOrDefaultAsync(h => h.HistorialId == id);
        if (historial == null) return NotFound("Historial no encontrado.");

        historial.Estatus = 5;
        await _context.SaveChangesAsync();
        return Ok("Estatus cambiado a 5.");
    }

    // Obtener Historial de Comunicación por Cliente
    [HttpGet("by-cliente/{clienteId}")]
    public async Task<IActionResult> GetByCliente(int clienteId)
    {
        var historiales = await _context.HistorialComunicacions
            .Where(h => h.ClienteId == clienteId)
            .ToListAsync();

        if (!historiales.Any()) return NotFound("No se encontraron historiales para este cliente.");

        return Ok(historiales);
    }
}
