namespace soldaline_back.DTOs
{
    // DTO para registrar una nueva compra
    public class CompraRegisterDTO
    {
        public DateOnly? Fecha { get; set; }
        public int UsuarioId { get; set; }
        public List<DetalleCompraRegisterDTO> DetalleCompras { get; set; } = new List<DetalleCompraRegisterDTO>();
    }

    // DTO para registrar un nuevo detalle de compra
    public class DetalleCompraRegisterDTO
    {
        public int? Cantidad { get; set; }
        public string? Folio { get; set; }
        public string? Descripcion { get; set; }
        public double? Costo { get; set; }
    }

    // DTO para la respuesta de la compra
    public class CompraResponseDTO
    {
        public int Id { get; set; }
        public DateOnly? Fecha { get; set; }
        public int UsuarioId { get; set; }
        public List<DetalleCompraResponseDTO> DetalleCompras { get; set; } = new List<DetalleCompraResponseDTO>();
    }

    // DTO para la respuesta de detalle de compra
    public class DetalleCompraResponseDTO
    {
        public int Id { get; set; }
        public int? Cantidad { get; set; }
        public string? Folio { get; set; }
        public string? Descripcion { get; set; }
        public double? Costo { get; set; }
    }

}
