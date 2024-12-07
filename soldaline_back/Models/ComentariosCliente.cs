using System;
using System.Collections.Generic;

namespace soldaline_back.Models;

public partial class ComentariosCliente
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public int ClienteId { get; set; }

    public DateOnly Fecha { get; set; }

    public int Tipo { get; set; }

    public string Descripcion { get; set; } = null!;

    public int Estatus { get; set; }

    public int Calificacion { get; set; }

    public virtual ClientePotencial Cliente { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
