namespace BE_064RB
{
    public class BE_DetalleVenta_064RB
    {
        public decimal PrecioUnitario_064RB { get; set; }
        public int Cantidad_064RB { get; set; }
        public decimal Subtotal_064RB { get; set; }
        public BE_Producto_064RB Producto_064RB { get; set; }
        public BE_DetalleVenta_064RB(BE_Producto_064RB producto, decimal precioUnitario)
        {
            Producto_064RB = producto;
            PrecioUnitario_064RB = precioUnitario;
        }
    }
}
