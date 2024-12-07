using System;
using System.Collections.Generic;

namespace soldaline_back.Models;

public partial class Fabricacion
{
    public int Id { get; set; }

    public string? NombreProducto { get; set; }

    public string? ImagenProducto { get; set; }

    public int? Estatus { get; set; }

    public int? PrecioProducto { get; set; }

    public string? Categoria { get; set; }

    public virtual ICollection<Carrito> Carritos { get; set; } = new List<Carrito>();

    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

    public virtual ICollection<EstimacionProduccion> EstimacionProduccions { get; set; } = new List<EstimacionProduccion>();

    public virtual ICollection<InventarioProducto> InventarioProductos { get; set; } = new List<InventarioProducto>();

    public virtual ICollection<Materialfabricacion> Materialfabricacions { get; set; } = new List<Materialfabricacion>();

    public virtual ICollection<Solicitudproduccion> Solicitudproduccions { get; set; } = new List<Solicitudproduccion>();
}
