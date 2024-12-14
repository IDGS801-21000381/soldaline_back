-- =======================================================
-- BASE DE DATOS: soldaline_bd
-- Descripci n: Sistema de gesti n para una empresa que fabrica piezas de herrer a
-- =======================================================

-- -------------------------------------------------------
-- Creaci n de la base de datos
-- -------------------------------------------------------

CREATE DATABASE soldaline_bd2;
GO

USE soldaline_bd2;
GO

-- -------------------------------------------------------
-- Tabla: detallesUsuario
-- Descripci n: Almacena los detalles personales de los usuarios
-- -------------------------------------------------------
CREATE TABLE detallesUsuario (
  id INT PRIMARY KEY IDENTITY(1,1),  -- Identificador  nico del detalle de usuario
  nombres VARCHAR(100) NULL,         -- Nombres del usuario
  apellidoM VARCHAR(45) NULL,        -- Apellido materno del usuario
  apellidoP VARCHAR(45) NULL,        -- Apellido paterno del usuario
  correo VARCHAR(100) NULL,			 -- Correo electr nico del usuario
  estatus TINYINT NULL				 -- Estatus activo/inactivo del usuario
);
  
GO

-- -------------------------------------------------------
-- Tabla: usuario
-- Descripci n: Almacena la informaci n de los usuarios del sistema, incluyendo campos para marketing y segmentaci n
-- -------------------------------------------------------
CREATE TABLE usuario (
  id INT PRIMARY KEY IDENTITY(1,1),         -- Identificador  nico del usuario
  nombre VARCHAR(50) NULL,                  -- Nombre de usuario
  contrasenia VARCHAR(255) NULL,             -- Contrase a del usuario
  token VARCHAR(45) NULL,                   -- Token de autenticaci n
  rol VARCHAR(45) NULL,                     -- Rol del usuario (admin, cliente, etc.)
  estatus TINYINT NULL,                     -- Estatus activo/inactivo del usuario
  urlImage text,
  direccion text, 
  tarjeta varchar(21),
  detallesUsuario_id INT NULL,              -- Relaci n con detallesUsuario
  frecuencia_compra INT NULL,               -- N mero de compras realizadas en el mes actual
  cliente_potencial BIT NULL DEFAULT 0,     -- Indicador de si es cliente potencial (1 = S , 0 = No)
  FOREIGN KEY (detallesUsuario_id) REFERENCES detallesUsuario(id)
);
GO

-- -------------------------------------------------------
-- Tabla: proveedor
-- Descripci n: Almacena la informaci n de los proveedores
-- -------------------------------------------------------
CREATE TABLE proveedor (
  id INT PRIMARY KEY IDENTITY(1,1),         -- Identificador  nico del proveedor
  nombreEmpresa VARCHAR(100) NULL,          -- Nombre de la empresa del proveedor
  direccion VARCHAR(255) NULL,              -- Direcci n del proveedor
  telefonoContacto VARCHAR(20) NULL,        -- Tel fono de contacto del proveedor
  nombreContacto VARCHAR(100) NULL,         -- Nombre del contacto
  apellidoM VARCHAR(45) NULL,               -- Apellido materno del contacto
  apellidoP VARCHAR(45) NULL,               -- Apellido paterno del contacto
  estatus TINYINT NULL DEFAULT 1            -- Estatus activo/inactivo del proveedor
);
GO

-- -------------------------------------------------------
-- Tabla: material
-- Descripci n: Almacena los materiales o componentes disponibles
-- -------------------------------------------------------
CREATE TABLE material (
  id INT PRIMARY KEY IDENTITY(1,1),         -- Identificador  nico del material
  nombre VARCHAR(150) NULL,                  -- Nombre del material
  --cantidad int null
  -- aqui falta cantidad D:
);
GO

-- -------------------------------------------------------
-- Tabla: compra
-- Descripci n: Registra las compras realizadas a proveedores
-- -------------------------------------------------------
CREATE TABLE compra (
  id INT PRIMARY KEY IDENTITY(1,1),         -- Identificador  nico de la compra
  fecha DATE NULL,                          -- Fecha de la compra
  usuario_id INT NOT NULL,                  -- Usuario que realiz  la compra
  FOREIGN KEY (usuario_id) REFERENCES usuario(id)
);
GO

