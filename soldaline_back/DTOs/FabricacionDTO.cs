namespace soldaline_back.DTOs
{
    public class ProductoDTO
    {
        public string NombreProducto { get; set; }
        public string ImagenProducto { get; set; }
        public int PrecioProducto { get; set; }
        public int? Estatus { get; set; }
        public string Categoria { get; set; }
        public List<MaterialDTO> Materiales { get; set; }
    }

    public class MaterialDTO
    {
        public int MaterialId { get; set; }
        public int Cantidad { get; set; }
    }
}
