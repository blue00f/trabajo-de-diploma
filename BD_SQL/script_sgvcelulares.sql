CREATE DATABASE bd_santsung_064RB;
USE bd_santsung_064RB;

--
-- Tablas de aspectos técnicos
--

go
create table DV_064RB
(
    Tabla_064RB varchar(128) not null,
    DVH_064RB varchar(128) not null,
    DVV_064RB varchar(128) not null,
    constraint pk_dv_064RB primary key (Tabla_064RB)
);
go
go
create table DV_Registro_064RB
(
    Tabla_064RB varchar(128) not null,
    IdRegistro_064RB nvarchar(200) not null,
    DVH_064RB varchar(128) not null,
    Datos_064RB nvarchar(MAX) not null,
    constraint pk_dv_registro_064RB primary key (Tabla_064RB, IdRegistro_064RB)
);
go
go
create table Idioma_064RB (
    IdIdioma_064RB varchar(8) not null,
    Nombre_064RB varchar(50) not null,
    constraint pk_idioma_064RB primary key (IdIdioma_064RB)
);
go
go
create table Rol_064RB (
    IdRol_064RB int identity(1,1) not null,
    Nombre_064RB varchar(50) not null,
    constraint pk_rol_064RB primary key (IdRol_064RB)
);
go
go
create table Familia_064RB(
    IdFamilia_064RB int identity(1,1) not null,
    Nombre_064RB varchar(50) not null,
    constraint pk_familia_064RB primary key (IdFamilia_064RB)
);
go
go
create table Rol_familia_064RB(
    IdRol_064RB int not null,
    IdFamilia_064RB int not null,
    constraint pk_rol_familia_064RB primary key (IdRol_064RB, IdFamilia_064RB),
    constraint fk_rol_familia_rol_064RB foreign key (IdRol_064RB) references Rol_064RB(IdRol_064RB),
    constraint fk_rol_familia_familia_064RB foreign key (IdFamilia_064RB) references Familia_064RB(IdFamilia_064RB)
);
go
go
create table Familia_familia_064RB(
    IdFamiliaPadre_064RB int not null,
    IdFamiliaHija_064RB int not null,
    constraint pk_familia_familia_064RB primary key (IdFamiliaPadre_064RB, IdFamiliaHija_064RB),
    constraint fk_ff_padre_064RB foreign key (IdFamiliaPadre_064RB) references Familia_064RB(IdFamilia_064RB),
    constraint fk_ff_hija_064RB foreign key (IdFamiliaHija_064RB) references Familia_064RB(IdFamilia_064RB)
);
go
go
create table Permiso_064RB(
    IdPermiso_064RB int identity(1,1) not null,
    Nombre_064RB varchar(50) not null,
    constraint pk_permiso_064RB primary key (IdPermiso_064RB)
);
go
go
create table Permiso_familia_064RB(
    IdFamilia_064RB int not null,
    IdPermiso_064RB int not null,
    constraint pk_permiso_familia_064RB primary key (IdFamilia_064RB, IdPermiso_064RB),
    constraint fk_permiso_familia_familia_064RB foreign key (IdFamilia_064RB) references Familia_064RB(IdFamilia_064RB),
    constraint fk_permiso_familia_permiso_064RB foreign key (IdPermiso_064RB) references Permiso_064RB(IdPermiso_064RB)
);
go
go
create table Rol_permiso_064RB(
    IdRol_064RB int not null,
    IdPermiso_064RB int not null,
    constraint pk_rol_permiso_064RB primary key (IdRol_064RB, IdPermiso_064RB),
    constraint fk_rol_permiso_rol_064RB foreign key (IdRol_064RB) references Rol_064RB(IdRol_064RB),
    constraint fk_rol_permiso_permiso_064RB foreign key (IdPermiso_064RB) references Permiso_064RB(IdPermiso_064RB)
);
go
go
create table Usuario_064RB (
    Dni_064RB varchar(8) not null,
    Nombre_064RB varchar(50) not null,
    Apellido_064RB varchar(50) not null,
    Correo_064RB varchar(50) not null,
    NombreUsuario_064RB varchar(50) not null,
    Password_064RB varchar(64) not null,
    Bloqueo_064RB bit not null,
    Activo_064RB bit not null,
    CantIntentos_064RB tinyint not null,
    IdRol_064RB int not null,
    IdIdioma_064RB varchar(8) not null,
    constraint pk_usuario_064RB primary key (Dni_064RB),
    constraint fk_usuario_rol_064RB foreign key (IdRol_064RB) references Rol_064RB(IdRol_064RB),
    constraint fk_usuario_idioma_064RB foreign key (IdIdioma_064RB) REFERENCES Idioma_064RB(IdIdioma_064RB)
);
go
go
create table Bitacora_064RB (
    IdBitacora_064RB INT IDENTITY(1,1) not null,
    Dni_064RB VARCHAR(8) not null,
    Fecha_064RB DATETIME not null,
    Modulo_064RB VARCHAR(50) not null,
    Evento_064RB VARCHAR(50) not null,
    Criticidad_064RB TINYINT not null,
    CONSTRAINT pk_bitacora_064RB PRIMARY KEY (IdBitacora_064RB),
    CONSTRAINT fk_bitacora_usuario_064RB FOREIGN KEY (Dni_064RB) REFERENCES Usuario_064RB(Dni_064RB)
);
go

