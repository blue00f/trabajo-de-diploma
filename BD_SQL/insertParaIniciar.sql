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
('Generar Reporte de Stock'),
('Modificar Accesorio Copia');

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