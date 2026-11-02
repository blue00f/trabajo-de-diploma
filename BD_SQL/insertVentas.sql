BEGIN TRY
    BEGIN TRANSACTION;

    -------------------------------------------------------
    -- 1. CLIENTES
    -------------------------------------------------------

    INSERT INTO Cliente_064RB
    (Dni_064RB, Nombre_064RB, Apellido_064RB,
     Telefono_064RB, Email_064RB, FechaNacimiento_064RB)
    VALUES
    ('30000001', 'Juan', 'Perez', '1123456789', 'juan.perez@gmail.com', '1995-04-12'),
    ('30000002', 'Maria', 'Gonzalez', '1134567890', 'maria.g@gmail.com', '1998-08-21'),
    ('30000003', 'Lucas', 'Fernandez', '1145678901', 'lucas.f@gmail.com', '1992-01-15'),
    ('30000004', 'Sofia', 'Rodriguez', '1156789012', 'sofia.r@gmail.com', '2000-11-03'),
    ('30000005', 'Martin', 'Lopez', '1167890123', 'martin.l@gmail.com', '1990-06-25'),
    ('30000006', 'Camila', 'Martinez', '1178901234', 'camila.m@gmail.com', '1997-02-18'),
    ('30000007', 'Nicolas', 'Garcia', '1189012345', 'nicolas.g@gmail.com', '1994-09-09'),
    ('30000008', 'Valentina', 'Sanchez', '1190123456', 'valentina.s@gmail.com', '2001-12-30'),
    ('30000009', 'Agustin', 'Romero', '1101234567', 'agustin.r@gmail.com', '1996-05-17'),
    ('30000010', 'Florencia', 'Diaz', '1112345678', 'florencia.d@gmail.com', '1999-07-07');


    -------------------------------------------------------
    -- 2. PRODUCTOS: CELULARES
    -------------------------------------------------------

    INSERT INTO Producto_064RB
    (Sku_064RB, Marca_064RB, Modelo_064RB,
     Precio_064RB, Descripcion_064RB, StockMinimo_064RB)
    VALUES
    ('CEL000', 'Samsung', 'Galaxy S24', 950000, 'Samsung Galaxy S24', 2),
    ('CEL001', 'Samsung', 'Galaxy A55', 550000, 'Samsung Galaxy A55', 2),
    ('CEL002', 'Apple', 'iPhone 15', 1200000, 'Apple iPhone 15', 1),
    ('CEL003', 'Apple', 'iPhone 14', 950000, 'Apple iPhone 14', 1),
    ('CEL004', 'Motorola', 'Edge 50', 750000, 'Motorola Edge 50', 2),
    ('CEL005', 'Motorola', 'Moto G85', 450000, 'Motorola Moto G85', 2),
    ('CEL006', 'Xiaomi', 'Redmi Note 13', 380000, 'Xiaomi Redmi Note 13', 2),
    ('CEL007', 'Xiaomi', 'Poco X6', 520000, 'Xiaomi Poco X6', 2),
    ('CEL008', 'Samsung', 'Galaxy A15', 290000, 'Samsung Galaxy A15', 2),
    ('CEL009', 'Apple', 'iPhone 13', 800000, 'Apple iPhone 13', 1);


    -------------------------------------------------------
    -- 3. PRODUCTOS: ACCESORIOS
    -------------------------------------------------------

    INSERT INTO Producto_064RB
    (Sku_064RB, Marca_064RB, Modelo_064RB,
     Precio_064RB, Descripcion_064RB, StockMinimo_064RB)
    VALUES
    ('ACC000', 'Samsung', 'Cargador 25W', 35000, 'Cargador original 25W', 5),
    ('ACC001', 'Apple', 'Cargador USB-C', 45000, 'Cargador USB-C Apple', 5),
    ('ACC002', 'JBL', 'Tune 510BT', 85000, 'Auriculares Bluetooth', 3),
    ('ACC003', 'Xiaomi', 'Redmi Buds 4', 65000, 'Auriculares inalámbricos', 3),
    ('ACC004', 'Kingston', 'MicroSD 128GB', 30000, 'Memoria MicroSD 128GB', 5),
    ('ACC005', 'Sandisk', 'Pendrive 64GB', 18000, 'Pendrive USB 64GB', 5),
    ('ACC006', 'Genérico', 'Funda Silicona', 12000, 'Funda protectora', 5),
    ('ACC007', 'Tempered', 'Vidrio Templado', 8000, 'Protector de pantalla', 5),
    ('ACC008', 'Anker', 'Powerbank 10000mAh', 75000, 'Batería portátil', 3),
    ('ACC009', 'Logitech', 'Mouse M170', 25000, 'Mouse inalámbrico', 3);


    -------------------------------------------------------
    -- 4. TABLAS ESPECIALIZADAS
    -------------------------------------------------------

    INSERT INTO Celular_064RB
    (Sku_064RB, Almacenamiento_064RB, Color_064RB)
    VALUES
    ('CEL000', '256GB', 'Negro'),
    ('CEL001', '128GB', 'Azul'),
    ('CEL002', '128GB', 'Negro'),
    ('CEL003', '128GB', 'Blanco'),
    ('CEL004', '256GB', 'Verde'),
    ('CEL005', '256GB', 'Azul'),
    ('CEL006', '128GB', 'Negro'),
    ('CEL007', '256GB', 'Gris'),
    ('CEL008', '128GB', 'Negro'),
    ('CEL009', '128GB', 'Blanco');


    INSERT INTO Accesorio_064RB
    (Sku_064RB, Categoria_064RB, Stock_064RB)
    VALUES
    ('ACC000', 'Cargadores', 50),
    ('ACC001', 'Cargadores', 50),
    ('ACC002', 'Auriculares', 50),
    ('ACC003', 'Auriculares', 50),
    ('ACC004', 'Almacenamiento', 50),
    ('ACC005', 'Almacenamiento', 50),
    ('ACC006', 'Fundas', 50),
    ('ACC007', 'Protectores', 50),
    ('ACC008', 'Baterias portatiles', 50),
    ('ACC009', 'Perifericos', 50);


    -------------------------------------------------------
    -- 5. UNIDADES DE CELULARES
    -------------------------------------------------------
    -- Cada celular tendrá tres unidades físicas.
    -- La primera unidad de cada SKU estará vendida.
    -- Las otras dos estarán disponibles.
    -------------------------------------------------------

    ;WITH Numeros AS
    (
        SELECT *
        FROM (VALUES
            (0), (1), (2), (3), (4),
            (5), (6), (7), (8), (9)
        ) AS N(Numero)
    ),
    Unidades AS
    (
        SELECT
            Numero,
            Unidad,
            '35' +
            RIGHT(
                REPLICATE('0', 13) +
                CAST((Numero * 3 + Unidad) AS VARCHAR(13)),
                13
            ) AS Imei
        FROM Numeros
        CROSS JOIN (VALUES (1), (2), (3)) AS U(Unidad)
    )
    INSERT INTO UnidadCelular_064RB
    (Imei_064RB, Estado_064RB, SkuCelular_064RB)
    SELECT
        Imei,
        CASE
            WHEN Unidad = 1 THEN 'Vendido'
            ELSE 'Disponible'
        END,
        'CEL' + RIGHT('000' + CAST(Numero AS VARCHAR(3)), 3)
    FROM Unidades;


    -------------------------------------------------------
    -- 6. PREPARACIÓN DE LAS 30 VENTAS
    -------------------------------------------------------

    DECLARE @VentasSeed TABLE
    (
        SeedId INT PRIMARY KEY,
        FormaPago VARCHAR(50),
        Fecha DATETIME,
        Dni VARCHAR(8),
        MontoTotal DECIMAL(18,2)
    );

    INSERT INTO @VentasSeed
    (SeedId, FormaPago, Fecha, Dni, MontoTotal)
    SELECT
        N.Numero,
        CASE (N.Numero - 1) % 4
            WHEN 0 THEN 'Efectivo'
            WHEN 1 THEN 'Débito'
            WHEN 2 THEN 'Crédito'
            WHEN 3 THEN 'Transferencia'
        END,
        DATEADD(
            DAY,
            (N.Numero - 1) * 2,
            CAST('2026-09-01T10:00:00' AS DATETIME)
        ),
        '300000' + RIGHT('0' + CAST(((N.Numero - 1) % 10) + 1 AS VARCHAR(2)), 2),
        0
    FROM
    (
        SELECT Numero
        FROM (VALUES
            (1),(2),(3),(4),(5),(6),(7),(8),(9),(10),
            (11),(12),(13),(14),(15),(16),(17),(18),(19),(20),
            (21),(22),(23),(24),(25),(26),(27),(28),(29),(30)
        ) AS X(Numero)
    ) AS N;


    -------------------------------------------------------
    -- 7. PREPARACIÓN DE LOS DETALLES DE VENTA
    -------------------------------------------------------

    DECLARE @DetallesSeed TABLE
    (
        IdSeedDetalle INT IDENTITY(1,1) PRIMARY KEY,
        SeedId INT,
        Sku VARCHAR(50),
        Cantidad INT,
        Imei VARCHAR(15) NULL
    );

    -- Cada venta tendrá dos accesorios.

    INSERT INTO @DetallesSeed
    (SeedId, Sku, Cantidad, Imei)
    SELECT
        V.SeedId,
        'ACC' + RIGHT(
            '000' + CAST(((V.SeedId - 1) % 10) AS VARCHAR(3)),
            3
        ),
        1 + (V.SeedId % 3),
        NULL
    FROM @VentasSeed V;

    INSERT INTO @DetallesSeed
    (SeedId, Sku, Cantidad, Imei)
    SELECT
        V.SeedId,
        'ACC' + RIGHT(
            '000' + CAST(((V.SeedId + 2) % 10) AS VARCHAR(3)),
            3
        ),
        1 + (V.SeedId % 2),
        NULL
    FROM @VentasSeed V;


    -- Diez ventas incluirán además un celular.
    -- Se asigna una unidad física específica a cada venta.
    -- Ventas 1, 4, 7, 10, ..., 28.

    INSERT INTO @DetallesSeed
    (SeedId, Sku, Cantidad, Imei)
    SELECT
        V.SeedId,
        'CEL' + RIGHT(
            '000' + CAST(((V.SeedId - 1) / 3) AS VARCHAR(3)),
            3
        ),
        1,
        '35' + RIGHT(
            REPLICATE('0', 13) +
            CAST((((V.SeedId - 1) / 3) * 3 + 1) AS VARCHAR(13)),
            13
        )
    FROM @VentasSeed V
    WHERE (V.SeedId - 1) % 3 = 0;


    -------------------------------------------------------
    -- 8. CÁLCULO DE LOS TOTALES
    -------------------------------------------------------

    UPDATE V
    SET MontoTotal = T.Total
    FROM @VentasSeed V
    INNER JOIN
    (
        SELECT
            D.SeedId,
            SUM(P.Precio_064RB * D.Cantidad) AS Total
        FROM @DetallesSeed D
        INNER JOIN Producto_064RB P
            ON P.Sku_064RB = D.Sku
        GROUP BY D.SeedId
    ) T
        ON T.SeedId = V.SeedId;


    -------------------------------------------------------
    -- 9. INSERTAR LAS VENTAS
    -------------------------------------------------------

    DECLARE @VentaMap TABLE
    (
        SeedId INT PRIMARY KEY,
        NumVenta INT
    );

    MERGE INTO Venta_064RB AS DESTINO
    USING @VentasSeed AS ORIGEN
        ON 1 = 0
    WHEN NOT MATCHED THEN
        INSERT
        (
            FormaPago_064RB,
            MontoTotal_064RB,
            Fecha_064RB,
            Dni_064RB
        )
        VALUES
        (
            ORIGEN.FormaPago,
            ORIGEN.MontoTotal,
            ORIGEN.Fecha,
            ORIGEN.Dni
        )
    OUTPUT
        ORIGEN.SeedId,
        INSERTED.NumVenta_064RB
    INTO @VentaMap (SeedId, NumVenta);


    -------------------------------------------------------
    -- 10. INSERTAR LOS DETALLES DE VENTA
    -------------------------------------------------------

    DECLARE @DetalleMap TABLE
    (
        IdSeedDetalle INT PRIMARY KEY,
        IdDetalle INT
    );

    MERGE INTO DetalleVenta_064RB AS DESTINO
    USING
    (
        SELECT
            D.IdSeedDetalle,
            VM.NumVenta,
            D.Sku,
            P.Precio_064RB AS Precio,
            D.Cantidad,
            P.Precio_064RB * D.Cantidad AS Subtotal
        FROM @DetallesSeed D
        INNER JOIN @VentaMap VM
            ON VM.SeedId = D.SeedId
        INNER JOIN Producto_064RB P
            ON P.Sku_064RB = D.Sku
    ) AS ORIGEN
        ON 1 = 0
    WHEN NOT MATCHED THEN
        INSERT
        (
            NumVenta_064RB,
            SkuProducto_064RB,
            PrecioUnitario_064RB,
            Cantidad_064RB,
            Subtotal_064RB
        )
        VALUES
        (
            ORIGEN.NumVenta,
            ORIGEN.Sku,
            ORIGEN.Precio,
            ORIGEN.Cantidad,
            ORIGEN.Subtotal
        )
    OUTPUT
        ORIGEN.IdSeedDetalle,
        INSERTED.IdDetalle_064RB
    INTO @DetalleMap (IdSeedDetalle, IdDetalle);


    -------------------------------------------------------
    -- 11. RELACIONAR LOS IMEI CON LOS DETALLES DE VENTA
    -------------------------------------------------------

    INSERT INTO DetalleVenta_Imeis_064RB
    (IdDetalle_064RB, Imei_064RB)
    SELECT
        DM.IdDetalle,
        D.Imei
    FROM @DetallesSeed D
    INNER JOIN @DetalleMap DM
        ON DM.IdSeedDetalle = D.IdSeedDetalle
    WHERE D.Imei IS NOT NULL;


    -------------------------------------------------------
    -- 12. ACTUALIZAR EL STOCK DE LOS ACCESORIOS
    -------------------------------------------------------

    UPDATE A
    SET A.Stock_064RB = A.Stock_064RB - V.CantidadVendida
    FROM Accesorio_064RB A
    INNER JOIN
    (
        SELECT
            D.Sku,
            SUM(D.Cantidad) AS CantidadVendida
        FROM @DetallesSeed D
        WHERE D.Sku LIKE 'ACC%'
        GROUP BY D.Sku
    ) V
        ON V.Sku = A.Sku_064RB;


    COMMIT TRANSACTION;

    PRINT 'Carga de datos realizada correctamente.';
    PRINT 'Se generaron 30 ventas y sus respectivos detalles.';

END TRY
BEGIN CATCH

    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;

    PRINT 'Error al realizar la carga de datos.';
    PRINT ERROR_MESSAGE();

    THROW;

END CATCH;