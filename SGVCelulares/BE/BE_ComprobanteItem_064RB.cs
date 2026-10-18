namespace BE_064RB
{
    public class BE_ComprobanteItem_064RB
    {
        public string Descripcion_064RB { get; set; }
        public int Cantidad_064RB { get; set; }
        public decimal PrecioUnitario_064RB { get; set; }
        public decimal Subtotal_064RB { get; set; }
        public BE_ComprobanteItem_064RB(string descripcion, int cantidad, decimal precioUnitario, decimal subtotal)
        {
            Descripcion_064RB = descripcion;
            Cantidad_064RB = cantidad;
            PrecioUnitario_064RB = precioUnitario;
            Subtotal_064RB = subtotal;
        }
    }
}
