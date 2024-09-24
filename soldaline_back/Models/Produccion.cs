using System;
using System.Collections.Generic;

namespace soldaline_back.Models;

public partial class Produccion
{
    public int Id { get; set; }

    public DateOnly? Fecha { get; set; }

    public double? Costo { get; set; }

    public int UsuarioId { get; set; }

    public int SolicitudproduccionId { get; set; }

    public virtual ICollection<Detalleproduccion> Detalleproduccions { get; set; } = new List<Detalleproduccion>();

    public virtual ICollection<InventarioProducto> InventarioProductos { get; set; } = new List<InventarioProducto>();

    public virtual Solicitudproduccion Solicitudproduccion { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
