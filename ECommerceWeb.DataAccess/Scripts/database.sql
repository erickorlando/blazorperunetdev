IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Categoria] (
    [Id] int NOT NULL IDENTITY,
    [Nombre] nvarchar(200) NOT NULL,
    CONSTRAINT [PK_Categoria] PRIMARY KEY ([Id])
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Nombre') AND [object_id] = OBJECT_ID(N'[Categoria]'))
    SET IDENTITY_INSERT [Categoria] ON;
INSERT INTO [Categoria] ([Id], [Nombre])
VALUES (1, N'Celulares'),
(2, N'Televisores'),
(3, N'Electrodomésticos');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Nombre') AND [object_id] = OBJECT_ID(N'[Categoria]'))
    SET IDENTITY_INSERT [Categoria] OFF;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231006011121_PrimeraMigracion', N'7.0.12');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Categoria] ADD [Comentarios] nvarchar(500) NULL;
GO

UPDATE [Categoria] SET [Comentarios] = NULL
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [Categoria] SET [Comentarios] = NULL
WHERE [Id] = 2;
SELECT @@ROWCOUNT;

GO

UPDATE [Categoria] SET [Comentarios] = NULL
WHERE [Id] = 3;
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231006012528_NuevaColumnaEnCategoria', N'7.0.12');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Categoria] ADD [Estado] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

CREATE TABLE [Marca] (
    [Id] int NOT NULL IDENTITY,
    [Nombre] nvarchar(100) NOT NULL,
    [Estado] bit NOT NULL,
    CONSTRAINT [PK_Marca] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [TipoCliente] (
    [Id] int NOT NULL IDENTITY,
    [Descripcion] nvarchar(70) NOT NULL,
    [Estado] bit NOT NULL,
    CONSTRAINT [PK_TipoCliente] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Producto] (
    [Id] int NOT NULL IDENTITY,
    [Nombre] nvarchar(150) NOT NULL,
    [Descripcion] nvarchar(500) NOT NULL,
    [PrecioUnitario] decimal(11,2) NOT NULL,
    [UrlImagen] varchar(600) NULL,
    [CategoriaId] int NOT NULL,
    [MarcaId] int NOT NULL,
    [Estado] bit NOT NULL,
    CONSTRAINT [PK_Producto] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Producto_Categoria_CategoriaId] FOREIGN KEY ([CategoriaId]) REFERENCES [Categoria] ([Id]),
    CONSTRAINT [FK_Producto_Marca_MarcaId] FOREIGN KEY ([MarcaId]) REFERENCES [Marca] ([Id])
);
GO

CREATE TABLE [Cliente] (
    [Id] int NOT NULL IDENTITY,
    [Nombres] nvarchar(200) NOT NULL,
    [Apellidos] nvarchar(200) NOT NULL,
    [Email] varchar(500) NOT NULL,
    [FechaNacimiento] DATE NOT NULL,
    [TipoClienteId] int NOT NULL,
    [Estado] bit NOT NULL,
    CONSTRAINT [PK_Cliente] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Cliente_TipoCliente_TipoClienteId] FOREIGN KEY ([TipoClienteId]) REFERENCES [TipoCliente] ([Id])
);
GO

UPDATE [Categoria] SET [Estado] = CAST(1 AS bit)
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [Categoria] SET [Estado] = CAST(1 AS bit)
WHERE [Id] = 2;
SELECT @@ROWCOUNT;

GO

UPDATE [Categoria] SET [Estado] = CAST(1 AS bit)
WHERE [Id] = 3;
SELECT @@ROWCOUNT;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Descripcion', N'Estado') AND [object_id] = OBJECT_ID(N'[TipoCliente]'))
    SET IDENTITY_INSERT [TipoCliente] ON;
INSERT INTO [TipoCliente] ([Id], [Descripcion], [Estado])
VALUES (1, N'Cliente Normal', CAST(1 AS bit)),
(2, N'Cliente Especial', CAST(1 AS bit));
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Descripcion', N'Estado') AND [object_id] = OBJECT_ID(N'[TipoCliente]'))
    SET IDENTITY_INSERT [TipoCliente] OFF;
GO

CREATE INDEX [IX_Cliente_TipoClienteId] ON [Cliente] ([TipoClienteId]);
GO

CREATE INDEX [IX_Producto_CategoriaId] ON [Producto] ([CategoriaId]);
GO

CREATE INDEX [IX_Producto_MarcaId] ON [Producto] ([MarcaId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231011020420_TablasAdicionales', N'7.0.12');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Rol] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_Rol] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Usuario] (
    [Id] nvarchar(450) NOT NULL,
    [NombreCompleto] nvarchar(200) NOT NULL,
    [FechaNacimiento] DATE NOT NULL,
    [Direccion] nvarchar(500) NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_Usuario] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_Rol_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Rol] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_Usuario_UserId] FOREIGN KEY ([UserId]) REFERENCES [Usuario] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_Usuario_UserId] FOREIGN KEY ([UserId]) REFERENCES [Usuario] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_Usuario_UserId] FOREIGN KEY ([UserId]) REFERENCES [Usuario] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [UsuarioRol] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_UsuarioRol] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_UsuarioRol_Rol_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Rol] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UsuarioRol_Usuario_UserId] FOREIGN KEY ([UserId]) REFERENCES [Usuario] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [Rol] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [EmailIndex] ON [Usuario] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [Usuario] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

CREATE INDEX [IX_UsuarioRol_RoleId] ON [UsuarioRol] ([RoleId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231018010638_TablasDeUsuario', N'7.0.12');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Venta] (
    [Id] int NOT NULL IDENTITY,
    [ClienteId] int NOT NULL,
    [Total] decimal(11,2) NOT NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [Estado] bit NOT NULL,
    CONSTRAINT [PK_Venta] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Venta_Cliente_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Cliente] ([Id])
);
GO

CREATE TABLE [VentaDetalle] (
    [Id] int NOT NULL IDENTITY,
    [VentaId] int NOT NULL,
    [ProductoId] int NOT NULL,
    [Precio] decimal(11,2) NOT NULL,
    [Cantidad] int NOT NULL,
    [Total] decimal(11,2) NOT NULL,
    [Estado] bit NOT NULL,
    CONSTRAINT [PK_VentaDetalle] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_VentaDetalle_Producto_ProductoId] FOREIGN KEY ([ProductoId]) REFERENCES [Producto] ([Id]),
    CONSTRAINT [FK_VentaDetalle_Venta_VentaId] FOREIGN KEY ([VentaId]) REFERENCES [Venta] ([Id])
);
GO

CREATE INDEX [IX_Venta_ClienteId] ON [Venta] ([ClienteId]);
GO

CREATE INDEX [IX_VentaDetalle_ProductoId] ON [VentaDetalle] ([ProductoId]);
GO

CREATE INDEX [IX_VentaDetalle_VentaId] ON [VentaDetalle] ([VentaId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231027013119_TablasVenta', N'7.0.12');
GO

COMMIT;
GO

