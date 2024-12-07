namespace soldaline_back.DTOs
{
    // DTO para registrar un cliente y empresa
    public class EmpresaClienteRegisterDTO
    {
        public string NombreCliente { get; set; } = null!;
        public string DireccionCliente { get; set; } = null!;
        public string TelefonoCliente { get; set; } = null!;
        public string CorreoCliente { get; set; } = null!;
        public string? RedesSociales { get; set; }
        public string Origen { get; set; } = null!;
        public string PreferenciaComunicacion { get; set; } = null!;
        public int UsuarioId { get; set; }

        // Datos de la empresa asociada al cliente
        public string NombreEmpresa { get; set; } = null!;
        public string DireccionEmpresa { get; set; } = null!;
        public string TelefonoEmpresa { get; set; } = null!;
        public string CorreoEmpresa { get; set; } = null!;
        public string? SitioWeb { get; set; }
    }

    // DTO para actualizar cliente y empresa
    public class EmpresaClienteUpdateDTO
    {
        public int ClienteId { get; set; } // Necesitamos el ID del cliente para actualizarlo
        public int? EmpresaId { get; set; } // ID de la empresa asociada (opcional)

        // Datos del cliente
        public string? NombreCliente { get; set; }
        public string? DireccionCliente { get; set; }
        public string? TelefonoCliente { get; set; }
        public string? CorreoCliente { get; set; }
        public string? RedesSociales { get; set; }

        // Datos de la empresa
        public string? NombreEmpresa { get; set; }
        public string? DireccionEmpresa { get; set; }
        public string? TelefonoEmpresa { get; set; }
        public string? CorreoEmpresa { get; set; }
        public string? SitioWeb { get; set; }
    }

    // DTO para cambiar el estatus de cliente y empresa
    public class EstatusUpdateDTO
    {
        // Estatus del cliente: 1 (Activo), 2 (Inactivo), 3 (Pendiente), 4 (Cancelado)
        public int EstatusCliente { get; set; }
    }
}