-- -------------------------------------------------------
-- Tabla: detallecompra
-- Descripci n: Detalles de cada compra realizada
-- -------------------------------------------------------
CREATE TABLE detallecompra (
  id INT PRIMARY KEY IDENTITY(1,1),         -- Identificador  nico del detalle de compra
  cantidad INT NULL,                        -- Cantidad comprada
  folio VARCHAR(15),                        -- Folio de la compra
  descripcion TEXT NULL,                    -- Descripci n del art culo comprado
  costo FLOAT NULL,                         -- Costo del art culo
  compra_id INT NOT NULL,                   -- Relaci n con la compra
  FOREIGN KEY (compra_id) REFERENCES compra(id)
);
GO

-- -------------------------------------------------------
-- Tabla: inventariomateriales
-- Descripci n: Almacena el inventario de materiales adquiridos
-- -------------------------------------------------------
CREATE TABLE inventariomateriales (
  id INT PRIMARY KEY IDENTITY(1,1),         -- Identificador  nico del inventario de materiales
  cantidad INT NULL,                        -- Cantidad en inventario
  proveedor_id INT NOT NULL,                -- Proveedor del material
  material_id INT NOT NULL,                 -- Material adquirido
  detallecompra_id INT NOT NULL,            -- Relaci n con detallecompra
  FOREIGN KEY (proveedor_id) REFERENCES proveedor(id),
  FOREIGN KEY (material_id) REFERENCES material(id),
  FOREIGN KEY (detallecompra_id) REFERENCES detallecompra(id)
);
GO

-- -------------------------------------------------------
-- Tabla: fabricacion
-- Descripci n: Almacena los productos fabricados (recetas)
-- -------------------------------------------------------
CREATE TABLE fabricacion (
  id INT PRIMARY KEY IDENTITY(1,1),         -- Identificador  nico del producto
  nombreProducto VARCHAR(100) NULL,         -- Nombre del producto
  imagen_producto TEXT,                     -- Imagen del producto
  estatus INT NULL ,                         -- Estatus activo/inactivo del producto
--esto talvez eliminarlo
precioProducto INT,
--
[categoria] [nvarchar](255) NULL,
);


GOcotizacion

-- -------------------------------------------------------
-- Tabla: materialfabricacion
-- Descripci n: Relaciona materiales con productos fabricados
-- -------------------------------------------------------
CREATE TABLE materialfabricacion (
  id INT PRIMARY KEY IDENTITY(1,1),         -- Identificador  nico
  cantidad INT NULL,                        -- Cantidad de material usado
  estatus INT NULL,                         -- Estatus
  fabricacion_id INT NOT NULL,              -- Producto fabricado
  material_id INT NOT NULL,                 -- Material utilizado
  FOREIGN KEY (fabricacion_id) REFERENCES fabricacion(id),
  FOREIGN KEY (material_id) REFERENCES material(id)
);
GO

-- -------------------------------------------------------
-- Tabla: solicitudproduccion
-- Descripci n: Almacena las solicitudes de producci n
-- -------------------------------------------------------
CREATE TABLE solicitudproduccion (
  id INT PRIMARY KEY IDENTITY(1,1),         -- Identificador  nico de la solicitud
  descripcion VARCHAR(255) NULL,            -- Descripci n de la solicitud
  estatus INT NULL,                         -- Estatus de la solicitud
  cantidad INT,								-- cuantos productos se van a producir
  fabricacion_id INT NOT NULL,              -- Producto a fabricar
  usuario_id INT NOT NULL,                  -- Usuario que solicita la producci n
  FOREIGN KEY (fabricacion_id) REFERENCES fabricacion(id),
  FOREIGN KEY (usuario_id) REFERENCES usuario(id)
);
GO

-- -------------------------------------------------------
-- Tabla: produccion
-- Descripci n: Registra la producci n de productos
-- -------------------------------------------------------
CREATE TABLE produccion (
  id INT PRIMARY KEY IDENTITY(1,1),         -- Identificador  nico de la producci n
  fecha DATE NULL,                          -- Fecha de producci n
  costo FLOAT NULL,                         -- Costo de producci n
  usuario_id INT NOT NULL,                  -- Usuario responsable
  solicitudproduccion_id INT NOT NULL,      -- Solicitud asociada
  FOREIGN KEY (usuario_id) REFERENCES usuario(id),
  FOREIGN KEY (solicitudproduccion_id) REFERENCES solicitudproduccion(id)
);
GO

