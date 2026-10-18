using Microsoft.Data.SqlClient;

namespace DAL_064RB
{
    public class TransaccionBD_064RB
    {
        private readonly SqlConnection con;
        private readonly SqlTransaction tran;
        internal TransaccionBD_064RB(SqlConnection con, SqlTransaction tran)
        {
            this.con = con;
            this.tran = tran;
        }
        public int Ejecutar_064RB(string sql, List<ParametroBD_064RB>? parametros = null)
        {
            using SqlCommand cmd = new SqlCommand(sql, con, tran);
            ComandoBD_064RB.ConfigurarComando_064RB(cmd, parametros);
            return cmd.ExecuteNonQuery();
        }
        public object? Escalar_064RB(string sql, List<ParametroBD_064RB>? parametros = null)
        {
            using SqlCommand cmd = new SqlCommand(sql, con, tran);
            ComandoBD_064RB.ConfigurarComando_064RB(cmd, parametros);
            return cmd.ExecuteScalar();
        }
    }
}
