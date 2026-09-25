CREATE DATABASE bd_sgvcelulares;
USE bd_sgvcelulares;

GO
CREATE TABLE Idioma_064RB (
    idIdioma varchar(8) not null,
    nombre varchar(50) not null,
    constraint PK_idioma primary key (idIdioma)
);
GO

GO
CREATE TABLE Dvv_064RB(
    nombreTabla varchar(50) not null,
    valor varchar(64) not null,
    constraint PK_DVV primary key (nombreTabla)
);
GO

GO
CREATE TABLE Dvh_064RB(
    nombreTabla varchar(50) not null,
    idRegistro varchar(50) not null,
    valor varchar(64) not null,
    constraint PK_DVH primary key (nombreTabla, idRegistro)
);
GO

-- Composite

GO
create table Rol_064RB (
    idRol int identity(1,1) not null,
    nombre varchar(50) not null,
    constraint pk_rol primary key (idRol)
);
GO

GO
create table Familia_064RB(
    idFamilia int identity(1,1) not null,
    nombre varchar(50) not null,
    constraint pk_familia primary key (idFamilia)
);
GO

GO
create table Rol_familia_064RB(
    idRol int not null,
    idFamilia int not null,
    constraint pk_rol_familia primary key (idRol, idFamilia),
    constraint fk_rol_familia_rol foreign key (idRol) references rol(idRol),
    constraint fk_rol_familia_familia foreign key (idFamilia) references familia(idFamilia)
);
GO

GO
create table Familia_familia_064RB(
    idFamiliaPadre int not null,
    idFamiliaHija int not null,
    constraint pk_familia_familia primary key (idFamiliaPadre, idFamiliaHija),
    constraint fk_ff_padre foreign key (idFamiliaPadre) references familia(idFamilia),
    constraint fk_ff_hija foreign key (idFamiliaHija) references familia(idFamilia)
);
GO

GO
create table Permiso_064RB(
    idPermiso int identity(1,1) not null,
    nombre varchar(50) not null,
    constraint pk_permiso primary key (idPermiso)
);
GO

GO
create table Permiso_familia_064RB(
    idFamilia int not null,
    idPermiso int not null,
    constraint pk_permiso_familia primary key (idFamilia, idPermiso),
    constraint fk_permiso_familia_familia foreign key (idFamilia) references familia(idFamilia),
    constraint fk_permiso_familia_permiso foreign key (idPermiso) references permiso(idPermiso)
);
GO

GO
create table Rol_permiso_064RB(
    idRol int not null,
    idPermiso int not null,
    constraint pk_rol_permiso primary key (idRol, idPermiso),
    constraint fk_rol_permiso_rol foreign key (idRol) references rol(idRol),
    constraint fk_rol_permiso_permiso foreign key (idPermiso) references permiso(idPermiso)
);
GO

GO
CREATE TABLE Usuario_064RB (
    dni VARCHAR(8) NOT NULL,
    nombre VARCHAR(50) NOT NULL,
    apellido VARCHAR(50) NOT NULL,
    correo VARCHAR(50) NOT NULL,
    nombreUsuario VARCHAR(50) NOT NULL,
    password VARCHAR(64) NOT NULL,
    bloqueo BIT NOT NULL,
    activo BIT NOT NULL,
    cantIntentos TINYINT not null,
    idRol int not null,
    idIdioma varchar(8) not null,
    CONSTRAINT PK_usuario PRIMARY KEY (dni),
    CONSTRAINT FK_usuario_rol FOREIGN KEY (idRol) REFERENCES rol(idRol),
    CONSTRAINT FK_usuario_idioma FOREIGN KEY (idIdioma) REFERENCES idioma(idIdioma)
);
GO

GO
CREATE TABLE Bitacora_064RB (
    idBitacora INT IDENTITY(1,1) NOT NULL,
    dni VARCHAR(8) NOT NULL,
    fecha DATETIME NOT NULL,
    modulo VARCHAR(50) NOT NULL,
    evento VARCHAR(50) NOT NULL,
    criticidad TINYINT NOT NULL,
    CONSTRAINT PK_bitacora PRIMARY KEY (idBitacora),
    CONSTRAINT FK_bitacora_usuario FOREIGN KEY (dni) REFERENCES usuario(dni)
);
GO


-- INSERT para Permiso
insert into permiso_064RB (nombre)
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
insert into permiso_064RB (nombre)
values
('Crear Producto'),
('Borrar Producto'),
('Modificar Producto'),
('Agregar Unidad Celular'),
('Borrar Unidad Celular'),
('Modificar Unidad Celular'),
('Crear Cliente'),
('Borrar Cliente'),
('Modificar Cliente'),
('Generar Reporte de Ventas'),
('Realizar Venta');

insert into idioma_064RB (idIdioma, nombre)
values
('es-AR','Español (Argentina)'),
('en-US','English (United States)');


insert into rol_064RB (nombre)
values
('Administrador');

