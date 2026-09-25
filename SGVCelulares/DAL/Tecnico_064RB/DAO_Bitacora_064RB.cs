using System.Data;

namespace DAL_064RB.Tecnico_064RB
{
    public class DAO_Bitacora_064RB
    {
        ComandoBD_064RB com;
        public DAO_Bitacora_064RB() => com = new();
        public void Agregar_064RB(string dni, DateTime fecha, string modulo, string evento, int criticidad)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@dni", SqlDbType.VarChar, dni),
                new("@fecha", SqlDbType.DateTime, fecha),
                new("@modulo", SqlDbType.VarChar, modulo),
                new("@evento", SqlDbType.VarChar, evento),
                new("@criticidad", SqlDbType.TinyInt, criticidad)
            };
            com.Ejecutar_064RB("INSERT INTO Bitacora_064RB(dni,fecha,modulo,evento,criticidad) values (@dni,@fecha,@modulo,@evento,@criticidad)", parametros);
        }
        public DataTable Consultar_064RB() => com.Consultar_064RB("select * from Bitacora_064RB");
    }
}
