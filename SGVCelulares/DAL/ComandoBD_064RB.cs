using DotNetEnv;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DAL_064RB
{
    public class ComandoBD_064RB
    {
        private readonly string cadenaConexion_064RB;
        public ComandoBD_064RB(string? catalogoOverride = null)
        {
            string basePath_064RB = AppDomain.CurrentDomain.BaseDirectory;
            string envPath_064RB = Path.Combine(basePath_064RB, ".env");
            Env.Load(envPath_064RB);
            string? c = Environment.GetEnvironmentVariable("DB_CONNECTION");
            if (string.IsNullOrEmpty(c)) throw new Exception("No se encontró la cadena de conexión a la base de datos.");
            if (catalogoOverride != null)
            {
                var builder_064RB = new SqlConnectionStringBuilder(c) { InitialCatalog = catalogoOverride };
                cadenaConexion_064RB = builder_064RB.ConnectionString;
            }
            else cadenaConexion_064RB = c;
        }
        public DataTable Consultar_064RB(string sql, List<ParametroBD_064RB> parametros = null)
        {
            DataTable dt = new DataTable();
            using SqlConnection con = new SqlConnection(cadenaConexion_064RB);
            using SqlCommand cmd = new SqlCommand(sql, con);
            ConfigurarComando_064RB(cmd, parametros);
            con.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            dt.Load(reader);
            return dt;
        }
        public int Ejecutar_064RB(string sql, List<ParametroBD_064RB> parametros = null)
        {
            using SqlConnection con = new SqlConnection(cadenaConexion_064RB);
            using SqlCommand cmd = new SqlCommand(sql, con);
            ConfigurarComando_064RB(cmd, parametros);
            con.Open();
            return cmd.ExecuteNonQuery();
        }
        public object? Escalar_064RB(string sql, List<ParametroBD_064RB>? parametros = null)
        {
            using SqlConnection con = new SqlConnection(cadenaConexion_064RB);
            using SqlCommand cmd = new SqlCommand(sql, con);
            ConfigurarComando_064RB(cmd, parametros);
            con.Open();
            return cmd.ExecuteScalar();
        }
        private void ConfigurarComando_064RB(SqlCommand comando, List<ParametroBD_064RB>? parametros)
        {
            if (parametros != null)
            {
                foreach (ParametroBD_064RB p in parametros)
                {
                    comando.Parameters.Add(p.Nombre_064RB, p.Tipo_064RB).Value = p.Valor_064RB;
                }
            }
        }

        public void EjecutarTransaccion_064RB(List<(string Sql, List<ParametroBD_064RB> Parametros)> comandos)
        {
            using SqlConnection con = new SqlConnection(cadenaConexion_064RB);
            con.Open();
            using SqlTransaction tran = con.BeginTransaction();
            try
            {
                foreach (var comando in comandos)
                {
                    using SqlCommand cmd = new SqlCommand(comando.Sql, con, tran);
                    ConfigurarComando_064RB(cmd, comando.Parametros);
                    cmd.ExecuteNonQuery();
                }
                tran.Commit();
            }
            catch
            {
                tran.Rollback();
                throw;
            }
        }
    }
}
