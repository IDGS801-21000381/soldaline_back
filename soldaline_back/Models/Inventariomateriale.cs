using System;
using System.Collections.Generic;

namespace soldaline_back.Models;

public partial class Inventariomateriale
{
    public int Id { get; set; }

    public int? Cantidad { get; set; }

    public int MaterialId { get; set; }

    public int DetallecompraId { get; set; }

    public virtual Detallecompra Detallecompra { get; set; } = null!;

    public virtual ICollection<Detalleproduccion> Detalleproduccions { get; set; } = new List<Detalleproduccion>();

    public virtual Material Material { get; set; } = null!;

    public virtual ICollection<Merma> Mermas { get; set; } = new List<Merma>();
}
