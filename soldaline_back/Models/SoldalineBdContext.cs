using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace soldaline_back.Models;

public partial class SoldalineBdContext : DbContext
{
    public SoldalineBdContext()
    {
    }

    public SoldalineBdContext(DbContextOptions<SoldalineBdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Carrito> Carritos { get; set; }

    public virtual DbSet<Compra> Compras { get; set; }

    public virtual DbSet<DetallePedido> DetallePedidos { get; set; }

    public virtual DbSet<Detallecompra> Detallecompras { get; set; }

    public virtual DbSet<Detalleproduccion> Detalleproduccions { get; set; }

    public virtual DbSet<DetallesUsuario> DetallesUsuarios { get; set; }

    public virtual DbSet<Detalleventum> Detalleventa { get; set; }

    public virtual DbSet<EstimacionProduccion> EstimacionProduccions { get; set; }

    public virtual DbSet<Fabricacion> Fabricacions { get; set; }

    public virtual DbSet<HistorialDescuento> HistorialDescuentos { get; set; }

    public virtual DbSet<InventarioProducto> InventarioProductos { get; set; }

    public virtual DbSet<Inventariomateriale> Inventariomateriales { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<Materialfabricacion> Materialfabricacions { get; set; }

    public virtual DbSet<Merma> Mermas { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Produccion> Produccions { get; set; }

    public virtual DbSet<Productoproveedor> Productoproveedors { get; set; }

    public virtual DbSet<Proveedor> Proveedors { get; set; }

    public virtual DbSet<SegmentoCliente> SegmentoClientes { get; set; }

    public virtual DbSet<Solicitudproduccion> Solicitudproduccions { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioSegmento> UsuarioSegmentos { get; set; }

    public virtual DbSet<Ventum> Venta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-0LPQM8K; Initial Catalog=soldaline_bd; user id=sa; password=root;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Carrito>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__carrito__3213E83FCC40B843");

            entity.ToTable("carrito");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estatus).HasColumnName("estatus");
            entity.Property(e => e.FabricacionId).HasColumnName("fabricacion_id");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Fabricacion).WithMany(p => p.Carritos)
                .HasForeignKey(d => d.FabricacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__carrito__fabrica__71D1E811");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Carritos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__carrito__usuario__72C60C4A");
        });

        modelBuilder.Entity<Compra>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__compra__3213E83FE4F5BF0E");

