USE [Prueba]
GO
/****** Object:  Table [dbo].[Categoria]    Script Date: 03-02-2024 23:39:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categoria](
	[idCategoria] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](50) NULL,
	[isActive] [bit] NULL,
	[fechaRegistro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idCategoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Configuracion]    Script Date: 03-02-2024 23:39:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Configuracion](
	[idConfiguracion] [int] IDENTITY(1,1) NOT NULL,
	[recurso] [varchar](100) NULL,
	[propiedad] [varchar](100) NULL,
	[valor] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[idConfiguracion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Producto]    Script Date: 03-02-2024 23:39:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producto](
	[idProducto] [int] IDENTITY(1,1) NOT NULL,
	[codigoBarra] [varchar](50) NULL,
	[marca] [varchar](50) NULL,
	[descripcion] [varchar](100) NULL,
	[idCategoria] [int] NULL,
	[stock] [int] NULL,
	[urlImagen] [varchar](500) NULL,
	[nombreImagen] [varchar](100) NULL,
	[precio] [decimal](10, 2) NULL,
	[esActivo] [bit] NULL,
	[fechaRegistro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 03-02-2024 23:39:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[idUsuario] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NULL,
	[apellido] [varchar](50) NULL,
	[correo] [varchar](50) NULL,
	[clave] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[idUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Categoria] ON 

INSERT [dbo].[Categoria] ([idCategoria], [descripcion], [isActive], [fechaRegistro]) VALUES (1, N'Teclados', 1, CAST(N'2024-02-01T19:29:41.433' AS DateTime))
INSERT [dbo].[Categoria] ([idCategoria], [descripcion], [isActive], [fechaRegistro]) VALUES (2, N'Mouse', 1, CAST(N'2024-02-01T19:29:48.547' AS DateTime))
INSERT [dbo].[Categoria] ([idCategoria], [descripcion], [isActive], [fechaRegistro]) VALUES (3, N'Monitores', 1, CAST(N'2024-02-01T19:29:54.013' AS DateTime))
SET IDENTITY_INSERT [dbo].[Categoria] OFF
GO
SET IDENTITY_INSERT [dbo].[Configuracion] ON 

INSERT [dbo].[Configuracion] ([idConfiguracion], [recurso], [propiedad], [valor]) VALUES (1, N'FireBase_Storage', N'email', N'')
INSERT [dbo].[Configuracion] ([idConfiguracion], [recurso], [propiedad], [valor]) VALUES (2, N'FireBase_Storage', N'clave', N'')
INSERT [dbo].[Configuracion] ([idConfiguracion], [recurso], [propiedad], [valor]) VALUES (3, N'FireBase_Storage', N'ruta', N'')
INSERT [dbo].[Configuracion] ([idConfiguracion], [recurso], [propiedad], [valor]) VALUES (4, N'FireBase_Storage', N'api_key', N'')
INSERT [dbo].[Configuracion] ([idConfiguracion], [recurso], [propiedad], [valor]) VALUES (5, N'FireBase_Storage', N'carpeta_usuario', N'IMAGENES_USUARIO')
INSERT [dbo].[Configuracion] ([idConfiguracion], [recurso], [propiedad], [valor]) VALUES (6, N'FireBase_Storage', N'carpeta_producto', N'IMAGENES_PRODUCTO')
INSERT [dbo].[Configuracion] ([idConfiguracion], [recurso], [propiedad], [valor]) VALUES (7, N'FireBase_Storage', N'carpeta_logo', N'IMAGENES_LOGO')
SET IDENTITY_INSERT [dbo].[Configuracion] OFF
GO
SET IDENTITY_INSERT [dbo].[Producto] ON 

INSERT [dbo].[Producto] ([idProducto], [codigoBarra], [marca], [descripcion], [idCategoria], [stock], [urlImagen], [nombreImagen], [precio], [esActivo], [fechaRegistro]) VALUES (6, N'10104', N'sasd', N'computadora', 1, 20, N'https://firebasestorage.googleapis.com/v0/b/mi-tienda-a0cf9.appspot.com/o/IMAGENES_PRODUCTO%2Fc34fd92edeae46df8bbcc56fabc16ebb.jpg?alt=media&token=02b4ccb7-bf1c-4beb-83d6-df91d681bd47', N'c34fd92edeae46df8bbcc56fabc16ebb.jpg', CAST(2000.00 AS Decimal(10, 2)), 1, CAST(N'2024-02-03T04:31:27.047' AS DateTime))
INSERT [dbo].[Producto] ([idProducto], [codigoBarra], [marca], [descripcion], [idCategoria], [stock], [urlImagen], [nombreImagen], [precio], [esActivo], [fechaRegistro]) VALUES (7, N'10102', N'Gigabyte', N'Monitor', 3, 200, N'https://firebasestorage.googleapis.com/v0/b/mi-tienda-a0cf9.appspot.com/o/IMAGENES_PRODUCTO%2F8f6579d670e64213af222a649dfba6c9.jpg?alt=media&token=6d277d4c-b8e0-4c7f-b83f-c6db47de8ebd', N'8f6579d670e64213af222a649dfba6c9.jpg', CAST(1000000.00 AS Decimal(10, 2)), 1, CAST(N'2024-02-03T04:32:35.750' AS DateTime))
SET IDENTITY_INSERT [dbo].[Producto] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuario] ON 

INSERT [dbo].[Usuario] ([idUsuario], [nombre], [apellido], [correo], [clave]) VALUES (1, N'Nicolas', N'estudiante', N'codigo@example.com', N'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3')
SET IDENTITY_INSERT [dbo].[Usuario] OFF
GO
ALTER TABLE [dbo].[Categoria] ADD  DEFAULT (getdate()) FOR [fechaRegistro]
GO
ALTER TABLE [dbo].[Producto] ADD  DEFAULT (getdate()) FOR [fechaRegistro]
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD FOREIGN KEY([idCategoria])
REFERENCES [dbo].[Categoria] ([idCategoria])
GO
