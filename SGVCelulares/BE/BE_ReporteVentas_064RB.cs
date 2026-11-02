namespace BE_064RB
{
    public class BE_ReporteVentas_064RB
    {
        public DateTime Desde_064RB { get; set; }
        public DateTime Hasta_064RB { get; set; }
        public int CantidadVentas_064RB { get; set; }
        public int UnidadesVendidas_064RB { get; set; }
        public decimal MontoTotal_064RB { get; set; }
        public decimal TicketPromedio_064RB { get; set; }
        public List<BE_ReporteDia_064RB> VentasPorDia_064RB { get; set; } = new List<BE_ReporteDia_064RB>();
        public List<BE_ReporteFormaPago_064RB> VentasPorFormaPago_064RB { get; set; } = new List<BE_ReporteFormaPago_064RB>();
        public List<BE_ReporteProducto_064RB> ProductosVendidos_064RB { get; set; } = new List<BE_ReporteProducto_064RB>();
        public List<BE_ReporteProducto_064RB> SinMovimiento_064RB { get; set; } = new List<BE_ReporteProducto_064RB>();
        public string Agrupacion_064RB { get; set; } = "día";
        public List<BE_ReporteProducto_064RB> MasVendidos_064RB { get; set; } = new List<BE_ReporteProducto_064RB>();
    }
}
