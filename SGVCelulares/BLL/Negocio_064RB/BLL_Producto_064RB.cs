using BE_064RB;
using BLL_064RB.Tecnico_064RB;
using MAP_064RB.Negocio_064RB;
using Servicio_064RB;

namespace BLL_064RB.Negocio_064RB
{
    public class BLL_Producto_064RB
    {
        MAP_Producto_064RB map_producto;
        BLL_Bitacora_064RB bll_bitacora;
        BLL_DV_064RB bll_dv;
        public BLL_Producto_064RB()
        {
            map_producto = new();
            bll_bitacora = new();
            bll_dv = new();
        }
        public void AgregarCelular_064RB(BE_Celular_064RB celular)
        {
            if (celular.StockMinimo_064RB < 0) throw new Exception("El stock mínimo no puede ser negativo!");
            if (Consultar_064RB().Any(p => p.Sku_064RB == celular.Sku_064RB)) throw new Exception("El celular está repetido!");
            map_producto.AgregarCelular_064RB(celular);

            SER_Usuario_064RB usu = SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB;
            bll_bitacora.RegistrarBitacora_064RB(new SER_Bitacora_064RB(usu, DateTime.Now, "Productos", "Alta", 3));
            bll_dv.ActualizarDV_064RB("Producto_064RB", "Celular_064RB");

        }
        public void AgregarAccesorio_064RB(BE_Accesorio_064RB accesorio)
        {
            if (accesorio.StockMinimo_064RB < 0) throw new Exception("El stock mínimo no puede ser negativo!");
            if (Consultar_064RB().Any(p => p.Sku_064RB == accesorio.Sku_064RB)) throw new Exception("El accesorio está repetido!");
            map_producto.AgregarAccesorio_064RB(accesorio);
            SER_Usuario_064RB usu = SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB;
            bll_bitacora.RegistrarBitacora_064RB(new SER_Bitacora_064RB(usu, DateTime.Now, "Productos", "Alta", 3));
            bll_dv.ActualizarDV_064RB("Producto_064RB", "Accesorio_064RB");
        }
        public void Borrar_064RB(string sku)
        {
            map_producto.Borrar_064RB(sku);
            SER_Usuario_064RB usu = SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB;
            bll_bitacora.RegistrarBitacora_064RB(new SER_Bitacora_064RB(usu, DateTime.Now, "Productos", "Baja", 3));
            BE_Producto_064RB producto = ConsultarPorSku_064RB(sku);
            if (producto is BE_Celular_064RB) bll_dv.ActualizarDV_064RB("Producto_064RB", "Celular_064RB");
            if (producto is BE_Accesorio_064RB) bll_dv.ActualizarDV_064RB("Producto_064RB", "Accesorio_064RB");
        }
        public void Modificar_064RB(BE_Producto_064RB producto)
        {
            if (producto.StockMinimo_064RB < 0) throw new Exception("El stock mínimo no puede ser negativo!");
            map_producto.Modificar_064RB(producto);
            SER_Usuario_064RB usu = SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB;
            bll_bitacora.RegistrarBitacora_064RB(new SER_Bitacora_064RB(usu, DateTime.Now, "Productos", "Modificar", 3));
            if (producto is BE_Celular_064RB) bll_dv.ActualizarDV_064RB("Producto_064RB", "Celular_064RB");
            if (producto is BE_Accesorio_064RB) bll_dv.ActualizarDV_064RB("Producto_064RB", "Accesorio_064RB");
        }
        public List<BE_Producto_064RB> Consultar_064RB() => map_producto.Consultar_064RB();
        private List<object> ProyectarGrilla_064RB(IEnumerable<BE_Producto_064RB> productos)
        {
            var consulta = from p in productos
                           let cel = p as BE_Celular_064RB
                           let acc = p as BE_Accesorio_064RB
                           select (object)new
                           {
                               SKU = p.Sku_064RB,
                               Marca = p.Marca_064RB,
                               Modelo = p.Modelo_064RB,
                               Precio = p.Precio_064RB,
                               Descripcion = p.Descripcion_064RB,
                               Tipo = cel != null ? "Celular" : acc != null ? "Accesorio" : "Desconocido",
                               Almacenamiento = cel != null ? cel.Almacenamiento_064RB : "-",
                               Color = cel != null ? cel.Color_064RB : "-",
                               Categoria = acc != null ? acc.Categoria_064RB : "-",
                               Stock = ObtenerStock_064RB(p),
                               StockMinimo = p.StockMinimo_064RB
                           };
            return consulta.ToList<object>();
        }
        public List<object> ConsultarGrilla_064RB() => ProyectarGrilla_064RB(Consultar_064RB());
        public List<object> ConsultarDisponibles_064RB() => ProyectarGrilla_064RB(Consultar_064RB().Where(p => ObtenerStock_064RB(p) >= 1));
        private static bool Contiene_064RB(string? valor, string? filtro) => string.IsNullOrWhiteSpace(filtro) || (valor?.StartsWith(filtro.Trim(), StringComparison.OrdinalIgnoreCase) ?? false);
        public List<object> Buscar_064RB(string? sku = null, string? marca = null, string? modelo = null, decimal? precioMax = null, string? tipo = null, string? almacenamiento = null, string? color = null, string? categoria = null, bool soloDisponible = true)
        {
            var filtrados = from p in Consultar_064RB()
                            let cel = p as BE_Celular_064RB
                            let acc = p as BE_Accesorio_064RB
                            let tipoReal = cel != null ? "Celular" : acc != null ? "Accesorio" : "Desconocido"
                            where (!soloDisponible || ObtenerStock_064RB(p) >= 1)
                            && Contiene_064RB(p.Sku_064RB, sku)
                            && Contiene_064RB(p.Marca_064RB, marca)
                            && Contiene_064RB(p.Modelo_064RB, modelo)
                            && (precioMax == null || p.Precio_064RB <= precioMax)
                            && (string.IsNullOrWhiteSpace(tipo) || tipoReal == tipo)
                            && Contiene_064RB(cel?.Almacenamiento_064RB, almacenamiento)
                            && Contiene_064RB(cel?.Color_064RB, color)
                            && Contiene_064RB(acc?.Categoria_064RB, categoria)
                            select p;
            return ProyectarGrilla_064RB(filtrados);
        }
        public int ObtenerStock_064RB(BE_Producto_064RB producto) => producto switch
        {
            BE_Celular_064RB c => c.UnidadCelulares_064RB?.Count(u => u.Estado_064RB == "Disponible") ?? 0,
            BE_Accesorio_064RB a => a.Stock_064RB,
            _ => 0
        };
        public List<object> ConsultarCelulares_064RB()
        {
            var consulta =
            from p in Consultar_064RB()
            where p is BE_Celular_064RB
            select new
            {
                SKU = p.Sku_064RB,
                TextoVisual = $"{p.Sku_064RB} - {p.Marca_064RB} - {p.Modelo_064RB} - ${p.Precio_064RB}",
            };
            return consulta.ToList<object>();
        }
        public BE_Celular_064RB ConsultarCelularPorSku_064RB(string sku) => map_producto.ConsultarCelularPorSku(sku);
        public BE_Accesorio_064RB ConsultarAccesorioPorSku_064RB(string sku) => map_producto.ConsultarAccesorioPorSku(sku);
        public BE_Producto_064RB? ConsultarPorSku_064RB(string sku) => Consultar_064RB().FirstOrDefault(p => p.Sku_064RB == sku);
        public bool VerificarStock_064RB(BE_Producto_064RB producto, int cantidad)
        {
            bool rdo = false;
            if (producto is BE_Accesorio_064RB accesorio)
            {
                if (cantidad > accesorio.Stock_064RB) rdo = true;
            }
            else if(producto is BE_Celular_064RB celular)
            {
                if (cantidad > celular.UnidadCelulares_064RB.Count) rdo = true;
            }
            return rdo;
        }
    }
}
