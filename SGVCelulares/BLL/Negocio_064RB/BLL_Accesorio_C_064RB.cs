using BE_064RB;
using BLL_064RB.Tecnico_064RB;
using MAP_064RB.Negocio_064RB;
using Servicio_064RB;

namespace BLL_064RB.Negocio_064RB
{
    public class BLL_Accesorio_C_064RB
    {
        MAP_Accesorio_C_064RB map_accesorio_c;
        BLL_Bitacora_064RB bll_bitacora;
        BLL_DV_064RB bll_dv;
        public BLL_Accesorio_C_064RB()
        {
            map_accesorio_c = new();
            bll_bitacora = new();
            bll_dv = new();
        }
        public void Activar_064RB(int id)
        {
            map_accesorio_c.Activar_064RB(id);
            SER_Usuario_064RB usu = SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB;
            bll_bitacora.RegistrarBitacora_064RB(new SER_Bitacora_064RB(usu, DateTime.Now, "Bitácora", "Restaurar accesorio", (int)Criticidad_064RB.Media));
            bll_dv.ActualizarDV_064RB("Accesorio_C_064RB", "Producto_064RB", "Accesorio_064RB");
        }
        public List<BE_Accesorio_C_064RB> Consultar_064RB() => map_accesorio_c.Consultar_064RB();
        public List<object> ConsultarGrilla_064RB()
        {
            var consulta = from a in Consultar_064RB()
                           orderby a.Id_064RB
                           select new
                           {
                               a.Id_064RB,
                               a.Sku_064RB,
                               a.Marca_064RB,
                               a.Modelo_064RB,
                               a.Precio_064RB,
                               a.Descripcion_064RB,
                               a.StockMinimo_064RB,
                               a.Categoria_064RB,
                               a.Stock_064RB,
                               EstadoProducto = a.Activo_064RB,
                               Fecha = a.FechaHora_064RB.ToShortDateString(),
                               Hora = a.FechaHora_064RB.ToString("HH:mm"),
                               Activo = a.ActivoHistorico_064RB
                           };
            return consulta.ToList<object>();
        }
        public List<object> ConsultarFiltrado_064RB(string? sku, string? marca, string? modelo, DateTime? fechaInicio, DateTime? fechaFin)
        {
            var consulta = from a in Consultar_064RB()
                           orderby a.Id_064RB ascending
                           where
                           (string.IsNullOrWhiteSpace(sku)
                           || a.Sku_064RB.Contains(sku, StringComparison.OrdinalIgnoreCase))
                           &&
                           (string.IsNullOrWhiteSpace(marca)
                           || a.Marca_064RB.Contains(marca, StringComparison.OrdinalIgnoreCase))
                           &&
                           (string.IsNullOrWhiteSpace(modelo)
                           || a.Modelo_064RB.Contains(modelo, StringComparison.OrdinalIgnoreCase))
                           &&
                           (!fechaInicio.HasValue
                           || a.FechaHora_064RB >= fechaInicio.Value)
                           &&
                           (!fechaFin.HasValue
                           || a.FechaHora_064RB <= fechaFin.Value)
                           select new
                           {
                               a.Id_064RB,
                               a.Sku_064RB,
                               a.Marca_064RB,
                               a.Modelo_064RB,
                               a.Precio_064RB,
                               a.Descripcion_064RB,
                               a.StockMinimo_064RB,
                               a.Categoria_064RB,
                               a.Stock_064RB,
                               EstadoProducto = a.Activo_064RB,
                               Fecha = a.FechaHora_064RB.ToShortDateString(),
                               Hora = a.FechaHora_064RB.ToString("HH:mm"),
                               Activo = a.ActivoHistorico_064RB
                           };
            return consulta.ToList<object>();
        }
    }
}