INSERT INTO usuario_064RB
(dni,nombre,apellido,correo,nombreUsuario,password,bloqueo,activo,cantIntentos,idRol,idIdioma)
VALUES('12345678','Bruno','Roca','admin@sgv.com','admin',LOWER(CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', 'Admin!123'), 2)),0,1,0,1,'es-AR');

-- Asignar todos los permisos existentes al rol Administrador (idRol = 1)
INSERT INTO Rol_permiso_064RB (idRol, idPermiso)
SELECT 1, idPermiso
FROM permiso;

select * from Idioma_064RB;
select * from Usuario_064RB;
select * from Bitacora_064RB;
select * from Permiso_064RB;
select * from Rol_064RB;
select * from Familia_064RB;
select * from Dvv_064RB;
select * from Dvh_064RB;

-- CONSULTAS TEST PARA ARMAR EL COMPOSITE
-- Consulto a una familia determinada
select idFamilia, nombre from familia_064RB where idFamilia = 1;

-- Consulto los permisos de una familia determinada
select p.idPermiso, p.nombre from permiso_064RB p
inner join permiso_familia_064RB pf
on p.idPermiso = pf.idPermiso
where pf.idFamilia = 1;

-- Consulto las familias hijas de una familia determinada (familia padre)
select f.idFamilia, f.nombre from familia_064RB f 
inner join familia_familia_064RB ff
on f.idFamilia = ff.idFamiliaHija
where ff.idFamiliaPadre = 1;


-- Pruebas de inconsistencia
insert into rol_064RB(nombre) values ('test');
/*
    usuarios test:
        admin
        Admin!123
        
        gaymer77777777
        Gaymer!123
*/



--
-- TABLAS NEGOCIO
--

CREATE TABLE Cliente_064RB (
    dni VARCHAR(8) PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    apellido VARCHAR(50) NOT NULL,
    telefono VARCHAR(20) NULL,
    email VARCHAR(100) NULL,
    fechaNacimiento DATE NOT NULL
);

CREATE TABLE Producto_064RB (
    sku VARCHAR(50) PRIMARY KEY,
    marca VARCHAR(50) NOT NULL,
    modelo VARCHAR(50) NOT NULL,
    precio DECIMAL(18, 2) NOT NULL,
    descripcion VARCHAR(100) NULL
);

CREATE TABLE Celular_064RB (
    sku VARCHAR(50) PRIMARY KEY, -- PK y FK al mismo tiempo (Relación 1 a 1)
    almacenamiento VARCHAR(50) NOT NULL,
    color VARCHAR(50) NOT NULL,
    CONSTRAINT FK_Celulares_Productos FOREIGN KEY (sku) REFERENCES Producto_064RB(sku) ON DELETE CASCADE
);

CREATE TABLE Accesorio_064RB (
    sku VARCHAR(50) PRIMARY KEY, -- PK y FK al mismo tiempo (Relación 1 a 1)
    categoria VARCHAR(100) NOT NULL,
    stock INT NOT NULL DEFAULT 0,
    CONSTRAINT FK_Accesorios_Productos FOREIGN KEY (sku) REFERENCES Producto_064RB(sku) ON DELETE CASCADE
);

CREATE TABLE UnidadCelular_064RB (
    imei VARCHAR(15) PRIMARY KEY,
    estado VARCHAR(50) NOT NULL,
    skuCelular VARCHAR(50) NOT NULL,
    CONSTRAINT FK_Unidades_Celulares FOREIGN KEY (skuCelular) REFERENCES Celular_064RB(sku)
);

CREATE TABLE Venta_064RB (
    numVenta INT IDENTITY(1,1) PRIMARY KEY,
    formaPago VARCHAR(50) NOT NULL,
    montoTotal DECIMAL(18, 2) NOT NULL,
    fecha DATETIME NOT NULL,
    dniCliente VARCHAR(8) NOT NULL,
    CONSTRAINT FK_Ventas_Clientes FOREIGN KEY (dniCliente) REFERENCES Cliente_064RB(dni)
);

CREATE TABLE DetalleVenta_064RB (
    idDetalle INT IDENTITY(1,1) PRIMARY KEY,
    numVenta INT NOT NULL,
    skuProducto VARCHAR(50) NOT NULL,
    precioUnitario DECIMAL(18, 2) NOT NULL,
    cantidad INT NOT NULL,
    subtotal DECIMAL(18, 2) NOT NULL,
    CONSTRAINT FK_Detalles_Ventas FOREIGN KEY (numVenta) REFERENCES Venta_064RB(numVenta) ON DELETE CASCADE,
    CONSTRAINT FK_Detalles_Productos FOREIGN KEY (skuProducto) REFERENCES Producto_064RB(sku)
);

CREATE TABLE DetalleVenta_Imeis_064RB (
    id_detalle INT NOT NULL,
    imei VARCHAR(15) NOT NULL,
    PRIMARY KEY (id_detalle, imei),
    CONSTRAINT FK_DetalleImeis_Detalles FOREIGN KEY (id_detalle) REFERENCES DetalleVenta_064RB(idDetalle) ON DELETE CASCADE,
    CONSTRAINT FK_DetalleImeis_Unidades FOREIGN KEY (imei) REFERENCES UnidadCelular_064RB(imei)
);


select * from Producto_064RB;
select * from Accesorio_064RB;
select * from Celular_064RB;
select * from Cliente_064RB;
select * from UnidadCelular_064RB;
select * from Venta_064RB;
select * from DetalleVenta_064RB;
select * from DetalleVenta_Imeis_064RB;

delete from Producto_064RB;

select p.sku, p.marca, p.modelo, p.precio, p.descripcion, c.almacenamiento, c.color
from Producto_064RB p
inner join Celular_064RB c ON p.sku = c.sku
where c.sku='CEL001'

select p.sku, p.marca, p.modelo, p.precio, p.descripcion, a.categoria, a.stock
from Producto_064RB p
inner join Accesorio_064RB a ON p.sku = a.sku
where a.sku='ACC001'