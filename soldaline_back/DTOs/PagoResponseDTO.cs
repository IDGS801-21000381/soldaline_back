namespace soldaline_back.DTOs
{
	public class PagoResponseDTO
	{
		public int Id { get; set; }
		public int CuentaId { get; set; }
		public decimal Monto { get; set; }
		public DateOnly FechaPago { get; set; }
		public int MetodoPago { get; set; }
		public int ClientePotencialId { get; set; }
		public int UsuarioId { get; set; }
	}

}