-- -------------------------------------------------------
-- Tabla: detalleproduccion
-- Descripci n: Detalles de los materiales usados en producci n
-- -------------------------------------------------------
CREATE TABLE detalleproduccion (
  id INT PRIMARY KEY IDENTITY(1,1),         -- Identificador  nico
  produccion_id INT NOT NULL,               -- Producci n asociada
  inventariomateriales_id INT NOT NULL,     -- Material utilizado
  FOREIGN KEY (produccion_id) REFERENCES produccion(id),
  FOREIGN KEY (inventariomateriales_id) REFERENCES inventariomateriales(id)
);
GO

-- -------------------------------------------------------
-- Tabla: inventarioProducto
-- Descripci n: Almacena los productos terminados en inventario
-- -------------------------------------------------------
CREATE TABLE inventarioProducto (
  id INT PRIMARY KEY IDENTITY(1,1),         -- Identificador  nico del producto en inventario
  cantidad INT NULL,                        -- Cantidad disponible
  precio FLOAT NULL,                        -- Precio de venta
  fechaCreacion DATE NULL,                  -- Fecha de creaci n
  lote VARCHAR(15) NULL,                    -- Lote de producci n
  fabricacion_id INT NOT NULL,              -- Producto fabricado
  produccion_id INT NOT NULL,               -- Producci n asociada
  nivelMinimoStock INT NULL,                -- Nivel m nimo de stock
  FOREIGN KEY (fabricacion_id) REFERENCES fabricacion(id),
  FOREIGN KEY (produccion_id) REFERENCES produccion(id)
);
GO

-- -------------------------------------------------------
-- Tabla: venta
-- Descripci n: Registra las ventas realizadas
-- -------------------------------------------------------
CREATE TABLE venta (
  id INT PRIMARY KEY IDENTITY(1,1),         -- Identificador  nico de la venta
  fecha DATETIME NULL,                      -- Fecha de la venta
  folio VARCHAR(15) NULL,                   -- Folio de la venta
  usuario_id INT NOT NULL,                  -- Cliente que realiz  la compra
  FOREIGN KEY (usuario_id) REFERENCES usuario(id)
);
GO

-- -------------------------------------------------------
-- Tabla: detalleventa
-- Descripci n: Detalles de los productos vendidos
-- -------------------------------------------------------
CREATE TABLE detalleventa (
  id INT PRIMARY KEY IDENTITY(1,1),         -- Identificador  nico
  cantidad INT NULL,                        -- Cantidad vendida
  precioUnitario FLOAT NULL,                -- Precio unitario
  venta_id INT NOT NULL,                    -- Venta asociada
  inventarioProducto_id INT NOT NULL,       -- Producto vendido
  clientePotencialDescuento FLOAT DEFAULT 0, -- Descuento aplicado por ser cliente potencial
  FOREIGN KEY (venta_id) REFERENCES venta(id),
  FOREIGN KEY (inventarioProducto_id) REFERENCES inventarioProducto(id)
);
GO

-- -------------------------------------------------------
-- Tabla: merma
-- Descripci n: Registra las mermas de materiales y productos
-- -------------------------------------------------------
CREATE TABLE merma (
  id INT PRIMARY KEY IDENTITY(1,1),         -- Identificador  nico de la merma
  cantidad INT NULL,                        -- Cantidad de merma
  descripcion VARCHAR(700) NULL,            -- Descripci n de la merma
  fecha DATETime NULL,                          -- Fecha de registro
  usuario_id INT NOT NULL,                  -- Usuario que registra la merma
  inventarioProducto_id INT NULL,           -- Producto afectado (si aplica)
  inventariomateriales_id INT NULL,         -- Material afectado (si aplica)
  FOREIGN KEY (usuario_id) REFERENCES usuario(id),
  FOREIGN KEY (inventarioProducto_id) REFERENCES inventarioProducto(id),
  FOREIGN KEY (inventariomateriales_id) REFERENCES inventariomateriales(id)
);
GO

-- -------------------------------------------------------
-- Tabla: productoproveedor
-- Descripci n: Relaciona productos con proveedores
-- -------------------------------------------------------
CREATE TABLE productoproveedor (
  id INT PRIMARY KEY IDENTITY(1,1),         -- Identificador  nico
  proveedor_id INT NOT NULL,                -- Proveedor
  material_id INT NOT NULL,                 -- Material proporcionado
  FOREIGN KEY (proveedor_id) REFERENCES proveedor(id),
  FOREIGN KEY (material_id) REFERENCES material(id)
);
GO

