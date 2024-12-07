using System;
using System.Collections.Generic;

namespace soldaline_back.Models;

public partial class RecetasProteccione
{
    public int IdReceta { get; set; }

    public int IdFabricacion { get; set; }

    public string TipoProteccion { get; set; } = null!;

    public string Material { get; set; } = null!;

    public int? Cantidad { get; set; }

    public string Unidad { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public int? Estatus { get; set; }

    public virtual Fabricacion IdFabricacionNavigation { get; set; } = null!;
}
