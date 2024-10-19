namespace soldaline_back.DTOs
{
    // DTO para registrar un proveedor
    public class ProveedorRegisterDTO
    {
        public string? NombreEmpresa { get; set; }
        public string? Direccion { get; set; }
        public string? TelefonoContacto { get; set; }
        public string? NombreContacto { get; set; }
        public string? ApellidoM { get; set; }
        public string? ApellidoP { get; set; }
    }

    // DTO para respuesta de proveedor
    public class ProveedorResponseDTO
    {
        public int Id { get; set; }
        public string? NombreEmpresa { get; set; }
        public string? Direccion { get; set; }
        public string? TelefonoContacto { get; set; }
        public string? NombreContacto { get; set; }
        public string? ApellidoM { get; set; }
        public string? ApellidoP { get; set; }
        public int? Estatus { get; set; }
    }

    // (Opcional) DTO para actualizar proveedor
    public class ProveedorUpdateDTO
    {
        public string? NombreEmpresa { get; set; }
        public string? Direccion { get; set; }
        public string? TelefonoContacto { get; set; }
        public string? NombreContacto { get; set; }
        public string? ApellidoM { get; set; }
        public string? ApellidoP { get; set; }
        public byte? Estatus { get; set; }
    }

}