-- -------------------------------------------------------
-- Tabla: carrito
-- Descripci n: Almacena los carritos de compras de los usuarios
-- -------------------------------------------------------
CREATE TABLE carrito (
  id INT PRIMARY KEY IDENTITY(1,1),         -- Identificador  nico del carrito
  fecha DATE NULL,                          -- Fecha de creaci n del carrito
  estatus TINYINT NULL,                     -- Estatus del carrito (activo, abandonado, etc.)
  fabricacion_id INT NOT NULL,              -- Producto agregado al carrito
  usuario_id INT NOT NULL,                  -- Usuario propietario del carrito
  FOREIGN KEY (fabricacion_id) REFERENCES fabricacion(id),
  FOREIGN KEY (usuario_id) REFERENCES usuario(id)
);
GO

-- -------------------------------------------------------
-- Tabla: pedido
-- Descripci n: Registra los pedidos realizados por los clientes
-- -------------------------------------------------------
CREATE TABLE pedido (
  id INT PRIMARY KEY IDENTITY(1,1),         -- Identificador  nico del pedido
  usuario_id INT NOT NULL,                  -- Cliente que realiz  el pedido
  fecha DATE NULL,                          -- Fecha del pedido
  estatus TINYINT NULL,                     -- Estatus del pedido (pendiente, enviado, etc.)
  totalPedido FLOAT NULL,                   -- Monto total del pedido
  FOREIGN KEY (usuario_id) REFERENCES usuario(id)
);
GO

-- -------------------------------------------------------
-- Tabla: detallePedido
-- Descripci n: Detalles de los productos incluidos en el pedido
-- -------------------------------------------------------
CREATE TABLE detallePedido (
  id INT PRIMARY KEY IDENTITY(1,1),         -- Identificador  nico
  fabricacion_id INT NOT NULL,              -- Producto pedido
  pedido_id INT NOT NULL,                   -- Pedido asociado
  cantidad INT NULL,                        -- Cantidad pedida
  precioUnitario FLOAT NULL,                -- Precio unitario
  FOREIGN KEY (fabricacion_id) REFERENCES fabricacion(id),
  FOREIGN KEY (pedido_id) REFERENCES pedido(id)
);
GO

-- -------------------------------------------------------
-- Tabla: HistorialDescuentos
-- Descripci n: Registra los descuentos aplicados a los usuarios
-- -------------------------------------------------------
CREATE TABLE HistorialDescuentos (
  id INT PRIMARY KEY IDENTITY(1,1),         -- Identificador  nico
  usuario_id INT NOT NULL,                  -- Usuario beneficiado
  compra_id INT NOT NULL,                   -- Compra en la que se aplic 
  fecha DATE NOT NULL,                      -- Fecha de aplicaci n
  monto_descuento FLOAT NOT NULL,           -- Monto del descuento
  FOREIGN KEY (usuario_id) REFERENCES usuario(id),
  FOREIGN KEY (compra_id) REFERENCES compra(id)
);
GO

-- -------------------------------------------------------
-- Tabla: SegmentoCliente
-- Descripci n: Define los segmentos para clasificar a los clientes
-- -------------------------------------------------------
CREATE TABLE SegmentoCliente (
  id INT PRIMARY KEY IDENTITY(1,1),         -- Identificador  nico
  nombreSegmento VARCHAR(50) NOT NULL,      -- Nombre del segmento
  descripcion VARCHAR(255) NULL             -- Descripci n del segmento
);
GO

-- -------------------------------------------------------
-- Tabla: UsuarioSegmento
-- Descripci n: Relaciona usuarios con segmentos
-- -------------------------------------------------------
CREATE TABLE UsuarioSegmento (
  id INT PRIMARY KEY IDENTITY(1,1),         -- Identificador  nico
  usuario_id INT NOT NULL,                  -- Usuario
  segmento_id INT NOT NULL,                 -- Segmento al que pertenece
  FOREIGN KEY (usuario_id) REFERENCES usuario(id),
  FOREIGN KEY (segmento_id) REFERENCES SegmentoCliente(id)
);
GO