--
-- Tablas de negocio
--

go
create table Cliente_064RB (
    Dni_064RB varchar(8) not null,
    Nombre_064RB varchar(50) not null,
    Apellido_064RB varchar(50) not null,
    Telefono_064RB varchar(20),
    Email_064RB varchar(100),
    FechaNacimiento_064RB date not null,
    constraint pk_cliente_064RB primary key (Dni_064RB)
);
go
go
create table Producto_064RB (
    Sku_064RB varchar(50),
    Marca_064RB varchar(50) not null,
    Modelo_064RB varchar(50) not null,
    Precio_064RB decimal(18, 2) not null,
    Descripcion_064RB varchar(100),
    constraint pk_producto_064RB primary key (Sku_064RB)
);
go
go
create table Celular_064RB (
    Sku_064RB varchar(50) not null,
    Almacenamiento_064RB varchar(50) not null,
    Color_064RB varchar(50) not null,
    constraint pk_celular_064RB primary key (Sku_064RB),
    constraint fk_celular_producto_064RB foreign key (Sku_064RB) references Producto_064RB(Sku_064RB) on delete cascade
);
go
go
create table Accesorio_064RB (
    Sku_064RB varchar(50),
    Categoria_064RB varchar(100) not null,
    Stock_064RB int not null DEFAULT 0,
    constraint pk_accesorio_064RB primary key (Sku_064RB),
    constraint fk_accesorio_producto_064RB foreign key (Sku_064RB) references Producto_064RB(Sku_064RB) on delete cascade
);
go
go
create table UnidadCelular_064RB (
    Imei_064RB varchar(15) not null,
    Estado_064RB varchar(50) not null,
    SkuCelular_064RB varchar(50) not null,
    constraint pk_unidadcelular_064RB primary key (Imei_064RB),
    constraint fk_unidadcelular_producto_064RB foreign key (SkuCelular_064RB) references Celular_064RB(Sku_064RB)
);
go
go
create table Venta_064RB (
    NumVenta_064RB int identity(1,1) not null,
    FormaPago_064RB varchar(50) not null,
    MontoTotal_064RB decimal(18, 2) not null,
    Fecha_064RB datetime not null,
    Dni_064RB varchar(8) not null,
    constraint pk_venta_064RB primary key (NumVenta_064RB),
    constraint fk_venta_cliente_064RB foreign key (Dni_064RB) references Cliente_064RB(Dni_064RB)
);
go
go
create table DetalleVenta_064RB (
    IdDetalle_064RB int identity(1,1) not null,
    NumVenta_064RB int not null,
    SkuProducto_064RB varchar(50) not null,
    PrecioUnitario_064RB decimal(18, 2) not null,
    Cantidad_064RB int not null,
    Subtotal_064RB decimal(18, 2) not null,
    constraint pk_detalleVenta_064RB primary key (IdDetalle_064RB),
    constraint fk_detalleVenta_venta_064RB foreign key (NumVenta_064RB) references Venta_064RB(NumVenta_064RB) on delete cascade,
    constraint fk_detalleVenta_producto_064RB foreign key (SkuProducto_064RB) references Producto_064RB(Sku_064RB)
);
go
go
create table DetalleVenta_Imeis_064RB (
    IdDetalle_064RB int not null,
    Imei_064RB varchar(15) not null,
    constraint pk_detalleVentaImei_064RB primary key (IdDetalle_064RB, Imei_064RB),
    constraint fk_detalleVentaImei_detalleVenta_064RB foreign key (IdDetalle_064RB) references DetalleVenta_064RB(IdDetalle_064RB) on delete cascade,
    constraint fk_detalleVentaImei_unidadCelular_064RB foreign key (Imei_064RB) references UnidadCelular_064RB(Imei_064RB)
);
go

--
-- INSERT iniciales
--

-- INSERT para Permiso_064RB
insert into Permiso_064RB (Nombre_064RB)
values
('Crear Usuario'),
('Modificar Usuario'),
('Desbloquear Usuario'),
('Activar Usuario'),
('Desactivar Usuario'),
('Consultar Bitácora'),
('Exportar Bitácora'),
('Crear Rol'),
('Crear Familia'),
('Realizar Backup'),
('Realizar Restore'),
('Eliminar Rol'),
('Eliminar Familia');

