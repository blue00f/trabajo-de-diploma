using BE_064RB;
using DAL_064RB.Negocio_064RB;
using System.Data;

namespace MAP_064RB.Negocio_064RB
{
    public class MAP_Producto_064RB
    {
        DAO_Producto_064RB dao_producto;
        DAO_UnidadCelular_064RB dao_unidadCelular;

        public MAP_Producto_064RB()
        {
            dao_producto = new();
            dao_unidadCelular = new();
        }
        public void AgregarCelular_064RB(BE_Celular_064RB celular)
        {
            dao_producto.AgregarCelular_064RB(celular.Sku_064RB, celular.Marca_064RB, celular.Modelo_064RB, celular.Precio_064RB, celular.Descripcion_064RB, celular.Almacenamiento_064RB, celular.Color_064RB);      
        }
        public void AgregarAccesorio_064RB(BE_Accesorio_064RB accesorio)
        {
            dao_producto.AgregarAccesorio_064RB(accesorio.Sku_064RB, accesorio.Marca_064RB, accesorio.Modelo_064RB, accesorio.Precio_064RB, accesorio.Descripcion_064RB, accesorio.Categoria_064RB, accesorio.Stock_064RB);
        }
        public void Borrar_064RB(string sku)
        {
            dao_producto.Borrar_064RB(sku);
        }
        public void Modificar_064RB(BE_Producto_064RB producto)
        {
            if(producto is BE_Celular_064RB cel)
            {
                dao_producto.ModificarCelular_064RB(cel.Sku_064RB, cel.Marca_064RB, cel.Modelo_064RB, cel.Precio_064RB, cel.Descripcion_064RB, cel.Almacenamiento_064RB, cel.Color_064RB);
            }
            else if (producto is BE_Accesorio_064RB acc)
            {
                dao_producto.ModificarAccesorio_064RB(acc.Sku_064RB, acc.Marca_064RB, acc.Modelo_064RB, acc.Precio_064RB, acc.Descripcion_064RB, acc.Categoria_064RB, acc.Stock_064RB);
            }
        }
        public BE_Celular_064RB ConsultarCelularPorSku(string sku)
        {
            DataTable dt = dao_producto.ConsultarCelularPorSku_064RB(sku);
            if (dt.Rows.Count == 0) throw new Exception("No hay filas en celulares");
            DataRow fila = dt.Rows[0];
            return MapearCelular_064RB(fila);
        }
        public BE_Accesorio_064RB ConsultarAccesorioPorSku(string sku)
        {
            DataTable dt = dao_producto.ConsultarAccesorioPorSku_064RB(sku);
            if (dt.Rows.Count == 0) throw new Exception("No hay filas en accesorios");
            DataRow fila = dt.Rows[0];
            return MapearAccesorio_064RB(fila);
        }
        public List<BE_Producto_064RB> Consultar_064RB()
        {
            DataTable dtC = dao_producto.ConsultarCelulares_064RB();
            DataTable dtA = dao_producto.ConsultarAccesorios_064RB();
            DataTable dtUc = dao_unidadCelular.Consultar_064RB();
            var unidadesPorSku = MapearUnidadesPorSku_064RB(dtUc);
            List<BE_Producto_064RB> productos = new();
            
            foreach (DataRow fila in dtC.Rows)
            {
                BE_Celular_064RB celular = MapearCelular_064RB(fila);
                celular.UnidadCelulares_064RB = unidadesPorSku.ContainsKey(celular.Sku_064RB) ? unidadesPorSku[celular.Sku_064RB] : new List<BE_UnidadCelular_064RB>();
                foreach (BE_UnidadCelular_064RB unidad in celular.UnidadCelulares_064RB)
                {
                    unidad.Celular_064RB = celular;
                }
                productos.Add(celular);
            }
            foreach (DataRow fila in dtA.Rows)
            {
                productos.Add(MapearAccesorio_064RB(fila));
            }
            return productos;
        }
        private BE_Celular_064RB MapearCelular_064RB(DataRow fila)
        {
            return new BE_Celular_064RB(
                Convert.ToString(fila[0]),
                Convert.ToString(fila[1]),
                Convert.ToString(fila[2]),
                Convert.ToDecimal(fila[3]),
                Convert.ToString(fila[4]),
                Convert.ToString(fila[5]),
                Convert.ToString(fila[6])
            );
        }
        private BE_Accesorio_064RB MapearAccesorio_064RB(DataRow fila)
        {
            return new BE_Accesorio_064RB(
                Convert.ToString(fila[0]),
                Convert.ToString(fila[1]),
                Convert.ToString(fila[2]),
                Convert.ToDecimal(fila[3]),
                Convert.ToString(fila[4]),
                Convert.ToString(fila[5]),
                Convert.ToInt16(fila[6])
            );
        }
        private Dictionary<string, List<BE_UnidadCelular_064RB>> MapearUnidadesPorSku_064RB(DataTable dt)
        {
            var res = new Dictionary<string, List<BE_UnidadCelular_064RB>>();
            foreach (DataRow fila in dt.Rows)
            {
                string skuCelular = fila["SkuCelular_064RB"].ToString();
                if (!res.ContainsKey(skuCelular)) res[skuCelular] = new List<BE_UnidadCelular_064RB>();
                res[skuCelular].Add(new BE_UnidadCelular_064RB(fila["Imei_064RB"].ToString(), fila["Estado_064RB"].ToString()));
            }
            return res;
        }
    }
}
