using System;
using System.Collections.Generic;

namespace soldaline_back.Models;

public partial class Ventum
{
    public int Id { get; set; }

    public DateTime? Fecha { get; set; }

    public string? Folio { get; set; }

    public int UsuarioId { get; set; }

    public virtual ICollection<CuentasPorPagar> CuentasPorPagars { get; set; } = new List<CuentasPorPagar>();

    public virtual ICollection<Detalleventum> Detalleventa { get; set; } = new List<Detalleventum>();

    public virtual Usuario Usuario { get; set; } = null!;
}
