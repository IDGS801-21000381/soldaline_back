using System;
using System.Collections.Generic;

namespace soldaline_back.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Contraseña { get; set; }

    public string? Token { get; set; }

    public string? Rol { get; set; }

    public byte? Estatus { get; set; }

    public int? DetallesUsuarioId { get; set; }

    public virtual ICollection<Carrito> Carritos { get; set; } = new List<Carrito>();

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

    public virtual DetallesUsuario? DetallesUsuario { get; set; }

    public virtual ICollection<Merma> Mermas { get; set; } = new List<Merma>();

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    public virtual ICollection<Produccion> Produccions { get; set; } = new List<Produccion>();

    public virtual ICollection<Solicitudproduccion> Solicitudproduccions { get; set; } = new List<Solicitudproduccion>();

    public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();
}
