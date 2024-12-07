using System;
using System.Collections.Generic;

namespace soldaline_back.Models;

public partial class Proyecto
{
    public int ProyectoId { get; set; }

    public int ClienteId { get; set; }

    public int? EmpresaId { get; set; }

    public string NombreProyecto { get; set; } = null!;

    public string? Descripcion { get; set; }

    public int Estatus { get; set; }

    public decimal? Monto { get; set; }

    public DateOnly FechaInicio { get; set; }

    public DateOnly? FechaFin { get; set; }

    public virtual ClientePotencial Cliente { get; set; } = null!;

    public virtual Empresa? Empresa { get; set; }
}