-- INSERT permisos de Maestros
insert into Permiso_064RB (Nombre_064RB)
values
('Crear Producto'),
('Borrar Producto'),
('Modificar Producto'),
('Crear Unidad Celular'),
('Borrar Unidad Celular'),
('Modificar Unidad Celular'),
('Crear Cliente'),
('Borrar Cliente'),
('Modificar Cliente'),
('Generar Reporte de Ventas'),
('Realizar Venta');

insert into Idioma_064RB (IdIdioma_064RB, Nombre_064RB)
values
('es-AR','Español (Argentina)'),
('en-US','English (United States)');


insert into Rol_064RB (Nombre_064RB)
values
('Administrador');

INSERT INTO Usuario_064RB
(Dni_064RB,Nombre_064RB,Apellido_064RB,Correo_064RB,NombreUsuario_064RB,Password_064RB,Bloqueo_064RB,Activo_064RB,CantIntentos_064RB,IdRol_064RB,IdIdioma_064RB)
VALUES('12345678','Bruno','Roca','admin@sgv.com','admin',LOWER(CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', 'Admin!123'), 2)),0,1,0,1,'es-AR');

-- Asignar todos los permisos existentes al rol Administrador (idRol = 1)
INSERT INTO Rol_permiso_064RB (IdRol_064RB, IdPermiso_064RB)
SELECT 1, IdPermiso_064RB
FROM Permiso_064RB;

-- Prueba de inconsistencia
-- insert into Rol_064RB(Nombre_064RB) values ('test');

--
-- Consultas SELECT
--

select * from Idioma_064RB;
select * from Usuario_064RB;
select * from Bitacora_064RB;
select * from Permiso_064RB;
select * from Rol_064RB;
select * from Familia_064RB;
select * from DV_064RB;
select * from DV_Registro_064RB;

-- CONSULTAS TEST PARA ARMAR EL COMPOSITE
-- Consulto a una familia determinada
select IdFamilia_064RB, Nombre_064RB from Familia_064RB where IdFamilia_064RB = 1;

-- Consulto los permisos de una familia determinada
select p.IdPermiso_064RB, p.Nombre_064RB from Permiso_064RB p
inner join Permiso_familia_064RB pf
on p.IdPermiso_064RB = pf.IdPermiso_064RB
where pf.IdFamilia_064RB = 1;

-- Consulto las familias hijas de una familia determinada (familia padre)
select f.IdFamilia_064RB, f.Nombre_064RB from Familia_064RB f 
inner join Familia_familia_064RB ff
on f.IdFamilia_064RB = ff.IdFamiliaHija_064RB
where ff.IdFamiliaPadre_064RB = 1;



/*
    usuarios test:
        admin
        Admin!123
*/


select * from Producto_064RB;
select * from Accesorio_064RB;
select * from Celular_064RB;
select * from Cliente_064RB;
select * from Venta_064RB;
select * from UnidadCelular_064RB;
select * from DetalleVenta_064RB;
select * from DetalleVenta_Imeis_064RB;

-- consultar celular
select p.Sku_064RB, p.Marca_064RB, p.Modelo_064RB, p.Precio_064RB, p.Descripcion_064RB, c.Almacenamiento_064RB, c.Color_064RB from Producto_064RB p
inner join Celular_064RB c
on p.Sku_064RB = c.Sku_064RB

-- consultar accesorio
select p.Sku_064RB, p.Marca_064RB, p.Modelo_064RB, p.Precio_064RB, p.Descripcion_064RB, a.Categoria_064RB, a.Stock_064RB from Producto_064RB p
inner join Accesorio_064RB a
on p.Sku_064RB = a.Sku_064RB

-- consultar unidad celulares por accesorio
select * from UnidadCelular_064RB;

-- consultar celular por sku
select p.Sku_064RB, p.Marca_064RB, p.Modelo_064RB, p.Precio_064RB, p.Descripcion_064RB, c.Almacenamiento_064RB, c.Color_064RB
from Producto_064RB p
inner join Celular_064RB c on p.Sku_064RB = c.Sku_064RB
where c.Sku_064RB='CEL001'

-- consultar accesorio por sku
select p.Sku_064RB, p.Marca_064RB, p.Modelo_064RB, p.Precio_064RB, p.Descripcion_064RB, a.Categoria_064RB, a.Stock_064RB
from Producto_064RB p
inner join Accesorio_064RB a on p.Sku_064RB = a.Sku_064RB
where a.Sku_064RB='ACC001'
