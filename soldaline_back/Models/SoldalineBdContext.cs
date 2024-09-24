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

    public virtual DbSet<Fabricacion> Fabricacions { get; set; }

    public virtual DbSet<InventarioProducto> InventarioProductos { get; set; }

    public virtual DbSet<Inventariomateriale> Inventariomateriales { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<Materialfabricacion> Materialfabricacions { get; set; }

    public virtual DbSet<Merma> Mermas { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Produccion> Produccions { get; set; }

    public virtual DbSet<Productoproveedor> Productoproveedors { get; set; }

    public virtual DbSet<Proveedor> Proveedors { get; set; }

    public virtual DbSet<Solicitudproduccion> Solicitudproduccions { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Ventum> Venta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-Q3QN8OR; Initial Catalog=soldaline_bd; Integrated Security=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Carrito>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__carrito__3213E83FF189AAE2");

            entity.ToTable("carrito");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estatus).HasColumnName("estatus");
            entity.Property(e => e.FabricacionId).HasColumnName("fabricacion_id");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Fabricacion).WithMany(p => p.Carritos)
                .HasForeignKey(d => d.FabricacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__carrito__fabrica__6FE99F9F");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Carritos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__carrito__usuario__70DDC3D8");
        });

        modelBuilder.Entity<Compra>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__compra__3213E83F64905E4D");

            entity.ToTable("compra");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Compras)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__compra__usuario___3C69FB99");
        });

        modelBuilder.Entity<DetallePedido>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__detalleP__3213E83F139F55A3");

            entity.ToTable("detallePedido");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FabricacionId).HasColumnName("fabricacion_id");
            entity.Property(e => e.PedidoId).HasColumnName("pedido_id");

            entity.HasOne(d => d.Fabricacion).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.FabricacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__detallePe__fabri__76969D2E");

            entity.HasOne(d => d.Pedido).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.PedidoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__detallePe__pedid__778AC167");
        });

        modelBuilder.Entity<Detallecompra>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__detallec__3213E83FBFE3B9E4");

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
                .HasConstraintName("FK__detalleco__compr__3F466844");
        });

        modelBuilder.Entity<Detalleproduccion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__detallep__3213E83F570DE85A");

            entity.ToTable("detalleproduccion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.InventariomaterialesId).HasColumnName("inventariomateriales_id");
            entity.Property(e => e.ProduccionId).HasColumnName("produccion_id");

            entity.HasOne(d => d.Inventariomateriales).WithMany(p => p.Detalleproduccions)
                .HasForeignKey(d => d.InventariomaterialesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__detallepr__inven__5629CD9C");

            entity.HasOne(d => d.Produccion).WithMany(p => p.Detalleproduccions)
                .HasForeignKey(d => d.ProduccionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__detallepr__produ__5535A963");
        });

        modelBuilder.Entity<DetallesUsuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__detalles__3213E83FFDEB43E3");

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
            entity.Property(e => e.Nombres)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombres");
        });

        modelBuilder.Entity<Detalleventum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__detallev__3213E83F89F832C3");

            entity.ToTable("detalleventa");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.InventarioProductoId).HasColumnName("inventarioProducto_id");
            entity.Property(e => e.PrecioUnitario).HasColumnName("precioUnitario");
            entity.Property(e => e.VentaId).HasColumnName("venta_id");

            entity.HasOne(d => d.InventarioProducto).WithMany(p => p.Detalleventa)
                .HasForeignKey(d => d.InventarioProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__detalleve__inven__60A75C0F");

            entity.HasOne(d => d.Venta).WithMany(p => p.Detalleventa)
                .HasForeignKey(d => d.VentaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__detalleve__venta__5FB337D6");
        });

        modelBuilder.Entity<Fabricacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__fabricac__3213E83FF112FCB5");

            entity.ToTable("fabricacion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estatus).HasColumnName("estatus");
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombreProducto");
        });

        modelBuilder.Entity<InventarioProducto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__inventar__3213E83F7EFFD7EF");

            entity.ToTable("inventarioProducto");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.FabricacionId).HasColumnName("fabricacion_id");
            entity.Property(e => e.FechaCreacion).HasColumnName("fechaCreacion");
            entity.Property(e => e.Lote)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("lote");
            entity.Property(e => e.Precio).HasColumnName("precio");
            entity.Property(e => e.ProduccionId).HasColumnName("produccion_id");

            entity.HasOne(d => d.Fabricacion).WithMany(p => p.InventarioProductos)
                .HasForeignKey(d => d.FabricacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__inventari__fabri__5BE2A6F2");

            entity.HasOne(d => d.Produccion).WithMany(p => p.InventarioProductos)
                .HasForeignKey(d => d.ProduccionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__inventari__produ__5CD6CB2B");
        });

        modelBuilder.Entity<Inventariomateriale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__inventar__3213E83F9E31733A");

            entity.ToTable("inventariomateriales");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.DetallecompraId).HasColumnName("detallecompra_id");
            entity.Property(e => e.MaterialId).HasColumnName("material_id");
            entity.Property(e => e.ProveedorId).HasColumnName("proveedor_id");

            entity.HasOne(d => d.Detallecompra).WithMany(p => p.Inventariomateriales)
                .HasForeignKey(d => d.DetallecompraId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__inventari__detal__52593CB8");

            entity.HasOne(d => d.Material).WithMany(p => p.Inventariomateriales)
                .HasForeignKey(d => d.MaterialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__inventari__mater__5165187F");

            entity.HasOne(d => d.Proveedor).WithMany(p => p.Inventariomateriales)
                .HasForeignKey(d => d.ProveedorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__inventari__prove__5070F446");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__material__3213E83F163A58F7");

            entity.ToTable("material");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Materialfabricacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__material__3213E83F237B99C2");

            entity.ToTable("materialfabricacion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Estatus).HasColumnName("estatus");
            entity.Property(e => e.FabricacionId).HasColumnName("fabricacion_id");
            entity.Property(e => e.MaterialId).HasColumnName("material_id");

            entity.HasOne(d => d.Fabricacion).WithMany(p => p.Materialfabricacions)
                .HasForeignKey(d => d.FabricacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__materialf__fabri__6383C8BA");

            entity.HasOne(d => d.Material).WithMany(p => p.Materialfabricacions)
                .HasForeignKey(d => d.MaterialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__materialf__mater__6477ECF3");
        });

        modelBuilder.Entity<Merma>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__merma__3213E83F03A769AC");

            entity.ToTable("merma");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(700)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.InventarioProductoId).HasColumnName("inventarioProducto_id");
            entity.Property(e => e.InventariomaterialesId).HasColumnName("inventariomateriales_id");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.InventarioProducto).WithMany(p => p.Mermas)
                .HasForeignKey(d => d.InventarioProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__merma__inventari__68487DD7");

            entity.HasOne(d => d.Inventariomateriales).WithMany(p => p.Mermas)
                .HasForeignKey(d => d.InventariomaterialesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__merma__inventari__693CA210");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Mermas)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__merma__usuario_i__6754599E");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__pedido__3213E83FE5449C1D");

            entity.ToTable("pedido");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estatus).HasColumnName("estatus");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__pedido__usuario___73BA3083");
        });

        modelBuilder.Entity<Produccion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__producci__3213E83F375B6009");

            entity.ToTable("produccion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Costo).HasColumnName("costo");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.SolicitudproduccionId).HasColumnName("solicitudproduccion_id");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Solicitudproduccion).WithMany(p => p.Produccions)
                .HasForeignKey(d => d.SolicitudproduccionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__produccio__solic__48CFD27E");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Produccions)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__produccio__usuar__47DBAE45");
        });

        modelBuilder.Entity<Productoproveedor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__producto__3213E83FB4A996C4");

            entity.ToTable("productoproveedor");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MaterialId).HasColumnName("material_id");
            entity.Property(e => e.ProveedorId).HasColumnName("proveedor_id");

            entity.HasOne(d => d.Material).WithMany(p => p.Productoproveedors)
                .HasForeignKey(d => d.MaterialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__productop__mater__6D0D32F4");

            entity.HasOne(d => d.Proveedor).WithMany(p => p.Productoproveedors)
                .HasForeignKey(d => d.ProveedorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__productop__prove__6C190EBB");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__proveedo__3213E83FC6FC60F7");

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

        modelBuilder.Entity<Solicitudproduccion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__solicitu__3213E83FD719F764");

            entity.ToTable("solicitudproduccion");

            entity.Property(e => e.Id).HasColumnName("id");
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
                .HasConstraintName("FK__solicitud__fabri__440B1D61");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Solicitudproduccions)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__solicitud__usuar__44FF419A");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__usuario__3213E83F2C609193");

            entity.ToTable("usuario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Contraseña)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("contraseña");
            entity.Property(e => e.DetallesUsuarioId).HasColumnName("detallesUsuario_id");
            entity.Property(e => e.Estatus).HasColumnName("estatus");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Rol)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("rol");
            entity.Property(e => e.Token)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("token");

            entity.HasOne(d => d.DetallesUsuario).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.DetallesUsuarioId)
                .HasConstraintName("FK__usuario__detalle__398D8EEE");
        });

        modelBuilder.Entity<Ventum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__venta__3213E83F0B1473D4");

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
                .HasConstraintName("FK__venta__usuario_i__59063A47");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
