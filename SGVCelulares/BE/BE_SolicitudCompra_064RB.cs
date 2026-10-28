namespace BE_064RB
{
    public class BE_SolicitudCompra_064RB
    {
        public int IdSolicitud_064RB { get; set; }
        public BE_Producto_064RB Producto_064RB { get; set; }
        public int Cantidad_064RB { get; set; }
        public string Estado_064RB { get; set; }
        public DateTime FechaSolicitud_064RB { get; set; }

        public BE_Proveedor_064RB? Proveedor_064RB { get; set; }
        public decimal? PrecioUnitario_064RB { get; set; }
        public decimal? MontoTotal_064RB { get; set; }
        public DateTime? FechaDecision_064RB { get; set; }
        public string? Motivo_064RB { get; set; }
    }
}
