//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using soldaline_back.Models;
//using soldaline_back.DTOs;

//namespace soldaline_back.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class EmpresaClienteController : ControllerBase
//    {
//        private readonly SoldalineBdContext _context;

//        public EmpresaClienteController(SoldalineBdContext context)
//        {
//            _context = context;
//        }

//        // Obtener vista resumen de clientes y empresas
//        [HttpGet("vista")]
//        public async Task<IActionResult> GetResumenVista()
//        {
//            var clientesVista = await _context.ClientePotencials
//                .Include(c => c.Empresa) // Incluimos los datos de la empresa asociada
//                .Select(c => new
//                {
//                    NombreCliente = c.Nombre,
//                    NombreEmpresa = c.Empresa != null ? c.Empresa.Nombre : "Sin Empresa",
//                    Correo = c.Correo,
//                    Telefono = c.Telefono,
//                    Direccion = c.Direccion,
//                    Estatus = c.Estatus
//                })
//                .ToListAsync();

//            return Ok(clientesVista);
//        }

//        // Obtener detalles completos de un cliente y su empresa
//        [HttpGet("detalles/{id}")]
//        public async Task<IActionResult> GetDetallesCliente(int id)
//        {
//            var cliente = await _context.ClientePotencials
//                .Include(c => c.Empresa) // Incluimos los datos de la empresa asociada
//                .FirstOrDefaultAsync(c => c.ClienteId == id);

//            if (cliente == null) return NotFound("Cliente no encontrado.");

//            var detalles = new
//            {
//                Cliente = new
//                {
//                    cliente.ClienteId,
//                    cliente.Nombre,
//                    cliente.Direccion,
//                    cliente.Telefono,
//                    cliente.Correo,
//                    cliente.RedesSociales,
//                    cliente.Origen,
//                    cliente.PreferenciaComunicacion,
//                    cliente.Estatus,
//                    cliente.FechaRegistro
//                },
//                Empresa = cliente.Empresa != null ? new
//                {
//                    cliente.Empresa.EmpresaId,
//                    cliente.Empresa.Nombre,
//                    cliente.Empresa.Direccion,
//                    cliente.Empresa.Telefono,
//                    cliente.Empresa.Correo,
//                    cliente.Empresa.SitioWeb,
//                    cliente.Empresa.FechaRegistro
//                } : null
//            };

//            return Ok(detalles);
//        }

//        // Crear Cliente y Empresa
//        [HttpPost("register")]
//        public async Task<IActionResult> Register([FromBody] EmpresaClienteRegisterDTO dto)
//        {
//            if (dto == null) return BadRequest("Datos inválidos.");

//            var empresa = new Empresa
//            {
//                Nombre = dto.NombreEmpresa,
//                Direccion = dto.DireccionEmpresa,
//                Telefono = dto.TelefonoEmpresa,
//                Correo = dto.CorreoEmpresa,
//                SitioWeb = dto.SitioWeb,
//                FechaRegistro = DateTime.Now
//            };

//            _context.Empresas.Add(empresa);
//            await _context.SaveChangesAsync();

//            var cliente = new ClientePotencial
//            {
//                Nombre = dto.NombreCliente,
//                Direccion = dto.DireccionCliente,
//                Telefono = dto.TelefonoCliente,
//                Correo = dto.CorreoCliente,
//                RedesSociales = dto.RedesSociales,
//                Origen = dto.Origen,
//                PreferenciaComunicacion = dto.PreferenciaComunicacion,
//                UsuarioId = dto.UsuarioId,
//                EmpresaId = empresa.EmpresaId,
//                Estatus = 1,
//                FechaRegistro = DateTime.Now
//            };

//            _context.ClientePotencials.Add(cliente);
//            await _context.SaveChangesAsync();

//            return Ok("Cliente y Empresa registrados correctamente.");
//        }

//        // Actualizar Cliente y Empresa
//        [HttpPut("update")]
//        public async Task<IActionResult> Update([FromBody] EmpresaClienteUpdateDTO dto)
//        {
//            var cliente = await _context.ClientePotencials.FirstOrDefaultAsync(c => c.ClienteId == dto.ClienteId);
//            if (cliente == null) return NotFound("Cliente no encontrado.");

//            var empresa = dto.EmpresaId.HasValue
//                ? await _context.Empresas.FirstOrDefaultAsync(e => e.EmpresaId == dto.EmpresaId.Value)
//                : null;

//            if (dto.NombreCliente != null) cliente.Nombre = dto.NombreCliente;
//            if (dto.DireccionCliente != null) cliente.Direccion = dto.DireccionCliente;
//            if (dto.TelefonoCliente != null) cliente.Telefono = dto.TelefonoCliente;
//            if (dto.CorreoCliente != null) cliente.Correo = dto.CorreoCliente;
//            if (dto.RedesSociales != null) cliente.RedesSociales = dto.RedesSociales;

//            if (empresa != null)
//            {
//                if (dto.NombreEmpresa != null) empresa.Nombre = dto.NombreEmpresa;
//                if (dto.DireccionEmpresa != null) empresa.Direccion = dto.DireccionEmpresa;
//                if (dto.TelefonoEmpresa != null) empresa.Telefono = dto.TelefonoEmpresa;
//                if (dto.CorreoEmpresa != null) empresa.Correo = dto.CorreoEmpresa;
//                if (dto.SitioWeb != null) empresa.SitioWeb = dto.SitioWeb;
//            }

//            await _context.SaveChangesAsync();
//            return Ok("Cliente y Empresa actualizados correctamente.");
//        }

//        // Cambiar estatus de Cliente
//        [HttpPut("change-status/{id}")]
//        public async Task<IActionResult> ChangeStatus(int id, [FromBody] int nuevoEstatus)
//        {
//            var cliente = await _context.ClientePotencials.FirstOrDefaultAsync(c => c.ClienteId == id);
//            if (cliente == null) return NotFound("Cliente no encontrado.");

//            cliente.Estatus = nuevoEstatus;
//            await _context.SaveChangesAsync();
//            return Ok($"Estatus cambiado a {nuevoEstatus}.");
//        }

//        // Eliminar Cliente
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> Delete(int id)
//        {
//            var cliente = await _context.ClientePotencials.FirstOrDefaultAsync(c => c.ClienteId == id);
//            if (cliente == null) return NotFound("Cliente no encontrado.");

//            _context.ClientePotencials.Remove(cliente);
//            await _context.SaveChangesAsync();
//            return Ok("Cliente eliminado.");
//        }

//        // Buscar clientes por nombre de cliente o empresa
//        [HttpGet("buscar")]
//        public async Task<IActionResult> SearchClientes(string searchTerm)
//        {
//            var clientes = await _context.ClientePotencials
//                .Include(c => c.Empresa) // Incluir los datos de la empresa asociada
//                .Where(c => c.Nombre.Contains(searchTerm) ||
//                            (c.Empresa != null && c.Empresa.Nombre.Contains(searchTerm))) // Filtrar por cliente o empresa
//                .Select(c => new
//                {
//                    NombreCliente = c.Nombre,
//                    NombreEmpresa = c.Empresa != null ? c.Empresa.Nombre : "Sin Empresa",
//                    Correo = c.Correo,
//                    Telefono = c.Telefono,
//                    Direccion = c.Direccion,
//                    Estatus = c.Estatus
//                })
//                .ToListAsync();

//            if (clientes.Count == 0)
//                return NotFound("No se encontraron clientes o empresas con ese nombre.");

//            return Ok(clientes);
//        }
//    }
//}
