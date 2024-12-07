namespace soldaline_back.DTOs
{
    public class RecetaDTO
    {
        public int IdFabricacion { get; set; }
        public string? TipoProteccion { get; set; }
        public List<MaterialCantidadDTO> Materiales { get; set; }  // Lista de materiales con sus cantidades
        public string? Unidad { get; set; }  // Puede ser nulo
        public string? Descripcion { get; set; }  // Puede ser nulo
        public DateTime? FechaCreacion { get; set; }
        public int Estatus { get; set; }
    }

    public class MaterialCantidadDTO
    {
        public string Material { get; set; }
    }
    // DTO para la respuesta de receta (respuesta con ID)
    public class RecetaResponseDTO
    {
        public int IdReceta { get; set; }
        public string ProductoNombre { get; set; }  // Nombre del producto de la tabla Fabricacion
        public string CategoriaNombre { get; set; }  // Categoría del producto de la tabla Fabricacion
        public string Material { get; set; }
        public int? Cantidad { get; set; }
        public string Unidad { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int? Estatus { get; set; }
    }


    // DTO para actualizar una receta
    public class RecetaUpdateDTO
    {
        public int? IdReceta { get; set; }
        public int? IdFabricacion { get; set; }
        public string? TipoProteccion { get; set; }
        public string? Material { get; set; }
        public int? Cantidad { get; set; }
        public string? Unidad { get; set; }
        public string? Descripcion { get; set; }
        public int? Estatus { get; set; }
    }


}
