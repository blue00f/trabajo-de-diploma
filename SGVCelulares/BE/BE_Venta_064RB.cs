namespace BE_064RB
{
    public class BE_Venta_064RB
    {
        public int NumVenta_064RB { get; set; }
        public string FormaPago_064RB { get; set; }
        public decimal MontoTotal_064RB { get; set; }
        public DateTime Fecha_064RB { get; set; }
        public BE_Cliente_064RB Cliente_064RB { get; set; }
        public List<BE_DetalleVenta_064RB> Detalle_064RB { get; set; }
    }
}