--francis 
-- ---------------------------------------
-- ---------------------------------------
CREATE TABLE EstimacionProduccion (
    Id INT PRIMARY KEY IDENTITY(1,1),           -- Identificador único
    HorasP FLOAT NOT NULL,                      -- Cantidad de horas necesarias para producir el producto
    FabricacionId INT NOT NULL,                 -- ID de la fabricación
    FOREIGN KEY (FabricacionId) REFERENCES Fabricacion(Id) -- Relación con la tabla 'Fabricacion'
);


-- Angel
-- --------------------------------------- 
-- ---------------------------------------



CREATE TABLE Empresa (
    EmpresaID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Direccion NVARCHAR(255),
    Telefono NVARCHAR(20),
    Correo NVARCHAR(100),
    SitioWeb NVARCHAR(100),
    FechaRegistro DATETIME DEFAULT GETDATE()
);

CREATE TABLE ClientePotencial (
    ClienteID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Direccion NVARCHAR(255),
    Telefono NVARCHAR(20),
    Correo NVARCHAR(100),
    RedesSociales NVARCHAR(255),
    Origen NVARCHAR(50), -- Ejemplo: página web, contacto directo
    PreferenciaComunicacion NVARCHAR(50), -- Teléfono, correo, etc.
    EmpresaID INT NULL, -- Referencia opcional si el cliente pertenece a una empresa
    UsuarioID INT NOT NULL, -- Relación con usuario que maneja al cliente
    Estatus INT NOT NULL, -- Campo de estado (int, según los valores definidos)
    FechaRegistro DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_Cliente_Empresa FOREIGN KEY (EmpresaID) REFERENCES Empresa(EmpresaID),
    CONSTRAINT FK_Cliente_Usuario FOREIGN KEY (UsuarioID) REFERENCES Usuario(id)
);




CREATE TABLE HistorialComunicacion (
    HistorialID INT PRIMARY KEY IDENTITY(1,1),
    ClienteID INT NOT NULL, -- Relación con el cliente
    UsuarioID INT NOT NULL, -- Usuario que realizó la comunicación
    FechaComunicacion date NOT NULL,
    Estatus INT NOT NULL, -- Estado de la comunicación
    TipoComunicacion INT NOT NULL, -- Ejemplo: llamada, correo, WhatsApp
    DetallesComunicado TEXT NOT NULL, -- Descripción de la comunicación
    FechaProximaCita date, -- Fecha de seguimiento o próxima cita
    Solicitud NVARCHAR(MAX), -- Detalles sobre qué busca el cliente
    CONSTRAINT FK_Historial_Cliente FOREIGN KEY (ClienteID) REFERENCES ClientePotencial(ClienteID),
    CONSTRAINT FK_Historial_Usuario FOREIGN KEY (UsuarioID) REFERENCES Usuario(id)
);


CREATE TABLE Proyecto (
    ProyectoID INT PRIMARY KEY IDENTITY(1,1),
    ClienteID INT NOT NULL, -- Relación con cliente
    EmpresaID INT NULL, -- Relación con empresa si aplica
    NombreProyecto NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(MAX),
    Estatus INT NOT NULL, -- Estado del proyecto
    Monto DECIMAL(18,2), -- Costo del proyecto
    FechaInicio date NOT NULL,
    FechaFin DATE NULL,
    CONSTRAINT FK_Proyecto_Cliente FOREIGN KEY (ClienteID) REFERENCES ClientePotencial(ClienteID),
    CONSTRAINT FK_Proyecto_Empresa FOREIGN KEY (EmpresaID) REFERENCES Empresa(EmpresaID)
);


CREATE TABLE cuentas_por_pagar (
    id INT PRIMARY KEY IDENTITY(1,1),         -- Identificador único de la cuenta
    ventas_id INT NOT NULL,              -- Relación con el presupuesto (FK)
    ClienteID INT NOT NULL,         -- Relación con cliente potencial (FK)
	usuario_id INT NOT NULL,                  -- Relación con el usuario que realiza el pago (FK)
    saldo_total DECIMAL(10,2) NOT NULL,       -- Total a pagar por el cliente
    saldo_pendiente DECIMAL(10,2) NOT NULL,   -- Saldo pendiente
    fecha DATE NOT NULL,                      -- Fecha de apertura de la cuenta
    dias_plazo INT NOT NULL,                  -- Días máximo para pagar desde la apertura de la cuenta
    estatus INT NOT NULL,                     -- Estatus de la cuenta (Ej: 0 = Abierta, 1 = Pagada, 2 = Cancelada)
    CONSTRAINT FK_cuenta_venta FOREIGN KEY (ventas_id) REFERENCES venta(id),
    CONSTRAINT FK_cuenta_clientePotencial FOREIGN KEY (ClienteID) REFERENCES clientePotencial(ClienteID),
	CONSTRAINT FK_pago_usuario FOREIGN KEY (usuario_id) REFERENCES usuario(id)
);

