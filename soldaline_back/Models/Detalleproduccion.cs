using System;
using System.Collections.Generic;

namespace soldaline_back.Models;

public partial class Detalleproduccion
{
    public int Id { get; set; }

    public int ProduccionId { get; set; }

    public int InventariomaterialesId { get; set; }

    public virtual Inventariomateriale Inventariomateriales { get; set; } = null!;

    public virtual Produccion Produccion { get; set; } = null!;
}
