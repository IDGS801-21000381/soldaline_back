namespace soldaline_back.DTOs
{
    public class ClientePotencialDto
    {
        public int ClienteID { get; set; } // ID del cliente (solo para respuestas)

        // Datos básicos
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string RedesSociales { get; set; }
        public string Origen { get; set; } // Ejemplo: página web, contacto directo
        public string PreferenciaComunicacion { get; set; } // Teléfono, correo, etc.

        // Estado y relaciones
        public int? EmpresaID { get; set; } // Referencia opcional
        public int UsuarioID { get; set; } // Usuario que maneja al cliente
        public int Estatus { get; set; } // Estado del cliente

        // Fecha de registro
        public DateTime FechaRegistro { get; set; } // Solo para respuestas
    }

}
