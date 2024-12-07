namespace soldaline_back.DTOs
{
    // DTO para registrar un nuevo material en el inventario de materiales
    public class InventarioMaterialRegisterDTO
    {
        public int? Cantidad { get; set; }
        public int ProveedorId { get; set; }
        public int MaterialId { get; set; }
        public int DetallecompraId { get; set; }
    }

    public class InventarioMaterialesResponseDTO
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public int ProveedorId { get; set; }  // ID del proveedor
        public int MaterialId { get; set; }    // ID del material
        public int DetalleCompraId { get; set; }
        public string? NombreEmpresaProveedor { get; set; }  // Nombre de la empresa del proveedor
        public string? NombreMaterial { get; set; } // Nombre del material
    }


    // DTO para registrar un nuevo producto en el inventario de productos
    public class InventarioProductoRegisterDTO
    {
        public int? Cantidad { get; set; }
        public double? Precio { get; set; }
        public DateOnly? FechaCreacion { get; set; }
        public string? Lote { get; set; }
        public int FabricacionId { get; set; }
        public int ProduccionId { get; set; }
        public int? NivelMinimoStock { get; set; }
    }

    // DTO para la respuesta de inventario de productos
    public class InventarioProductoResponseDTO
    {
        public int Id { get; set; }
        public int? Cantidad { get; set; }
        public double? Precio { get; set; }
        public DateOnly? FechaCreacion { get; set; }
        public string? Lote { get; set; }
        public int FabricacionId { get; set; }
        public int ProduccionId { get; set; }
        public int? NivelMinimoStock { get; set; }
        public string? NombreFabricacion { get; set; }
        public string? NombreProduccion { get; set; }
    }

}
