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
                Convert.ToInt32(f["numVenta"]),
                Convert.ToDateTime(f["fecha"]),
                f["formaPago"].ToString(),
                Convert.ToDecimal(f["montoTotal"]),
                f["dni"].ToString(),
                $"{f["nombre"]} {f["apellido"]}"
            );
            foreach (DataRow fila in dt.Rows)
            {
                string desc = fila["descripcion"] == DBNull.Value ? "" : $" - {fila["descripcion"]}";
                comprobante.Items_064RB.Add(new BE_ComprobanteItem_064RB
                (
                    $"{fila["marca"]} {fila["modelo"]}{desc}",
                    Convert.ToInt32(fila["cantidad"]),
                    Convert.ToDecimal(fila["precioUnitario"]),
                    Convert.ToDecimal(fila["subtotal"])
                ));
            }
            return comprobante;
        }
    }
}
