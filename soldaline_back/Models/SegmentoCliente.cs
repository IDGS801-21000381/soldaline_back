using System;
using System.Collections.Generic;

namespace soldaline_back.Models;

public partial class SegmentoCliente
{
    public int Id { get; set; }

    public string NombreSegmento { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<UsuarioSegmento> UsuarioSegmentos { get; set; } = new List<UsuarioSegmento>();
}
