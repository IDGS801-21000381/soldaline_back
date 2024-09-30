namespace soldaline_back.DTOs
{
    // DTO para registro de usuario
    public class UsuarioRegisterDTO
    {
        public string? Nombre { get; set; }
        public string? Contrasenia { get; set; }
        public string? Rol { get; set; }
        public int? DetallesUsuarioId { get; set; }
        public string Correo { get; set; } = null!;
    }


    // DTO para login de usuario
    public class UsuarioLoginDTO
    {
        public string Correo { get; set; } = null!;
        public string Contrasenia { get; set; } = null!;
    }

    // DTO para respuesta de usuario
    public class UsuarioResponseDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Rol { get; set; }
        public byte? Estatus { get; set; }
        public int? FrecuenciaCompra { get; set; }
        public bool? ClientePotencial { get; set; }
    }

    // DTO para la relación UsuarioSegmento
    public class UsuarioSegmentoDTO
    {
        public int UsuarioId { get; set; }
        public int SegmentoId { get; set; }
    }

}
