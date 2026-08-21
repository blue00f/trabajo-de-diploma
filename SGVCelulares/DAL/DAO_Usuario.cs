using Microsoft.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class DAO_Usuario : Conexion,IABMC
    {
        SqlCommand cm;
        public DAO_Usuario() : base()
        {
            cm = new SqlCommand("select * from usuario", con);
        }
        public void Agregar(params object[] T)
        {
            cm.Parameters.Clear();
            cm.Parameters.Add("@dni", SqlDbType.VarChar).Value = T[0];
            cm.Parameters.Add("@nombre", SqlDbType.VarChar).Value = T[1];
            cm.Parameters.Add("@apellido", SqlDbType.VarChar).Value = T[2];
            cm.Parameters.Add("@correo", SqlDbType.VarChar).Value = T[3];
            cm.Parameters.Add("@nombreUsuario", SqlDbType.VarChar).Value = T[4];
            cm.Parameters.Add("@password", SqlDbType.VarChar).Value = T[5];
            cm.Parameters.Add("@bloqueo", SqlDbType.Bit).Value = T[6];
            cm.Parameters.Add("@activo", SqlDbType.Bit).Value = T[7];
            cm.Parameters.Add("@cantIntentos", SqlDbType.TinyInt).Value = T[8];
            cm.Parameters.Add("@idRol", SqlDbType.Int).Value = T[9];
            cm.Parameters.Add("@idIdioma", SqlDbType.VarChar).Value = T[10];

            cm.CommandText = "INSERT INTO usuario(dni,nombre,apellido,correo,nombreUsuario,password,bloqueo,activo,cantIntentos,idRol,idIdioma) values (@dni,@nombre,@apellido,@correo,@nombreUsuario,@password,@bloqueo,@activo,@cantIntentos,@idRol,@idIdioma)";
            con.Open();
            cm.ExecuteNonQuery();
            con.Close();
        }

        public void Borrar(string id)
        {
            throw new NotImplementedException();
        }
        public void Modificar(string dni, params object[] T)
        {
            cm.Parameters.Clear();
            cm.Parameters.Add("@dni", SqlDbType.VarChar).Value = dni;
            cm.Parameters.Add("@nombre", SqlDbType.VarChar).Value = T[0];
            cm.Parameters.Add("@apellido", SqlDbType.VarChar).Value = T[1];
            cm.Parameters.Add("@nombreUsuario", SqlDbType.VarChar).Value = T[2];
            cm.CommandText = "UPDATE usuario SET nombre=@nombre,apellido=@apellido,nombreUsuario=@nombreUsuario WHERE dni=@dni";
            con.Open();
            cm.ExecuteNonQuery();
            con.Close();
        }
        public void ModificarPassword(string dni, string password)
        {
            cm.Parameters.Clear();
            cm.Parameters.Add("@dni", SqlDbType.VarChar).Value = dni;
            cm.Parameters.Add("@password", SqlDbType.VarChar).Value = password;
            cm.CommandText = "UPDATE usuario SET password=@password WHERE dni=@dni";
            con.Open();
            cm.ExecuteNonQuery();
            con.Close();
        }
        public SqlDataReader Consultar()
        {
            cm.Parameters.Clear();
            cm.CommandText = "SELECT * FROM usuario";
            con.Open();
            return cm.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public SqlDataReader ConsultarPorId(string dni)
        {
            cm.Parameters.Clear();
            cm.Parameters.Add("@dni", SqlDbType.VarChar).Value = dni;
            cm.CommandText = "SELECT * FROM usuario WHERE dni=@dni";
            con.Open();
            return cm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader ConsultarPorNombreUsuario(string nombreUsuario)
        {
            cm.Parameters.Clear();
            cm.Parameters.Add("@nombreUsuario", SqlDbType.VarChar).Value = nombreUsuario;
            cm.CommandText = "SELECT * FROM usuario WHERE nombreUsuario=@nombreUsuario";
            con.Open();
            return cm.ExecuteReader(CommandBehavior.CloseConnection);
        }
      
        public bool ValidarRepetido(string dni)
        {
            cm.Parameters.Clear();
            cm.Parameters.Add("@dni", SqlDbType.VarChar).Value = dni;
            cm.CommandText = "SELECT COUNT(*) FROM usuario WHERE dni = @dni";
            con.Open();
            bool existe = Convert.ToInt16(cm.ExecuteScalar()) > 0;
            con.Close();
            return existe;
        }
        public bool ExisteNombreUsuario(string nombreUsuario)
        {
            cm.Parameters.Clear();
            cm.Parameters.Add("@nombreUsuario", SqlDbType.VarChar).Value = nombreUsuario;
            cm.CommandText = "SELECT COUNT(*) FROM usuario WHERE nombreUsuario = @nombreUsuario";
            con.Open();
            bool existe = Convert.ToInt16(cm.ExecuteScalar()) > 0;
            con.Close();
            return existe;
        }
        public bool ValidarCorreoRepetido(string correo)
        {
            cm.Parameters.Clear();
            cm.Parameters.Add("@correo", SqlDbType.VarChar).Value = correo;
            cm.CommandText = "SELECT COUNT(*) FROM usuario WHERE correo = @correo";
            con.Open();
            bool existe = Convert.ToInt16(cm.ExecuteScalar()) > 0;
            con.Close();
            return existe;
        }
        public void ReiniciarIntentos(string dni)
        {
            cm.Parameters.Clear();
            cm.Parameters.Add("@dni", SqlDbType.VarChar).Value = dni;
            cm.CommandText = "UPDATE usuario SET cantIntentos=0 WHERE dni=@dni";
            con.Open();
            cm.ExecuteNonQuery();
            con.Close();
        }
        public void SumarCantidadIntento(string dni)
        {
            cm.Parameters.Clear();
            cm.Parameters.Add("@dni", SqlDbType.VarChar).Value = dni;
            cm.CommandText = "UPDATE usuario SET cantIntentos=cantIntentos+1 WHERE dni=@dni";
            con.Open();
            cm.ExecuteNonQuery();
            con.Close();
        }
        public void Bloquear(string dni)
        {
            cm.Parameters.Clear();
            cm.Parameters.Add("@dni", SqlDbType.VarChar).Value = dni;
            cm.CommandText = "UPDATE usuario SET bloqueo=1 WHERE dni=@dni";
            con.Open();
            cm.ExecuteNonQuery();
            con.Close();
        }
        public void Desbloquear(string dni)
        {
            cm.Parameters.Clear();
            cm.Parameters.Add("@dni", SqlDbType.VarChar).Value = dni;
            cm.CommandText = "UPDATE usuario SET bloqueo=0 WHERE dni=@dni";
            con.Open();
            cm.ExecuteNonQuery();
            con.Close();
        }
        public void CambiarEstadoActivo(string dni, bool activo)
        {
            cm.Parameters.Clear();
            cm.Parameters.Add("@dni", SqlDbType.VarChar).Value = dni;
            cm.Parameters.Add("@activo", SqlDbType.Bit).Value = activo;
            cm.CommandText = "UPDATE usuario SET activo=@activo WHERE dni=@dni";
            con.Open();
            cm.ExecuteNonQuery();
            con.Close();
        }
        public void ModificarIdioma(string dni, string idIdioma)
        {
            cm.Parameters.Clear();
            cm.Parameters.Add("@dni", SqlDbType.VarChar).Value = dni;
            cm.Parameters.Add("@idIdioma", SqlDbType.VarChar).Value = idIdioma;
            cm.CommandText = "UPDATE usuario SET idIdioma=@idIdioma WHERE dni=@dni";
            con.Open();
            cm.ExecuteNonQuery();
            con.Close();
        }
    }
}
