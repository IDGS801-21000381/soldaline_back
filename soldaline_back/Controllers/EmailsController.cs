using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace soldaline_back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailsController : ControllerBase
    {
        // POST: api/emails/send
        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] EmailRequest emailRequest)
        {
            if (emailRequest == null || string.IsNullOrEmpty(emailRequest.To) || string.IsNullOrEmpty(emailRequest.Subject) || string.IsNullOrEmpty(emailRequest.Message))
            {
                return BadRequest("Todos los campos son obligatorios: To, Subject, Message.");
            }

            try
            {
                // Configuración del cliente SMTP
                var smtpClient = new SmtpClient("smtp.tuservidor.com")
                {
                    Port = 587, // Cambia al puerto adecuado
                    Credentials = new NetworkCredential("tuemail@tuservidor.com", "tucontraseña"),
                    EnableSsl = true
                };

                // Crear el mensaje de correo
                var mailMessage = new MailMessage
                {
                    From = new MailAddress("tuemail@tuservidor.com"),
                    Subject = emailRequest.Subject,
                    Body = emailRequest.Message,
                    IsBodyHtml = true // Cambiar a false si el mensaje no tiene HTML
                };
                mailMessage.To.Add(emailRequest.To);

                // Enviar el correo
                await smtpClient.SendMailAsync(mailMessage);

                return Ok("Correo enviado exitosamente.");
            }
            catch (SmtpException ex)
            {
                return StatusCode(500, $"Error enviando el correo: {ex.Message}");
            }
        }
    }

    // Modelo para recibir los datos del correo
    public class EmailRequest
    {
        public string To { get; set; } // Dirección del destinatario
        public string Subject { get; set; } // Asunto del correo
        public string Message { get; set; } // Contenido del correo
    }
}