            entity.ToTable("compra");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Compras)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__compra__usuario___4222D4EF");
        });

        modelBuilder.Entity<DetallePedido>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__detalleP__3213E83F9D60DA98");

            entity.ToTable("detallePedido");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.FabricacionId).HasColumnName("fabricacion_id");
            entity.Property(e => e.PedidoId).HasColumnName("pedido_id");
            entity.Property(e => e.PrecioUnitario).HasColumnName("precioUnitario");

            entity.HasOne(d => d.Fabricacion).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.FabricacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__detallePe__fabri__787EE5A0");

            entity.HasOne(d => d.Pedido).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.PedidoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__detallePe__pedid__797309D9");
        });

        modelBuilder.Entity<Detallecompra>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__detallec__3213E83F6CDA8F35");

            entity.ToTable("detallecompra");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.CompraId).HasColumnName("compra_id");
            entity.Property(e => e.Costo).HasColumnName("costo");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Folio)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("folio");

            entity.HasOne(d => d.Compra).WithMany(p => p.Detallecompras)
                .HasForeignKey(d => d.CompraId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__detalleco__compr__44FF419A");
        });

        modelBuilder.Entity<Detalleproduccion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__detallep__3213E83FC8FB0D11");

            entity.ToTable("detalleproduccion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.InventariomaterialesId).HasColumnName("inventariomateriales_id");
            entity.Property(e => e.ProduccionId).HasColumnName("produccion_id");

            entity.HasOne(d => d.Inventariomateriales).WithMany(p => p.Detalleproduccions)
                .HasForeignKey(d => d.InventariomaterialesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__detallepr__inven__5AEE82B9");

            entity.HasOne(d => d.Produccion).WithMany(p => p.Detalleproduccions)
                .HasForeignKey(d => d.ProduccionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__detallepr__produ__59FA5E80");
        });

        modelBuilder.Entity<DetallesUsuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__detalles__3213E83F93B082AA");

            entity.ToTable("detallesUsuario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApellidoM)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("apellidoM");
            entity.Property(e => e.ApellidoP)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("apellidoP");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Estatus).HasColumnName("estatus");
            entity.Property(e => e.Nombres)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombres");
        });

        modelBuilder.Entity<Detalleventum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__detallev__3213E83F555BC516");

            entity.ToTable("detalleventa");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.ClientePotencialDescuento)
                .HasDefaultValue(0.0)
                .HasColumnName("clientePotencialDescuento");
            entity.Property(e => e.InventarioProductoId).HasColumnName("inventarioProducto_id");
            entity.Property(e => e.PrecioUnitario).HasColumnName("precioUnitario");
            entity.Property(e => e.VentaId).HasColumnName("venta_id");

            entity.HasOne(d => d.InventarioProducto).WithMany(p => p.Detalleventa)
                .HasForeignKey(d => d.InventarioProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__detalleve__inven__66603565");

            entity.HasOne(d => d.Venta).WithMany(p => p.Detalleventa)
                .HasForeignKey(d => d.VentaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__detalleve__venta__656C112C");
        });

        modelBuilder.Entity<EstimacionProduccion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Estimaci__3214EC0758E1CDFE");

            entity.ToTable("EstimacionProduccion");

            entity.HasOne(d => d.Fabricacion).WithMany(p => p.EstimacionProduccions)
                .HasForeignKey(d => d.FabricacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Estimacio__Fabri__29221CFB");
        });

        modelBuilder.Entity<Fabricacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__fabricac__3213E83FB7EA9D0A");

            entity.ToTable("fabricacion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estatus).HasColumnName("estatus");
            entity.Property(e => e.ImagenProducto)
                .HasColumnType("text")
                .HasColumnName("imagen_producto");
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombreProducto");
        });

        modelBuilder.Entity<HistorialDescuento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Historia__3213E83FB85A9DDA");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CompraId).HasColumnName("compra_id");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.MontoDescuento).HasColumnName("monto_descuento");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Compra).WithMany(p => p.HistorialDescuentos)
                .HasForeignKey(d => d.CompraId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Historial__compr__7D439ABD");

            entity.HasOne(d => d.Usuario).WithMany(p => p.HistorialDescuentos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Historial__usuar__7C4F7684");
        });

        modelBuilder.Entity<InventarioProducto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__inventar__3213E83FEBCBBC44");

            entity.ToTable("inventarioProducto");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.FabricacionId).HasColumnName("fabricacion_id");
            entity.Property(e => e.FechaCreacion).HasColumnName("fechaCreacion");
            entity.Property(e => e.Lote)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("lote");
            entity.Property(e => e.NivelMinimoStock).HasColumnName("nivelMinimoStock");
            entity.Property(e => e.Precio).HasColumnName("precio");
            entity.Property(e => e.ProduccionId).HasColumnName("produccion_id");

            entity.HasOne(d => d.Fabricacion).WithMany(p => p.InventarioProductos)
                .HasForeignKey(d => d.FabricacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__inventari__fabri__5DCAEF64");

            entity.HasOne(d => d.Produccion).WithMany(p => p.InventarioProductos)
                .HasForeignKey(d => d.ProduccionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__inventari__produ__5EBF139D");
        });

        modelBuilder.Entity<Inventariomateriale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__inventar__3213E83FF931B094");

            entity.ToTable("inventariomateriales");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.DetallecompraId).HasColumnName("detallecompra_id");
            entity.Property(e => e.MaterialId).HasColumnName("material_id");
            entity.Property(e => e.ProveedorId).HasColumnName("proveedor_id");

            entity.HasOne(d => d.Detallecompra).WithMany(p => p.Inventariomateriales)
                .HasForeignKey(d => d.DetallecompraId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__inventari__detal__49C3F6B7");

            entity.HasOne(d => d.Material).WithMany(p => p.Inventariomateriales)
                .HasForeignKey(d => d.MaterialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__inventari__mater__48CFD27E");

            entity.HasOne(d => d.Proveedor).WithMany(p => p.Inventariomateriales)
                .HasForeignKey(d => d.ProveedorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__inventari__prove__47DBAE45");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__material__3213E83F654B9754");

            entity.ToTable("material");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Materialfabricacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__material__3213E83FC39E672C");

            entity.ToTable("materialfabricacion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Estatus).HasColumnName("estatus");
            entity.Property(e => e.FabricacionId).HasColumnName("fabricacion_id");
            entity.Property(e => e.MaterialId).HasColumnName("material_id");

            entity.HasOne(d => d.Fabricacion).WithMany(p => p.Materialfabricacions)
                .HasForeignKey(d => d.FabricacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__materialf__fabri__4E88ABD4");

            entity.HasOne(d => d.Material).WithMany(p => p.Materialfabricacions)
                .HasForeignKey(d => d.MaterialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__materialf__mater__4F7CD00D");
        });

        modelBuilder.Entity<Merma>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__merma__3213E83F93ABF088");

            entity.ToTable("merma");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(700)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.InventarioProductoId).HasColumnName("inventarioProducto_id");
            entity.Property(e => e.InventariomaterialesId).HasColumnName("inventariomateriales_id");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.InventarioProducto).WithMany(p => p.Mermas)
                .HasForeignKey(d => d.InventarioProductoId)
                .HasConstraintName("FK__merma__inventari__6A30C649");

            entity.HasOne(d => d.Inventariomateriales).WithMany(p => p.Mermas)
                .HasForeignKey(d => d.InventariomaterialesId)
                .HasConstraintName("FK__merma__inventari__6B24EA82");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Mermas)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__merma__usuario_i__693CA210");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__pedido__3213E83F9F47B009");

            entity.ToTable("pedido");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estatus).HasColumnName("estatus");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.TotalPedido).HasColumnName("totalPedido");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__pedido__usuario___75A278F5");
        });

        modelBuilder.Entity<Produccion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__producci__3213E83F1FDB352E");

            entity.ToTable("produccion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Costo).HasColumnName("costo");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.SolicitudproduccionId).HasColumnName("solicitudproduccion_id");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Solicitudproduccion).WithMany(p => p.Produccions)
                .HasForeignKey(d => d.SolicitudproduccionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__produccio__solic__571DF1D5");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Produccions)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__produccio__usuar__5629CD9C");
        });

        modelBuilder.Entity<Productoproveedor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__producto__3213E83F2C5D1071");

            entity.ToTable("productoproveedor");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MaterialId).HasColumnName("material_id");
            entity.Property(e => e.ProveedorId).HasColumnName("proveedor_id");

            entity.HasOne(d => d.Material).WithMany(p => p.Productoproveedors)
                .HasForeignKey(d => d.MaterialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__productop__mater__6EF57B66");

            entity.HasOne(d => d.Proveedor).WithMany(p => p.Productoproveedors)
                .HasForeignKey(d => d.ProveedorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__productop__prove__6E01572D");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__proveedo__3213E83F70183119");

            entity.ToTable("proveedor");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApellidoM)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("apellidoM");
            entity.Property(e => e.ApellidoP)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("apellidoP");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Estatus)
                .HasDefaultValue((byte)1)
                .HasColumnName("estatus");
            entity.Property(e => e.NombreContacto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombreContacto");
            entity.Property(e => e.NombreEmpresa)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombreEmpresa");
            entity.Property(e => e.TelefonoContacto)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telefonoContacto");
        });

        modelBuilder.Entity<SegmentoCliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Segmento__3213E83F263B37C8");

            entity.ToTable("SegmentoCliente");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.NombreSegmento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombreSegmento");
        });

        modelBuilder.Entity<Solicitudproduccion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__solicitu__3213E83FC012B52C");

            entity.ToTable("solicitudproduccion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estatus).HasColumnName("estatus");
            entity.Property(e => e.FabricacionId).HasColumnName("fabricacion_id");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Fabricacion).WithMany(p => p.Solicitudproduccions)
                .HasForeignKey(d => d.FabricacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__solicitud__fabri__52593CB8");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Solicitudproduccions)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__solicitud__usuar__534D60F1");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__usuario__3213E83F183E7C89");

            entity.ToTable("usuario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClientePotencial)
                .HasDefaultValue(false)
                .HasColumnName("cliente_potencial");
            entity.Property(e => e.Contrasenia)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("contrasenia");
            entity.Property(e => e.DetallesUsuarioId).HasColumnName("detallesUsuario_id");
            entity.Property(e => e.Direccion)
                .HasColumnType("text")
                .HasColumnName("direccion");
            entity.Property(e => e.Estatus).HasColumnName("estatus");
            entity.Property(e => e.FrecuenciaCompra).HasColumnName("frecuencia_compra");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Rol)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("rol");
            entity.Property(e => e.Tarjeta)
                .HasMaxLength(21)
                .IsUnicode(false)
                .HasColumnName("tarjeta");
            entity.Property(e => e.Token)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("token");
            entity.Property(e => e.UrlImage)
                .HasColumnType("text")
                .HasColumnName("urlImage");

            entity.HasOne(d => d.DetallesUsuario).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.DetallesUsuarioId)
                .HasConstraintName("FK__usuario__detalle__3A81B327");
        });

        modelBuilder.Entity<UsuarioSegmento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UsuarioS__3213E83F3202836F");

            entity.ToTable("UsuarioSegmento");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.SegmentoId).HasColumnName("segmento_id");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Segmento).WithMany(p => p.UsuarioSegmentos)
                .HasForeignKey(d => d.SegmentoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsuarioSe__segme__02FC7413");

            entity.HasOne(d => d.Usuario).WithMany(p => p.UsuarioSegmentos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsuarioSe__usuar__02084FDA");
        });

        modelBuilder.Entity<Ventum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__venta__3213E83F42D71204");

            entity.ToTable("venta");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.Folio)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("folio");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Venta)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__venta__usuario_i__619B8048");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
