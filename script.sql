/****** Object:  Database [ComidasDb]    Script Date: 17/04/2024 8:10:18 ******/
CREATE DATABASE [ComidasDb]  (EDITION = 'Standard', SERVICE_OBJECTIVE = 'S0', MAXSIZE = 1 GB) WITH CATALOG_COLLATION = SQL_Latin1_General_CP1_CI_AS;
GO
ALTER DATABASE [ComidasDb] SET COMPATIBILITY_LEVEL = 150
GO
ALTER DATABASE [ComidasDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ComidasDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ComidasDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ComidasDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ComidasDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [ComidasDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ComidasDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ComidasDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ComidasDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ComidasDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ComidasDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ComidasDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ComidasDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ComidasDb] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO
ALTER DATABASE [ComidasDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ComidasDb] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [ComidasDb] SET  MULTI_USER 
GO
ALTER DATABASE [ComidasDb] SET ENCRYPTION ON
GO
ALTER DATABASE [ComidasDb] SET QUERY_STORE = ON
GO
ALTER DATABASE [ComidasDb] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 100, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
/*** Los scripts de las configuraciones con ámbito de base de datos en Azure deben ejecutarse dentro de la conexión de base de datos de destino. ***/
GO
-- ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 8;
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 17/04/2024 8:10:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clientes]    Script Date: 17/04/2024 8:10:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clientes](
	[IdCliente] [int] IDENTITY(1,1) NOT NULL,
	[Cedula] [varchar](20) NOT NULL,
	[Nombres] [varchar](100) NOT NULL,
	[Apellidos] [varchar](100) NOT NULL,
	[Direccion] [varchar](100) NULL,
	[Telefono] [varchar](10) NULL,
 CONSTRAINT [PK_Clientes] PRIMARY KEY CLUSTERED 
(
	[IdCliente] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetalleXFacturas]    Script Date: 17/04/2024 8:10:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetalleXFacturas](
	[IdDetalleXFactura] [int] IDENTITY(1,1) NOT NULL,
	[NroFactura] [int] NOT NULL,
	[IdProducto] [int] NOT NULL,
	[Precio] [decimal](12, 2) NOT NULL,
	[Cantidad] [int] NOT NULL,
	[Total] [decimal](12, 2) NOT NULL,
 CONSTRAINT [PK_DetalleXFacturas] PRIMARY KEY CLUSTERED 
(
	[IdDetalleXFactura] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Facturas]    Script Date: 17/04/2024 8:10:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Facturas](
	[NroFactura] [int] IDENTITY(1,1) NOT NULL,
	[IdCliente] [int] NOT NULL,
	[NroMesa] [int] NOT NULL,
	[IdMesero] [int] NOT NULL,
	[IdSupervisor] [int] NOT NULL,
	[Fecha] [datetime2](7) NOT NULL,
	[Total] [decimal](12, 2) NOT NULL,
 CONSTRAINT [PK_Facturas] PRIMARY KEY CLUSTERED 
(
	[NroFactura] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Mesas]    Script Date: 17/04/2024 8:10:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mesas](
	[NroMesa] [int] NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Reservada] [bit] NOT NULL,
	[Puestos] [int] NOT NULL,
 CONSTRAINT [PK_Mesas] PRIMARY KEY CLUSTERED 
(
	[NroMesa] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Productos]    Script Date: 17/04/2024 8:10:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Productos](
	[IdProducto] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Precio] [decimal](12, 2) NOT NULL,
	[Fecha] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Productos] PRIMARY KEY CLUSTERED 
(
	[IdProducto] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 17/04/2024 8:10:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[IdRol] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 17/04/2024 8:10:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Nombres] [varchar](100) NOT NULL,
	[Apellidos] [varchar](100) NOT NULL,
	[Edad] [int] NOT NULL,
	[Antiguedad] [int] NOT NULL,
	[Correo] [varchar](100) NOT NULL,
	[Clave] [varchar](100) NOT NULL,
	[IdRol] [int] NOT NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [IX_DetalleXFacturas_IdProducto]    Script Date: 17/04/2024 8:10:18 ******/
CREATE NONCLUSTERED INDEX [IX_DetalleXFacturas_IdProducto] ON [dbo].[DetalleXFacturas]
(
	[IdProducto] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_DetalleXFacturas_NroFactura]    Script Date: 17/04/2024 8:10:18 ******/
