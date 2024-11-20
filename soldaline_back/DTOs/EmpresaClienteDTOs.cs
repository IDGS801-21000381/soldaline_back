namespace soldaline_back.DTOs;

public class EmpresaClienteRegisterDTO
{
    public string NombreCliente { get; set; } = null!;
    public string? DireccionCliente { get; set; }
    public string? TelefonoCliente { get; set; }
    public string? CorreoCliente { get; set; }
    public string? RedesSociales { get; set; }
    public string? Origen { get; set; }
    public string? PreferenciaComunicacion { get; set; }
    public int UsuarioId { get; set; }
    public string NombreEmpresa { get; set; } = null!;
    public string? DireccionEmpresa { get; set; }
    public string? TelefonoEmpresa { get; set; }
    public string? CorreoEmpresa { get; set; }
    public string? SitioWeb { get; set; }
}

public class EmpresaClienteUpdateDTO
{
    public int ClienteId { get; set; }
    public string? NombreCliente { get; set; }
    public string? DireccionCliente { get; set; }
    public string? TelefonoCliente { get; set; }
    public string? CorreoCliente { get; set; }
    public string? RedesSociales { get; set; }
    public int? EmpresaId { get; set; }
    public string? NombreEmpresa { get; set; }
    public string? DireccionEmpresa { get; set; }
    public string? TelefonoEmpresa { get; set; }
    public string? CorreoEmpresa { get; set; }
    public string? SitioWeb { get; set; }
}
