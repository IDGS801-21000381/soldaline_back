namespace soldaline_back.DTOs
{
    public class SolicitudProduccionDTO
    {
        public int UsuarioId { get; set; }
        public int FabricacionId { get; set; }
        public int? Cantidad { get; set; }
        public string Descripcion { get; set; }
        public int SolicitudId { get; set; }
        public int? estatus { get; set; }
    }

}
