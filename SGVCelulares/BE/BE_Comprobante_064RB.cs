namespace BE_064RB
{
    public class BE_Comprobante_064RB
    {
        public int NumVenta_064RB { get; set; }
        public DateTime Fecha_064RB { get; set; }
        public string FormaPago_064RB { get; set; }
        public decimal MontoTotal_064RB { get; set; }
        public string DniCliente_064RB { get; set; }
        public string NombreCliente_064RB { get; set; }
        public List<BE_ComprobanteItem_064RB> Items_064RB { get; set; } = new();
        public BE_Comprobante_064RB(int numVenta, DateTime fecha, string formaPago, decimal montoTotal, string dni, string nombreCliente)
        {
            NumVenta_064RB = numVenta;
            Fecha_064RB = fecha;
            FormaPago_064RB = formaPago;
            MontoTotal_064RB = montoTotal;
            DniCliente_064RB = dni;
            NombreCliente_064RB = nombreCliente;
        }
    }
}
