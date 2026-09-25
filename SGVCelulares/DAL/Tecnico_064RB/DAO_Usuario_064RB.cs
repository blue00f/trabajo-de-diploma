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
            com.Ejecutar_064RB("INSERT INTO Usuario_064RB(dni,nombre,apellido,correo,nombreUsuario,password,bloqueo,activo,cantIntentos,idRol,idIdioma) values (@dni,@nombre,@apellido,@correo,@nombreUsuario,@password,@bloqueo,@activo,@cantIntentos,@idRol,@idIdioma)", parametros);
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
            com.Ejecutar_064RB("UPDATE Usuario_064RB SET nombre=@nombre,apellido=@apellido,nombreUsuario=@nombreUsuario WHERE dni=@dni", parametros);
        }
        public void ModificarPassword_064RB(string dni, string password)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@dni", SqlDbType.VarChar, dni),
                new("@password", SqlDbType.VarChar, password)
            };
            com.Ejecutar_064RB("UPDATE Usuario_064RB SET password=@password WHERE dni=@dni", parametros);
        }
        public DataTable Consultar_064RB() => com.Consultar_064RB("select * from Usuario_064RB");

        public DataTable ConsultarPorId_064RB(string dni)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@dni", SqlDbType.VarChar, dni)
            };
            return com.Consultar_064RB("SELECT * FROM Usuario_064RB WHERE dni=@dni", parametros);
        }
        public DataTable ConsultarPorNombreUsuario_064RB(string nombreUsuario)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@nombreUsuario", SqlDbType.VarChar, nombreUsuario)
            };
            return com.Consultar_064RB("SELECT * FROM Usuario_064RB WHERE nombreUsuario=@nombreUsuario", parametros);
        }
      
        public bool ValidarRepetido_064RB(string dni)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@dni", SqlDbType.VarChar, dni)
            };
            object? res = com.Escalar_064RB("SELECT COUNT(*) FROM Usuario_064RB WHERE dni=@dni", parametros);
            return Convert.ToInt32(res) > 0;
        }
        public bool ExisteNombreUsuario_064RB(string nombreUsuario)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@nombreUsuario", SqlDbType.VarChar, nombreUsuario)
            };
            object? res = com.Escalar_064RB("SELECT COUNT(*) FROM Usuario_064RB WHERE nombreUsuario=@nombreUsuario", parametros);
            return Convert.ToInt32(res) > 0;
        }
        public bool ValidarCorreoRepetido_064RB(string correo)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@correo", SqlDbType.VarChar, correo)
            };
            object? res = com.Escalar_064RB("SELECT COUNT(*) FROM Usuario_064RB WHERE correo=@correo", parametros);
            return Convert.ToInt32(res) > 0;
        }
        public void ReiniciarIntentos_064RB(string dni)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@dni", SqlDbType.VarChar, dni)
            };
            com.Ejecutar_064RB("UPDATE Usuario_064RB SET cantIntentos=0 WHERE dni=@dni", parametros);
        }
        public void SumarCantidadIntento_064RB(string dni)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@dni", SqlDbType.VarChar, dni)
            };
            com.Ejecutar_064RB("UPDATE Usuario_064RB SET cantIntentos=cantIntentos+1 WHERE dni=@dni", parametros);
        }
        public void Bloquear_064RB(string dni)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@dni", SqlDbType.VarChar, dni)
            };
            com.Ejecutar_064RB("UPDATE Usuario_064RB SET bloqueo=1 WHERE dni=@dni", parametros);
        }
        public void Desbloquear_064RB(string dni)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@dni", SqlDbType.VarChar, dni)
            };
            com.Ejecutar_064RB("UPDATE Usuario_064RB SET bloqueo=0 WHERE dni=@dni", parametros);
        }
        public void CambiarEstadoActivo_064RB(string dni, bool activo)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@dni", SqlDbType.VarChar, dni),
                new("@activo", SqlDbType.Bit, activo)
            };
            com.Ejecutar_064RB("UPDATE Usuario_064RB SET activo=@activo WHERE dni=@dni", parametros);
        }
        public void ModificarIdioma_064RB(string dni, string idIdioma)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@dni", SqlDbType.VarChar, dni),
                new("@idIdioma", SqlDbType.VarChar, idIdioma)
            };
            com.Ejecutar_064RB("UPDATE Usuario_064RB SET idIdioma=@idIdioma WHERE dni=@dni", parametros);
        }
    }
}
