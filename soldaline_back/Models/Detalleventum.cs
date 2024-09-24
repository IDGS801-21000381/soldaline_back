using System;
using System.Collections.Generic;

namespace soldaline_back.Models;

public partial class Detalleventum
{
    public int Id { get; set; }

    public int? Cantidad { get; set; }

    public double? PrecioUnitario { get; set; }

    public int VentaId { get; set; }

    public int InventarioProductoId { get; set; }

    public virtual InventarioProducto InventarioProducto { get; set; } = null!;

    public virtual Ventum Venta { get; set; } = null!;
}
