using BE_064RB;
using DAL_064RB.Negocio_064RB;
using Servicio_064RB;
using System.Data;

namespace MAP_064RB.Negocio_064RB
{
    public class MAP_UnidadCelular_064RB
    {
        MAP_Producto_064RB map_producto;
        DAO_UnidadCelular_064RB dao_unidadCelular;
        public MAP_UnidadCelular_064RB()
        {
            dao_unidadCelular = new();
            map_producto = new();
        }

        public void Agregar_064RB(BE_UnidadCelular_064RB unidadC)
        {
            dao_unidadCelular.Agregar_064RB(unidadC.Imei_064RB, unidadC.Estado_064RB, unidadC.Celular_064RB.Sku_064RB);
        }
        public void Borrar_064RB(BE_UnidadCelular_064RB unidadC)
        {
            dao_unidadCelular.Borrar_064RB(unidadC.Imei_064RB);
        }
        public void Modificar_064RB(BE_UnidadCelular_064RB unidadC)
        {
            dao_unidadCelular.Modificar_064RB(unidadC.Estado_064RB, unidadC.Celular_064RB.Sku_064RB, unidadC.Imei_064RB);
        }
        public List<BE_UnidadCelular_064RB> Consultar_064RB()
        {
            DataTable dt = dao_unidadCelular.Consultar_064RB();
            List<BE_UnidadCelular_064RB> lista = new();
            foreach (DataRow f in dt.Rows)
            {
                lista.Add(Mapear_064RB(f));
            }
            return lista;
        }
        private BE_UnidadCelular_064RB Mapear_064RB(DataRow fila)
        {
            return new BE_UnidadCelular_064RB(
                Convert.ToString(fila[0]),
                Convert.ToString(fila[1]),
                map_producto.ConsultarCelularPorSku(Convert.ToString(fila[2]))
            );
        }
    }
}
