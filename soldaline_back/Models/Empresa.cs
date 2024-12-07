using System;
using System.Collections.Generic;

namespace soldaline_back.Models;

public partial class Empresa
{
    public int EmpresaId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public string? Correo { get; set; }

    public string? SitioWeb { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<ClientePotencial> ClientePotencials { get; set; } = new List<ClientePotencial>();

    public virtual ICollection<Proyecto> Proyectos { get; set; } = new List<Proyecto>();
}
