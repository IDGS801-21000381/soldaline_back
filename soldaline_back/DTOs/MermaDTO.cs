namespace soldaline_back.DTOs
{
    // DTO para registrar una nueva merma
    public class MermaRegisterDTO
    {
        public int? Cantidad { get; set; }
        public string? Descripcion { get; set; }
        public DateOnly? Fecha { get; set; }
        public int UsuarioId { get; set; }
        public int? InventarioProductoId { get; set; }
        public int? InventariomaterialesId { get; set; }
    }

    // DTO para la respuesta de merma
    public class MermaResponseDTO
    {
        public int Id { get; set; }
        public int? Cantidad { get; set; }
        public string? Descripcion { get; set; }
        public DateOnly? Fecha { get; set; }
        public int UsuarioId { get; set; }
        public int? InventarioProductoId { get; set; }
        public int? InventariomaterialesId { get; set; }
        public string? UsuarioNombre { get; set; }
    }

}
