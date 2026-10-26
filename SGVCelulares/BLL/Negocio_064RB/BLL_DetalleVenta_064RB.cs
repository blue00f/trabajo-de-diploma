using BE_064RB;

namespace BLL_064RB.Negocio_064RB
{
    public class BLL_DetalleVenta_064RB
    {
        BLL_Producto_064RB bll_producto;
        public BLL_DetalleVenta_064RB()
        {
            bll_producto = new();
        }
        public void Agregar_064RB(List<BE_DetalleVenta_064RB> detalle, BE_Producto_064RB producto, int cantidad)
        {
            if (producto == null) throw new Exception("El producto está vacío!");
            if (cantidad <= 0) throw new Exception("La cantidad debe ser mayor a cero!");
            var linea = detalle.FirstOrDefault(d => d.Producto_064RB.Sku_064RB == producto.Sku_064RB);
            int cantidadTotal = cantidad + (linea?.Cantidad_064RB ?? 0);
            int stock = bll_producto.ObtenerStock_064RB(producto);
            if (cantidadTotal > stock) throw new Exception($"La cantidad supera el stock disponible ({stock} unidades)");

            if (linea == null)
            {
                linea = new BE_DetalleVenta_064RB(producto, producto.Precio_064RB);
                detalle.Add(linea);
            }
            linea.Cantidad_064RB = cantidadTotal;
            linea.Subtotal_064RB = linea.PrecioUnitario_064RB * cantidadTotal;
        }
        public void Quitar_064RB(List<BE_DetalleVenta_064RB> detalle, string sku)
        {
            var linea = detalle.FirstOrDefault(d => d.Producto_064RB.Sku_064RB == sku) ?? throw new Exception("No hay un detalle venta");
            detalle.Remove(linea);
        }
        public List<object> ConsultarGrilla_064RB(List<BE_DetalleVenta_064RB> detalle)
        {
            var consulta = from d in detalle
                           select new
                           {
                               SKU = d.Producto_064RB.Sku_064RB,
                               Marca = d.Producto_064RB.Marca_064RB,
                               Modelo = d.Producto_064RB.Modelo_064RB,
                               Cantidad = d.Cantidad_064RB,
                               PrecioUnitario = d.PrecioUnitario_064RB,
                               Subtotal = d.Subtotal_064RB
                           };
            return consulta.ToList<object>();
        }
    }
}
