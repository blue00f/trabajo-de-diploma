using BE_064RB;
using DAL_064RB.Negocio_064RB;
using System.Data;

namespace MAP_064RB.Negocio_064RB
{
    public class MAP_Accesorio_C_064RB
    {
        DAO_Accesorio_C_064RB dao_accesorio_c;
        public MAP_Accesorio_C_064RB() => dao_accesorio_c = new();
        public List<BE_Accesorio_C_064RB> Consultar_064RB()
        {
            DataTable dt = dao_accesorio_c.Consultar_064RB();
            var lista = new List<BE_Accesorio_C_064RB>();
            foreach (DataRow dr in dt.Rows)
            {
                lista.Add(Mapear(dr));
            }
            return lista;
        }
        public void Activar_064RB(int id) => dao_accesorio_c.Activar_064RB(id);
        private BE_Accesorio_C_064RB Mapear(DataRow fila)
        {
            return new BE_Accesorio_C_064RB
            (
                Convert.ToInt32(fila[0]),
                Convert.ToString(fila[1]),
                Convert.ToString(fila[2]),
                Convert.ToString(fila[3]),
                Convert.ToDecimal(fila[4]),
                Convert.ToString(fila[5]),
                Convert.ToInt32(fila[6]),
                Convert.ToString(fila[7]),
                Convert.ToInt32(fila[8]),
                Convert.ToBoolean(fila[9]),
                Convert.ToDateTime(fila[10]),
                Convert.ToBoolean(fila[11])
            );
        }
    }
}
