using System;
using System.Collections.Generic;

namespace soldaline_back.Models;

public partial class Material
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Detallecompra> Detallecompras { get; set; } = new List<Detallecompra>();

    public virtual ICollection<Inventariomateriale> Inventariomateriales { get; set; } = new List<Inventariomateriale>();

    public virtual ICollection<Materialfabricacion> Materialfabricacions { get; set; } = new List<Materialfabricacion>();

    public virtual ICollection<Productoproveedor> Productoproveedors { get; set; } = new List<Productoproveedor>();
}
