using System.Data;

namespace DAL_064RB.Tecnico_064RB
{
    public class DAO_DV_064RB
    {
        public const string TablaDV_064RB = "DV_064RB";
        public const string TablaRegistros_064RB = "DV_Registro_064RB";
        public const string FilaBD_064RB = "__BD__";
        ComandoBD_064RB com;
        public DAO_DV_064RB() => com = new();

        private static readonly string[] TablasExcluidas_064RB =
        {
            TablaDV_064RB,
            TablaRegistros_064RB,
            "Bitacora_064RB"
        };

        // Tablas de negocio (excluye las tablas de DV y las de sistema)
        public List<string> ObtenerTablas_064RB()
        {
            var parametros = new List<ParametroBD_064RB>();
            var nombres = new List<string>();

            for (int i = 0; i < TablasExcluidas_064RB.Length; i++)
            {
                parametros.Add(new($"@ex{i}", SqlDbType.VarChar, TablasExcluidas_064RB[i]));
                nombres.Add($"@ex{i}");
            }
            DataTable dt = com.Consultar_064RB(
                $"SELECT name FROM sys.tables WHERE is_ms_shipped = 0 AND name NOT IN ({string.Join(", ", nombres)}) ORDER BY name", parametros);
            return dt.AsEnumerable().Select(r => r.Field<string>("name")!).ToList();
        }

        // Columnas de la clave primaria (vacio si la tabla no tiene PK)
        public List<string> ObtenerClavePrimaria_064RB(string tabla)
        {
            DataTable dt = com.Consultar_064RB(
                @"SELECT c.name
                  FROM sys.indexes i
                  JOIN sys.index_columns ic ON ic.object_id = i.object_id AND ic.index_id = i.index_id
                  JOIN sys.columns c ON c.object_id = ic.object_id AND c.column_id = ic.column_id
                  WHERE i.is_primary_key = 1 AND i.object_id = OBJECT_ID(@tabla)
                  ORDER BY ic.key_ordinal",
                new List<ParametroBD_064RB> { new("@tabla", SqlDbType.NVarChar, tabla) });
            return dt.AsEnumerable().Select(r => r.Field<string>("name")!).ToList();
        }

        public DataTable ObtenerRegistros_064RB(string tabla)
        {
            if (!ObtenerTablas_064RB().Contains(tabla)) throw new Exception($"La tabla {tabla} no existe o no está habilitada para DV.");
            return com.Consultar_064RB($"SELECT * FROM [{tabla.Replace("]", "]]")}]");
        }

        public DataTable ObtenerDV_064RB() => com.Consultar_064RB($"SELECT Tabla_064RB, DVH_064RB, DVV_064RB FROM {TablaDV_064RB}");

        public DataTable ObtenerRegistrosDV_064RB() => com.Consultar_064RB($"SELECT Tabla_064RB, IdRegistro_064RB, DVH_064RB, Datos_064RB FROM {TablaRegistros_064RB}");

        public DataTable ObtenerRegistrosDV_064RB(string tabla) => com.Consultar_064RB(
            $"SELECT Tabla_064RB, IdRegistro_064RB, DVH_064RB, Datos_064RB FROM {TablaRegistros_064RB} WHERE Tabla_064RB = @tabla",
            new List<ParametroBD_064RB> { new("@tabla", SqlDbType.VarChar, tabla) });

        // Upsert de filas de DV (tablas + BD) y de registros, y baja de registros, todo en una transaccion
        public void Guardar_064RB(
            List<(string Tabla, string Dvh, string Dvv)> filas,
            List<(string Tabla, string Id, string Dvh, string Datos)> registrosUpsert,
            List<(string Tabla, string Id)> registrosEliminar)
        {
            var comandos = new List<(string Sql, List<ParametroBD_064RB> Parametros)>();
            foreach (var f in filas)
            {
                comandos.Add((
                    $@"IF EXISTS (SELECT 1 FROM {TablaDV_064RB} WHERE Tabla_064RB = @tabla)
                          UPDATE {TablaDV_064RB} SET DVH_064RB = @dvh, DVV_064RB = @dvv WHERE Tabla_064RB = @tabla
                       ELSE
                          INSERT INTO {TablaDV_064RB}(Tabla_064RB, DVH_064RB, DVV_064RB) VALUES (@tabla, @dvh, @dvv)",
                    ParametrosFila_064RB(f)));
            }
            foreach (var r in registrosEliminar)
            {
                comandos.Add((
                    $"DELETE FROM {TablaRegistros_064RB} WHERE Tabla_064RB = @tabla AND IdRegistro_064RB = @id",
                    new List<ParametroBD_064RB>
                    {
                        new("@tabla", SqlDbType.VarChar, r.Tabla),
                        new("@id", SqlDbType.NVarChar, r.Id)
                    }));
            }
            foreach (var r in registrosUpsert)
            {
                comandos.Add((
                    $@"IF EXISTS (SELECT 1 FROM {TablaRegistros_064RB} WHERE Tabla_064RB = @tabla AND IdRegistro_064RB = @id)
                          UPDATE {TablaRegistros_064RB} SET DVH_064RB = @dvh, Datos_064RB = @datos WHERE Tabla_064RB = @tabla AND IdRegistro_064RB = @id
                       ELSE
                          INSERT INTO {TablaRegistros_064RB}(Tabla_064RB, IdRegistro_064RB, DVH_064RB, Datos_064RB) VALUES (@tabla, @id, @dvh, @datos)",
                    ParametrosRegistro_064RB(r)));
            }
            com.EjecutarTransaccion_064RB(comandos);
        }

        // Reemplaza todo el contenido de DV y de registros (recalculo completo)
        public void ReemplazarTodo_064RB(
            List<(string Tabla, string Dvh, string Dvv)> filas,
            List<(string Tabla, string Id, string Dvh, string Datos)> registros)
        {
            var comandos = new List<(string Sql, List<ParametroBD_064RB> Parametros)>
            {
                ($"DELETE FROM {TablaDV_064RB}", new List<ParametroBD_064RB>()),
                ($"DELETE FROM {TablaRegistros_064RB}", new List<ParametroBD_064RB>())
            };
            foreach (var f in filas)
                comandos.Add(($"INSERT INTO {TablaDV_064RB}(Tabla_064RB, DVH_064RB, DVV_064RB) VALUES (@tabla, @dvh, @dvv)", ParametrosFila_064RB(f)));
            foreach (var r in registros)
                comandos.Add(($"INSERT INTO {TablaRegistros_064RB}(Tabla_064RB, IdRegistro_064RB, DVH_064RB, Datos_064RB) VALUES (@tabla, @id, @dvh, @datos)", ParametrosRegistro_064RB(r)));
            com.EjecutarTransaccion_064RB(comandos);
        }

        private static List<ParametroBD_064RB> ParametrosFila_064RB((string Tabla, string Dvh, string Dvv) f) => new()
        {
            new("@tabla", SqlDbType.VarChar, f.Tabla),
            new("@dvh", SqlDbType.VarChar, f.Dvh),
            new("@dvv", SqlDbType.VarChar, f.Dvv)
        };
        private static List<ParametroBD_064RB> ParametrosRegistro_064RB((string Tabla, string Id, string Dvh, string Datos) r) => new()
        {
            new("@tabla", SqlDbType.VarChar, r.Tabla),
            new("@id", SqlDbType.NVarChar, r.Id),
            new("@dvh", SqlDbType.VarChar, r.Dvh),
            new("@datos", SqlDbType.NVarChar, r.Datos)
        };
    }
}
