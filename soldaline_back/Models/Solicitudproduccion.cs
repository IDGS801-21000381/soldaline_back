using System;
using System.Collections.Generic;

namespace soldaline_back.Models;

public partial class Solicitudproduccion
{
    public int Id { get; set; }

    public string? Descripcion { get; set; }

    public int? Estatus { get; set; }

    public int? Cantidad { get; set; }

    public int FabricacionId { get; set; }

    public int UsuarioId { get; set; }

    public virtual Fabricacion Fabricacion { get; set; } = null!;

    public virtual ICollection<Produccion> Produccions { get; set; } = new List<Produccion>();

    public virtual Usuario Usuario { get; set; } = null!;
}
