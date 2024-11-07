using System;
using System.Collections.Generic;

namespace soldaline_back.Models;

public partial class DetallesUsuario
{
    public int Id { get; set; }

    public string? Nombres { get; set; }

    public string? ApellidoM { get; set; }

    public string? ApellidoP { get; set; }

    public string? Correo { get; set; }

    public byte? Estatus { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
