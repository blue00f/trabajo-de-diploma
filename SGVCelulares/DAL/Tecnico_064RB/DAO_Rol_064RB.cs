using System.Data;

namespace DAL_064RB.Tecnico_064RB
{
    public class DAO_Rol_064RB
    {
        ComandoBD_064RB com;
        public DAO_Rol_064RB() => com = new();
        public void Agregar_064RB(string nombre)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@nombre", SqlDbType.VarChar, nombre)
            };
            com.Ejecutar_064RB("INSERT INTO Rol_064RB(Nombre_064RB) values (@nombre)", parametros);
        }
        public DataTable ObtenerIdPorNombre_064RB(string nombre)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@nombre", SqlDbType.VarChar, nombre)
            };
            return com.Consultar_064RB("SELECT TOP 1 IdRol_064RB, Nombre_064RB FROM Rol_064RB WHERE Nombre_064RB=@nombre", parametros);
        }
        public DataTable Consultar_064RB() => com.Consultar_064RB("select * from Rol_064RB");
        public DataTable ConsultarRol_064RB(int idRol)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@idRol", SqlDbType.Int, idRol)
            };
            return com.Consultar_064RB("SELECT IdRol_064RB, Nombre_064RB FROM Rol_064RB WHERE IdRol_064RB=@idRol", parametros);
        }
        public DataTable ConsultarPermisos_064RB(int idRol)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@idRol", SqlDbType.Int, idRol)
            };
            return com.Consultar_064RB("select p.IdPermiso_064RB, p.Nombre_064RB from Permiso_064RB p inner join Rol_permiso_064RB rp on p.IdPermiso_064RB = rp.IdPermiso_064RB where rp.IdRol_064RB = @idRol", parametros);
        }
        public DataTable ConsultarFamilia_064RB(int idRol)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@idRol", SqlDbType.Int, idRol)
            };
            return com.Consultar_064RB("select f.IdFamilia_064RB, f.Nombre_064RB from Familia_064RB f inner join Rol_familia_064RB rf on f.IdFamilia_064RB = rf.IdFamilia_064RB where rf.IdRol_064RB = @idRol", parametros);
        }
        public DataTable ConsultarRolFamilia_064RB() => com.Consultar_064RB("select * from Rol_familia_064RB");
        public DataTable ConsultarRolPermiso_064RB() => com.Consultar_064RB("select * from Rol_permiso_064RB");
        public bool ExisteRol_064RB(string nombreRol)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@rol", SqlDbType.VarChar, nombreRol)
            };
            object? res = com.Escalar_064RB("SELECT COUNT(*) FROM Rol_064RB WHERE Nombre_064RB=@rol", parametros);
            return Convert.ToInt32(res) > 0;
        }
        public void AgregarPermiso_064RB(int idRol, int idPermiso)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@idRol", SqlDbType.Int, idRol),
                new("@idPermiso", SqlDbType.Int, idPermiso)
            };
            com.Ejecutar_064RB("INSERT INTO Rol_permiso_064RB(IdRol_064RB,IdPermiso_064RB) values (@idRol,@idPermiso)", parametros);
        }
        public void AgregarFamilia_064RB(int idRol, int idFamilia)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@idRol", SqlDbType.Int, idRol),
                new("@idFamilia", SqlDbType.Int, idFamilia)
            };
            com.Ejecutar_064RB("INSERT INTO Rol_familia_064RB(IdRol_064RB,IdFamilia_064RB) values (@idRol,@idFamilia)", parametros);
        }
        public void Borrar_064RB(int idRol)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@idRol", SqlDbType.Int, idRol)
            };
            com.Ejecutar_064RB("DELETE FROM Rol_permiso_064RB WHERE IdRol_064RB=@idRol; DELETE FROM Rol_familia_064RB WHERE IdRol_064RB=@idRol; DELETE FROM Rol_064RB WHERE IdRol_064RB=@idRol", parametros);
        }
        public bool EstaEnUso_064RB(int idRol)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@idRol", SqlDbType.Int, idRol)
            };
            object? res = com.Escalar_064RB("SELECT COUNT(*) FROM Usuario_064RB WHERE IdRol_064RB=@idRol", parametros);
            return Convert.ToInt32(res) > 0;
        }
    }
}
