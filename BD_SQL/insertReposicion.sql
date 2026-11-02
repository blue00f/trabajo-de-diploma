BEGIN TRY
    BEGIN TRANSACTION;

    -------------------------------------------------------
    -- 1. PROVEEDORES
    -------------------------------------------------------

    INSERT INTO Proveedor_064RB
    (Cuit_064RB, RazonSocial_064RB, Activo_064RB)
    VALUES
    ('30700000001', 'Distribuidora Samsung Argentina', 1),
    ('30700000002', 'Apple Distribution SRL', 1),
    ('30700000003', 'Motorola Mobility Argentina', 1),
    ('30700000004', 'Xiaomi Technology Argentina', 1),
    ('30700000005', 'JBL Argentina SA', 1),
    ('30700000006', 'Kingston Technology SRL', 1),
    ('30700000007', 'Accesorios del Sur SA', 1),
    ('30700000008', 'Tecno Importaciones SRL', 1),
    ('30700000009', 'Global Electronics SA', 1),
    ('30700000010', 'Distribuidora Digital SRL', 1),
    ('30700000011', 'Mayorista Central SA', 1),
    ('30700000012', 'Electronica del Plata SRL', 1),
    ('30700000013', 'Importadora Buenos Aires SA', 1),
    ('30700000014', 'Smart Technology SRL', 1),
    ('30700000015', 'Comercializadora Nacional SA', 0);


    -------------------------------------------------------
    -- 2. OBTENER LOS PRODUCTOS DISPONIBLES
    -------------------------------------------------------

    DECLARE @Productos TABLE
    (
        Numero INT PRIMARY KEY,
        Sku VARCHAR(50),
        Precio DECIMAL(18,2)
    );

    INSERT INTO @Productos (Numero, Sku, Precio)
    SELECT
        ROW_NUMBER() OVER (ORDER BY Sku_064RB),
        Sku_064RB,
        Precio_064RB
    FROM Producto_064RB
    WHERE Sku_064RB LIKE 'CEL%'
       OR Sku_064RB LIKE 'ACC%';

    IF (SELECT COUNT(*) FROM @Productos) < 20
        THROW 50001,
        'Se necesitan al menos 20 productos cargados previamente.',
        1;


    -------------------------------------------------------
    -- 3. PREPARAR LAS SOLICITUDES
    -------------------------------------------------------

    DECLARE @Solicitudes TABLE
    (
        SeedId INT IDENTITY(1,1) PRIMARY KEY,
        Sku VARCHAR(50),
        Cantidad INT,
        Estado VARCHAR(30),
        FechaSolicitud DATETIME,
        CuitProveedor VARCHAR(11),
        PrecioUnitario DECIMAL(18,2),
        MontoTotal DECIMAL(18,2),
        FechaDecision DATETIME NULL,
        Motivo VARCHAR(200) NULL
    );


    -------------------------------------------------------
    -- 4. HISTORIAL: 300 SOLICITUDES
    -------------------------------------------------------
    -- 20 productos x 15 solicitudes = 300.
    --
    -- Por producto se generan solicitudes recibidas
    -- y rechazadas.
    --
    -- La distribución se equilibra:
    -- 150 rechazadas y 150 recibidas.
    -------------------------------------------------------

    ;WITH Ciclos AS
    (
        SELECT Numero
        FROM (VALUES
            (1),(2),(3),(4),(5),
            (6),(7),(8),(9),(10),
            (11),(12),(13),(14),(15)
        ) AS C(Numero)
    ),
    Historial AS
    (
        SELECT
            P.Numero AS NumProducto,
            P.Sku,
            P.Precio,
            C.Numero AS Ciclo,

            CASE
                WHEN P.Numero <= 10 AND C.Numero <= 8
                    THEN 'Recibida'
                WHEN P.Numero > 10 AND C.Numero <= 7
                    THEN 'Recibida'
                ELSE 'Rechazada'
            END AS Estado,

            DATEADD(
                DAY,
                (C.Numero - 1) * 20 + P.Numero * 3,
                CAST('2025-09-01' AS DATETIME)
            ) AS Fecha

        FROM @Productos P
        CROSS JOIN Ciclos C
    )
    INSERT INTO @Solicitudes
    (
        Sku,
        Cantidad,
        Estado,
        FechaSolicitud,
        CuitProveedor,
        PrecioUnitario,
        MontoTotal,
        FechaDecision,
        Motivo
    )
    SELECT
        H.Sku,

        5 + ((H.NumProducto + H.Ciclo * 3) % 26),

        H.Estado,

        H.Fecha,

        '307000000' + RIGHT(
            '00' +
            CAST(((H.NumProducto + H.Ciclo) % 14) + 1 AS VARCHAR(2)),
            2
        ),

        CAST(
            H.Precio * (
                0.70 +
                ((H.NumProducto + H.Ciclo) % 20) / 100.0
            )
            AS DECIMAL(18,2)
        ),

        CAST(
            H.Precio * (
                0.70 +
                ((H.NumProducto + H.Ciclo) % 20) / 100.0
            ) *
            (5 + ((H.NumProducto + H.Ciclo * 3) % 26))
            AS DECIMAL(18,2)
        ),

        DATEADD(
            HOUR,
            4 + (H.Ciclo % 48),
            H.Fecha
        ),

        CASE
            WHEN H.Estado = 'Rechazada' THEN
                CASE (H.Ciclo + H.NumProducto) % 6
                    WHEN 0 THEN 'Precio superior al presupuesto disponible.'
                    WHEN 1 THEN 'Stock actual suficiente para cubrir la demanda.'
                    WHEN 2 THEN 'Proveedor con antecedentes comerciales desfavorables.'
                    WHEN 3 THEN 'Producto discontinuado por el fabricante.'
                    WHEN 4 THEN 'No se considera prioritaria la reposicion.'
                    ELSE 'La cotizacion no resulta conveniente.'
                END
            ELSE NULL
        END

    FROM Historial H;


    -------------------------------------------------------
    -- 5. SOLICITUDES ACTIVAS: 20 REGISTROS
    -------------------------------------------------------
    -- Una solicitud activa por cada producto.
    -- Cinco de cada estado:
    -- Pendiente, Cotizada, Aprobada y Pendiente de recepción.
    -------------------------------------------------------

    INSERT INTO @Solicitudes
    (
        Sku,
        Cantidad,
        Estado,
        FechaSolicitud,
        CuitProveedor,
        PrecioUnitario,
        MontoTotal,
        FechaDecision,
        Motivo
    )
    SELECT
        P.Sku,

        10 + (P.Numero % 20),

        CASE (P.Numero - 1) % 4
            WHEN 0 THEN 'Pendiente de compra'
            WHEN 1 THEN 'Cotizada'
            WHEN 2 THEN 'Aprobada'
            ELSE 'Pendiente de recepción'
        END,

        DATEADD(
            DAY,
            -(P.Numero * 2),
            CAST('2026-11-02T10:00:00' AS DATETIME)
        ),

        CASE
            WHEN (P.Numero - 1) % 4 = 0 THEN NULL
            ELSE '307000000' + RIGHT(
                '00' +
                CAST((P.Numero % 14) + 1 AS VARCHAR(2)),
                2
            )
        END,

        CASE
            WHEN (P.Numero - 1) % 4 = 0 THEN NULL
            ELSE CAST(P.Precio * 0.82 AS DECIMAL(18,2))
        END,

        CASE
            WHEN (P.Numero - 1) % 4 = 0 THEN NULL
            ELSE CAST(
                P.Precio * 0.82 * (10 + (P.Numero % 20))
                AS DECIMAL(18,2)
            )
        END,

        CASE
            WHEN (P.Numero - 1) % 4 IN (2,3)
                THEN DATEADD(
                    DAY,
                    -(P.Numero % 3),
                    CAST('2026-11-02T09:00:00' AS DATETIME)
                )
            ELSE NULL
        END,

        NULL

    FROM @Productos P;


    -------------------------------------------------------
    -- 6. INSERTAR SOLICITUDES EN SQL SERVER
    -------------------------------------------------------

    DECLARE @MapaSolicitudes TABLE
    (
        SeedId INT PRIMARY KEY,
        IdSolicitud INT
    );

    MERGE INTO SolicitudCompra_064RB AS DESTINO
    USING @Solicitudes AS ORIGEN
        ON 1 = 0

    WHEN NOT MATCHED THEN
        INSERT
        (
            SkuProducto_064RB,
            Cantidad_064RB,
            Estado_064RB,
            FechaSolicitud_064RB,
            CuitProveedor_064RB,
            PrecioUnitario_064RB,
            MontoTotal_064RB,
            FechaDecision_064RB,
            Motivo_064RB
        )
        VALUES
        (
            ORIGEN.Sku,
            ORIGEN.Cantidad,
            ORIGEN.Estado,
            ORIGEN.FechaSolicitud,
            ORIGEN.CuitProveedor,
            ORIGEN.PrecioUnitario,
            ORIGEN.MontoTotal,
            ORIGEN.FechaDecision,
            ORIGEN.Motivo
        )

    OUTPUT
        ORIGEN.SeedId,
        INSERTED.IdSolicitud_064RB
    INTO @MapaSolicitudes (SeedId, IdSolicitud);


    -------------------------------------------------------
    -- 7. GENERAR ORDENES DE COMPRA
    -------------------------------------------------------
    -- Las solicitudes recibidas tienen orden.
    -- También las pendientes de recepción.
    -- Las aprobadas todavía no tienen orden.
    -------------------------------------------------------

    DECLARE @MapaOrdenes TABLE
    (
        SeedId INT PRIMARY KEY,
        NumOrden INT
    );

    MERGE INTO OrdenCompra_064RB AS DESTINO
    USING
    (
        SELECT
            S.SeedId,
            M.IdSolicitud,
            S.MontoTotal,
            S.FechaSolicitud,
            S.Estado
        FROM @Solicitudes S
        INNER JOIN @MapaSolicitudes M
            ON M.SeedId = S.SeedId
        WHERE S.Estado IN
        (
            'Recibida',
            'Pendiente de recepción'
        )
    ) AS ORIGEN

        ON 1 = 0

    WHEN NOT MATCHED THEN
        INSERT
        (
            IdSolicitud_064RB,
            Fecha_064RB,
            MontoPagado_064RB
        )
        VALUES
        (
            ORIGEN.IdSolicitud,

            CASE
                WHEN ORIGEN.Estado = 'Recibida'
                    THEN DATEADD(DAY, 2, ORIGEN.FechaSolicitud)
                ELSE DATEADD(DAY, 1, ORIGEN.FechaSolicitud)
            END,

            ORIGEN.MontoTotal
        )

    OUTPUT
        ORIGEN.SeedId,
        INSERTED.NumOrden_064RB
    INTO @MapaOrdenes (SeedId, NumOrden);


    -------------------------------------------------------
    -- 8. GENERAR REMITOS DE COMPRA
    -------------------------------------------------------
    -- Solo las solicitudes recibidas tienen remito.
    -- Se distribuyen entre conformes y no conformes.
    -------------------------------------------------------

    DECLARE @MapaRemitos TABLE
    (
        SeedId INT PRIMARY KEY,
        IdRemito INT
    );

    MERGE INTO RemitoCompra_064RB AS DESTINO
    USING
    (
        SELECT
            S.SeedId,
            O.NumOrden,
            S.FechaSolicitud,
            CASE
                WHEN S.SeedId % 5 = 0 THEN 'No conforme'
                ELSE 'Conforme'
            END AS EstadoRemito
        FROM @Solicitudes S
        INNER JOIN @MapaOrdenes O
            ON O.SeedId = S.SeedId
        WHERE S.Estado = 'Recibida'
    ) AS ORIGEN

        ON 1 = 0

    WHEN NOT MATCHED THEN
        INSERT
        (
            NumOrden_064RB,
            NumRemito_064RB,
            Estado_064RB,
            FechaRecepcion_064RB
        )
        VALUES
        (
            ORIGEN.NumOrden,
            100000 + ORIGEN.SeedId,
            ORIGEN.EstadoRemito,
            DATEADD(DAY, 3, ORIGEN.FechaSolicitud)
        )

    OUTPUT
        ORIGEN.SeedId,
        INSERTED.IdRemito_064RB
    INTO @MapaRemitos (SeedId, IdRemito);


    -------------------------------------------------------
    -- 9. GENERAR DISCREPANCIAS
    -------------------------------------------------------
    -- Aproximadamente el 20% de los remitos.
    -- Se registran faltantes y/o unidades dañadas.
    -------------------------------------------------------

    INSERT INTO Discrepancia_064RB
    (
        IdRemito_064RB,
        CantidadFaltante_064RB,
        CantidadDanada_064RB,
        Observaciones_064RB
    )
    SELECT
        R.IdRemito,

        CASE
            WHEN R.SeedId % 2 = 0 THEN 1 + (R.SeedId % 4)
            ELSE 0
        END,

        CASE
            WHEN R.SeedId % 2 <> 0 THEN 1 + (R.SeedId % 3)
            ELSE 0
        END,

        CASE (R.SeedId % 5)
            WHEN 0 THEN
                'Se detectaron diferencias entre la cantidad solicitada y la recibida. Se requiere revision del proveedor.'
            ELSE
                'Se detectaron unidades dañadas durante la recepcion. Se deja constancia para gestionar el reclamo correspondiente.'
        END

    FROM @MapaRemitos R
    INNER JOIN @Solicitudes S
        ON S.SeedId = R.SeedId
    WHERE S.SeedId % 5 = 0;


    -------------------------------------------------------
    -- 10. CONFIRMACION
    -------------------------------------------------------

    COMMIT TRANSACTION;

    PRINT '==========================================';
    PRINT 'CARGA DE ABASTECIMIENTO COMPLETADA';
    PRINT '==========================================';
    PRINT 'Proveedores: 15';
    PRINT 'Solicitudes: 320';
    PRINT 'Ordenes de compra: 165';
    PRINT 'Remitos: 160';
    PRINT '==========================================';

END TRY
BEGIN CATCH

    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;

    PRINT 'ERROR DURANTE LA CARGA DE DATOS';
    PRINT ERROR_MESSAGE();

    THROW;

END CATCH;
