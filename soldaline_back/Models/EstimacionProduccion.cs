using System;
using System.Collections.Generic;

namespace soldaline_back.Models;

public partial class EstimacionProduccion
{
    public int Id { get; set; }

    public double HorasP { get; set; }

    public int FabricacionId { get; set; }

    public virtual Fabricacion Fabricacion { get; set; } = null!;
}
