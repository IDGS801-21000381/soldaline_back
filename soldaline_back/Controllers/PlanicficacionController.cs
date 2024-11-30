using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using soldaline_back.Models;
using System.Threading.Tasks;

namespace soldaline_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanificacionController : ControllerBase
    {
        private readonly SoldalineBdContext _context;

        private const int HorasLaboralesPorDia = 8;

        public PlanificacionController(SoldalineBdContext context)
        {
            _context = context;
        }

        public class CalcularTiempoProduccionRequest
        {
            public double Cantidad { get; set; }
            public int FabricacionId { get; set; }
        }

        public class CalcularTiempoProduccionResponse
        {
            public int FabricacionId { get; set; }
            public double Cantidad { get; set; }
            public double TiempoTotalHoras { get; set; }
            public double DiasLaborales { get; set; }
            public double DiasEntrega { get; set; }
        }

        [HttpPost("calcularTiempoProduccion")]
        public async Task<IActionResult> CalcularTiempoProduccion([FromBody] CalcularTiempoProduccionRequest request)
        {
            if (request.Cantidad <= 0)
            {
                return BadRequest("La cantidad debe ser mayor a 0.");
            }

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

            var estimacionProduccion = await _context.EstimacionProduccions
                .FirstOrDefaultAsync(ep => ep.FabricacionId == request.FabricacionId);

            if (estimacionProduccion == null)
            {
                return NotFound("No se encontró una estimación de producción para el producto especificado.");
            }

            double tiempoTotalHoras = request.Cantidad * estimacionProduccion.HorasP;

            double diasLaborales = tiempoTotalHoras / HorasLaboralesPorDia;

            var response = new CalcularTiempoProduccionResponse
            {
                FabricacionId = request.FabricacionId,
                Cantidad = request.Cantidad,
                TiempoTotalHoras = tiempoTotalHoras,
                DiasLaborales = diasLaborales,
                DiasEntrega = diasLaborales + 7
            };

            return Ok(response);
        }
    }
}