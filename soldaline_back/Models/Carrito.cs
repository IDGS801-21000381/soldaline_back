using System;
using System.Collections.Generic;

namespace soldaline_back.Models;

public partial class Carrito
{
    public int Id { get; set; }

    public DateOnly? Fecha { get; set; }

    public byte? Estatus { get; set; }

    public int FabricacionId { get; set; }

    public int UsuarioId { get; set; }

    public virtual Fabricacion Fabricacion { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
