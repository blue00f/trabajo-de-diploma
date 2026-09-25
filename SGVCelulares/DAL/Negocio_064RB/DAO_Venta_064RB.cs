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
        public void Agregar_064RB(int numVenta, decimal precioUnitario, int cantidad, string sku, decimal subtotal)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@numVenta", SqlDbType.Int, numVenta),
                new("@precioUnitario", SqlDbType.Decimal, precioUnitario),
                new("@cantidad", SqlDbType.Int, cantidad),
                new("@sku", SqlDbType.VarChar, sku),
                new("@subtotal", SqlDbType.Decimal, subtotal),
            };
            com.Ejecutar_064RB("INSERT INTO DetalleVenta_064RB(imei,estado,skuCelular) values (@imei,@estado,@sku)", parametros);
        }
    }
}
