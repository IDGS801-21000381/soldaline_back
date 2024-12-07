namespace soldaline_back.DTOs
{
	public class VentaDTO
	{
		public DateTime Fecha { get; set; }
		public string Folio { get; set; }
		public int UsuarioId { get; set; }

		//public List<DetalleVentaRequest> Detalleventa { get; set; } = new List<DetalleVentaRequest>();
	}
	public class DetalleVentaRequest
	{
		public int Cantidad { get; set; }
		public float PrecioUnitario { get; set; }
		
	}
}
