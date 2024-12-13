namespace soldaline_back.DTOs
{
    // DTO para actualizar datos de usuario (excepto contraseña)
    public class UsuarioUpdateDTO
    {
        public string? Nombre { get; set; }
        public string? Rol { get; set; }
        public string? UrlImage { get; set; }
        public string? Direccion { get; set; }
        public string? Tarjeta { get; set; }
    }

    // DTO para actualizar solo la contraseña del usuario
    public class UsuarioUpdatePasswordDTO
    {
        public string ContraseniaActual { get; set; } = null!;
        public string NuevaContrasenia { get; set; } = null!;
    }

    // DTO para login de usuario
    public class UsuarioLoginDTO
    {
        public string Correo { get; set; } = null!;
        public string Contrasenia { get; set; } = null!;
    }

    // DTO para registro de usuario
    // DTO para registro de usuario
    public class UsuarioRegisterDTO
    {
        public string Nombre { get; set; }
        public string Contrasenia { get; set; }
        public string Rol { get; set; }
        public string UrlImage { get; set; }
        public string Direccion { get; set; }
        public string Tarjeta { get; set; }
        public string EstatusUsuario { get; set; }
        public string Nombres { get; set; }
        public string ApellidoM { get; set; }
        public string ApellidoP { get; set; }
        public string Correo { get; set; }
        public string EstatusDetalle { get; set; }
    }

    // DTO para respuesta de usuario
    public class UsuarioResponseDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Rol { get; set; }
        public int? Estatus { get; set; }
        public string? UrlImage { get; set; }
        public string? Direccion { get; set; }
        public bool? ClientePotencial { get; set; }
    }

    

    public class DetallesUsuarioDTO
    {
        public string? Nombres { get; set; }
        public string? ApellidoP { get; set; }
        public string? ApellidoM { get; set; }
        public string? Correo { get; set; }
    }

}
