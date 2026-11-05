create database bd_santsung_064RB;
use bd_santsung_064RB;

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
    constraint fk_usuario_idioma_064RB foreign key (IdIdioma_064RB) references Idioma_064RB(IdIdioma_064RB)
);
go
go
create table Bitacora_064RB (
    IdBitacora_064RB int identity(1,1) not null,
    Dni_064RB varchar(8) not null,
    Fecha_064RB datetime not null,
    Modulo_064RB varchar(50) not null,
    Evento_064RB varchar(50) not null,
    Criticidad_064RB tinyint not null,
    constraint pk_bitacora_064RB primary key (IdBitacora_064RB),
    constraint fk_bitacora_usuario_064RB foreign key (Dni_064RB) references Usuario_064RB(Dni_064RB)
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
    Activo_064RB bit not null default 1,
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
    StockMinimo_064RB int not null default 0,
    Activo_064RB bit not null default 1,
    constraint pk_producto_064RB primary key (Sku_064RB),
    constraint chk_producto_stockminimo check (StockMinimo_064RB >= 0)
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
    Activo_064RB bit not null default 1,
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
go
create table Proveedor_064RB (
    Cuit_064RB varchar(11) not null,
    RazonSocial_064RB varchar(100) not null,
    Activo_064RB bit not null default 1,
    constraint pk_proveedor_064RB primary key (Cuit_064RB)
);
go
go
create table SolicitudCompra_064RB (
    IdSolicitud_064RB int identity(1,1),
    SkuProducto_064RB varchar(50) not null,
    Cantidad_064RB int not null check (Cantidad_064RB > 0),
    Estado_064RB varchar(30) not null default 'Pendiente de compra' check (Estado_064RB in ('Pendiente de compra','Cotizada','Aprobada','Rechazada','Pendiente de recepción','Recibida')),
    FechaSolicitud_064RB datetime not null default GETDATE(),
    -- paso 3: cotización
    CuitProveedor_064RB varchar(11),
    PrecioUnitario_064RB decimal(18,2),
    MontoTotal_064RB decimal(18,2),
    -- paso 4: decisión del gerente
    FechaDecision_064RB datetime,
    Motivo_064RB varchar(200),
    constraint pk_solicitudCompra_064RB primary key (IdSolicitud_064RB),
    constraint fk_solicitudCompra_producto_064RB foreign key (SkuProducto_064RB) references Producto_064RB(Sku_064RB),
    constraint fk_solicitudCompra_proveedor_064RB foreign key (CuitProveedor_064RB) references Proveedor_064RB(Cuit_064RB)
);
go
go
create unique index ux_solicitud_activa_064RB on SolicitudCompra_064RB(SkuProducto_064RB)
where Estado_064RB <> 'Rechazada' and Estado_064RB <> 'Recibida';
go
go
create table OrdenCompra_064RB (
    NumOrden_064RB int identity(1,1),
    IdSolicitud_064RB int not null,
    Fecha_064RB datetime not null default GETDATE(),
    MontoPagado_064RB decimal(18,2) not null check (MontoPagado_064RB > 0),
    constraint pk_ordenCompra_064RB primary key (NumOrden_064RB),
    constraint fk_ordenCompra_solicitud_064RB foreign key (IdSolicitud_064RB) references SolicitudCompra_064RB(IdSolicitud_064RB),
    constraint uq_idSolicitud_064RB unique (IdSolicitud_064RB)
);
go
go
create table RemitoCompra_064RB (
    IdRemito_064RB int identity(1,1) not null,
    NumOrden_064RB int not null,
    NumRemito_064RB int not null,
    Estado_064RB varchar(20) not null,
    FechaRecepcion_064RB datetime not null default GETDATE(),
    constraint pk_remito_064RB primary key (IdRemito_064RB),
    constraint fk_remito_orden_064RB foreign key (NumOrden_064RB) references OrdenCompra_064RB(NumOrden_064RB),
    constraint uq_remito_orden_064RB unique (NumOrden_064RB),
    constraint chk_remito_estado_064RB check (Estado_064RB in ('Conforme', 'No conforme'))
);
go
go
create table Discrepancia_064RB (
    IdRemito_064RB int not null,
    CantidadFaltante_064RB int not null default 0,
    CantidadDanada_064RB int not null default 0,
    Observaciones_064RB varchar(300) not null,
    constraint pk_discrepancia_064RB primary key (IdRemito_064RB),
    constraint fk_discrepancia_remito_064RB foreign key (IdRemito_064RB) references RemitoCompra_064RB(IdRemito_064RB),
    constraint chk_discrepancia_faltante_064RB check (CantidadFaltante_064RB >= 0),
    constraint chk_discrepancia_danada_064RB check (CantidadDanada_064RB >= 0),
    constraint chk_discrepancia_algo_064RB check (CantidadFaltante_064RB + CantidadDanada_064RB > 0),
    constraint chk_discrepancia_observaciones_064RB check (LEN(LTRIM(RTRIM(Observaciones_064RB))) > 0)
);
go
go
CREATE TABLE Accesorio_C_064RB (
    Id_064RB int identity(1,1) not null,
    Sku_064RB varchar(50) not null,
    Marca_064RB varchar(50) not null,
    Modelo_064RB varchar(50) not null,
    Precio_064RB decimal(18,2) not null,
    Descripcion_064RB varchar(100),
    StockMinimo_064RB int not null,
    Categoria_064RB varchar(100) not null,
    Stock_064RB int not null,
    ActivoProducto_064RB bit not null,
    FechaHora_064RB datetime not null default GETDATE(),
    Act_064RB bit NOT NULL default 1,
    constraint PK_Accesorio_C_064RB primary key (Id_064RB),
    constraint FK_Accesorio_C_Accesorio_064RB foreign key (Sku_064RB) references Accesorio_064RB(Sku_064RB),
    constraint CK_Accesorio_C_Stock_064RB check (Stock_064RB >= 0),
    constraint CK_Accesorio_C_StockMinimo_064RB check (StockMinimo_064RB >= 0),
    constraint CK_Accesorio_C_Precio_064RB check (Precio_064RB >= 0),
);
go
go
create unique index ux_accesorio_c_activo_064RB on Accesorio_C_064RB (Sku_064RB) where Act_064RB = 1;
go
-- Trigger de producto
go
create trigger tr_producto_actualizado_064RB
on Producto_064RB
after update
as
begin
    set nocount on;
    -- Permite omitir la auditoría durante una restauración
    if TRY_CONVERT(bit, SESSION_CONTEXT(N'omitir_auditoria_accesorio')) = 1 return;
    if TRY_CONVERT(bit, SESSION_CONTEXT(N'omitir_auditoria_producto')) = 1 return;
    declare @Cambios table (
        Sku_064RB varchar(50) primary key
    );
    -- Identificar accesorios cuyos atributos comunes cambiaron
    insert into @Cambios (Sku_064RB)
    select i.Sku_064RB from inserted i
    inner join deleted d on d.Sku_064RB = i.Sku_064RB
    inner join Accesorio_064RB a on a.Sku_064RB = i.Sku_064RB
    where exists (
        select i.Marca_064RB, i.Modelo_064RB, i.Precio_064RB, i.Descripcion_064RB, i.StockMinimo_064RB, i.Activo_064RB
        except
        select d.Marca_064RB, d.Modelo_064RB, d.Precio_064RB, d.Descripcion_064RB, d.StockMinimo_064RB, d.Activo_064RB
    );
    -- Desactivar la versión histórica anterior
    update h set Act_064RB = 0 from Accesorio_C_064RB h
    inner join @Cambios c on c.Sku_064RB = h.Sku_064RB
    where h.Act_064RB = 1;
    -- Registrar la nueva versión
    insert into Accesorio_C_064RB (Sku_064RB, Marca_064RB, Modelo_064RB, Precio_064RB, Descripcion_064RB, StockMinimo_064RB, Categoria_064RB, Stock_064RB, ActivoProducto_064RB, FechaHora_064RB, Act_064RB)
    select i.Sku_064RB, i.Marca_064RB, i.Modelo_064RB, i.Precio_064RB, i.Descripcion_064RB, i.StockMinimo_064RB, a.Categoria_064RB, a.Stock_064RB, i.Activo_064RB, GETDATE(), 1
    from inserted i
    inner join Accesorio_064RB a on a.Sku_064RB = i.Sku_064RB
    inner join @Cambios c on c.Sku_064RB = i.Sku_064RB;
end;
go

-- Trigger de Accesorio
go
create trigger tr_accesorio_actualizado_064RB
on Accesorio_064RB
after insert, update
as
begin
    set nocount on;
    -- Permite omitir la auditoría durante una restauración
    if TRY_CONVERT(bit, SESSION_CONTEXT(N'omitir_auditoria_accesorio')) = 1 return;
    declare @Cambios table (
        Sku_064RB varchar(50) primary key
    );
    -- Identificar accesorios nuevos o con modificaciones reales
    insert into @Cambios (Sku_064RB)
    select i.Sku_064RB from inserted i
    left join deleted d on d.Sku_064RB = i.Sku_064RB
    where d.Sku_064RB is null or exists (
        select i.Categoria_064RB, i.Stock_064RB
        except
        select d.Categoria_064RB, d.Stock_064RB
    );
    -- Desactivar la versión histórica anterior
    update h set Act_064RB = 0 from Accesorio_C_064RB h
    inner join @Cambios c on c.Sku_064RB = h.Sku_064RB
    where h.Act_064RB = 1;
    -- Registrar la nueva versión
    insert into Accesorio_C_064RB (Sku_064RB, Marca_064RB, Modelo_064RB, Precio_064RB, Descripcion_064RB, StockMinimo_064RB, Categoria_064RB, Stock_064RB, ActivoProducto_064RB, FechaHora_064RB, Act_064RB)
    select p.Sku_064RB, p.Marca_064RB, p.Modelo_064RB, p.Precio_064RB, p.Descripcion_064RB, p.StockMinimo_064RB, i.Categoria_064RB, i.Stock_064RB, p.Activo_064RB, GETDATE(), 1
    from inserted i
    inner join Producto_064RB p on p.Sku_064RB = i.Sku_064RB
    inner join @Cambios c on c.Sku_064RB = i.Sku_064RB;
end;
go

go
create trigger tr_accesorio_c_restaurar_064RB
on Accesorio_C_064RB
after update
as
begin
    set nocount on;
    -- Evitar recursividad durante una restauración
    if TRY_CONVERT(bit, SESSION_CONTEXT(N'omitir_restauracion_accesorio')) = 1 return;
    -- Detectar las versiones que pasaron de inactivas a activas
    declare @Versiones table (
        Id_064RB int primary key
    );
    insert into @Versiones (Id_064RB)
    select i.Id_064RB from inserted i
    inner join deleted d on d.Id_064RB = i.Id_064RB
    where d.Act_064RB = 0 and i.Act_064RB = 1;
    if not exists (select 1 from @Versiones) return;
    begin try
        exec sys.sp_set_session_context @key = N'omitir_auditoria_accesorio', @value = 1;
        exec sys.sp_set_session_context @key = N'omitir_auditoria_producto', @value = 1;
        -- Evitar que los cambios internos vuelvan a disparar este trigger de restauración
        exec sys.sp_set_session_context @key = N'omitir_restauracion_accesorio', @value = 1;
        -- Restaurar los atributos generales del producto
        update p set Marca_064RB = h.Marca_064RB, Modelo_064RB = h.Modelo_064RB, Precio_064RB = h.Precio_064RB, Descripcion_064RB = h.Descripcion_064RB, StockMinimo_064RB = h.StockMinimo_064RB, Activo_064RB = h.ActivoProducto_064RB
        from Producto_064RB p
        inner join inserted i on i.Sku_064RB = p.Sku_064RB
        inner join @Versiones v on v.Id_064RB = i.Id_064RB
        inner join Accesorio_C_064RB h on h.Id_064RB = i.Id_064RB;
        -- Restaurar los atributos propios del accesorio
        update a set Categoria_064RB = h.Categoria_064RB, Stock_064RB = h.Stock_064RB
        from Accesorio_064RB a
        inner join inserted i on i.Sku_064RB = a.Sku_064RB
        inner join @Versiones v on v.Id_064RB = i.Id_064RB
        inner join Accesorio_C_064RB h on h.Id_064RB = i.Id_064RB;
        -- Restablecer los contextos
        exec sys.sp_set_session_context @key = N'omitir_auditoria_accesorio', @value = NULL;
        exec sys.sp_set_session_context @key = N'omitir_auditoria_producto', @value = NULL;
        exec sys.sp_set_session_context @key = N'omitir_restauracion_accesorio', @value = NULL;
    end try
    begin catch
        exec sys.sp_set_session_context @key = N'omitir_auditoria_accesorio', @value = NULL;
        exec sys.sp_set_session_context @key = N'omitir_auditoria_producto', @value = NULL;
        exec sys.sp_set_session_context @key = N'omitir_restauracion_accesorio', @value = NULL;
        throw;
    end catch;
end;
go
-- Procedimiento almacenado para modificar accesorio
go
create procedure pa_accesorio_modificar_064RB
    @Sku varchar(50),
    @Marca varchar(50),
    @Modelo varchar(50),
    @Precio decimal(18,2),
    @Descripcion varchar(100),
    @StockMinimo int,
    @Categoria varchar(100),
    @Stock int
as
begin
    set nocount on;
    set XACT_ABORT on;
    begin try
        begin transaction;
        -- Comprobar si cambiaron los atributos propios del accesorio
        if exists (
            select 1 from Accesorio_064RB a where a.Sku_064RB = @Sku
              and exists (select @Categoria, @Stock except select a.Categoria_064RB, a.Stock_064RB)
        )
        begin
            exec sys.sp_set_session_context @key = N'omitir_auditoria_producto', @value = 1;
        end
        else
        begin
            exec sys.sp_set_session_context @key = N'omitir_auditoria_producto', @value = NULL;
        end;
        -- Modificar Producto
        update Producto_064RB
        set Marca_064RB = @Marca, Modelo_064RB = @Modelo, Precio_064RB = @Precio, Descripcion_064RB = @Descripcion, StockMinimo_064RB = @StockMinimo
        where Sku_064RB = @Sku;
        -- Habilitar nuevamente la auditoría de Producto
        exec sys.sp_set_session_context @key = N'omitir_auditoria_producto', @value = NULL;
        -- Modificar Accesorio
        update Accesorio_064RB
        set Categoria_064RB = @Categoria, Stock_064RB = @Stock
        where Sku_064RB = @Sku;
        commit transaction;
    end try
    begin catch
        exec sys.sp_set_session_context @key = N'omitir_auditoria_producto', @value = NULL;
        if XACT_STATE() <> 0 rollback transaction;
        throw;
    end catch;
end;
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
('Eliminar Rol'),
('Eliminar Familia'),

('Realizar Backup'),
('Realizar Restore');

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

('Crear Proveedor'),
('Borrar Proveedor'),
('Modificar Proveedor'),

('Realizar Venta'),
('Procesar Pago'),
('Emitir Comprobante de Venta'),

('Solicitar Reposicion'),
('Cotizar Solicitud'),
('Evaluar Solicitud'),
('Efectuar Pedido al Proveedor'),

('Recibir Mercadería'),
('Registrar Discrepancia'),

('Generar Reporte de Ventas'),
('Generar Reporte de Stock');

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
