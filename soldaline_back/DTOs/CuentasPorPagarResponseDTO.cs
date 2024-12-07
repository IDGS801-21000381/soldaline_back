namespace soldaline_back.DTOs
{
	public class CuentasPorPagarResponseDTO
	{
		public int Id { get; set; }
		public int VentasId { get; set; }
		public int ClienteID { get; set; }
		public int UsuarioId { get; set; }
		public decimal SaldoTotal { get; set; }
		public decimal SaldoPendiente { get; set; }
		public DateOnly Fecha { get; set; }
		public int DiasPlazo { get; set; }
		public int Estatus { get; set; }
	}

}
