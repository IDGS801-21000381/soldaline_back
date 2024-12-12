using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using soldaline_back.DTOs;
using soldaline_back.Models;
using System;
using System.Collections.Generic;
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
        [HttpPost("solicitarProduccion")]
        public async Task<IActionResult> SolicitarProduccion([FromBody] SolicitudProduccionDTO request)
        {
            if (request == null || request.Cantidad <= 0)
            {
                return BadRequest("Datos inválidos.");
            }

            // Verificar los materiales disponibles en el inventario
            var materialesNecesarios = await _context.Materialfabricacions
                .Where(mf => mf.FabricacionId == request.FabricacionId)
                .ToListAsync();

            foreach (var material in materialesNecesarios)
            {
                var inventarioMaterial = await _context.Inventariomateriales
                    .FirstOrDefaultAsync(im => im.MaterialId == material.MaterialId);

                if (inventarioMaterial == null || inventarioMaterial.Cantidad < material.Cantidad * request.Cantidad)
                {
                    return BadRequest($"No hay suficiente material disponible para el material ID: {material.MaterialId}");
                }
            }

            // Crear la solicitud de producción
            var nuevaSolicitud = new Solicitudproduccion
            {
                Descripcion = request.Descripcion,
                Estatus = 1, // Pendiente
                Cantidad = request.Cantidad,
                FabricacionId = request.FabricacionId,
                UsuarioId = request.UsuarioId
            };

            _context.Solicitudproduccions.Add(nuevaSolicitud);
            await _context.SaveChangesAsync();

            return Ok(new { Mensaje = "Solicitud de producción registrada exitosamente.", SolicitudId = nuevaSolicitud.Id });
        }

        [HttpPost("iniciarProduccion")]
        public async Task<IActionResult> IniciarProduccion([FromBody] IniciarProduccionDTO request)
        {
            // Obtener la solicitud de producción
            var solicitudProduccion = await _context.Solicitudproduccions
                .FirstOrDefaultAsync(sp => sp.Id == request.SolicitudProduccionId);

            if (solicitudProduccion == null)
            {
                return NotFound("Solicitud de producción no encontrada.");
            }

            // Crear un registro de producción
            var nuevaProduccion = new Produccion
            {
                //Fecha = DateTime.Now,
                Costo = 0,
                UsuarioId = request.UsuarioId,
                SolicitudproduccionId = request.SolicitudProduccionId
            };

            _context.Produccions.Add(nuevaProduccion);
            await _context.SaveChangesAsync();

            // Registrar el detalle de producción y actualizar el inventario
            var materiales = await _context.Materialfabricacions
                .Where(m => m.FabricacionId == solicitudProduccion.FabricacionId)
                .ToListAsync();

            foreach (var material in materiales)
            {
                var inventarioMaterial = await _context.Inventariomateriales
                    .FirstOrDefaultAsync(im => im.MaterialId == material.MaterialId);

                if (inventarioMaterial == null || inventarioMaterial.Cantidad < material.Cantidad * solicitudProduccion.Cantidad)
                {
                    return BadRequest($"Material insuficiente: {material.MaterialId}");
                }

                // Restar el material utilizado del inventario
                inventarioMaterial.Cantidad -= material.Cantidad * solicitudProduccion.Cantidad;

                // Registrar el detalle de producción
                var detalleProduccion = new Detalleproduccion
                {
                    ProduccionId = nuevaProduccion.Id,
                    InventariomaterialesId = inventarioMaterial.Id
                };

                _context.Detalleproduccions.Add(detalleProduccion);
                _context.Inventariomateriales.Update(inventarioMaterial);
            }

            await _context.SaveChangesAsync();

            // Actualizar el estado de la solicitud a "En proceso"
            solicitudProduccion.Estatus = 2;
            _context.Solicitudproduccions.Update(solicitudProduccion);
            await _context.SaveChangesAsync();

            return Ok(new { Mensaje = "Producción iniciada exitosamente.", ProduccionId = nuevaProduccion.Id });
        }

        [HttpPost("terminarProduccion")]
        public async Task<IActionResult> TerminarProduccion([FromBody] TerminarProduccionDTO request)
        {
            Console.WriteLine(request);
            // Obtener la solicitud de producción
            var solicitudProduccion = await _context.Solicitudproduccions
                .FirstOrDefaultAsync(sp => sp.Id == request.SolicitudProduccionId);

            if (solicitudProduccion == null)
            {
                return NotFound("Solicitud de producción no encontrada.");
            }

            var fabricacionId = solicitudProduccion.FabricacionId;

            // Crear un nuevo registro en la tabla de producción si no existe
            var produccion = await _context.Produccions
                .FirstOrDefaultAsync(p => p.SolicitudproduccionId == solicitudProduccion.Id);

            if (produccion == null)
            {
                // Crear un nuevo registro de producción
                produccion = new Produccion
                {
                    Costo = 0.0f,  // Ajusta según sea necesario
                    UsuarioId = solicitudProduccion.UsuarioId,
                    SolicitudproduccionId = solicitudProduccion.Id
                };

                _context.Produccions.Add(produccion);
                await _context.SaveChangesAsync(); // Guardar el nuevo registro de producción
            }

            var produccionId = produccion.Id;

            // Actualizar el stock de productos en inventario
            var productoInventario = await _context.InventarioProductos
                .FirstOrDefaultAsync(ip => ip.FabricacionId == fabricacionId && ip.Lote == request.Lote);

            if (productoInventario != null)
            {
                // Sumar la cantidad al inventario existente
                productoInventario.Cantidad += solicitudProduccion.Cantidad;
                _context.InventarioProductos.Update(productoInventario);
            }
            else
            {
                // Insertar un nuevo registro en inventarioProducto
                var nuevoInventario = new InventarioProducto
                {
                    Cantidad = solicitudProduccion.Cantidad,
                    Precio = 0.0f,
                    Lote = request.Lote,
                    FabricacionId = fabricacionId,
                    ProduccionId = produccionId,  // Usar el ID correcto de la producción
                    NivelMinimoStock = 0
                };

                _context.InventarioProductos.Add(nuevoInventario);
            }

            await _context.SaveChangesAsync();

            // Actualizar el estado de la solicitud a "Completada"
            solicitudProduccion.Estatus = 3;
            _context.Solicitudproduccions.Update(solicitudProduccion);
            await _context.SaveChangesAsync();

            return Ok(new { Mensaje = "Producción finalizada y stock actualizado." });
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var solicitudes = await _context.Solicitudproduccions
                    .Where(s => s.Estatus == 1 || s.Estatus == 2)
                    .Select(s => new SolicitudProduccionDTO
                    {
                        UsuarioId = s.UsuarioId,
                        FabricacionId = s.FabricacionId,
                        Cantidad = s.Cantidad,
                        Descripcion = s.Descripcion,
                        SolicitudId = s.Id,
                        estatus = s.Estatus
                        
                        
                        
                    })
                    .ToListAsync();

                if (solicitudes == null || !solicitudes.Any())
                {
                    return NotFound("No se encontraron solicitudes de producción.");
                }

                return Ok(solicitudes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al obtener las solicitudes: {ex.Message}");
            }
        }


    }

}
