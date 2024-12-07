namespace soldaline_back.DTOs
{
    public class ProductoInventarioDto
    {
        public int ProductoInventarioId { get; set; }
        public int CantidadDisponible { get; set; }
        public decimal PrecioUnitario { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string NumeroLote { get; set; }
        public int IdFabricacion { get; set; }
        public DetalleFabricacion DetalleFabricacion { get; set; }
        public int IdProduccion { get; set; }
        public DetalleProduccion DetalleProduccion { get; set; }
        public int StockMinimo { get; set; }
    }

    public class DetalleFabricacion
    {
        public int FabricacionId { get; set; }
        public string DescripcionFabricacion { get; set; }
        // Otros campos relacionados con Fabricación
    }

    public class DetalleProduccion
    {
        public int ProduccionId { get; set; }
        public string DescripcionProduccion { get; set; }
        // Otros campos relacionados con Producción
    }
}
