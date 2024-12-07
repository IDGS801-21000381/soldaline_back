namespace soldaline_back.DTOs
{
	public class PagoRegisterDTO
	{
		public int CuentaId { get; set; }
		public decimal Monto { get; set; }
		public DateOnly FechaPago { get; set; }
		public int MetodoPago { get; set; }
		public int ClientePotencialId { get; set; }
		public int UsuarioId { get; set; }
	}

}
