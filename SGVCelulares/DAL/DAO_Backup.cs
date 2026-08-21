using Microsoft.Data.SqlClient;
using System.Data;
using System.Net;
using DotNetEnv;

namespace DAL
{
    public class DAO_Backup : Conexion
    {
        SqlCommand cm;
        SqlConnection con;
        public DAO_Backup()
        {
            string cadena = this.ObtenerCadenaConexion();
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(cadena);
            builder.InitialCatalog = "master";
            con = new SqlConnection(builder.ConnectionString);
            cm = new SqlCommand();
            cm.Connection = con;
            cm.CommandType = CommandType.Text;
        }

        public void Backup(string ruta)
        {
            cm.Parameters.Clear();
            cm.Parameters.Add("@ruta", SqlDbType.VarChar).Value = ruta;
            cm.CommandText = "BACKUP DATABASE bd_sgvcelulares TO DISK = @ruta WITH INIT";
            con.Open();
            try
            {
                cm.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        public void Restore(string ruta)
        {
            cm.Parameters.Clear();
            cm.Parameters.Add("@ruta", SqlDbType.VarChar).Value = ruta;
            cm.CommandText = @"ALTER DATABASE bd_sgvcelulares SET SINGLE_USER WITH ROLLBACK IMMEDIATE; RESTORE DATABASE bd_sgvcelulares FROM DISK = @ruta WITH REPLACE; ALTER DATABASE bd_sgvcelulares SET MULTI_USER;";
            con.Open();
            try
            {
                cm.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }
    }
}
