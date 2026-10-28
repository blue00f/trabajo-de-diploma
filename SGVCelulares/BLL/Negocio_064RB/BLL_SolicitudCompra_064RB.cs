using BE_064RB;
using BLL_064RB.Tecnico_064RB;
using MAP_064RB.Negocio_064RB;
using Servicio_064RB;

namespace BLL_064RB.Negocio_064RB
{
    public class BLL_SolicitudCompra_064RB
    {
        MAP_SolicitudCompra_064RB map_solicitud;
        BLL_Producto_064RB bll_producto;
        BLL_Proveedor_064RB bll_proveedor;
        BLL_Bitacora_064RB bll_bitacora;
        BLL_DV_064RB bll_dv;
        public BLL_SolicitudCompra_064RB()
        {
            map_solicitud = new();
            bll_producto = new();
            bll_proveedor = new();
            bll_bitacora = new();
            bll_dv = new();
        }

        public List<BE_Producto_064RB> ConsultarProductosAReponer_064RB()
        {
            var skusEnCurso = map_solicitud.ConsultarSkusConSolicitudEnCurso_064RB().ToHashSet();
            return bll_producto.Consultar_064RB().Where(p => bll_producto.ObtenerStock_064RB(p) < p.StockMinimo_064RB && !skusEnCurso.Contains(p.Sku_064RB)).ToList();
        }
        public List<object> ConsultarProductosAReponerGrilla_064RB() =>
            ConsultarProductosAReponer_064RB().Select(p => (object)new
            {
                SKU = p.Sku_064RB,
                Marca = p.Marca_064RB,
                Modelo = p.Modelo_064RB,
                Tipo = p is BE_Celular_064RB ? "Celular" : "Accesorio",
                StockActual = bll_producto.ObtenerStock_064RB(p),
                StockMinimo = p.StockMinimo_064RB
            }).ToList();
        public void Solicitar_064RB(BE_Producto_064RB? producto, int cantidad)
        {
            if (producto == null) throw new Exception("Seleccione un producto del listado.");
            if (cantidad <= 0) throw new Exception("La cantidad ingresada debe ser mayor a cero.");
            if (map_solicitud.ConsultarSkusConSolicitudEnCurso_064RB().Contains(producto.Sku_064RB)) throw new Exception("El producto seleccionado ya tiene una solicitud de reposición en curso.");

            map_solicitud.Agregar_064RB(new BE_SolicitudCompra_064RB
            {
                Producto_064RB = producto,
                Cantidad_064RB = cantidad,
                Estado_064RB = EstadoSolicitud_064RB.PendienteDeCompra_064RB,
                FechaSolicitud_064RB = DateTime.Now
            });
            bll_dv.ActualizarDV_064RB("SolicitudCompra_064RB");
            SER_Usuario_064RB usu = SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB;
            bll_bitacora.RegistrarBitacora_064RB(new SER_Bitacora_064RB(usu, DateTime.Now, "Solicitudes de compra", "Alta", 3));
        }

        private Dictionary<string, BE_Producto_064RB> ProductosPorSku_064RB() => bll_producto.Consultar_064RB().ToDictionary(p => p.Sku_064RB);
        private Dictionary<string, BE_Proveedor_064RB> ProveedoresPorCuit_064RB() => bll_proveedor.Consultar_064RB().ToDictionary(p => p.Cuit_064RB);
        public List<BE_SolicitudCompra_064RB> ConsultarPorEstado_064RB(string estado) => map_solicitud.ConsultarPorEstado_064RB(estado, ProductosPorSku_064RB(), ProveedoresPorCuit_064RB());
        public BE_SolicitudCompra_064RB? ObtenerPorId_064RB(int idSolicitud) => map_solicitud.ConsultarPorId_064RB(idSolicitud, ProductosPorSku_064RB(), ProveedoresPorCuit_064RB());

        public List<object> ConsultarPendientesDeCompraGrilla_064RB() =>
            ConsultarPorEstado_064RB(EstadoSolicitud_064RB.PendienteDeCompra_064RB).Select(s => (object)new
            {
                Solicitud = s.IdSolicitud_064RB,
                SKU = s.Producto_064RB.Sku_064RB,
                Marca = s.Producto_064RB.Marca_064RB,
                Modelo = s.Producto_064RB.Modelo_064RB,
                Cantidad = s.Cantidad_064RB,
                Fecha = s.FechaSolicitud_064RB.ToString("dd/MM/yyyy")
            }).ToList();
  
