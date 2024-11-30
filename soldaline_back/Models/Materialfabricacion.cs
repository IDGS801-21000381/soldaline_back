using System;
using System.Collections.Generic;

namespace soldaline_back.Models;

public partial class Materialfabricacion
{
    public int Id { get; set; }

    public int? Cantidad { get; set; }

    public int? Estatus { get; set; }

    public int FabricacionId { get; set; }

    public int MaterialId { get; set; }

    public virtual Fabricacion Fabricacion { get; set; } = null!;

    public virtual Material Material { get; set; } = null!;
}
