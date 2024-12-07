namespace soldaline_back.DTOs;

public class ProyectoRegisterDTO
{
    public int ClienteId { get; set; }
    public int? EmpresaId { get; set; }
    public string NombreProyecto { get; set; } = null!;
    public string? Descripcion { get; set; }
    public decimal? Monto { get; set; }
    public DateOnly FechaInicio { get; set; }
    public DateOnly? FechaFin { get; set; }
}
