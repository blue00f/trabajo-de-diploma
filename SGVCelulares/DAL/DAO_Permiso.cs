using Microsoft.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class DAO_Permiso : Conexion
    {
        SqlCommand cm;
        public DAO_Permiso() : base()
        {
            cm = new SqlCommand("select * from permiso", con);
        }

        public SqlDataReader Consultar()
        {
            cm.Parameters.Clear();
            cm.CommandText = "SELECT * FROM permiso";
            con.Open();
            return cm.ExecuteReader(CommandBehavior.CloseConnection);
        }
    }
}
