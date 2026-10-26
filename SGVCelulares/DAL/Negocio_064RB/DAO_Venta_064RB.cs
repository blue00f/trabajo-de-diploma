using System.Data;

namespace DAL_064RB.Negocio_064RB
{
    public class DAO_Venta_064RB
    {
        ComandoBD_064RB com;
        public DAO_Venta_064RB()
        {
            com = new();
        }
        public int Agregar_064RB(string formaPago, decimal montoTotal, DateTime fecha, string dniCliente, List<(string sku, decimal precioUnitario, int cantidad, decimal subtotal, bool esCelular)> detalle)
        {
            return com.EjecutarEnTransaccion_064RB(tx =>
            {
                int numVenta = Convert.ToInt32(tx.Escalar_064RB(@"INSERT INTO Venta_064RB(FormaPago_064RB,MontoTotal_064RB,Fecha_064RB,Dni_064RB) VALUES (@formaPago,@montoTotal,@fecha,@dniCliente); SELECT CAST(SCOPE_IDENTITY() AS INT);",
                    new List<ParametroBD_064RB>
                    {
                        new("@formaPago", SqlDbType.VarChar, formaPago),
                        new("@montoTotal", SqlDbType.Decimal, montoTotal),
                        new("@fecha", SqlDbType.DateTime, fecha),
                        new("@dniCliente", SqlDbType.VarChar, dniCliente)
                    }));
                foreach (var item in detalle)
                {
                    int idDetalle = Convert.ToInt32(tx.Escalar_064RB(@"INSERT INTO DetalleVenta_064RB(NumVenta_064RB,SkuProducto_064RB,PrecioUnitario_064RB,Cantidad_064RB,Subtotal_064RB) VALUES (@numVenta,@sku,@precioUnitario,@cantidad,@subtotal); SELECT CAST(SCOPE_IDENTITY() AS INT);",
                        new List<ParametroBD_064RB>
                        {
                            new("@numVenta", SqlDbType.Int, numVenta),
                            new("@sku", SqlDbType.VarChar, item.sku),
                            new("@precioUnitario", SqlDbType.Decimal, item.precioUnitario),
                            new("@cantidad", SqlDbType.Int, item.cantidad),
                            new("@subtotal", SqlDbType.Decimal, item.subtotal)
                        }));
                    if (item.esCelular)
                    {
                        int asignadas = Convert.ToInt32(tx.Escalar_064RB(
                            @"DECLARE @asignadas TABLE (Imei_064RB VARCHAR(15));
                            UPDATE TOP (@cantidad) UnidadCelular_064RB
                            SET Estado_064RB = 'Vendido'
                            OUTPUT inserted.Imei_064RB INTO @asignadas
                            WHERE SkuCelular_064RB = @sku AND Estado_064RB = 'Disponible';
                            INSERT INTO DetalleVenta_Imeis_064RB(IdDetalle_064RB, Imei_064RB)
                            SELECT @idDetalle, Imei_064RB FROM @asignadas;
                            SELECT COUNT(*) FROM @asignadas;",
                            new List<ParametroBD_064RB>
                            {
                                new("@cantidad", SqlDbType.Int, item.cantidad),
                                new("@sku", SqlDbType.VarChar, item.sku),
                                new("@idDetalle", SqlDbType.Int, idDetalle)
                            }));
                        if (asignadas != item.cantidad) throw new Exception($"El stock del producto {item.sku} ya no alcanza para la cantidad vendida.");
                    }
                    else
                    {
                        int afectadas = tx.Ejecutar_064RB("UPDATE Accesorio_064RB SET Stock_064RB = Stock_064RB - @cantidad WHERE Sku_064RB = @sku AND Stock_064RB >= @cantidad",
                            new List<ParametroBD_064RB>
                            {
                                new("@cantidad", SqlDbType.Int, item.cantidad),
                                new("@sku", SqlDbType.VarChar, item.sku)
                            });
                        if (afectadas != 1) throw new Exception($"El stock del producto {item.sku} ya no alcanza para la cantidad vendida.");
                    }
                }
                return numVenta;
            });
        }
        public DataTable ConsultarComprobante_064RB(int numVenta)
        {
            return com.Consultar_064RB("SELECT v.NumVenta_064RB, v.Fecha_064RB, v.FormaPago_064RB, v.MontoTotal_064RB, c.Dni_064RB, c.Nombre_064RB, c.Apellido_064RB, p.Marca_064RB, p.Modelo_064RB, p.Descripcion_064RB, d.Cantidad_064RB, d.PrecioUnitario_064RB, d.Subtotal_064RB" +
                " FROM Venta_064RB v" +
                " INNER JOIN Cliente_064RB c ON c.Dni_064RB = v.Dni_064RB" +
                " INNER JOIN DetalleVenta_064RB d ON d.NumVenta_064RB = v.NumVenta_064RB" +
                " INNER JOIN Producto_064RB p ON p.Sku_064RB = d.SkuProducto_064RB" +
                " WHERE v.NumVenta_064RB = @numVenta" +
                "   ORDER BY d.IdDetalle_064RB", new List<ParametroBD_064RB> { new("@numVenta", SqlDbType.Int, numVenta) });
        }
    }
}
