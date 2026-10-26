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
            com.Ejecutar_064RB("INSERT INTO Familia_064RB(Nombre_064RB) values (@nombre)", parametros);
        }
        public void Borrar_064RB(int idFamilia)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@idFamilia", SqlDbType.Int, idFamilia)
            };
            com.Ejecutar_064RB("DELETE FROM Permiso_familia_064RB WHERE IdFamilia_064RB=@idFamilia; DELETE FROM Familia_familia_064RB WHERE IdFamiliaPadre_064RB=@idFamilia; DELETE FROM Familia_064RB WHERE IdFamilia_064RB=@idFamilia", parametros);
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
            com.Ejecutar_064RB("INSERT INTO Permiso_familia_064RB(IdFamilia_064RB, IdPermiso_064RB) values (@idFamilia, @idPermiso)", parametros);
        }
        public void AgregarFamilia_064RB(int idFamiliaPadre, int idFamiliaHija)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@idFamiliaPadre", SqlDbType.Int, idFamiliaPadre),
                new("@idFamiliaHija", SqlDbType.Int, idFamiliaHija)
            };
            com.Ejecutar_064RB("INSERT INTO Familia_familia_064RB(IdFamiliaPadre_064RB,IdFamiliaHija_064RB) values (@idFamiliaPadre,@idFamiliaHija)", parametros);
        }
        public DataTable ObtenerIdPorNombre_064RB(string nombre)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@nombre", SqlDbType.VarChar, nombre)
            };
            return com.Consultar_064RB("SELECT TOP 1 IdFamilia_064RB, Nombre_064RB FROM Familia_064RB WHERE Nombre_064RB=@nombre", parametros);
        }

        // Métodos que van a servir para armar recursivamente los nodos de un arbol determinado
        public DataTable ConsultarPermisos_064RB(int idFamilia)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@idFamilia", SqlDbType.Int, idFamilia)
            };
            return com.Consultar_064RB("select p.IdPermiso_064RB, p.Nombre_064RB from Permiso_064RB p inner join Permiso_familia_064RB pf on p.IdPermiso_064RB=pf.IdPermiso_064RB where pf.IdFamilia_064RB=@idFamilia", parametros);
        }
        public DataTable ConsultarFamilia_064RB(int idFamilia)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@idFamilia", SqlDbType.Int, idFamilia)
            };
            return com.Consultar_064RB("select IdFamilia_064RB, Nombre_064RB from Familia_064RB where IdFamilia_064RB=@idFamilia", parametros);
        }
        public DataTable ConsultarFamiliasHijas_064RB(int idFamilia)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@idFamilia", SqlDbType.Int, idFamilia)
            };
            return com.Consultar_064RB("select f.IdFamilia_064RB, f.Nombre_064RB from Familia_064RB f inner join Familia_familia_064RB ff on f.IdFamilia_064RB=ff.IdFamiliaHija_064RB where ff.IdFamiliaPadre_064RB=@idFamilia", parametros);
        }
        public bool ExisteFamilia_064RB(string nombreFamilia)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@familia", SqlDbType.VarChar, nombreFamilia)
            };
            object? res = com.Escalar_064RB("SELECT COUNT(*) FROM Familia_064RB WHERE Nombre_064RB=@familia", parametros);
            return Convert.ToInt32(res) > 0;
        }
        public bool EstaEnUso_064RB(int idFamilia)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@idFamilia", SqlDbType.Int, idFamilia)
            };
            object? res = com.Escalar_064RB("SELECT (SELECT COUNT(*) FROM Rol_familia_064RB WHERE IdFamilia_064RB=@idFamilia) (SELECT COUNT(*) FROM Familia_familia_064RB WHERE IdFamiliaHija_064RB=@idFamilia)", parametros);
            return Convert.ToInt32(res) > 0;
        }
    }
}
