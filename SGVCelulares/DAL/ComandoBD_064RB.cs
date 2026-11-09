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
        internal static void ConfigurarComando_064RB(SqlCommand comando, List<ParametroBD_064RB>? parametros)
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
        public T EjecutarEnTransaccion_064RB<T>(Func<TransaccionBD_064RB, T> accion)
        {
            using SqlConnection con = new SqlConnection(cadenaConexion_064RB);
            con.Open();
            using SqlTransaction tran = con.BeginTransaction();
            try
            {
                T res = accion(new TransaccionBD_064RB(con, tran));
                tran.Commit();
                return res;
            }
            catch
            {
                tran.Rollback();
                throw;
            }
        }
        public int EjecutarProcedimiento_064RB(string nombreProcedimiento, List<ParametroBD_064RB> parametros = null)
        {
            using SqlConnection con = new SqlConnection(cadenaConexion_064RB);
            using SqlCommand cmd = new SqlCommand(nombreProcedimiento, con);
            cmd.CommandType = CommandType.StoredProcedure;
            ConfigurarComando_064RB(cmd, parametros);
            con.Open();
            return cmd.ExecuteNonQuery();
        }
        public DataTable ConsultarProcedimiento_064RB(string nombreProcedimiento, List<ParametroBD_064RB> parametros = null)
        {
            DataTable dt = new DataTable();
            using SqlConnection con = new SqlConnection(cadenaConexion_064RB);
            using SqlCommand cmd = new SqlCommand(nombreProcedimiento, con);
            cmd.CommandType = CommandType.StoredProcedure;
            ConfigurarComando_064RB(cmd, parametros);
            con.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            dt.Load(reader);
            return dt;
        }

        public void RestaurarBaseDatosSiNoExiste_064RB(string rutaBak)
        {
            const string nombreBD = "bd_santsung_064RB";
            if (!File.Exists(rutaBak)) throw new FileNotFoundException("No se encontró el archivo de respaldo de San Tsung.", rutaBak);
            // Conectar a master, no a la base que todavía puede no existir.
            using SqlConnection con = new SqlConnection(cadenaConexion_064RB);
            con.Open();
            // Evitar que dos instancias intenten inicializar la BD simultáneamente.
            using (SqlCommand cmdLock = new SqlCommand(@"
                    DECLARE @resultado INT;

                    EXEC @resultado = sp_getapplock
                        @Resource = N'Inicializacion_SanTsung_064RB',
                        @LockMode = 'Exclusive',
                        @LockOwner = 'Session',
                        @LockTimeout = 60000;

                    SELECT @resultado;", con))
            {
                int resultadoLock = Convert.ToInt32(cmdLock.ExecuteScalar());
                if (resultadoLock < 0) throw new Exception("No se pudo obtener el bloqueo para inicializar la base de datos.");
            }

            // Comprobar si la base ya existe y está disponible.
            using (SqlCommand cmdExiste = new SqlCommand(@"
                    SELECT state_desc
                    FROM sys.databases
                    WHERE name = @NombreBD;", con))
            {
                cmdExiste.Parameters.Add("@NombreBD", SqlDbType.NVarChar, 128).Value = nombreBD;
                object? estado = cmdExiste.ExecuteScalar();
                if (estado != null)
                {
                    string estadoBD = estado.ToString()!;
                    if (estadoBD != "ONLINE") throw new Exception($"La base de datos {nombreBD} existe, pero su estado es {estadoBD}.");
                    // La base ya existe y está disponible. No restaurar nuevamente.
                    return;
                }
            }

            // Obtener los archivos contenidos en el backup.
            List<(string LogicalName, string Type, int FileId)> archivos = new();
            using (SqlCommand cmdLista = new SqlCommand("RESTORE FILELISTONLY FROM DISK = @RutaBak;", con))
            {
                cmdLista.Parameters.Add("@RutaBak", SqlDbType.NVarChar, 4000).Value = rutaBak;
                using SqlDataReader reader = cmdLista.ExecuteReader();
                while (reader.Read())
                {
                    archivos.Add((
                        reader["LogicalName"].ToString()!,
                        reader["Type"].ToString()!,
                        Convert.ToInt32(reader["FileId"])
                    ));
                }
            }

            if (archivos.Count == 0) throw new Exception("El archivo de respaldo no contiene archivos de base de datos.");

            if (archivos.Any(a => a.Type != "D" && a.Type != "L")) throw new Exception("El backup contiene tipos de archivos adicionales que requieren una configuración de restauración específica.");
            // Obtener las rutas predeterminadas de SQL Server.
            string rutaDatos;
            string rutaLogs;
            using (SqlCommand cmdRutas = new SqlCommand(@"
                SELECT
                    CAST(SERVERPROPERTY('InstanceDefaultDataPath') AS NVARCHAR(4000)) AS RutaDatos,
                    CAST(SERVERPROPERTY('InstanceDefaultLogPath') AS NVARCHAR(4000)) AS RutaLogs,
                    (SELECT TOP 1 physical_name
                     FROM sys.master_files
                     WHERE database_id = DB_ID('master') AND type = 0) AS MasterDatos,
                    (SELECT TOP 1 physical_name
                     FROM sys.master_files
                     WHERE database_id = DB_ID('master') AND type = 1) AS MasterLogs;", con))
            using (SqlDataReader reader = cmdRutas.ExecuteReader())
            {
                if (!reader.Read()) throw new Exception("No se pudieron obtener las rutas de SQL Server.");
                string? datos = reader["RutaDatos"]?.ToString();
                string? logs = reader["RutaLogs"]?.ToString();
                string masterDatos = reader["MasterDatos"].ToString()!;
                string masterLogs = reader["MasterLogs"].ToString()!;
                rutaDatos = !string.IsNullOrWhiteSpace(datos)
                    ? datos
                    : Path.GetDirectoryName(masterDatos)!;
                rutaLogs = !string.IsNullOrWhiteSpace(logs)
                    ? logs
                    : Path.GetDirectoryName(masterLogs)!;
            }

            if (string.IsNullOrWhiteSpace(rutaDatos) || string.IsNullOrWhiteSpace(rutaLogs)) throw new Exception("No se pudieron determinar las carpetas de datos y logs de SQL Server.");
            // Construir las instrucciones MOVE dinámicamente.
            List<string> movimientos = new();
            foreach (var archivo in archivos)
            {
                string nombreArchivo;
                if (archivo.Type == "D")
                {
                    nombreArchivo = archivo.FileId == 1
                        ? $"{nombreBD}.mdf"
                        : $"{nombreBD}_{archivo.FileId}.ndf";
                    movimientos.Add(
                        $"MOVE N'{archivo.LogicalName.Replace("'", "''")}' " +
                        $"TO N'{Path.Combine(rutaDatos, nombreArchivo).Replace("'", "''")}'"
                    );
                }
                else
                {
                    nombreArchivo = archivos.Count(a => a.Type == "L") == 1
                        ? $"{nombreBD}_log.ldf"
                        : $"{nombreBD}_log_{archivo.FileId}.ldf";
                    movimientos.Add(
                        $"MOVE N'{archivo.LogicalName.Replace("'", "''")}' " +
                        $"TO N'{Path.Combine(rutaLogs, nombreArchivo).Replace("'", "''")}'"
                    );
                }
            }

            // Restaurar la base de datos.
            string sqlRestaurar = $@"
                RESTORE DATABASE [{nombreBD}]
                FROM DISK = @RutaBak
                WITH
                    {string.Join("," + Environment.NewLine, movimientos)},
                    RECOVERY,
                    STATS = 10;
            ";

            using (SqlCommand cmdRestaurar = new SqlCommand(sqlRestaurar, con))
            {
                cmdRestaurar.CommandTimeout = 0;
                cmdRestaurar.Parameters.Add("@RutaBak", SqlDbType.NVarChar, 4000).Value = rutaBak;
                cmdRestaurar.ExecuteNonQuery();
            }

            // Verificar que la restauración haya finalizado correctamente.
            using (SqlCommand cmdVerificar = new SqlCommand(@"
                SELECT state_desc
                FROM sys.databases
                WHERE name = @NombreBD;
            ", con))
            {
                cmdVerificar.Parameters.Add("@NombreBD", SqlDbType.NVarChar, 128).Value = nombreBD;
                object? estado = cmdVerificar.ExecuteScalar();
                if (estado == null || estado.ToString() != "ONLINE") throw new Exception("La restauración finalizó, pero la base no quedó en estado ONLINE.");
            }
        }
    }
}