        public void Cotizar_064RB(int idSolicitud, string? cuitProveedor, decimal precioUnitario)
        {
            if (string.IsNullOrWhiteSpace(cuitProveedor)) throw new Exception("Seleccione un proveedor.");
            var solicitud = ObtenerPorId_064RB(idSolicitud) ?? throw new Exception("No se encontró la solicitud seleccionada.");
            var proveedor = bll_proveedor.Consultar_064RB().FirstOrDefault(p => p.Cuit_064RB == cuitProveedor) ?? throw new Exception("El proveedor seleccionado no está registrado.");

            if (precioUnitario <= 0) throw new Exception("El precio unitario ingresado debe ser mayor a cero.");

            solicitud.Proveedor_064RB = proveedor;
            solicitud.PrecioUnitario_064RB = precioUnitario;
            solicitud.MontoTotal_064RB = precioUnitario * solicitud.Cantidad_064RB;

            if (solicitud.Estado_064RB != EstadoSolicitud_064RB.PendienteDeCompra_064RB || !map_solicitud.Cotizar_064RB(solicitud)) throw new Exception("La solicitud seleccionada ya no está pendiente de compra.");

            SER_Usuario_064RB usu = SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB;
            bll_bitacora.RegistrarBitacora_064RB(new SER_Bitacora_064RB(usu, DateTime.Now, "Solicitudes de compra", "Modificar", 3));
            bll_dv.ActualizarDV_064RB("SolicitudCompra_064RB");
        }

        public List<object> ConsultarCotizadasGrilla_064RB() =>
            ConsultarPorEstado_064RB(EstadoSolicitud_064RB.Cotizada_064RB).Select(s => (object)new
            {
                Solicitud = s.IdSolicitud_064RB,
                SKU = s.Producto_064RB.Sku_064RB,
                Marca = s.Producto_064RB.Marca_064RB,
                Modelo = s.Producto_064RB.Modelo_064RB,
                Cantidad = s.Cantidad_064RB,
                Proveedor = s.Proveedor_064RB?.RazonSocial_064RB,
                CUIT = s.Proveedor_064RB?.Cuit_064RB,
                PrecioUnitario = s.PrecioUnitario_064RB,
                MontoTotal = s.MontoTotal_064RB,
                Fecha = s.FechaSolicitud_064RB.ToString("dd/MM/yyyy")
            }).ToList();

        public void Evaluar_064RB(int idSolicitud, bool aprobar, string? motivo)
        {
            if (idSolicitud <= 0) throw new Exception("Seleccione una solicitud del listado.");
            motivo = string.IsNullOrWhiteSpace(motivo) ? null : motivo.Trim();
            if (!aprobar && motivo == null) throw new Exception("Para rechazar una solicitud se debe ingresar el motivo del rechazo.");

            var solicitud = ObtenerPorId_064RB(idSolicitud) ?? throw new Exception("No se encontró la solicitud seleccionada.");
            if (solicitud.Estado_064RB != EstadoSolicitud_064RB.Cotizada_064RB) throw new Exception("La solicitud seleccionada ya no está en estado cotizada.");

            solicitud.Estado_064RB = aprobar ? EstadoSolicitud_064RB.Aprobada_064RB : EstadoSolicitud_064RB.Rechazada_064RB;
            solicitud.FechaDecision_064RB = DateTime.Now;
            solicitud.Motivo_064RB = motivo;

            if (!map_solicitud.Evaluar_064RB(solicitud)) throw new Exception("La solicitud seleccionada ya no está en estado cotizada.");

            SER_Usuario_064RB usu = SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB;
            bll_bitacora.RegistrarBitacora_064RB(new SER_Bitacora_064RB(usu, DateTime.Now, "Solicitudes de compra", "Modificar", 3));
            bll_dv.ActualizarDV_064RB("SolicitudCompra_064RB");
        }

        public List<object> ConsultarAprobadasGrilla_064RB() =>
            ConsultarPorEstado_064RB(EstadoSolicitud_064RB.Aprobada_064RB).Select(s => (object)new
            {
                Solicitud = s.IdSolicitud_064RB,
                SKU = s.Producto_064RB.Sku_064RB,
                Marca = s.Producto_064RB.Marca_064RB,
                Modelo = s.Producto_064RB.Modelo_064RB,
                Cantidad = s.Cantidad_064RB,
                Proveedor = s.Proveedor_064RB?.RazonSocial_064RB,
                CUIT = s.Proveedor_064RB?.Cuit_064RB,
                PrecioUnitario = s.PrecioUnitario_064RB,
                MontoTotal = s.MontoTotal_064RB,
                Aprobada = s.FechaDecision_064RB?.ToString("dd/MM/yyyy")
            }).ToList();
    } 
}
