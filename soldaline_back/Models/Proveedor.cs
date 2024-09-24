using System;
using System.Collections.Generic;

namespace soldaline_back.Models;

public partial class Proveedor
{
    public int Id { get; set; }

    public string? NombreEmpresa { get; set; }

    public string? Direccion { get; set; }

    public string? TelefonoContacto { get; set; }

    public string? NombreContacto { get; set; }

    public string? ApellidoM { get; set; }

    public string? ApellidoP { get; set; }

    public byte? Estatus { get; set; }

    public virtual ICollection<Inventariomateriale> Inventariomateriales { get; set; } = new List<Inventariomateriale>();

    public virtual ICollection<Productoproveedor> Productoproveedors { get; set; } = new List<Productoproveedor>();
}
