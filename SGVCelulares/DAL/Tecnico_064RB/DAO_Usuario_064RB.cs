using System.Data;

namespace DAL_064RB
{
    public class DAO_Usuario_064RB
    {
        ComandoBD_064RB com;
        public DAO_Usuario_064RB() => com = new();
        public void Agregar_064RB(string dni, string nombre, string apellido, string correo, string nombreUsuario, string password, bool bloqueo, bool activo, int cantIntentos, int idRol, string idIdioma)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@dni", SqlDbType.VarChar, dni),
                new("@nombre", SqlDbType.VarChar, nombre),
                new("@apellido", SqlDbType.VarChar, apellido),
                new("@correo", SqlDbType.VarChar, correo),
                new("@nombreUsuario", SqlDbType.VarChar, nombreUsuario),
                new("@password", SqlDbType.VarChar, password),
                new("@bloqueo", SqlDbType.Bit, bloqueo),
                new("@activo", SqlDbType.Bit, activo),
                new("@cantIntentos", SqlDbType.TinyInt, cantIntentos),
                new("@idRol", SqlDbType.TinyInt, idRol),
                new("@idIdioma", SqlDbType.VarChar, idIdioma)
            };
            com.Ejecutar_064RB("INSERT INTO Usuario_064RB(Dni_064RB,Nombre_064RB,Apellido_064RB,Correo_064RB,NombreUsuario_064RB,Password_064RB,Bloqueo_064RB,Activo_064RB,CantIntentos_064RB,IdRol_064RB,IdIdioma_064RB) values (@dni,@nombre,@apellido,@correo,@nombreUsuario,@password,@bloqueo,@activo,@cantIntentos,@idRol,@idIdioma)", parametros);
        }
        public void Modificar_064RB(string dni, string nombre, string apellido, string nombreUsuario)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@dni", SqlDbType.VarChar, dni),
                new("@nombre", SqlDbType.VarChar, nombre),
                new("@apellido", SqlDbType.VarChar, apellido),
                new("@nombreUsuario", SqlDbType.VarChar, nombreUsuario)
            };
            com.Ejecutar_064RB("UPDATE Usuario_064RB SET Nombre_064RB=@nombre,Apellido_064RB=@apellido,NombreUsuario_064RB=@nombreUsuario WHERE Dni_064RB=@dni", parametros);
        }
        public void ModificarPassword_064RB(string dni, string password)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@dni", SqlDbType.VarChar, dni),
                new("@password", SqlDbType.VarChar, password)
            };
            com.Ejecutar_064RB("UPDATE Usuario_064RB SET Password_064RB=@password WHERE Dni_064RB=@dni", parametros);
        }
        public DataTable Consultar_064RB() => com.Consultar_064RB("select * from Usuario_064RB");

        public DataTable ConsultarPorId_064RB(string dni)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@dni", SqlDbType.VarChar, dni)
            };
            return com.Consultar_064RB("SELECT * FROM Usuario_064RB WHERE Dni_064RB=@dni", parametros);
        }
        public DataTable ConsultarPorNombreUsuario_064RB(string nombreUsuario)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@nombreUsuario", SqlDbType.VarChar, nombreUsuario)
            };
            return com.Consultar_064RB("SELECT * FROM Usuario_064RB WHERE NombreUsuario_064RB=@nombreUsuario", parametros);
        }
      
        public bool ValidarRepetido_064RB(string dni)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@dni", SqlDbType.VarChar, dni)
            };
            object? res = com.Escalar_064RB("SELECT COUNT(*) FROM Usuario_064RB WHERE Dni_064RB=@dni", parametros);
            return Convert.ToInt32(res) > 0;
        }
        public bool ExisteNombreUsuario_064RB(string nombreUsuario)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@nombreUsuario", SqlDbType.VarChar, nombreUsuario)
            };
            object? res = com.Escalar_064RB("SELECT COUNT(*) FROM Usuario_064RB WHERE NombreUsuario_064RB=@nombreUsuario", parametros);
            return Convert.ToInt32(res) > 0;
        }
        public bool ValidarCorreoRepetido_064RB(string correo)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@correo", SqlDbType.VarChar, correo)
            };
            object? res = com.Escalar_064RB("SELECT COUNT(*) FROM Usuario_064RB WHERE Correo_064RB=@correo", parametros);
            return Convert.ToInt32(res) > 0;
        }
        public void ReiniciarIntentos_064RB(string dni)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@dni", SqlDbType.VarChar, dni)
            };
            com.Ejecutar_064RB("UPDATE Usuario_064RB SET CantIntentos_064RB=0 WHERE Dni_064RB=@dni", parametros);
        }
        public void SumarCantidadIntento_064RB(string dni)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@dni", SqlDbType.VarChar, dni)
            };
            com.Ejecutar_064RB("UPDATE Usuario_064RB SET CantIntentos_064RB=CantIntentos_064RB+1 WHERE Dni_064RB=@dni", parametros);
        }
        public void Bloquear_064RB(string dni)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@dni", SqlDbType.VarChar, dni)
            };
            com.Ejecutar_064RB("UPDATE Usuario_064RB SET Bloqueo_064RB=1 WHERE Dni_064RB=@dni", parametros);
        }
        public void Desbloquear_064RB(string dni)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@dni", SqlDbType.VarChar, dni)
            };
            com.Ejecutar_064RB("UPDATE Usuario_064RB SET Bloqueo_064RB=0 WHERE Dni_064RB=@dni", parametros);
        }
        public void CambiarEstadoActivo_064RB(string dni, bool activo)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@dni", SqlDbType.VarChar, dni),
                new("@activo", SqlDbType.Bit, activo)
            };
            com.Ejecutar_064RB("UPDATE Usuario_064RB SET Activo_064RB=@activo WHERE Dni_064RB=@dni", parametros);
        }
        public void ModificarIdioma_064RB(string dni, string idIdioma)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@dni", SqlDbType.VarChar, dni),
                new("@idIdioma", SqlDbType.VarChar, idIdioma)
            };
            com.Ejecutar_064RB("UPDATE Usuario_064RB SET IdIdioma_064RB=@idIdioma WHERE Dni_064RB=@dni", parametros);
        }
    }
}
