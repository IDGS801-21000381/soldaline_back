//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using soldaline_back.DTOs;
//using soldaline_back.Models;
//using System;
//using System.Linq;
//using System.Threading.Tasks;

//namespace soldaline_back.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ClientePotencialController : ControllerBase
//    {
//        private readonly SoldalineBdContext _context;

//        public ClientePotencialController(SoldalineBdContext context)
//        {
//            _context = context;
//        }

//        // GET: api/ClientePotencial
//        [HttpGet]
//        public async Task<IActionResult> GetClientesPotenciales()
//        {
//            var clientes = await _context.ClientePotencials
//                .Select(c => new ClientePotencialDto
//                {
//                    ClienteID = c.ClienteId,
//                    Nombre = c.Nombre,
//                    Direccion = c.Direccion,
//                    Telefono = c.Telefono,
//                    Correo = c.Correo,
//                    RedesSociales = c.RedesSociales,
//                    Origen = c.Origen,
//                    PreferenciaComunicacion = c.PreferenciaComunicacion,
//                    EmpresaID = c.EmpresaId,
//                    UsuarioID = c.UsuarioId,
//                    Estatus = c.Estatus
//                })
//                .ToListAsync();

//            return Ok(clientes);
//        }

//        // GET: api/ClientePotencial/{id}
//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetClientePotencial(int id)
//        {
//            var cliente = await _context.ClientePotencials
//                .Where(c => c.ClienteId == id)
//                .Select(c => new ClientePotencialDto
//                {
//                    ClienteID = c.ClienteId,
//                    Nombre = c.Nombre,
//                    Direccion = c.Direccion,
//                    Telefono = c.Telefono,
//                    Correo = c.Correo,
//                    RedesSociales = c.RedesSociales,
//                    Origen = c.Origen,
//                    PreferenciaComunicacion = c.PreferenciaComunicacion,
//                    EmpresaID = c.EmpresaId,
//                    UsuarioID = c.UsuarioId,
//                    Estatus = c.Estatus
//                })
//                .FirstOrDefaultAsync();

//            if (cliente == null)
//            {
//                return NotFound("No se encontró el cliente potencial.");
//            }

//            return Ok(cliente);
//        }

//        // POST: api/ClientePotencial
//        [HttpPost]
//        public async Task<IActionResult> CreateClientePotencial([FromBody] ClientePotencialDto request)
//        {
//            if (request == null || string.IsNullOrEmpty(request.Nombre))
//            {
//                return BadRequest("Datos inválidos.");
//            }

//            var nuevoCliente = new ClientePotencial
//            {
//                Nombre = request.Nombre,
//                Direccion = request.Direccion,
//                Telefono = request.Telefono,
//                Correo = request.Correo,
//                RedesSociales = request.RedesSociales,
//                Origen = request.Origen,
//                PreferenciaComunicacion = request.PreferenciaComunicacion,
//                EmpresaId = request.EmpresaID,
//                UsuarioId = request.UsuarioID,
//                Estatus = 1, // Estatus inicial
//                FechaRegistro = DateTime.Now // Fecha automática
//            };

//            _context.ClientePotencials.Add(nuevoCliente);
//            await _context.SaveChangesAsync();

//            return Ok(new { Mensaje = "Cliente potencial creado exitosamente.", ClienteId = nuevoCliente.ClienteId });
//        }

//        // PUT: api/ClientePotencial/{id}
//        [HttpPut("{id}")]
//        public async Task<IActionResult> UpdateClientePotencial(int id, [FromBody] ClientePotencialDto request)
//        {
//            if (id != request.ClienteID)
//            {
//                return BadRequest("El ID proporcionado no coincide con el cliente.");
//            }

//            var cliente = await _context.ClientePotencials.FindAsync(id);
//            if (cliente == null)
//            {
//                return NotFound("No se encontró el cliente potencial.");
//            }

//            cliente.Nombre = request.Nombre;
//            cliente.Direccion = request.Direccion;
//            cliente.Telefono = request.Telefono;
//            cliente.Correo = request.Correo;
//            cliente.RedesSociales = request.RedesSociales;
//            cliente.Origen = request.Origen;
//            cliente.PreferenciaComunicacion = request.PreferenciaComunicacion;
//            cliente.Estatus = request.Estatus;

//            _context.ClientePotencials.Update(cliente);
//            await _context.SaveChangesAsync();

//            return Ok(new { Mensaje = "Cliente potencial actualizado exitosamente." });
//        }

//        // DELETE: api/ClientePotencial/{id}
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteClientePotencial(int id)
//        {
//            var cliente = await _context.ClientePotencials.FindAsync(id);
//            if (cliente == null)
//            {
//                return NotFound("No se encontró el cliente potencial.");
//            }

//            _context.ClientePotencials.Remove(cliente);
//            await _context.SaveChangesAsync();

//            return Ok(new { Mensaje = "Cliente potencial eliminado exitosamente." });
//        }
//    }
//}
