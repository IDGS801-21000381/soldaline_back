using System;
using System.Collections.Generic;

namespace soldaline_back.Models;

public partial class Pago
{
    public int Id { get; set; }

    public int CuentaId { get; set; }

    public decimal Monto { get; set; }

    public DateOnly FechaPago { get; set; }

    public int MetodoPago { get; set; }

    public int ClientePotencialId { get; set; }

    public int UsuarioId { get; set; }

    public virtual ClientePotencial ClientePotencial { get; set; } = null!;

    public virtual CuentasPorPagar Cuenta { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
