using System;
using System.Collections.Generic;

namespace soldaline_back.Models;

public partial class Productoproveedor
{
    public int Id { get; set; }

    public int ProveedorId { get; set; }

    public int MaterialId { get; set; }

    public virtual Material Material { get; set; } = null!;

    public virtual Proveedor Proveedor { get; set; } = null!;
}
