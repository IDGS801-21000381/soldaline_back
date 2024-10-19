using System;
using System.Collections.Generic;

namespace soldaline_back.Models;

public partial class Compra
{
    public int Id { get; set; }

    public DateOnly? Fecha { get; set; }

    public int ProveedorId { get; set; }

    public int UsuarioId { get; set; }

    public virtual ICollection<Detallecompra> Detallecompras { get; set; } = new List<Detallecompra>();

    public virtual ICollection<HistorialDescuento> HistorialDescuentos { get; set; } = new List<HistorialDescuento>();

    public virtual Proveedor Proveedor { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
