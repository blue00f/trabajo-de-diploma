use bd_santsung_064RB;

select * from Idioma_064RB;
select * from Usuario_064RB;
select * from Bitacora_064RB;
select * from Permiso_064RB;
select * from Rol_064RB;
select * from Familia_064RB;
select * from DV_064RB;
select * from DV_Registro_064RB;


/*
    usuarios test:
        admin
        Admin!123
*/

-- Tablas para roles y permisos para persistir el composite
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


-- Tablas de negocio
select * from Accesorio_C_064RB;

select * from Producto_064RB;
select * from Accesorio_064RB;
select * from Celular_064RB;
select * from Cliente_064RB;
select * from Venta_064RB;
select * from UnidadCelular_064RB;
select * from DetalleVenta_064RB;
select * from DetalleVenta_Imeis_064RB;

select * from Proveedor_064RB;
select * from SolicitudCompra_064RB;
select * from OrdenCompra_064RB;
select * from RemitoCompra_064RB;
select * from Discrepancia_064RB;

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