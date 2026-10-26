using MAP_064RB.Tecnico_064RB;
using Servicio_064RB;
using System.Data;
using System.Globalization;
using System.Numerics;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace BLL_064RB.Tecnico_064RB
{
    public class BLL_DV_064RB
    {
        private static readonly JsonSerializerOptions JsonOpts_064RB = new() { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };

        MAP_DV_064RB map_dv;
        public BLL_DV_064RB() => map_dv = new();
        public void ActualizarDV_064RB(params string[] tablas)
        {
            var porTabla = map_dv.ObtenerDV_064RB().Where(d => d.Tabla_064RB != MAP_DV_064RB.FilaBD_064RB).ToDictionary(d => d.Tabla_064RB);
            var dvs = new List<SER_DV_064RB>();
            var upsert = new List<SER_DVRegistro_064RB>();
            var eliminar = new List<SER_DVRegistro_064RB>();

            foreach (string tabla in tablas.Distinct())
            {
                var (dv, registros) = CalcularTabla_064RB(tabla);
                porTabla[tabla] = dv;
                dvs.Add(dv);

                Dictionary<string, SER_DVRegistro_064RB> guardados = map_dv.ObtenerRegistrosDV_064RB(tabla).ToDictionary(r => r.IdRegistro_064RB);
                var idsActuales = new HashSet<string>();
                foreach (SER_DVRegistro_064RB r in registros)
                {
                    idsActuales.Add(r.IdRegistro_064RB);
                    if (!guardados.TryGetValue(r.IdRegistro_064RB, out SER_DVRegistro_064RB? g) || !IgualesRegistro_064RB(g, r)) upsert.Add(r);
                }
                eliminar.AddRange(guardados.Values.Where(g => !idsActuales.Contains(g.IdRegistro_064RB)));
            }
            dvs.Add(CalcularBD_064RB(porTabla.Values.ToList()));
            map_dv.Guardar_064RB(dvs, upsert, eliminar);
        }

        public void RecalcularDV_064RB()
        {
            var dvs = new List<SER_DV_064RB>();
            var registros = new List<SER_DVRegistro_064RB>();
            foreach (string tabla in map_dv.ObtenerTablas_064RB())
            {
                var (dv, regs) = CalcularTabla_064RB(tabla);
                dvs.Add(dv);
                registros.AddRange(regs);
            }
            dvs.Add(CalcularBD_064RB(dvs));
            map_dv.ReemplazarTodo_064RB(dvs, registros);
        }

        public bool VerificarIntegridad_064RB() => Analizar_064RB().Ok;

        public bool VerificarIntegridad_064RB(out string? mensaje)
        {
            var (ok, tablas, _) = Analizar_064RB();
            mensaje = ok
                ? null
                : tablas.Count > 0
                    ? "Se detectaron inconsistencias en las tablas: " + string.Join(", ", tablas)
                    : "Se detectó una inconsistencia en el dígito verificador de la base de datos.";
            return ok;
        }
        public List<string> ObtenerTablasInconsistentes_064RB() => Analizar_064RB().Tablas;
        public List<SER_Inconsistencia_064RB> ObtenerInconsistencias_064RB() => Analizar_064RB().Detalle;


        private (bool Ok, List<string> Tablas, List<SER_Inconsistencia_064RB> Detalle) Analizar_064RB()
        {
            var calculado = new List<SER_DV_064RB>();
            var registrosCalculados = new Dictionary<string, List<SER_DVRegistro_064RB>>();
            foreach (string tabla in map_dv.ObtenerTablas_064RB())
            {
                var (dv, regs) = CalcularTabla_064RB(tabla);
                calculado.Add(dv);
                registrosCalculados[tabla] = regs;
            }
            SER_DV_064RB calculadoBD = CalcularBD_064RB(calculado);

            List<SER_DV_064RB> guardado = map_dv.ObtenerDV_064RB();
            SER_DV_064RB? guardadoBD = guardado.FirstOrDefault(d => d.Tabla_064RB == MAP_DV_064RB.FilaBD_064RB);
            var guardadoPorTabla = guardado.Where(d => d.Tabla_064RB != MAP_DV_064RB.FilaBD_064RB).ToDictionary(d => d.Tabla_064RB);
            var registrosGuardados = map_dv.ObtenerRegistrosDV_064RB().GroupBy(r => r.Tabla_064RB).ToDictionary(g => g.Key, g => g.ToDictionary(r => r.IdRegistro_064RB));
            var tablasInconsistentes = new List<string>();
            var detalle = new List<SER_Inconsistencia_064RB>();

            foreach (SER_DV_064RB c in calculado)
            {
                bool tablaOk = guardadoPorTabla.TryGetValue(c.Tabla_064RB, out SER_DV_064RB? g) && Iguales_064RB(g, c);
                registrosGuardados.TryGetValue(c.Tabla_064RB, out Dictionary<string, SER_DVRegistro_064RB>? guardadosTabla);
                List<SER_Inconsistencia_064RB> diferencias = CompararRegistros_064RB(c.Tabla_064RB, registrosCalculados[c.Tabla_064RB], guardadosTabla);

                if (tablaOk && diferencias.Count == 0) continue;

                tablasInconsistentes.Add(c.Tabla_064RB);
                if (diferencias.Count > 0) detalle.AddRange(diferencias);
                else detalle.Add(new SER_Inconsistencia_064RB(c.Tabla_064RB, "-", "El dígito verificador de la tabla no coincide con el registrado, pero no se pudo identificar el registro afectado."));
            }

            // tablas registradas en DV que ya no existen en la BD
            foreach (string t in guardadoPorTabla.Keys.Where(t => calculado.All(c => c.Tabla_064RB != t)))
            {
                tablasInconsistentes.Add(t);
                detalle.Add(new SER_Inconsistencia_064RB(t, "-", "Operación detectada: DROP TABLE\r\nLa tabla fue eliminada de la base de datos."));
            }

            bool bdOk = guardadoBD != null && Iguales_064RB(guardadoBD, calculadoBD);
            if (!bdOk && tablasInconsistentes.Count == 0) detalle.Add(new SER_Inconsistencia_064RB("(BD)", "-", "El dígito verificador general de la base de datos no coincide con el registrado."));
            return (bdOk && tablasInconsistentes.Count == 0, tablasInconsistentes, detalle);
        }

        // Compara los registros actuales contra el snapshot guardado e indica la operacion realizada
        private static List<SER_Inconsistencia_064RB> CompararRegistros_064RB(string tabla, List<SER_DVRegistro_064RB> actuales, Dictionary<string, SER_DVRegistro_064RB>? guardados)
        {
            var resultado = new List<SER_Inconsistencia_064RB>();
            var idsActuales = new HashSet<string>();

            foreach (SER_DVRegistro_064RB a in actuales)
            {
                idsActuales.Add(a.IdRegistro_064RB);
                if (guardados == null || !guardados.TryGetValue(a.IdRegistro_064RB, out SER_DVRegistro_064RB? g))
                {
                    resultado.Add(new(tabla, a.IdRegistro_064RB, "Operación detectada: INSERT\r\nSe agregó un registro directamente en la BD, sin pasar por el sistema.\r\n\r\nDatos del registro:\r\n" + Listar_064RB(Deserializar_064RB(a.Datos_064RB), null)));
                }
                else if (!IgualesRegistro_064RB(g, a))
                {
                    resultado.Add(new(tabla, a.IdRegistro_064RB, "Operación detectada: UPDATE\r\nSe modificó un registro directamente en la BD, sin pasar por el sistema.\r\n\r\n" + "Datos actuales del registro (en las columnas modificadas se indica el valor anterior):\r\n" + Listar_064RB(Deserializar_064RB(a.Datos_064RB), Deserializar_064RB(g.Datos_064RB))));
                }
            }

            if (guardados != null)
            {
                foreach (SER_DVRegistro_064RB g in guardados.Values.Where(g => !idsActuales.Contains(g.IdRegistro_064RB)))
                {
                    resultado.Add(new(tabla, g.IdRegistro_064RB, "Operación detectada: DELETE\r\nSe eliminó un registro directamente de la BD, sin pasar por el sistema.\r\n\r\n" + "Datos del registro eliminado (último estado válido):\r\n" + Listar_064RB(Deserializar_064RB(g.Datos_064RB), null)));
                }
            }
            return resultado;
        }

        private static string Listar_064RB(Dictionary<string, string?> datos, Dictionary<string, string?>? previos)
        {
            var sb = new StringBuilder();
            foreach (var kv in datos)
            {
                sb.Append($"  {kv.Key}: {kv.Value ?? "NULL"}");
                if (previos != null && (!previos.TryGetValue(kv.Key, out string? anterior) || anterior != kv.Value))
                {
                    previos.TryGetValue(kv.Key, out string? valorPrevio);
                    sb.Append($"   <-- antes: {valorPrevio ?? "NULL"}");
                }
                sb.Append("\r\n");
            }
            return sb.ToString();
        }

        private static Dictionary<string, string?> Deserializar_064RB(string json)
        {
            try 
            {
                return JsonSerializer.Deserialize<Dictionary<string, string?>>(json) ?? new();
            }
            catch
            { 
                return new();
            }
        }

        // DV de la tabla + DVH y datos de cada registro
        private (SER_DV_064RB Dv, List<SER_DVRegistro_064RB> Registros) CalcularTabla_064RB(string tabla)
        {
            DataTable dt = map_dv.ObtenerRegistros_064RB(tabla);
            List<string> pk = map_dv.ObtenerClavePrimaria_064RB(tabla);

            // Cada celda se convierte a su hexadecimal una sola vez y se acumula:
            //  - por registro (horizontal -> DVH de cada registro)
            //  - por columna  (vertical   -> DVV de cada columna)
            var dvvColumnas = new BigInteger[dt.Columns.Count];
            var registros = new List<SER_DVRegistro_064RB>(dt.Rows.Count);
            var ids = new HashSet<string>();
            BigInteger dvhTabla = BigInteger.Zero;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow fila = dt.Rows[i];
                BigInteger dvhRegistro = BigInteger.Zero;
                var datos = new Dictionary<string, string?>();

                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    object valor = fila[j];
                    string texto = ValorATexto_064RB(valor);
                    BigInteger hash = HexANumero_064RB(SER_Cripto_064RB.Encriptar_064RB(texto));
                    dvhRegistro += hash;
                    dvvColumnas[j] += hash;
                    datos[dt.Columns[j].ColumnName] = valor is DBNull ? null : texto;
                }

                string dvhHex = NumeroAHex_064RB(dvhRegistro);
                // Id del registro: valores de la PK; si la tabla no tiene PK se usa el DVH del registro
                string id = pk.Count > 0 ? string.Join("-", pk.Select(c => ValorATexto_064RB(fila[c]))) : dvhHex;
                if (!ids.Add(id)) { id += "#" + i; ids.Add(id); }

                registros.Add(new SER_DVRegistro_064RB(tabla, id, dvhHex, JsonSerializer.Serialize(datos, JsonOpts_064RB)));
                dvhTabla += dvhRegistro; // DVH de la tabla = suma de los DVH de los registros
            }

            BigInteger dvvTabla = BigInteger.Zero;
            foreach (BigInteger v in dvvColumnas) dvvTabla += v; // DVV de la tabla = suma de los DVV de las columnas

            return (new SER_DV_064RB(tabla, NumeroAHex_064RB(dvhTabla), NumeroAHex_064RB(dvvTabla)), registros);
        }

        // DVH y DVV de la BD: suma de los DV de todas las tablas
        private static SER_DV_064RB CalcularBD_064RB(List<SER_DV_064RB> tablas)
        {
            BigInteger dvh = BigInteger.Zero, dvv = BigInteger.Zero;
            foreach (SER_DV_064RB t in tablas)
            {
                dvh += HexANumero_064RB(t.DVH_064RB);
                dvv += HexANumero_064RB(t.DVV_064RB);
            }
            return new SER_DV_064RB(MAP_DV_064RB.FilaBD_064RB, NumeroAHex_064RB(dvh), NumeroAHex_064RB(dvv));
        }

        // Representacion en texto estable de cada valor, que es lo que recibe Encriptar
        private static string ValorATexto_064RB(object? valor) => valor switch
        {
            null => "<NULL>",
            DBNull => "<NULL>",
            byte[] b => Convert.ToHexString(b),
            bool b => b ? "1" : "0",
            DateTime d => d.ToString("yyyy-MM-dd HH:mm:ss.fffffff", CultureInfo.InvariantCulture),
            IFormattable f => f.ToString(null, CultureInfo.InvariantCulture),
            _ => Convert.ToString(valor, CultureInfo.InvariantCulture) ?? string.Empty
        };

        private static BigInteger HexANumero_064RB(string? hex)
        {
            if (string.IsNullOrWhiteSpace(hex)) return BigInteger.Zero;
            // el "0" inicial evita que BigInteger interprete el numero como negativo
            return BigInteger.TryParse("0" + hex, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out BigInteger r) ? r : BigInteger.MinusOne;
        }

        private static string NumeroAHex_064RB(BigInteger n)
        {
            string hex = n.ToString("x").TrimStart('0');
            return hex.Length == 0 ? "0" : hex;
        }
        private static bool Iguales_064RB(SER_DV_064RB a, SER_DV_064RB b) => HexANumero_064RB(a.DVH_064RB) == HexANumero_064RB(b.DVH_064RB) && HexANumero_064RB(a.DVV_064RB) == HexANumero_064RB(b.DVV_064RB);
        private static bool IgualesRegistro_064RB(SER_DVRegistro_064RB a, SER_DVRegistro_064RB b) => HexANumero_064RB(a.DVH_064RB) == HexANumero_064RB(b.DVH_064RB) && a.Datos_064RB == b.Datos_064RB;
    }
}
