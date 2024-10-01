using System;
using System.Collections.Generic;

namespace soldaline_back.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Contrasenia { get; set; } // Guardaremos la contraseña encriptada.

    public string? Token { get; set; } // Token de autenticación para sesiones seguras.

    public string? Rol { get; set; }

    public byte? Estatus { get; set; } // 1 = Activo, 0 = Inactivo (Eliminación lógica).

    public string? UrlImage { get; set; } // URL de la imagen de perfil.

    public string? Direccion { get; set; } // Dirección de usuario.

    public string? Tarjeta { get; set; } // Información de tarjeta (esto debe guardarse de forma segura o usar un sistema externo para tokens de pago).

    public int? DetallesUsuarioId { get; set; }

    public int? FrecuenciaCompra { get; set; }

    public bool? ClientePotencial { get; set; }

    public virtual DetallesUsuario? DetallesUsuario { get; set; }

    public virtual ICollection<Carrito> Carritos { get; set; } = new List<Carrito>();

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

    public virtual ICollection<HistorialDescuento> HistorialDescuentos { get; set; } = new List<HistorialDescuento>();

    public virtual ICollection<Merma> Mermas { get; set; } = new List<Merma>();

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    public virtual ICollection<Produccion> Produccions { get; set; } = new List<Produccion>();

    public virtual ICollection<Solicitudproduccion> Solicitudproduccions { get; set; } = new List<Solicitudproduccion>();

    public virtual ICollection<UsuarioSegmento> UsuarioSegmentos { get; set; } = new List<UsuarioSegmento>();

    public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();
}
