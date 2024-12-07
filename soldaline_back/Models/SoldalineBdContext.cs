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

    public virtual DbSet<ClientePotencial> ClientePotencials { get; set; }

    public virtual DbSet<ComentariosCliente> ComentariosClientes { get; set; }

    public virtual DbSet<Compra> Compras { get; set; }

    public virtual DbSet<Cotizacion> Cotizacions { get; set; }

    public virtual DbSet<CuentasPorPagar> CuentasPorPagars { get; set; }

    public virtual DbSet<DetallePedido> DetallePedidos { get; set; }

    public virtual DbSet<Detallecompra> Detallecompras { get; set; }

    public virtual DbSet<Detalleproduccion> Detalleproduccions { get; set; }

    public virtual DbSet<DetallesUsuario> DetallesUsuarios { get; set; }

    public virtual DbSet<Detalleventum> Detalleventa { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<EstimacionProduccion> EstimacionProduccions { get; set; }

    public virtual DbSet<Fabricacion> Fabricacions { get; set; }

    public virtual DbSet<HistorialComunicacion> HistorialComunicacions { get; set; }

    public virtual DbSet<HistorialDescuento> HistorialDescuentos { get; set; }

    public virtual DbSet<InventarioProducto> InventarioProductos { get; set; }

    public virtual DbSet<Inventariomateriale> Inventariomateriales { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<Materialfabricacion> Materialfabricacions { get; set; }

    public virtual DbSet<Merma> Mermas { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Produccion> Produccions { get; set; }

    public virtual DbSet<Productoproveedor> Productoproveedors { get; set; }

    public virtual DbSet<Proveedor> Proveedors { get; set; }

    public virtual DbSet<Proyecto> Proyectos { get; set; }

    public virtual DbSet<SegmentoCliente> SegmentoClientes { get; set; }

    public virtual DbSet<Solicitudproduccion> Solicitudproduccions { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioSegmento> UsuarioSegmentos { get; set; }

    public virtual DbSet<Ventum> Venta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:bazar.database.windows.net,1433;Initial Catalog=soldaline_bd;Persist Security Info=False;User ID=juanmorua;Password=moruajuan123+;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Carrito>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__carrito__3213E83FDF461AFD");

            entity.ToTable("carrito");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estatus).HasColumnName("estatus");
            entity.Property(e => e.FabricacionId).HasColumnName("fabricacion_id");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Fabricacion).WithMany(p => p.Carritos)
                .HasForeignKey(d => d.FabricacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__carrito__fabrica__1D7B6025");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Carritos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__carrito__usuario__1E6F845E");
        });

        modelBuilder.Entity<ClientePotencial>(entity =>
        {
            entity.HasKey(e => e.ClienteId).HasName("PK__ClienteP__71ABD0A7BD9535DD");

            entity.ToTable("ClientePotencial");

            entity.Property(e => e.ClienteId).HasColumnName("ClienteID");
            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.Direccion).HasMaxLength(255);
            entity.Property(e => e.EmpresaId).HasColumnName("EmpresaID");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Origen).HasMaxLength(50);
            entity.Property(e => e.PreferenciaComunicacion).HasMaxLength(50);
            entity.Property(e => e.RedesSociales).HasMaxLength(255);
            entity.Property(e => e.Telefono).HasMaxLength(20);
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Empresa).WithMany(p => p.ClientePotencials)
                .HasForeignKey(d => d.EmpresaId)
                .HasConstraintName("FK_Cliente_Empresa");

            entity.HasOne(d => d.Usuario).WithMany(p => p.ClientePotencials)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cliente_Usuario");
        });

        modelBuilder.Entity<ComentariosCliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__comentar__3213E83FEF5A4FB7");

            entity.ToTable("comentarios_cliente");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Calificacion).HasColumnName("calificacion");
            entity.Property(e => e.ClienteId).HasColumnName("ClienteID");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Estatus).HasColumnName("estatus");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.Tipo).HasColumnName("tipo");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Cliente).WithMany(p => p.ComentariosClientes)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_pago_clientePotencial1");

            entity.HasOne(d => d.Usuario).WithMany(p => p.ComentariosClientes)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_comentario_usuario");
        });

        modelBuilder.Entity<Compra>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__compra__3213E83F3F89754D");

            entity.ToTable("compra");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Compras)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__compra__usuario___6DCC4D03");
        });

        modelBuilder.Entity<Cotizacion>(entity =>
        {
            entity.HasKey(e => e.CotizacionId).HasName("PK__Cotizaci__30443A592687F993");

            entity.ToTable("Cotizacion");

            entity.Property(e => e.CotizacionId).HasColumnName("CotizacionID");
            entity.Property(e => e.EmpresaId).HasColumnName("EmpresaID");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Subtotal).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Vendedor).HasMaxLength(100);

            entity.HasOne(d => d.Empresa).WithMany(p => p.Cotizacions)
                .HasForeignKey(d => d.EmpresaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cotizacio__Empre__53D770D6");
        });

        modelBuilder.Entity<CuentasPorPagar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__cuentas___3213E83F068244EE");

            entity.ToTable("cuentas_por_pagar");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClienteId).HasColumnName("ClienteID");
            entity.Property(e => e.DiasPlazo).HasColumnName("dias_plazo");
            entity.Property(e => e.Estatus).HasColumnName("estatus");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.SaldoPendiente)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("saldo_pendiente");
            entity.Property(e => e.SaldoTotal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("saldo_total");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");
            entity.Property(e => e.VentasId).HasColumnName("ventas_id");

            entity.HasOne(d => d.Cliente).WithMany(p => p.CuentasPorPagars)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cuenta_clientePotencial");

            entity.HasOne(d => d.Usuario).WithMany(p => p.CuentasPorPagars)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_pago_usuario");

            entity.HasOne(d => d.Ventas).WithMany(p => p.CuentasPorPagars)
                .HasForeignKey(d => d.VentasId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cuenta_venta");
        });

        modelBuilder.Entity<DetallePedido>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__detalleP__3213E83FD3ADD66B");

            entity.ToTable("detallePedido");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.FabricacionId).HasColumnName("fabricacion_id");
            entity.Property(e => e.PedidoId).HasColumnName("pedido_id");
            entity.Property(e => e.PrecioUnitario).HasColumnName("precioUnitario");

            entity.HasOne(d => d.Fabricacion).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.FabricacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__detallePe__fabri__24285DB4");

            entity.HasOne(d => d.Pedido).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.PedidoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__detallePe__pedid__251C81ED");
        });

        modelBuilder.Entity<Detallecompra>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__detallec__3213E83F384E03A6");

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
                .HasConstraintName("FK__detalleco__compr__70A8B9AE");
        });

        modelBuilder.Entity<Detalleproduccion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__detallep__3213E83F2B6FDA2F");

            entity.ToTable("detalleproduccion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.InventariomaterialesId).HasColumnName("inventariomateriales_id");
            entity.Property(e => e.ProduccionId).HasColumnName("produccion_id");

            entity.HasOne(d => d.Inventariomateriales).WithMany(p => p.Detalleproduccions)
                .HasForeignKey(d => d.InventariomaterialesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__detallepr__inven__0697FACD");

            entity.HasOne(d => d.Produccion).WithMany(p => p.Detalleproduccions)
                .HasForeignKey(d => d.ProduccionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__detallepr__produ__05A3D694");
        });

        modelBuilder.Entity<DetallesUsuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__detalles__3213E83FCED95C3C");

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
            entity.HasKey(e => e.Id).HasName("PK__detallev__3213E83F63F7125F");

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
                .HasConstraintName("FK__detalleve__inven__1209AD79");

            entity.HasOne(d => d.Venta).WithMany(p => p.Detalleventa)
                .HasForeignKey(d => d.VentaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__detalleve__venta__11158940");
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.EmpresaId).HasName("PK__Empresa__7B9F213648C3051E");

            entity.ToTable("Empresa");

            entity.Property(e => e.EmpresaId).HasColumnName("EmpresaID");
            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.Direccion).HasMaxLength(255);
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.SitioWeb).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(20);
        });

        modelBuilder.Entity<EstimacionProduccion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Estimaci__3214EC07B69B3DC3");

            entity.ToTable("EstimacionProduccion");

            entity.HasOne(d => d.Fabricacion).WithMany(p => p.EstimacionProduccions)
                .HasForeignKey(d => d.FabricacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Estimacio__Fabri__318258D2");
        });

        modelBuilder.Entity<Fabricacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__fabricac__3213E83F7B0C033F");

            entity.ToTable("fabricacion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Categoria)
                .HasMaxLength(255)
                .HasColumnName("categoria");
            entity.Property(e => e.Estatus).HasColumnName("estatus");
            entity.Property(e => e.ImagenProducto)
                .HasColumnType("text")
                .HasColumnName("imagen_producto");
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombreProducto");
            entity.Property(e => e.PrecioProducto).HasColumnName("precioProducto");
        });

        modelBuilder.Entity<HistorialComunicacion>(entity =>
        {
            entity.HasKey(e => e.HistorialId).HasName("PK__Historia__975206EFD4E79D56");

            entity.ToTable("HistorialComunicacion");

            entity.Property(e => e.HistorialId).HasColumnName("HistorialID");
            entity.Property(e => e.ClienteId).HasColumnName("ClienteID");
            entity.Property(e => e.DetallesComunicado).HasColumnType("text");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Cliente).WithMany(p => p.HistorialComunicacions)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Historial_Cliente");

            entity.HasOne(d => d.Usuario).WithMany(p => p.HistorialComunicacions)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Historial_Usuario");
        });

        modelBuilder.Entity<HistorialDescuento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Historia__3213E83FE6F4DDA0");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CompraId).HasColumnName("compra_id");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.MontoDescuento).HasColumnName("monto_descuento");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Compra).WithMany(p => p.HistorialDescuentos)
                .HasForeignKey(d => d.CompraId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Historial__compr__28ED12D1");

            entity.HasOne(d => d.Usuario).WithMany(p => p.HistorialDescuentos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Historial__usuar__27F8EE98");
        });

        modelBuilder.Entity<InventarioProducto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__inventar__3213E83FB1CC5D52");

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
                .HasConstraintName("FK__inventari__fabri__09746778");

            entity.HasOne(d => d.Produccion).WithMany(p => p.InventarioProductos)
                .HasForeignKey(d => d.ProduccionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__inventari__produ__0A688BB1");
        });

        modelBuilder.Entity<Inventariomateriale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__inventar__3213E83FDC911303");

            entity.ToTable("inventariomateriales");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.DetallecompraId).HasColumnName("detallecompra_id");
            entity.Property(e => e.MaterialId).HasColumnName("material_id");
            entity.Property(e => e.ProveedorId).HasColumnName("proveedor_id");

            entity.HasOne(d => d.Detallecompra).WithMany(p => p.Inventariomateriales)
                .HasForeignKey(d => d.DetallecompraId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__inventari__detal__756D6ECB");

            entity.HasOne(d => d.Material).WithMany(p => p.Inventariomateriales)
                .HasForeignKey(d => d.MaterialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__inventari__mater__74794A92");

            entity.HasOne(d => d.Proveedor).WithMany(p => p.Inventariomateriales)
                .HasForeignKey(d => d.ProveedorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__inventari__prove__73852659");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__material__3213E83FFBB28378");

            entity.ToTable("material");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Materialfabricacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__material__3213E83FFCE73F01");

            entity.ToTable("materialfabricacion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Estatus).HasColumnName("estatus");
            entity.Property(e => e.FabricacionId).HasColumnName("fabricacion_id");
            entity.Property(e => e.MaterialId).HasColumnName("material_id");

            entity.HasOne(d => d.Fabricacion).WithMany(p => p.Materialfabricacions)
                .HasForeignKey(d => d.FabricacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__materialf__fabri__7A3223E8");

            entity.HasOne(d => d.Material).WithMany(p => p.Materialfabricacions)
                .HasForeignKey(d => d.MaterialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__materialf__mater__7B264821");
        });

        modelBuilder.Entity<Merma>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__merma__3213E83F487DADC2");

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
                .HasConstraintName("FK__merma__inventari__15DA3E5D");

            entity.HasOne(d => d.Inventariomateriales).WithMany(p => p.Mermas)
                .HasForeignKey(d => d.InventariomaterialesId)
                .HasConstraintName("FK__merma__inventari__16CE6296");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Mermas)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__merma__usuario_i__14E61A24");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__pagos__3213E83F8E8A1D6E");

            entity.ToTable("pagos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClientePotencialId).HasColumnName("clientePotencial_id");
            entity.Property(e => e.CuentaId).HasColumnName("cuenta_id");
            entity.Property(e => e.FechaPago).HasColumnName("fecha_pago");
            entity.Property(e => e.MetodoPago).HasColumnName("metodo_pago");
            entity.Property(e => e.Monto)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("monto");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.ClientePotencial).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.ClientePotencialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_pago_clientePotencial");

            entity.HasOne(d => d.Cuenta).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.CuentaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_pago_cuenta");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_pago_usuario1");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__pedido__3213E83F2E4C3973");

            entity.ToTable("pedido");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estatus).HasColumnName("estatus");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.TotalPedido).HasColumnName("totalPedido");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__pedido__usuario___214BF109");
        });

        modelBuilder.Entity<Produccion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__producci__3213E83FE0750C98");

            entity.ToTable("produccion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Costo).HasColumnName("costo");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.SolicitudproduccionId).HasColumnName("solicitudproduccion_id");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Solicitudproduccion).WithMany(p => p.Produccions)
                .HasForeignKey(d => d.SolicitudproduccionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__produccio__solic__02C769E9");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Produccions)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__produccio__usuar__01D345B0");
        });

        modelBuilder.Entity<Productoproveedor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__producto__3213E83F03A28FF3");

            entity.ToTable("productoproveedor");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MaterialId).HasColumnName("material_id");
            entity.Property(e => e.ProveedorId).HasColumnName("proveedor_id");

            entity.HasOne(d => d.Material).WithMany(p => p.Productoproveedors)
                .HasForeignKey(d => d.MaterialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__productop__mater__1A9EF37A");

            entity.HasOne(d => d.Proveedor).WithMany(p => p.Productoproveedors)
                .HasForeignKey(d => d.ProveedorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__productop__prove__19AACF41");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__proveedo__3213E83FC9B52BA9");

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

        modelBuilder.Entity<Proyecto>(entity =>
        {
            entity.HasKey(e => e.ProyectoId).HasName("PK__Proyecto__CF241D45E0CF3928");

            entity.ToTable("Proyecto");

            entity.Property(e => e.ProyectoId).HasColumnName("ProyectoID");
            entity.Property(e => e.ClienteId).HasColumnName("ClienteID");
            entity.Property(e => e.EmpresaId).HasColumnName("EmpresaID");
            entity.Property(e => e.Monto).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.NombreProyecto).HasMaxLength(100);

            entity.HasOne(d => d.Cliente).WithMany(p => p.Proyectos)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Proyecto_Cliente");

            entity.HasOne(d => d.Empresa).WithMany(p => p.Proyectos)
                .HasForeignKey(d => d.EmpresaId)
                .HasConstraintName("FK_Proyecto_Empresa");
        });

        modelBuilder.Entity<SegmentoCliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Segmento__3213E83FB36CF6EB");

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
            entity.HasKey(e => e.Id).HasName("PK__solicitu__3213E83F4C98702C");

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
                .HasConstraintName("FK__solicitud__fabri__7E02B4CC");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Solicitudproduccions)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__solicitud__usuar__7EF6D905");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__usuario__3213E83F372527FB");

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
                .HasConstraintName("FK__usuario__detalle__662B2B3B");
        });

        modelBuilder.Entity<UsuarioSegmento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UsuarioS__3213E83F00D18F5C");

            entity.ToTable("UsuarioSegmento");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.SegmentoId).HasColumnName("segmento_id");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Segmento).WithMany(p => p.UsuarioSegmentos)
                .HasForeignKey(d => d.SegmentoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsuarioSe__segme__2EA5EC27");

            entity.HasOne(d => d.Usuario).WithMany(p => p.UsuarioSegmentos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsuarioSe__usuar__2DB1C7EE");
        });

        modelBuilder.Entity<Ventum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__venta__3213E83F442E7ED6");

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
                .HasConstraintName("FK__venta__usuario_i__0D44F85C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
