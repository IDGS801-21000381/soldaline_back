using System;
using System.Collections.Generic;

namespace soldaline_back.Models;

public partial class Cotizacion
{
    public int CotizacionId { get; set; }

    public int EmpresaId { get; set; }

    public DateTime? Fecha { get; set; }

    public decimal Subtotal { get; set; }

    public decimal Total { get; set; }

    public string? Vendedor { get; set; }

    public int Status { get; set; }

    public virtual Empresa Empresa { get; set; } = null!;
}
