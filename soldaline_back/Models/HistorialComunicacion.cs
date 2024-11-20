using System;
using System.Collections.Generic;

namespace soldaline_back.Models;

public partial class HistorialComunicacion
{
    public int HistorialId { get; set; }

    public int ClienteId { get; set; }

    public int UsuarioId { get; set; }

    public DateOnly FechaComunicacion { get; set; }

    public int Estatus { get; set; }

    public int TipoComunicacion { get; set; }

    public string DetallesComunicado { get; set; } = null!;

    public DateOnly? FechaProximaCita { get; set; }

    public string? Solicitud { get; set; }

    public virtual ClientePotencial Cliente { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
