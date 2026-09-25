using BE_064RB;
using DAL_064RB.Negocio_064RB;
using Servicio_064RB;
using System.Data;

namespace MAP_064RB.Negocio_064RB
{
    public class MAP_Producto_064RB
    {
        DAO_Producto_064RB dao_producto;
        public MAP_Producto_064RB()
        {
            dao_producto = new();
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
        public List<BE_ProductoListado_064RB> Consultar_064RB()
        {
            DataTable dt = dao_producto.Consultar_064RB();
            List<BE_ProductoListado_064RB> productos = new();

            foreach (DataRow row in dt.Rows)
            {
                productos.Add(new BE_ProductoListado_064RB
                {
                    Sku_064RB = row["sku"].ToString(),
                    Marca_064RB = row["marca"].ToString(),
                    Modelo_064RB = row["modelo"].ToString(),
                    Precio_064RB = Convert.ToDecimal(row["precio"]),
                    Descripcion_064RB = row["descripcion"].ToString(),
                    Tipo_064RB = row["tipo"].ToString(),
                    Almacenamiento_064RB = row["almacenamiento"] == DBNull.Value
                        ? null
                        : row["almacenamiento"].ToString(),
                    Color_064RB = row["color"] == DBNull.Value
                        ? null
                        : row["color"].ToString(),
                    Categoria_064RB = row["categoria"] == DBNull.Value
                        ? null
                        : row["categoria"].ToString(),
                    Stock_064RB = Convert.ToInt32(row["stock"])
                });
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
    }
}
