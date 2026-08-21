using Microsoft.Data.SqlClient;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DAL
{
    public class DAO_Rol : Conexion
    {
        SqlCommand cm;
        public DAO_Rol() : base()
        {
            cm = new SqlCommand("select * from rol", con);
        }
        public void Agregar(params object[] datos)
        {
            cm.Parameters.Clear();
            cm.Parameters.Add("@nombre", SqlDbType.VarChar).Value = datos[0];

            cm.CommandText = "INSERT INTO rol(nombre) values (@nombre)";
            con.Open();
            cm.ExecuteNonQuery();
            con.Close();
        }
        public SqlDataReader ObtenerIdPorNombre(string nombre)
        {
            cm.Parameters.Clear();
            cm.Parameters.Add("@nombre", SqlDbType.VarChar).Value = nombre;
            cm.CommandText = "SELECT TOP 1 idRol, nombre FROM rol WHERE nombre=@nombre";
            con.Open();
            return cm.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public SqlDataReader Consultar()
        {
            cm.Parameters.Clear();
            cm.CommandText = "SELECT * FROM rol";
            con.Open();
            return cm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader ConsultarRol(int idRol)
        {
            cm.Parameters.Clear();
            cm.Parameters.Add("@idRol", SqlDbType.Int).Value = idRol;
            cm.CommandText = "SELECT idRol, nombre FROM rol WHERE idRol=@idRol";
            con.Open();
            return cm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader ConsultarPermisos(int idRol)
        {
            cm.Parameters.Clear();
            cm.Parameters.Add("@idRol", SqlDbType.Int).Value = idRol;
            cm.CommandText = "select p.idPermiso, p.nombre from permiso p inner join rol_permiso rp on p.idPermiso = rp.idPermiso where rp.idRol = @idRol";
            con.Open();
            return cm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader ConsultarFamilia(int idRol)
        {
            cm.Parameters.Clear();
            cm.Parameters.Add("@idRol", SqlDbType.Int).Value = idRol;
            cm.CommandText = "select f.idFamilia, f.nombre from familia f inner join rol_familia rf on f.idFamilia = rf.idFamilia where rf.idRol = @idRol";
            con.Open();
            return cm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader ConsultarRolFamilia()
        {
            cm.Parameters.Clear();
            cm.CommandText = "select * from rol_familia";
            con.Open();
            return cm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader ConsultarRolPermiso()
        {
            cm.Parameters.Clear();
            cm.CommandText = "select * from rol_permiso";
            con.Open();
            return cm.ExecuteReader(CommandBehavior.CloseConnection);
        }


        public bool ExisteRol(string nombreRol)
        {
            cm.Parameters.Clear();
            cm.Parameters.Add("@rol", SqlDbType.VarChar).Value = nombreRol;
            cm.CommandText = "SELECT COUNT(*) FROM rol WHERE nombre = @rol";
            con.Open();
            bool existe = Convert.ToInt16(cm.ExecuteScalar()) > 0;
            con.Close();
            return existe;
        }
        public void AgregarPermiso(int idRol, int idPermiso)
        {
            cm.Parameters.Clear();
            cm.Parameters.Add("@idRol", SqlDbType.Int).Value = idRol;
            cm.Parameters.Add("@idPermiso", SqlDbType.Int).Value = idPermiso;

            cm.CommandText = "INSERT INTO rol_permiso(idRol,idPermiso) values (@idRol,@idPermiso)";
            con.Open();
            cm.ExecuteNonQuery();
            con.Close();
        }
        public void AgregarFamilia(int idRol, int idFamilia)
        {
            cm.Parameters.Clear();
            cm.Parameters.Add("@idRol", SqlDbType.Int).Value = idRol;
            cm.Parameters.Add("@idFamilia", SqlDbType.Int).Value = idFamilia;

            cm.CommandText = "INSERT INTO rol_familia(idRol,idFamilia) values (@idRol,@idFamilia)";
            con.Open();
            cm.ExecuteNonQuery();
            con.Close();
        }
        public void Borrar(int idRol)
        {
            cm.Parameters.Clear();
            cm.Parameters.Add("@idRol", SqlDbType.Int).Value = idRol;
            cm.CommandText = @"DELETE FROM rol_permiso WHERE idRol=@idRol;
                       DELETE FROM rol_familia WHERE idRol=@idRol;
                       DELETE FROM rol WHERE idRol=@idRol";
            con.Open();
            cm.ExecuteNonQuery();
            con.Close();
        }
        public bool EstaEnUso(int idRol)
        {
            cm.Parameters.Clear();
            cm.Parameters.Add("@idRol", SqlDbType.Int).Value = idRol;
            cm.CommandText = "SELECT COUNT(*) FROM usuario WHERE idRol=@idRol";
            con.Open();
            bool enUso = Convert.ToInt32(cm.ExecuteScalar()) > 0;
            con.Close();
            return enUso;
        }
    }
}
