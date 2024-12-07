using System;
using System.Collections.Generic;

namespace soldaline_back.Models;

public partial class HistorialDescuento
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public int CompraId { get; set; }

    public DateOnly Fecha { get; set; }

    public double MontoDescuento { get; set; }

    public virtual Compra Compra { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
