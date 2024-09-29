using System;
using System.Collections.Generic;

namespace soldaline_back.Models;

public partial class DetallePedido
{
    public int Id { get; set; }

    public int FabricacionId { get; set; }

    public int PedidoId { get; set; }

    public int? Cantidad { get; set; }

    public double? PrecioUnitario { get; set; }

    public virtual Fabricacion Fabricacion { get; set; } = null!;

    public virtual Pedido Pedido { get; set; } = null!;
}

