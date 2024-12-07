using System;
using System.Collections.Generic;

namespace soldaline_back.Models;

public partial class InventarioProducto
{
    public int Id { get; set; }

    public int? Cantidad { get; set; }

    public double? Precio { get; set; }

    public DateOnly? FechaCreacion { get; set; }

    public string? Lote { get; set; }

    public int FabricacionId { get; set; }

    public int ProduccionId { get; set; }

    public int? NivelMinimoStock { get; set; }

    public virtual ICollection<Detalleventum> Detalleventa { get; set; } = new List<Detalleventum>();

    public virtual Fabricacion Fabricacion { get; set; } = null!;

    public virtual ICollection<Merma> Mermas { get; set; } = new List<Merma>();

    public virtual Produccion Produccion { get; set; } = null!;
}