CREATE NONCLUSTERED INDEX [IX_DetalleXFacturas_NroFactura] ON [dbo].[DetalleXFacturas]
(
	[NroFactura] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Facturas_IdCliente]    Script Date: 17/04/2024 8:10:18 ******/
CREATE NONCLUSTERED INDEX [IX_Facturas_IdCliente] ON [dbo].[Facturas]
(
	[IdCliente] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Facturas_IdMesero]    Script Date: 17/04/2024 8:10:18 ******/
CREATE NONCLUSTERED INDEX [IX_Facturas_IdMesero] ON [dbo].[Facturas]
(
	[IdMesero] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Facturas_IdSupervisor]    Script Date: 17/04/2024 8:10:18 ******/
CREATE NONCLUSTERED INDEX [IX_Facturas_IdSupervisor] ON [dbo].[Facturas]
(
	[IdSupervisor] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Facturas_NroMesa]    Script Date: 17/04/2024 8:10:18 ******/
CREATE NONCLUSTERED INDEX [IX_Facturas_NroMesa] ON [dbo].[Facturas]
(
	[NroMesa] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Usuarios_IdRol]    Script Date: 17/04/2024 8:10:18 ******/
CREATE NONCLUSTERED INDEX [IX_Usuarios_IdRol] ON [dbo].[Usuarios]
(
	[IdRol] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DetalleXFacturas]  WITH CHECK ADD  CONSTRAINT [FK_DetalleXFacturas_Facturas_NroFactura] FOREIGN KEY([NroFactura])
REFERENCES [dbo].[Facturas] ([NroFactura])
GO
ALTER TABLE [dbo].[DetalleXFacturas] CHECK CONSTRAINT [FK_DetalleXFacturas_Facturas_NroFactura]
GO
ALTER TABLE [dbo].[DetalleXFacturas]  WITH CHECK ADD  CONSTRAINT [FK_DetalleXFacturas_Productos_IdProducto] FOREIGN KEY([IdProducto])
REFERENCES [dbo].[Productos] ([IdProducto])
GO
ALTER TABLE [dbo].[DetalleXFacturas] CHECK CONSTRAINT [FK_DetalleXFacturas_Productos_IdProducto]
GO
ALTER TABLE [dbo].[Facturas]  WITH CHECK ADD  CONSTRAINT [FK_Facturas_Clientes_IdCliente] FOREIGN KEY([IdCliente])
REFERENCES [dbo].[Clientes] ([IdCliente])
GO
ALTER TABLE [dbo].[Facturas] CHECK CONSTRAINT [FK_Facturas_Clientes_IdCliente]
GO
ALTER TABLE [dbo].[Facturas]  WITH CHECK ADD  CONSTRAINT [FK_Facturas_Mesas_NroMesa] FOREIGN KEY([NroMesa])
REFERENCES [dbo].[Mesas] ([NroMesa])
GO
ALTER TABLE [dbo].[Facturas] CHECK CONSTRAINT [FK_Facturas_Mesas_NroMesa]
GO
ALTER TABLE [dbo].[Facturas]  WITH CHECK ADD  CONSTRAINT [FK_Facturas_Usuarios_IdMesero] FOREIGN KEY([IdMesero])
REFERENCES [dbo].[Usuarios] ([IdUsuario])
GO
ALTER TABLE [dbo].[Facturas] CHECK CONSTRAINT [FK_Facturas_Usuarios_IdMesero]
GO
ALTER TABLE [dbo].[Facturas]  WITH CHECK ADD  CONSTRAINT [FK_Facturas_Usuarios_IdSupervisor] FOREIGN KEY([IdSupervisor])
REFERENCES [dbo].[Usuarios] ([IdUsuario])
GO
ALTER TABLE [dbo].[Facturas] CHECK CONSTRAINT [FK_Facturas_Usuarios_IdSupervisor]
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_Usuarios_Roles_IdRol] FOREIGN KEY([IdRol])
REFERENCES [dbo].[Roles] ([IdRol])
GO
ALTER TABLE [dbo].[Usuarios] CHECK CONSTRAINT [FK_Usuarios_Roles_IdRol]
GO
ALTER DATABASE [ComidasDb] SET  READ_WRITE 
GO
