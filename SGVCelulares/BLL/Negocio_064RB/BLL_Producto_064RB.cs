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
            map_producto.AgregarCelular_064RB(celular);

            SER_Usuario_064RB usu = SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB;
            bll_bitacora.RegistrarBitacora_064RB(new SER_Bitacora_064RB(usu, DateTime.Now, "Productos", "Alta", 3));
            bll_dv.GenerarDVH_064RB(celular, celular.Sku_064RB, "Producto_064RB");
            bll_dv.GenerarDVV_064RB("Producto_064RB");
        }
        public void AgregarAccesorio_064RB(BE_Accesorio_064RB accesorio)
        {
            map_producto.AgregarAccesorio_064RB(accesorio);

            SER_Usuario_064RB usu = SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB;
            bll_bitacora.RegistrarBitacora_064RB(new SER_Bitacora_064RB(usu, DateTime.Now, "Productos", "Alta", 3));
        }
        public void Borrar_064RB(string sku)
        {
            map_producto.Borrar_064RB(sku);
            SER_Usuario_064RB usu = SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB;
            bll_bitacora.RegistrarBitacora_064RB(new SER_Bitacora_064RB(usu, DateTime.Now, "Productos", "Baja", 3));
        }
        public void Modificar_064RB(BE_Producto_064RB producto)
        {
            map_producto.Modificar_064RB(producto);
            SER_Usuario_064RB usu = SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB;
            bll_bitacora.RegistrarBitacora_064RB(new SER_Bitacora_064RB(usu, DateTime.Now, "Productos", "Modificar", 3));
        }
        public List<BE_ProductoListado_064RB> Consultar()
        {
            return map_producto.Consultar_064RB();
        }
       
        public List<object> ConsultarGrilla()
        {
            var consulta =
            from p in Consultar()
            select new
            {
                SKU = p.Sku_064RB,
                Marca = p.Marca_064RB,
                Modelo = p.Modelo_064RB,
                Precio = p.Precio_064RB,
                Descripcion = p.Descripcion_064RB,
                Tipo = p.Tipo_064RB,
                Almacenamiento = p.Almacenamiento_064RB ?? "-",
                Color = p.Color_064RB ?? "-",
                Categoria = p.Categoria_064RB ?? "-",
                Stock = p.Stock_064RB
            };
            return consulta.Cast<object>().ToList();
        }
        public List<object> ConsultarDisponibles()
        {
            var consulta =
            from p in Consultar()
            where p.Stock_064RB > 0
            select new
            {
                SKU = p.Sku_064RB,
                Marca = p.Marca_064RB,
                Modelo = p.Modelo_064RB,
                Precio = p.Precio_064RB,
                Descripcion = p.Descripcion_064RB,
                Tipo = p.Tipo_064RB,
                Almacenamiento = p.Almacenamiento_064RB ?? "-",
                Color = p.Color_064RB ?? "-",
                Categoria = p.Categoria_064RB ?? "-",
                Stock = p.Stock_064RB
            };
            return consulta.Cast<object>().ToList();
        }
        public List<object> ConsultarCelulares()
        {
            var consulta =
            from p in Consultar()
            where p.Tipo_064RB == "Celular"
            select new
            {
                SKU = p.Sku_064RB,
                TextoVisual = $"{p.Sku_064RB} - {p.Marca_064RB} - {p.Modelo_064RB} - ${p.Precio_064RB}",
            };
            return consulta.ToList<object>();
        }
        public BE_Celular_064RB ConsultarCelularPorSku(string sku)
        {
            return map_producto.ConsultarCelularPorSku(sku);
        }
        public BE_Accesorio_064RB ConsultarAccesorioPorSku_064RB(string sku)
        {
            return map_producto.ConsultarAccesorioPorSku(sku);
        }
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
