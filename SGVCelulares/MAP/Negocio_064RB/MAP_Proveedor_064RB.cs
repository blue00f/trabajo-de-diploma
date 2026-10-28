using BE_064RB;
using DAL_064RB.Negocio_064RB;
using System.Data;

namespace MAP_064RB.Negocio_064RB
{
    public class MAP_Proveedor_064RB
    {
        DAO_Proveedor_064RB dao_proveedor;
        public MAP_Proveedor_064RB()
        {
            dao_proveedor = new();
        }
        public void Agregar_064RB(BE_Proveedor_064RB proveedor) => dao_proveedor.Agregar_064B(proveedor.Cuit_064RB, proveedor.RazonSocial_064RB);
        public void Borrar_064RB(BE_Proveedor_064RB proveedor) => dao_proveedor.Borrar_064RB(proveedor.Cuit_064RB);
        public void Modificar_064RB(BE_Proveedor_064RB proveedor) => dao_proveedor.Modificar_064RB(proveedor.Cuit_064RB, proveedor.RazonSocial_064RB);
        public List<BE_Proveedor_064RB> Consultar()
        {
            List<BE_Proveedor_064RB> lista = new();
            DataTable dt = dao_proveedor.Consultar_064RB();
            foreach (DataRow fila in dt.Rows)
            {
                lista.Add(Mapear_064RB(fila));
            }
            return lista;
        }
        private BE_Proveedor_064RB Mapear_064RB(DataRow fila)
        {
            return new BE_Proveedor_064RB(
                Convert.ToString(fila[0]),
                Convert.ToString(fila[1])
            );
        }
    }
}
