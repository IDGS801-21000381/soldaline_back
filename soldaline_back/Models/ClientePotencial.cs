using System;
using System.Collections.Generic;

namespace soldaline_back.Models;

public partial class ClientePotencial
{
    public int ClienteId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public string? Correo { get; set; }

    public string? RedesSociales { get; set; }

    public string? Origen { get; set; }

    public string? PreferenciaComunicacion { get; set; }

    public int? EmpresaId { get; set; }

    public int UsuarioId { get; set; }

    public int Estatus { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual Empresa? Empresa { get; set; }

    public virtual ICollection<HistorialComunicacion> HistorialComunicacions { get; set; } = new List<HistorialComunicacion>();

    public virtual ICollection<Proyecto> Proyectos { get; set; } = new List<Proyecto>();

    public virtual Usuario Usuario { get; set; } = null!;
}
