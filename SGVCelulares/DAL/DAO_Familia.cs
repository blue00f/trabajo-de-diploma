using Microsoft.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class DAO_Familia : Conexion, IABMC
    {
        SqlCommand cm;
        public DAO_Familia() : base()
        {
            cm = new SqlCommand("select * from familia", con);
        }
        public void Agregar(params object[] datos)
        {
            cm.Parameters.Clear();
            cm.Parameters.Add("@nombre", SqlDbType.VarChar).Value = datos[0];

            cm.CommandText = "INSERT INTO familia(nombre) values (@nombre)";
            con.Open();
            cm.ExecuteNonQuery();
            con.Close();
        }

        public void Borrar(string id)
        {
            throw new NotImplementedException();
        }
        public void Modificar(string id, params object[] datos)
        {
            throw new NotImplementedException();
        }
        public SqlDataReader Consultar()
        {
            cm.Parameters.Clear();
            cm.CommandText = "SELECT * FROM familia";
            con.Open();
            return cm.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public SqlDataReader ConsultarPorId(string id)
        {
            throw new NotImplementedException();
        }
        public SqlDataReader ConsultarFamiliaFamilia()
        {
            cm.Parameters.Clear();
            cm.CommandText = "select * from familia_familia";
            con.Open();
            return cm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader ConsultarFamiliaPermiso()
        {
            cm.Parameters.Clear();
            cm.CommandText = "select * from permiso_familia";
            con.Open();
            return cm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public bool ValidarRepetido(string id)
        {
            throw new NotImplementedException();
        }
        public void AgregarPermiso(int idFamilia, int idPermiso)
        {
            cm.Parameters.Clear();
            cm.Parameters.Add("@idFamilia", SqlDbType.Int).Value = idFamilia;
            cm.Parameters.Add("@idPermiso", SqlDbType.Int).Value = idPermiso;

            cm.CommandText = "INSERT INTO permiso_familia(idFamilia, idPermiso) values (@idFamilia, @idPermiso)";
            con.Open();
            cm.ExecuteNonQuery();
            con.Close();
        }
        public void AgregarFamilia(int idFamiliaPadre, int idFamiliaHija)
        {
            cm.Parameters.Clear();
            cm.Parameters.Add("@idFamiliaPadre", SqlDbType.Int).Value = idFamiliaPadre;
            cm.Parameters.Add("@idFamiliaHija", SqlDbType.Int).Value = idFamiliaHija;

            cm.CommandText = "INSERT INTO familia_familia(idFamiliaPadre,idFamiliaHija) values (@idFamiliaPadre,@idFamiliaHija)";
            con.Open();
            cm.ExecuteNonQuery();
            con.Close();
        }
        public SqlDataReader ObtenerIdPorNombre(string nombre)
        {
            cm.Parameters.Clear();
            cm.Parameters.Add("@nombre", SqlDbType.VarChar).Value = nombre;
            cm.CommandText = "SELECT TOP 1 idFamilia, nombre FROM familia WHERE nombre=@nombre";
            con.Open();
            return cm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        // Métodos que van a servir para armar recursivamente los nodos de un arbol determinado
        public SqlDataReader ConsultarPermisos(int idFamilia)
        {
            cm.Parameters.Clear();
            cm.Parameters.Add("@idFamilia", SqlDbType.Int).Value = idFamilia;
            cm.CommandText = "select p.idPermiso, p.nombre from permiso p inner join permiso_familia pf on p.idPermiso = pf.idPermiso where pf.idFamilia = @idFamilia";
            con.Open();
            return cm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader ConsultarFamilia(int idFamilia)
        {
            cm.Parameters.Clear();
            cm.Parameters.Add("@idFamilia", SqlDbType.Int).Value = idFamilia;
            cm.CommandText = "select idFamilia, nombre from familia where idFamilia = @idFamilia";
            con.Open();
            return cm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader ConsultarFamiliasHijas(int idFamilia)
        {
            cm.Parameters.Clear();
            cm.Parameters.Add("@idFamilia", SqlDbType.Int).Value = idFamilia;
            cm.CommandText = "select f.idFamilia, f.nombre from familia f inner join familia_familia ff on f.idFamilia = ff.idFamiliaHija where ff.idFamiliaPadre = @idFamilia";
            con.Open();
            return cm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public bool ExisteFamilia(string nombreFamilia)
        {
            cm.Parameters.Clear();
            cm.Parameters.Add("@familia", SqlDbType.VarChar).Value = nombreFamilia;
            cm.CommandText = "SELECT COUNT(*) FROM familia WHERE nombre = @familia";
            con.Open();
            bool existe = Convert.ToInt16(cm.ExecuteScalar()) > 0;
            con.Close();
            return existe;
        }
        public bool EstaEnUso(int idFamilia)
        {
            cm.Parameters.Clear();
            cm.Parameters.Add("@idFamilia", SqlDbType.Int).Value = idFamilia;
            cm.CommandText = @"SELECT
        (SELECT COUNT(*) FROM rol_familia WHERE idFamilia=@idFamilia) +
        (SELECT COUNT(*) FROM familia_familia WHERE idFamiliaHija=@idFamilia)";
            con.Open();
            bool enUso = Convert.ToInt32(cm.ExecuteScalar()) > 0;
            con.Close();
            return enUso;
        }

        public void Borrar(int idFamilia)
        {
            cm.Parameters.Clear();
            cm.Parameters.Add("@idFamilia", SqlDbType.Int).Value = idFamilia;
            cm.CommandText = @"DELETE FROM permiso_familia WHERE idFamilia=@idFamilia;
                       DELETE FROM familia_familia WHERE idFamiliaPadre=@idFamilia;
                       DELETE FROM familia WHERE idFamilia=@idFamilia";
            con.Open();
            cm.ExecuteNonQuery();
            con.Close();
        }
    }
}
