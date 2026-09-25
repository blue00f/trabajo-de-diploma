using System.Data;

namespace DAL_064RB.Tecnico_064RB
{
    public class DAO_DV_064RB
    {
        ComandoBD_064RB com;
        public DAO_DV_064RB() => com = new();
        public void AgregarDVH_064RB(string nombreTabla, string idRegistro, string valor)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@nombreTabla", SqlDbType.VarChar, nombreTabla),
                new("@idRegistro", SqlDbType.VarChar, idRegistro),
                new("@valor", SqlDbType.VarChar, valor)
            };
            com.Ejecutar_064RB("insert into Dvh_064RB (nombreTabla,idRegistro,valor) values (@nombreTabla,@idRegistro,@valor)", parametros);
        }
        public void BorrarDVH_064RB(string nombreTabla, string idRegistro)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@nombreTabla", SqlDbType.VarChar, nombreTabla),
                new("@idRegistro", SqlDbType.VarChar, idRegistro),
            };
            com.Ejecutar_064RB("DELETE FROM Dvh_064RB WHERE NombreTabla=@nombreTabla AND IdRegistro=@idRegistro", parametros);
        }

        public void ModificarDVH_064RB(string nombreTabla, string idRegistro, string valor)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@nombreTabla", SqlDbType.VarChar, nombreTabla),
                new("@idRegistro", SqlDbType.VarChar, idRegistro),
                new("@valor", SqlDbType.VarChar, valor)
            };
            com.Ejecutar_064RB("UPDATE Dvh_064RB SET Valor=@Valor WHERE NombreTabla=@nombreTabla AND IdRegistro=@idRegistro", parametros);
        }

        public void AgregarDVV_064RB(string nombreTabla, string valor)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@nombreTabla", SqlDbType.VarChar, nombreTabla),
                new("@valor", SqlDbType.VarChar, valor)
            };
            com.Ejecutar_064RB("INSERT INTO Dvv_064RB (NombreTabla, Valor) VALUES (@nombreTabla, @valor)", parametros);
        }
        public void BorrarDVV_064RB(string nombreTabla)
        {
            var parametros = new List<ParametroBD_064RB> { new("@nombreTabla", SqlDbType.VarChar, nombreTabla) };
            com.Ejecutar_064RB("DELETE FROM Dvv_064RB WHERE NombreTabla=@nombreTabla", parametros);
        }
        public void ModificarDVV_064RB(string nombreTabla, string valor)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@nombreTabla", SqlDbType.VarChar, nombreTabla),
                new("@valor", SqlDbType.VarChar, valor)
            };
            com.Ejecutar_064RB("UPDATE Dvv_064RB SET Valor=@valor WHERE NombreTabla=@nombreTabla", parametros);
        }

        public bool ExisteDVH_064RB(string nombreTabla, string idRegistro)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@nombreTabla", SqlDbType.VarChar, nombreTabla),
                new("@idRegistro", SqlDbType.VarChar, idRegistro)
            };
            object? res = com.Escalar_064RB("SELECT COUNT(*) FROM Dvh_064RB WHERE NombreTabla=@nombreTabla AND IdRegistro=@idRegistro", parametros);
            return Convert.ToInt32(res) > 0;
        }
        public bool ExisteDVV_064RB(string nombreTabla)
        {
            var parametros = new List<ParametroBD_064RB> { new("@nombreTabla", SqlDbType.VarChar, nombreTabla) };
            object? res = com.Escalar_064RB("SELECT COUNT(*) FROM Dvv_064RB WHERE NombreTabla=@nombreTabla", parametros);
            return Convert.ToInt32(res) > 0;
        }
        public DataTable ObtenerDVH_064RB(string nombreTabla, string idRegistro)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@nombreTabla", SqlDbType.VarChar, nombreTabla),
                new("@idRegistro", SqlDbType.VarChar, idRegistro)
            };
            return com.Consultar_064RB("SELECT NombreTabla, IdRegistro, Valor FROM Dvh_064RB WHERE NombreTabla=@nombreTabla AND IdRegistro=@idRegistro", parametros);
        }
        public DataTable ObtenerDVV_064RB(string nombreTabla)
        {
            var parametros = new List<ParametroBD_064RB> { new("@nombreTabla", SqlDbType.VarChar, nombreTabla) };
            return com.Consultar_064RB("SELECT NombreTabla, Valor FROM Dvv_064RB WHERE NombreTabla = @nombreTabla", parametros);
        }
        public DataTable ConsultarDVH_064RB(string nombreTabla)
        {
            DataTable tabla = null;
            var parametros = new List<ParametroBD_064RB> { new("@nombreTabla", SqlDbType.VarChar, nombreTabla) };
            string sql = @"SELECT * FROM Dvh_064RB WHERE NombreTabla=@nombreTabla ORDER BY CASE WHEN CHARINDEX('-', IdRegistro) = 0 THEN TRY_CAST(IdRegistro AS INT) ELSE TRY_CAST(SUBSTRING(IdRegistro, 1, CHARINDEX('-', IdRegistro) - 1) AS INT) END, CASE WHEN CHARINDEX('-', IdRegistro) > 0 THEN TRY_CAST(SUBSTRING(IdRegistro, CHARINDEX('-', IdRegistro) + 1, LEN(IdRegistro)) AS INT) END";
            return com.Consultar_064RB(sql, parametros);
        }
    }
}
