using System;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class EmailService : IHostedService, IDisposable
{
    private Timer _timer;
    private readonly ILogger<EmailService> _logger;

    public EmailService(ILogger<EmailService> logger)
    {
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
       
        _timer = new Timer(SendEmail, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
        _logger.LogInformation("Servicio de envío de correos iniciado.");
        return Task.CompletedTask;
    }

    private void SendEmail(object state)
    {
        try
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("hernandexzpedro@gmail.com");  
            mail.To.Add("pedrohernandexz904@gmail.com");               
            mail.Subject = "Correo automático";
            mail.Body = File.ReadAllText("./Services/Ofertas.html"); 
            mail.IsBodyHtml = true;
            ;
            mail.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new NetworkCredential("hernandexzpedro@gmail.com", "ixau eozk dmkd kwxe");
            smtpClient.EnableSsl = true;

            smtpClient.Send(mail);
            _logger.LogInformation("Correo enviado correctamente.");
        }
        catch (Exception ex)
        {
            _logger.LogError("Error al enviar el correo: " + ex.Message);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        _logger.LogInformation("Servicio de envío de correos detenido.");
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
