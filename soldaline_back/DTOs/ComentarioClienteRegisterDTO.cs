namespace soldaline_back.DTOs
{
	public class ComentarioClienteRegisterDTO
	{
		public int UsuarioId { get; set; }
		public int ClienteId { get; set; }
		public DateOnly Fecha { get; set; }
		public int Tipo { get; set; }
		public string Descripcion { get; set; }
		public int Estatus { get; set; } = 0; // Pendiente por defecto
		public int Calificacion { get; set; }
	}
}
