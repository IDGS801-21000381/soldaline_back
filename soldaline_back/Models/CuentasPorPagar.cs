using System;
using System.Collections.Generic;

namespace soldaline_back.Models;

public partial class CuentasPorPagar
{
    public int Id { get; set; }

    public int VentasId { get; set; }

    public int ClienteId { get; set; }

    public int UsuarioId { get; set; }

    public decimal SaldoTotal { get; set; }

    public decimal SaldoPendiente { get; set; }

    public DateOnly Fecha { get; set; }

    public int DiasPlazo { get; set; }

    public int Estatus { get; set; }

    public virtual ClientePotencial Cliente { get; set; } = null!;

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();

    public virtual Usuario Usuario { get; set; } = null!;

    public virtual Ventum Ventas { get; set; } = null!;
}