CREATE TABLE pagos(
    id INT PRIMARY KEY IDENTITY(1,1),        -- Identificador único del pago
    cuenta_id INT NOT NULL,                   -- Relación con la cuenta por pagar (FK)
    monto DECIMAL(10,2) NOT NULL,             -- Monto pagado
    fecha_pago DATE NOT NULL,                 -- Fecha del pago
    metodo_pago INT NOT NULL,                 -- Método de pago (1 = Tarjeta, 2 = Efectivo, 3 = Transferencia, etc.)
    clientePotencial_id INT NOT NULL,         -- Relación con el cliente potencial (FK)
    usuario_id INT NOT NULL,                  -- Relación con el usuario que realiza el pago (FK)
    CONSTRAINT FK_pago_cuenta FOREIGN KEY (cuenta_id) REFERENCES cuentas_por_pagar(id),
    CONSTRAINT FK_pago_clientePotencial FOREIGN KEY (clientePotencial_id) REFERENCES clientePotencial(ClienteID),
    CONSTRAINT FK_pago_usuario1 FOREIGN KEY (usuario_id) REFERENCES usuario(id)
);

CREATE TABLE comentarios_cliente (
    id INT PRIMARY KEY IDENTITY(1,1),        -- Identificador único del comentario
    usuario_id INT NOT NULL,                 -- Relación con el usuario que realiza el comentario (FK)
	ClienteID int not null, 
    fecha DATE NOT NULL,                      -- Fecha del comentario
    tipo INT NOT NULL,                       -- Tipo de comentario (Ej: 1 = Queja, 2 = Devolución, 3 = Asistencia técnica)
    descripcion TEXT NOT NULL,               -- Descripción detallada del comentario
    estatus INT NOT NULL DEFAULT 0,          -- Estatus del comentario (Ej: 0 = Pendiente, 1 = Resuelto, 2 = Cancelado)
    calificacion INT NOT NULL,               -- Calificación del cliente (1-5)
    CONSTRAINT FK_comentario_usuario FOREIGN KEY (usuario_id) REFERENCES usuario(id),
	CONSTRAINT FK_pago_clientePotencial1 FOREIGN KEY (ClienteID) REFERENCES clientePotencial(ClienteID)
);


CREATE TABLE Cotizacion (
    CotizacionID INT PRIMARY KEY IDENTITY(1,1),
    EmpresaID INT,
	ClienteID int 
    Fecha DATETIME DEFAULT GETDATE(),
    
    Total DECIMAL(18, 2) NOT NULL,
    Vendedor NVARCHAR(100),
	idVendedor in not null,

    Status INT NOT NULL DEFAULT 0, -- 0 = Pendiente, 1 = Aprobada, 2 = Rechazada
    FOREIGN KEY (EmpresaID) REFERENCES Empresa(EmpresaID),
	FOREIGN KEY (idVendedor) REFERENCES usuario(id),
	FOREIGN KEY (ClienteID) REFERENCES ClientePotencial(ClienteID)
	
);
CREATE TABLE detallecotizacion ( --en esta tabla se guardan los productos 
  id INT PRIMARY KEY IDENTITY(1,1),         -- Identificador  nico del detalle de cotizacion
  cantidad INT NULL,                        -- Cantidad comprada
  precioUnitario FLOAT NULL,                -- Precio unitario
  inventarioProducto_id INT NOT NULL,       -- Producto asociado
  costo FLOAT NULL,                         -- Costo del art culo
  cotizacion_id INT NOT NULL,                   -- Relaci n con la compra
  FOREIGN KEY (cotizacion_id) REFERENCES Cotizacion(CotizacionID),
    FOREIGN KEY (inventarioProducto_id) REFERENCES inventarioProducto(id)
);