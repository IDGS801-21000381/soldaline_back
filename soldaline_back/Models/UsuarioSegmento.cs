using System;
using System.Collections.Generic;

namespace soldaline_back.Models;

public partial class UsuarioSegmento
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public int SegmentoId { get; set; }

    public virtual SegmentoCliente Segmento { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
