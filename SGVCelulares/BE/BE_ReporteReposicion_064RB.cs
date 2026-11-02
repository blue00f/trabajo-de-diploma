namespace BE_064RB
{
    public class BE_ReporteReposicion_064RB
    {
        public DateTime Desde_064RB { get; set; }
        public DateTime Hasta_064RB { get; set; }
        public int CantidadSolicitudes_064RB { get; set; }
        public int UnidadesSolicitadas_064RB { get; set; }
        public decimal MontoPagado_064RB { get; set; }
        public int PendientesDeRecepcion_064RB { get; set; }
        public string Agrupacion_064RB { get; set; } = "día";
        public Dictionary<string, int> SolicitudesPorEstado_064RB { get; set; } = new Dictionary<string, int>();
        public List<BE_ReporteDia_064RB> SolicitudesPorPeriodo_064RB { get; set; } = new List<BE_ReporteDia_064RB>();
        public List<BE_ReporteProveedor_064RB> Proveedores_064RB { get; set; } = new List<BE_ReporteProveedor_064RB>();
        public List<BE_ReporteProductoReposicion_064RB> Productos_064RB { get; set; } = new List<BE_ReporteProductoReposicion_064RB>();
        public List<BE_ReporteProducto_064RB> ProductosAReponerHoy_064RB { get; set; } = new List<BE_ReporteProducto_064RB>();
        public List<BE_ReporteSolicitud_064RB> Detalle_064RB { get; set; } = new List<BE_ReporteSolicitud_064RB>();
    }
}
