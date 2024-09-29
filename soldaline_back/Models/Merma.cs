using System;
using System.Collections.Generic;

namespace soldaline_back.Models;

public partial class Merma
{
    public int Id { get; set; }

    public int? Cantidad { get; set; }

    public string? Descripcion { get; set; }

    public DateOnly? Fecha { get; set; }

    public int UsuarioId { get; set; }

    public int? InventarioProductoId { get; set; }

    public int? InventariomaterialesId { get; set; }

    public virtual InventarioProducto? InventarioProducto { get; set; }

    public virtual Inventariomateriale? Inventariomateriales { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;
}
