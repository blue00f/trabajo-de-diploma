using DAL;
using Microsoft.Data.SqlClient;
using Servicio;

namespace MAP
{
    public class MAP_Permiso
    {
        DAO_Permiso dao_permiso;

        public MAP_Permiso()
        {
            dao_permiso = new DAO_Permiso();
        }
        public List<SER_Permiso> Consultar()
        {
            List<SER_Permiso> lista = new List<SER_Permiso>();
            SqlDataReader dr = dao_permiso.Consultar();

            while (dr.Read())
            {
                object[] datos = new object[dr.FieldCount];
                dr.GetValues(datos);
                lista.Add(new SER_Permiso(datos));
            }
            dr.Close();
            return lista;
        }
    }
}
