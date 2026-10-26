using BE_064RB;
using DAL_064RB.Negocio_064RB;
using System.Data;

namespace MAP_064RB.Negocio_064RB
{
    public class MAP_Venta_064RB
    {
        DAO_Venta_064RB dao_venta;
        public MAP_Venta_064RB()
        {
            dao_venta = new();
        }
        public int Agregar_064RB(BE_Venta_064RB venta)
        {
            var detalle = venta.Detalle_064RB.Select(d => (
                sku: d.Producto_064RB.Sku_064RB,
                precioUnitario: d.PrecioUnitario_064RB,
                cantidad: d.Cantidad_064RB,
                subtotal: d.Subtotal_064RB,
                esCelular: d.Producto_064RB is BE_Celular_064RB)).ToList();

            return dao_venta.Agregar_064RB(venta.FormaPago_064RB, venta.MontoTotal_064RB, venta.Fecha_064RB, venta.Cliente_064RB.Dni_064RB, detalle);
        }
        public BE_Comprobante_064RB ConsultarComprobante_064RB(int numVenta)
        {
            DataTable dt = dao_venta.ConsultarComprobante_064RB(numVenta);
            if (dt.Rows.Count == 0) throw new Exception("La venta ingresada no pertenece a ninguna venta registrada");
            DataRow f = dt.Rows[0];
            var comprobante = new BE_Comprobante_064RB
            (
                Convert.ToInt32(f["NumVenta_064RB"]),
                Convert.ToDateTime(f["Fecha_064RB"]),
                f["FormaPago_064RB"].ToString(),
                Convert.ToDecimal(f["MontoTotal_064RB"]),
                f["Dni_064RB"].ToString(),
                $"{f["Nombre_064RB"]} {f["Apellido_064RB"]}"
            );
            foreach (DataRow fila in dt.Rows)
            {
                string desc = fila["Descripcion_064RB"] == DBNull.Value ? "" : $" - {fila["Descripcion_064RB"]}";
                comprobante.Items_064RB.Add(new BE_ComprobanteItem_064RB
                (
                    $"{fila["Marca_064RB"]} {fila["Modelo_064RB"]}{desc}",
                    Convert.ToInt32(fila["Cantidad_064RB"]),
                    Convert.ToDecimal(fila["PrecioUnitario_064RB"]),
                    Convert.ToDecimal(fila["Subtotal_064RB"])
                ));
            }
            return comprobante;
        }
    }
}
