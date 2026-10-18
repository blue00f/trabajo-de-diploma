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
                int numVenta = Convert.ToInt32(tx.Escalar_064RB(@"INSERT INTO Venta_064RB(formaPago,montoTotal,fecha,dniCliente) VALUES (@formaPago,@montoTotal,@fecha,@dniCliente); SELECT CAST(SCOPE_IDENTITY() AS INT);",
                    new List<ParametroBD_064RB>
                    {
                        new("@formaPago", SqlDbType.VarChar, formaPago),
                        new("@montoTotal", SqlDbType.Decimal, montoTotal),
                        new("@fecha", SqlDbType.DateTime, fecha),
                        new("@dniCliente", SqlDbType.VarChar, dniCliente)
                    }));
                foreach (var item in detalle)
                {
                    int idDetalle = Convert.ToInt32(tx.Escalar_064RB(@"INSERT INTO DetalleVenta_064RB(numVenta,skuProducto,precioUnitario,cantidad,subtotal) VALUES (@numVenta,@sku,@precioUnitario,@cantidad,@subtotal); SELECT CAST(SCOPE_IDENTITY() AS INT);",
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
                            @"DECLARE @asignadas TABLE (imei VARCHAR(15));
                            UPDATE TOP (@cantidad) UnidadCelular_064RB
                            SET estado = 'Vendido'
                            OUTPUT inserted.imei INTO @asignadas
                            WHERE skuCelular = @sku AND estado = 'Disponible';
                            INSERT INTO DetalleVenta_Imeis_064RB(id_detalle, imei)
                            SELECT @idDetalle, imei FROM @asignadas;
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
                        int afectadas = tx.Ejecutar_064RB("UPDATE Accesorio_064RB SET stock = stock - @cantidad WHERE sku = @sku AND stock >= @cantidad",
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
            return com.Consultar_064RB("SELECT v.numVenta, v.fecha, v.formaPago, v.montoTotal, c.dni, c.nombre, c.apellido, p.marca, p.modelo, p.descripcion,d.cantidad, d.precioUnitario, d.subtotal" +
                " FROM Venta_064RB v" +
                " INNER JOIN Cliente_064RB c ON c.dni = v.dniCliente" +
                " INNER JOIN DetalleVenta_064RB d ON d.numVenta = v.numVenta" +
                " INNER JOIN Producto_064RB p ON p.sku = d.skuProducto" +
                " WHERE v.numVenta = @numVenta" +
                "   ORDER BY d.idDetalle", new List<ParametroBD_064RB> { new("@numVenta", SqlDbType.Int, numVenta) });
        }
    }
}
