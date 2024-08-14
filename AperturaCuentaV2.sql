USE [AperturaCuentaV2]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 13/8/2024 12:53:08 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CuentaUsuario]    Script Date: 13/8/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CuentaUsuario](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Identificacion] [nvarchar](50) NOT NULL,
	[Codigo_Dactilar] [nvarchar](50) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Apellido] [nvarchar](100) NOT NULL,
	[Telefono] [nvarchar](20) NOT NULL,
	[Correo] [nvarchar](100) NOT NULL,
	[Provincia] [nvarchar](100) NOT NULL,
	[Canton] [nvarchar](100) NOT NULL,
	[Parroquia] [nvarchar](100) NOT NULL,
	[Direccion] [nvarchar](200) NOT NULL,
	[Referencia] [nvarchar](200) NULL,
	[Ingresos] [decimal](18, 2) NOT NULL,
	[Gastos] [decimal](18, 2) NOT NULL,
	[Gasto_de_Transporte] [decimal](18, 2) NOT NULL,
	[Gasto_de_Educacion] [decimal](18, 2) NOT NULL,
	[Creditos] [bit] NOT NULL,
	[Tarjetas_de_Credito] [bit] NOT NULL,
	[Ninguno] [bit] NOT NULL,
	[Casa] [bit] NOT NULL,
	[Carro] [bit] NOT NULL,
	[Terreno] [bit] NOT NULL,
	[PaisNacimiento] [nvarchar](100) NOT NULL,
	[CiudadNacimiento] [nvarchar](100) NOT NULL,
	[NivelDeInstruccion] [nvarchar](100) NOT NULL,
	[CondicionLaboral] [nvarchar](50) NOT NULL,
	[TipoVivienda] [nvarchar](50) NOT NULL,
	[ActividadesEnOtroPais] [bit] NOT NULL,
	[DetallesActividadesEnOtroPais] [nvarchar](500) NULL,
	[AceptoTerminos] [bit] NOT NULL,
	[ImageUrl] [varbinary](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Imagenes]    Script Date: 13/8/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Imagenes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Identificacion] [nvarchar](255) NOT NULL,
	[Codigo_Dactilar] [nvarchar](255) NOT NULL,
	[Foto] [varbinary](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RegistrosAuditoria]    Script Date: 13/8/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegistrosAuditoria](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DireccionIP] [nvarchar](50) NULL,
	[DatosNavegador] [nvarchar](255) NULL,
	[Pais] [nvarchar](100) NULL,
	[Correo_envio_OTP] [nvarchar](50) NULL,
	[Fecha_inicio] [datetime] NULL,
	[Fecha_Fin] [datetime] NULL,
	[Identificacion] [nvarchar](50) NULL,
	[CodigoDactilar] [nvarchar](100) NULL,
	[CodigoOTP] [nvarchar](10) NULL,
	[EstadoOTP] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 13/8/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](max) NOT NULL,
	[Apellido] [nvarchar](max) NOT NULL,
	[Telefono] [nvarchar](max) NOT NULL,
	[Correo] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[CuentaUsuario] ADD  DEFAULT ((0)) FOR [Gasto_de_Transporte]
GO
ALTER TABLE [dbo].[CuentaUsuario] ADD  DEFAULT ((0)) FOR [Gasto_de_Educacion]
GO
ALTER TABLE [dbo].[CuentaUsuario] ADD  DEFAULT ((0)) FOR [Creditos]
GO
ALTER TABLE [dbo].[CuentaUsuario] ADD  DEFAULT ((0)) FOR [Tarjetas_de_Credito]
GO
ALTER TABLE [dbo].[CuentaUsuario] ADD  DEFAULT ((0)) FOR [Ninguno]
GO
ALTER TABLE [dbo].[CuentaUsuario] ADD  DEFAULT ((0)) FOR [Casa]
GO
ALTER TABLE [dbo].[CuentaUsuario] ADD  DEFAULT ((0)) FOR [Carro]
GO
ALTER TABLE [dbo].[CuentaUsuario] ADD  DEFAULT ((0)) FOR [Terreno]
GO
ALTER TABLE [dbo].[CuentaUsuario] ADD  DEFAULT ((0)) FOR [ActividadesEnOtroPais]
GO
ALTER TABLE [dbo].[CuentaUsuario] ADD  DEFAULT ((0)) FOR [AceptoTerminos]
GO
/****** Object:  StoredProcedure [dbo].[CrearTablaTemporalUsuarios]    Script Date: 13/8/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CrearTablaTemporalUsuarios]
AS
BEGIN
    -- Crear una tabla temporal con la estructura deseada
    CREATE TABLE #Usuario (
        Id INT,
        Nombre NVARCHAR(50),
        Apellido NVARCHAR(50),
        Telefono NVARCHAR(20),
        Correo NVARCHAR(50)
    );

    -- Insertar los datos especificados en la tabla temporal
    INSERT INTO #Usuario (Id, Nombre, Apellido, Telefono, Correo)
    VALUES 
    (0987654321, 'Nombre1', 'Apellido1', 'Telefono1', 'jg38903@gmail.com'),
    (1234567890, 'Nombre2', 'Apellido2', 'Telefono2', 'jg38903x2@gmail.com');

    -- Seleccionar los datos de la tabla temporal
    SELECT * FROM #Usuario;

    -- Nota: La tabla temporal se elimina automáticamente al finalizar el procedimiento almacenado
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateImageUrl]    Script Date: 13/8/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UpdateImageUrl]
AS
BEGIN
    UPDATE cu
    SET cu.ImageUrl = i.Foto
    FROM CuentaUsuario cu
    INNER JOIN Imagenes i
    ON cu.Identificacion = i.Identificacion 
    AND cu.Codigo_Dactilar = i.Codigo_Dactilar;
END;
GO
USE [master]
GO
ALTER DATABASE [AperturaCuentaV2] SET  READ_WRITE 
GO
