using Microsoft.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class DAO_DV : Conexion
    {
        SqlCommand cm;
        //FALTA ACTUALIZAR EL SCRIPT INCLUYENDO LA TABLA DVH => NombreTabla,IdRegistro,Valor       DVV => NombreTabla,Valor
        public DAO_DV() : base()
        {
            cm = new SqlCommand("select * from dvh", con);
        }
        public void AgregarDVH(params object[] T)
        {
            cm.Parameters.Clear();
            cm.Parameters.Add("@NombreTabla", SqlDbType.VarChar).Value = T[0];
            cm.Parameters.Add("@IdRegistro", SqlDbType.VarChar).Value = T[1];
            cm.Parameters.Add("@Valor", SqlDbType.VarChar).Value = T[2];

            cm.CommandText = @"INSERT INTO dvh
                           (NombreTabla, IdRegistro, Valor)
                           VALUES
                           (@NombreTabla, @IdRegistro, @Valor)";
            con.Open();
            cm.ExecuteNonQuery();
            con.Close();
        }
        public void BorrarDVH(params object[] T)
        {
            cm.Parameters.Clear();
            cm.Parameters.Add("@NombreTabla", SqlDbType.VarChar).Value = T[0];
            cm.Parameters.Add("@IdRegistro", SqlDbType.VarChar).Value = T[1];
                cm.CommandText = @"DELETE FROM dvh
                           WHERE NombreTabla=@NombreTabla
                           AND IdRegistro=@IdRegistro";
            con.Open();
            cm.ExecuteNonQuery();
            con.Close();
        }

        public void ModificarDVH(params object[] T)
        {
            cm.Parameters.Clear();
            cm.Parameters.Add("@NombreTabla", SqlDbType.VarChar).Value = T[0];
            cm.Parameters.Add("@IdRegistro", SqlDbType.VarChar).Value = T[1];
            cm.Parameters.Add("@Valor", SqlDbType.VarChar).Value = T[2];

            cm.CommandText = @"UPDATE dvh
                           SET Valor=@Valor
                           WHERE NombreTabla=@NombreTabla
                           AND IdRegistro=@IdRegistro";
            con.Open();
            cm.ExecuteNonQuery();
            con.Close();
        }

        public void AgregarDVV(params object[] T)
        {
            cm.Parameters.Clear();
            cm.Parameters.Add("@NombreTabla", SqlDbType.VarChar).Value = T[0];
            cm.Parameters.Add("@Valor", SqlDbType.VarChar).Value = T[1];
            cm.CommandText = @"INSERT INTO dvv
                           (NombreTabla, Valor)
                           VALUES
                           (@NombreTabla, @Valor)";

            con.Open();
            cm.ExecuteNonQuery();
            con.Close();
        }
        public void BorrarDVV(params object[] T)
        {
            cm.Parameters.Clear();
            cm.Parameters.Add("@NombreTabla", SqlDbType.VarChar).Value = T[0];
            cm.CommandText = @"DELETE FROM dvv
                           WHERE NombreTabla=@NombreTabla";
            con.Open();
            cm.ExecuteNonQuery();
            con.Close();
        }
        public void ModificarDVV(params object[] T)
        {
            cm.Parameters.Clear();
            cm.Parameters.Add("@NombreTabla", SqlDbType.VarChar).Value = T[0];
            cm.Parameters.Add("@Valor", SqlDbType.VarChar).Value = T[1];
            cm.CommandText = @"UPDATE dvv
                           SET Valor=@Valor
                           WHERE NombreTabla=@NombreTabla";        
            con.Open();
            cm.ExecuteNonQuery();
            con.Close();
        }

        public bool ExisteDVH(string nombreTabla, string idRegistro)
        {
            cm.Parameters.Clear();
            cm.Parameters.Add("@NombreTabla", SqlDbType.VarChar).Value = nombreTabla;
            cm.Parameters.Add("@IdRegistro", SqlDbType.VarChar).Value = idRegistro;
            cm.CommandText = @"SELECT COUNT(*) 
                       FROM dvh 
                       WHERE NombreTabla = @NombreTabla 
                         AND IdRegistro = @IdRegistro";
            con.Open();

            int cantidad = (int)cm.ExecuteScalar();

            con.Close();

            return cantidad > 0;
        }
        public bool ExisteDVV(string nombreTabla)
        {
            cm.Parameters.Clear();
            cm.Parameters.Add("@NombreTabla", SqlDbType.VarChar).Value = nombreTabla;
            cm.CommandText = @"SELECT COUNT(*) 
                       FROM dvv 
                       WHERE NombreTabla = @NombreTabla";
            con.Open();
            int cantidad = (int)cm.ExecuteScalar();
            con.Close();
            return cantidad > 0;
        }
        public SqlDataReader ObtenerDVH(string nombreTabla, string idRegistro)
        {
            cm.Parameters.Clear();
            cm.Parameters.AddWithValue("@NombreTabla", nombreTabla);
            cm.Parameters.AddWithValue("@IdRegistro", idRegistro);
            cm.CommandText =(@"SELECT NombreTabla, IdRegistro, Valor
                              FROM dvh
                              WHERE NombreTabla = @NombreTabla
                              AND IdRegistro = @IdRegistro");
            con.Open();
            return cm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader ObtenerDVV(string nombreTabla)
        {
            cm.Parameters.Clear();
            cm.Parameters.AddWithValue("@NombreTabla", nombreTabla);
            cm.CommandText = (@"SELECT NombreTabla, Valor
                                FROM dvv
                                WHERE NombreTabla = @NombreTabla");
            con.Open();
            return cm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader ConsultarDVH(string nombreTabla)
        {
            cm.Parameters.Clear();
            cm.Parameters.Add("@NombreTabla", SqlDbType.VarChar).Value = nombreTabla;

            if (nombreTabla == "rol" || nombreTabla == "familia")
            {
                cm.CommandText = @"SELECT *
               FROM dvh
               WHERE NombreTabla = @NombreTabla
               ORDER BY CAST(IdRegistro AS INT)";
            }
            else if (nombreTabla == "usuario")
            {
                cm.CommandText = @"SELECT *
               FROM dvh
               WHERE NombreTabla = @NombreTabla
               ORDER BY IdRegistro";
            }
            else // tablas intermedias: rol_familia, rol_permiso, familia_familia, permiso_familia, etc.
            {
                cm.CommandText = @"SELECT *
               FROM dvh
               WHERE NombreTabla = @NombreTabla
               ORDER BY CAST(SUBSTRING(IdRegistro, 1, CHARINDEX('-', IdRegistro) - 1) AS INT),
                        CAST(SUBSTRING(IdRegistro, CHARINDEX('-', IdRegistro) + 1, LEN(IdRegistro)) AS INT)";
            }

            con.Open();
            SqlDataReader reader = cm.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }
    }
}
