namespace soldaline_back.DTOs;

public class HistorialComunicacionDTO
{
    public int ClienteId { get; set; }
    public int UsuarioId { get; set; }
    public DateOnly FechaComunicacion { get; set; }
    public int TipoComunicacion { get; set; }
    public string DetallesComunicado { get; set; } = null!;
    public DateOnly? FechaProximaCita { get; set; }
    public string? Solicitud { get; set; }
}

public class HistorialComunicacionUpdateDTO
{
    public int HistorialId { get; set; }
    public int TipoComunicacion { get; set; }
    public string? DetallesComunicado { get; set; }
    public DateOnly? FechaProximaCita { get; set; }
    public string? Solicitud { get; set; }
}
