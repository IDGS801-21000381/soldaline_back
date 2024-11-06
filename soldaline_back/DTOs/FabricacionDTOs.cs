namespace soldaline_back.DTOs
{
    // DTO para registrar un producto (crear)
    public class ProductoRegisterDTO
    {
        public string? NombreProducto { get; set; }
        public string? ImagenProducto { get; set; }
        public int? Estatus { get; set; }
    }

    // DTO para respuesta de producto (respuesta con ID)
    public class ProductoResponseDTO
    {
        public int? Id { get; set; }
        public string? NombreProducto { get; set; }
        public string? ImagenProducto { get; set; }
        public int? Estatus { get; set; }
    }

    // DTO para actualizar un producto
    public class ProductoUpdateDTO
    {
        public string? NombreProducto { get; set; }
        public string? ImagenProducto { get; set; }
        public int? Estatus { get; set; }
    }
}
