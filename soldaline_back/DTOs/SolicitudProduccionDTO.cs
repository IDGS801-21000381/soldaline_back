namespace soldaline_back.DTOs
{
    public class SolicitudMultipleProduccionDTO
    {
        public int UsuarioId { get; set; }
        public List<SolicitudProduccionProductoDTO> Productos { get; set; }
    }

    public class SolicitudProduccionProductoDTO
    {
        public int FabricacionId { get; set; }
        public int Cantidad { get; set; }
        public string? Descripcion { get; set; }
    }

    public class AceptarProduccionDTO
    {
        public List<int> SolicitudIds { get; set; }
    }

    public class FinalizarProduccionDTO
    {
        public List<int> SolicitudIds { get; set; }
        public string? Lote { get; set; }
    }
}
