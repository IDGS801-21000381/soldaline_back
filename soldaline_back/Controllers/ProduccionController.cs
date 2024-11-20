using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using soldaline_back.DTOs;
using soldaline_back.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace soldaline_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProduccionController : ControllerBase
    {
        private readonly SoldalineBdContext _context;

        public ProduccionController(SoldalineBdContext context)
        {
            _context = context;
        }

        // Registrar una solicitud de producción para múltiples productos
        [HttpPost("solicitarProduccion")]
        public async Task<IActionResult> SolicitarProduccion([FromBody] SolicitudMultipleProduccionDTO request)
        {
            if (request == null || request.Productos == null || !request.Productos.Any())
            {
                return BadRequest("Datos inválidos.");
            }

            foreach (var producto in request.Productos)
            {
                var nuevaSolicitud = new Solicitudproduccion
                {
                    Descripcion = producto.Descripcion ?? "Solicitud de producción",
                    Estatus = 1, // Pendiente
                    Cantidad = producto.Cantidad,
                    FabricacionId = producto.FabricacionId,
                    UsuarioId = request.UsuarioId
                };

                _context.Solicitudproduccions.Add(nuevaSolicitud);
            }

            await _context.SaveChangesAsync();
            return Ok(new { Mensaje = "Solicitud de producción registrada exitosamente." });
        }

        // Aceptar y validar la producción
        [HttpPost("aceptarProduccion")]
        public async Task<IActionResult> AceptarProduccion([FromBody] AceptarProduccionDTO request)
        {
            if (request.SolicitudIds == null || !request.SolicitudIds.Any())
            {
                return BadRequest("Debe proporcionar las solicitudes a aceptar.");
            }

            foreach (var solicitudId in request.SolicitudIds)
            {
                var solicitud = await _context.Solicitudproduccions.FindAsync(solicitudId);

                if (solicitud == null)
                {
                    return NotFound($"Solicitud de producción ID {solicitudId} no encontrada.");
                }

                // Verificar disponibilidad de materiales
                var materiales = await _context.Materialfabricacions
                    .Where(m => m.FabricacionId == solicitud.FabricacionId)
                    .ToListAsync();

                foreach (var material in materiales)
                {
                    var inventario = await _context.Inventariomateriales
                        .FirstOrDefaultAsync(im => im.MaterialId == material.MaterialId);

                    if (inventario == null || inventario.Cantidad < material.Cantidad * solicitud.Cantidad)
                    {
                        return BadRequest($"Material insuficiente para la solicitud ID {solicitudId}, Material ID {material.MaterialId}");
                    }
                }

                solicitud.Estatus = 2; // Aceptada
                _context.Solicitudproduccions.Update(solicitud);
            }

            await _context.SaveChangesAsync();
            return Ok(new { Mensaje = "Producción aceptada exitosamente." });
        }

        // Finalizar la producción
        [HttpPost("finalizarProduccion")]
        public async Task<IActionResult> FinalizarProduccion([FromBody] FinalizarProduccionDTO request)
        {
            if (request.SolicitudIds == null || !request.SolicitudIds.Any())
            {
                return BadRequest("Debe proporcionar las solicitudes a finalizar.");
            }

            foreach (var solicitudId in request.SolicitudIds)
            {
                var solicitud = await _context.Solicitudproduccions.FindAsync(solicitudId);

                if (solicitud == null)
                {
                    return NotFound($"Solicitud de producción ID {solicitudId} no encontrada.");
                }

                var produccion = new Produccion
                {
                    Costo = 0.0f,  // Ajustar según sea necesario
                    UsuarioId = solicitud.UsuarioId,
                    SolicitudproduccionId = solicitud.Id
                };

                _context.Produccions.Add(produccion);
                await _context.SaveChangesAsync();

                var inventario = await _context.InventarioProductos
                    .FirstOrDefaultAsync(ip => ip.FabricacionId == solicitud.FabricacionId);

                if (inventario != null)
                {
                    inventario.Cantidad += solicitud.Cantidad;
                    _context.InventarioProductos.Update(inventario);
                }
                else
                {
                    var nuevoInventario = new InventarioProducto
                    {
                        Cantidad = solicitud.Cantidad,
                        Precio = 0.0f,
                        Lote = request.Lote ?? "Sin lote",
                        FabricacionId = solicitud.FabricacionId,
                        ProduccionId = produccion.Id,
                        NivelMinimoStock = 0
                    };

                    _context.InventarioProductos.Add(nuevoInventario);
                }

                solicitud.Estatus = 3; // Finalizada
                _context.Solicitudproduccions.Update(solicitud);
            }

            await _context.SaveChangesAsync();
            return Ok(new { Mensaje = "Producción finalizada y stock actualizado." });
        }
    }
}
