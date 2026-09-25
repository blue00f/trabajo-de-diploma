using System.Data;

namespace DAL_064RB.Tecnico_064RB
{
    public class DAO_Familia_064RB
    {
        ComandoBD_064RB com;
        public DAO_Familia_064RB() => com = new();
        public void Agregar_064RB(string nombre)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@nombre", SqlDbType.VarChar, nombre)
            };
            com.Ejecutar_064RB("INSERT INTO Familia_064RB(nombre) values (@nombre)", parametros);
        }
        public void Borrar_064RB(int idFamilia)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@idFamilia", SqlDbType.Int, idFamilia)
            };
            com.Ejecutar_064RB("DELETE FROM Permiso_familia_064RB WHERE idFamilia=@idFamilia; DELETE FROM Familia_familia_064RB WHERE idFamiliaPadre=@idFamilia; DELETE FROM Familia_064RB WHERE idFamilia=@idFamilia", parametros);
        }
        public void Modificar_064RB(string id, params object[] datos) => throw new NotImplementedException();
        public DataTable Consultar_064RB() => com.Consultar_064RB("SELECT * FROM Familia_064RB");
        public DataTable ConsultarFamiliaFamilia_064RB() => com.Consultar_064RB("select * from Familia_familia_064RB");
        public DataTable ConsultarFamiliaPermiso_064RB() => com.Consultar_064RB("select * from Permiso_familia_064RB");
        public void AgregarPermiso_064RB(int idFamilia, int idPermiso)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@idFamilia", SqlDbType.Int, idFamilia),
                new("@idPermiso", SqlDbType.Int, idPermiso)
            };
            com.Ejecutar_064RB("INSERT INTO Permiso_familia_064RB(idFamilia, idPermiso) values (@idFamilia, @idPermiso)", parametros);
        }
        public void AgregarFamilia_064RB(int idFamiliaPadre, int idFamiliaHija)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@idFamiliaPadre", SqlDbType.Int, idFamiliaPadre),
                new("@idFamiliaHija", SqlDbType.Int, idFamiliaHija)
            };
            com.Ejecutar_064RB("INSERT INTO Familia_familia_064RB(idFamiliaPadre,idFamiliaHija) values (@idFamiliaPadre,@idFamiliaHija)", parametros);
        }
        public DataTable ObtenerIdPorNombre_064RB(string nombre)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@nombre", SqlDbType.VarChar, nombre)
            };
            return com.Consultar_064RB("SELECT TOP 1 idFamilia, nombre FROM Familia_064RB WHERE nombre=@nombre", parametros);
        }

        // Métodos que van a servir para armar recursivamente los nodos de un arbol determinado
        public DataTable ConsultarPermisos_064RB(int idFamilia)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@idFamilia", SqlDbType.Int, idFamilia)
            };
            return com.Consultar_064RB("select p.idPermiso, p.nombre from Permiso_064RB p inner join Permiso_familia_064RB pf on p.idPermiso=pf.idPermiso where pf.idFamilia=@idFamilia", parametros);
        }
        public DataTable ConsultarFamilia_064RB(int idFamilia)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@idFamilia", SqlDbType.Int, idFamilia)
            };
            return com.Consultar_064RB("select idFamilia, nombre from Familia_064RB where idFamilia=@idFamilia", parametros);
        }
        public DataTable ConsultarFamiliasHijas_064RB(int idFamilia)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@idFamilia", SqlDbType.Int, idFamilia)
            };
            return com.Consultar_064RB("select f.idFamilia, f.nombre from Familia_064RB f inner join Familia_familia_064RB ff on f.idFamilia=ff.idFamiliaHija where ff.idFamiliaPadre=@idFamilia", parametros);
        }
        public bool ExisteFamilia_064RB(string nombreFamilia)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@familia", SqlDbType.VarChar, nombreFamilia)
            };
            object? res = com.Escalar_064RB("SELECT COUNT(*) FROM Familia_064RB WHERE nombre=@familia", parametros);
            return Convert.ToInt32(res) > 0;
        }
        public bool EstaEnUso_064RB(int idFamilia)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@idFamilia", SqlDbType.Int, idFamilia)
            };
            object? res = com.Escalar_064RB("SELECT (SELECT COUNT(*) FROM Rol_familia_064RB WHERE idFamilia=@idFamilia) (SELECT COUNT(*) FROM Familia_familia_064RB WHERE idFamiliaHija=@idFamilia)", parametros);
            return Convert.ToInt32(res) > 0;
        }
    }
}
