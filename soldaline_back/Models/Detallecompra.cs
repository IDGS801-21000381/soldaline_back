using System;
using System.Collections.Generic;

namespace soldaline_back.Models;

public partial class Detallecompra
{
    public int Id { get; set; }

    public int? Cantidad { get; set; }

    public string? Folio { get; set; }

    public string? Descripcion { get; set; }

    public double? Costo { get; set; }
    public int CompraId { get; set; }

    public virtual Compra Compra { get; set; } = null!;

    public virtual ICollection<Inventariomateriale> Inventariomateriales { get; set; } = new List<Inventariomateriale>();
}
