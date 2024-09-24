//using Microsoft.AspNetCore.Mvc;
//using soldaline_back.Models; // Asegúrate de tener tus modelos aquí.
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;

//namespace soldaline_back.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UsuarioController : Controller
//    {
//        private readonly SoldalineContext _context;

//        public UsuarioController(SoldalineContext context)
//        {
//            _context = context;
//        }

//        // Método para registrar un nuevo usuario
//        [HttpPost("register")]
//        public async Task<IActionResult> Register([FromBody] Usuario usuario)
//        {
//            if (usuario == null)
//            {
//                return BadRequest("Datos inválidos.");
//            }

//            // Verificar si el correo ya está registrado
//            var existeCorreo = await _context.Usuarios
//                .Include(u => u.DetallesUsuario)
//                .AnyAsync(u => u.DetallesUsuario.Correo == usuario.DetallesUsuario.Correo);

//            if (existeCorreo)
//            {
//                return BadRequest("Correo ya registrado.");
//            }

//            // Guardar usuario
//            usuario.Estatus = 1; // Usuario activo
//            _context.Usuarios.Add(usuario);
//            await _context.SaveChangesAsync();

//            return Ok("Usuario registrado exitosamente.");
//        }

//        // Método para login de usuario
//        [HttpPost("login")]
//        public async Task<IActionResult> Login([FromBody] UsuarioLoginRequest loginRequest)
//        {
//            if (loginRequest == null)
//            {
//                return BadRequest("Datos inválidos.");
//            }

//            // Buscar usuario por correo
//            var usuario = await _context.Usuarios
//                .Include(u => u.DetallesUsuario)
//                .FirstOrDefaultAsync(u => u.DetallesUsuario.Correo == loginRequest.Correo);

//            if (usuario == null)
//            {
//                return Unauthorized("Usuario no encontrado.");
//            }

//            // Verificar la contraseña
//            if (usuario.Contraseña != loginRequest.Contraseña)
//            {
//                return Unauthorized("Contraseña incorrecta.");
//            }

//            // Si la autenticación es exitosa, devolver el usuario
//            return Ok(usuario);
//        }

//        // Método para obtener los detalles del usuario
//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetUsuario(int id)
//        {
//            var usuario = await _context.Usuarios
//                .Include(u => u.DetallesUsuario)
//                .FirstOrDefaultAsync(u => u.Id == id);

//            if (usuario == null)
//            {
//                return NotFound("Usuario no encontrado.");
//            }

//            return Ok(usuario);
//        }
//    }

//    // Clase para manejar el login (simplificada)
//    public class UsuarioLoginRequest
//    {
//        public string Correo { get; set; }
//        public string Contraseña { get; set; }
//    }
//}
