using MAP_064RB.Tecnico_064RB;
using Servicio_064RB;
using System.Reflection;
using System.Text;

namespace BLL_064RB.Tecnico_064RB
{
    public class BLL_DV_064RB
    {
        MAP_DV_064RB map_dv;
        private const string SEPARADOR_HASH = "|";
        private const string SEPARADOR_ID = "-";
        public BLL_DV_064RB() => map_dv = new();

        #region "Generaciones"
        public void GenerarDVH_064RB(List<string> valores, string nombreTabla)
        {
            string id = string.Join(SEPARADOR_ID, valores);
            GenerarDVH_064RB((object)valores, id, nombreTabla);
        }
        public void GenerarDVH_064RB(object valor, string id, string nombreTabla)
        {
            string hash = CalcularHash_064RB(valor);
            SER_DVH_064RB dvh = new SER_DVH_064RB(nombreTabla, id, hash);
            if (map_dv.ExisteDVH_064RB(nombreTabla, id)) map_dv.ModificarDVH_064RB(dvh);
            else map_dv.AgregarDVH_064RB(dvh);
        }
        public void GenerarDVV_064RB(string nombreTabla)
        {
            List<string> listaDVH = map_dv.ConsultarDVHValores_064RB(nombreTabla);
            if (listaDVH.Count == 0)
            {
                if (map_dv.ExisteDVV_064RB(nombreTabla)) map_dv.BorrarDVV_064RB(nombreTabla);
                return;
            }
            string hash = CalcularHash_064RB(listaDVH);
            SER_DVV_064RB dvv = new SER_DVV_064RB(nombreTabla, hash);
            if (map_dv.ExisteDVV_064RB(nombreTabla)) map_dv.ModificarDVV_064RB(dvv);
            else map_dv.AgregarDVV_064RB(dvv);
        }
        #endregion

        #region "Recalculos"
        public void RecalcularDV_064RB()
        {
            foreach (var entidad in ObtenerEntidadesRegistradas_064RB())
            {
                RecalcularEntidad_064RB(entidad);
            }
            foreach (var relacion in ObtenerRelacionesRegistradas_064RB())
            {
                RecalcularRelacion_064RB(relacion);
            }
        }
        private void RecalcularEntidad_064RB(IProtegidoPorDV_064RB entidad)
        {
            var registros = entidad.ObtenerRegistrosParaDV_064RB();
            BorrarDVHHuerfanos_064RB(entidad.NombreTabla_064RB, registros.Select(entidad.ObtenerIdParaDV_064RB).ToList());
            foreach (var r in registros)
            {
                GenerarDVH_064RB(r, entidad.ObtenerIdParaDV_064RB(r), entidad.NombreTabla_064RB);
            }
            GenerarDVV_064RB(entidad.NombreTabla_064RB);
        }
        private void RecalcularRelacion_064RB(RelacionDV_064RB relacion)
        {
            var registros = OrdenarCompuesta_064RB(relacion.ObtenerRegistros_064RB());
            var ids = registros.Select(ObtenerIdCompuesto_064RB).ToList();
            BorrarDVHHuerfanos_064RB(relacion.NombreTabla_064RB, ids);
            foreach (var r in registros)
            {
                GenerarDVH_064RB(new List<string> { r[0].ToString(), r[1].ToString() }, relacion.NombreTabla_064RB);
            }
            GenerarDVV_064RB(relacion.NombreTabla_064RB);
        }
        private void BorrarDVHHuerfanos_064RB(string nombreTabla, List<string> idsReales)
        {
            List<string> idsDVH = map_dv.ConsultarDVHIds_064RB(nombreTabla);
            foreach (var id in idsDVH)
            {
                if (!idsReales.Contains(id)) BorrarDVH_064RB(nombreTabla, id);
            }
        }
        public void BorrarDVH_064RB(string nombreTabla, string id)
        {
            if (map_dv.ExisteDVH_064RB(nombreTabla, id)) map_dv.BorrarDVH_064RB(nombreTabla, id);
        }
        #endregion

        #region "Catalogos"
        private List<IProtegidoPorDV_064RB> ObtenerEntidadesRegistradas_064RB()
        {
            return typeof(MAP_DV_064RB).Assembly.GetTypes()
                .Where(t => typeof(IProtegidoPorDV_064RB).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .Select(t => (IProtegidoPorDV_064RB)Activator.CreateInstance(t))
                .ToList();
        }
        private List<RelacionDV_064RB> ObtenerRelacionesRegistradas_064RB()
        {
            BLL_Rol_064RB bll_rol = new();
            BLL_Familia_064RB bll_familia = new();

            return new List<RelacionDV_064RB>
            {
                new RelacionDV_064RB { NombreTabla_064RB = "rol_familia_064RB", ObtenerRegistros_064RB = () => bll_rol.ConsultarRolFamilia_064RB() },
                new RelacionDV_064RB { NombreTabla_064RB = "rol_permiso_064RB", ObtenerRegistros_064RB = () => bll_rol.ConsultarRolPermiso_064RB() },
                new RelacionDV_064RB { NombreTabla_064RB = "familia_familia_064RB", ObtenerRegistros_064RB = () => bll_familia.ConsultarFamiliaFamilia_064RB() },
                new RelacionDV_064RB { NombreTabla_064RB = "permiso_familia_064RB", ObtenerRegistros_064RB = () => bll_familia.ConsultarFamiliaPermiso_064RB() },
            };
        }

        #endregion

        #region "Verificacion"
        public List<SER_Inconsistencia_064RB> ObtenerInconsistencias_064RB() => VerificarTodasLasTablas_064RB(); // Para FrmRepararInconsistencia: junta todo, no corta nunca
        public bool VerificarIntegridad_064RB() // Para el resto del sistema (ej. login): mantiene el comportamiento actual de cortar con excepción
        {
            var inconsistencias = VerificarTodasLasTablas_064RB();
            if (inconsistencias.Count > 0) throw new Exception("Se detecto inconsistencia en la base de datos. \n Redirigiendo a formulario de reparación.");
            return true;
        }
        private List<SER_Inconsistencia_064RB> VerificarTodasLasTablas_064RB()
        {
            var inconsistencias = new List<SER_Inconsistencia_064RB>();

            foreach (var entidad in ObtenerEntidadesRegistradas_064RB())
            {
                VerificarTabla_064RB(entidad.NombreTabla_064RB, entidad.ObtenerRegistrosParaDV_064RB(), entidad.ObtenerIdParaDV_064RB, inconsistencias);
            }
            foreach (var relacion in ObtenerRelacionesRegistradas_064RB())
            {
                var registros = OrdenarCompuesta_064RB(relacion.ObtenerRegistros_064RB()).Cast<object>().ToList();
                VerificarTabla_064RB(relacion.NombreTabla_064RB, registros, o => ObtenerIdCompuesto_064RB((object[])o), inconsistencias);
            }
            return inconsistencias;
        }
        private void VerificarTabla_064RB(string nombreTabla, List<object> registros, Func<object, string> getId, List<SER_Inconsistencia_064RB> inconsistencias)
        {
            int cantidadDVH = map_dv.ConsultarDVHValores_064RB(nombreTabla).Count;
            
            if (registros.Count == 0 && cantidadDVH == 0 && !map_dv.ExisteDVV_064RB(nombreTabla)) return;
            bool huboEliminacion = cantidadDVH > registros.Count;
            if (huboEliminacion)
            {
                inconsistencias.Add(new SER_Inconsistencia_064RB(nombreTabla, "-", $"La tabla '{nombreTabla}' tiene más entradas en DVH ({cantidadDVH}) que registros reales ({registros.Count}). Ocurrió una ELIMINACIÓN directa en BD."));
            }

            var dvhCalculados = new List<string>();
            bool huboErrorRegistro = huboEliminacion;

            foreach (var reg in registros)
            {
                string dvhCalc = CalcularHash_064RB(reg);
                SER_DVH_064RB dvh = map_dv.ObtenerDVH_064RB(nombreTabla, getId(reg));
                if (dvh == null)
                {
                    inconsistencias.Add(new SER_Inconsistencia_064RB(nombreTabla, getId(reg), $"No se encontró el DVH para el registro con Datos (INSERCIÓN):{Environment.NewLine}{MostrarDatos_064RB(reg, nombreTabla)}"));
                    huboErrorRegistro = true;
                    continue;
                }
                if (dvhCalc != dvh.Valor_064RB)
                {
                    inconsistencias.Add(new SER_Inconsistencia_064RB(nombreTabla, getId(reg), $"No coincide El DVH para el registro con Datos (MODIFICACIÓN):{Environment.NewLine}{MostrarDatos_064RB(reg, nombreTabla)}."));
                    huboErrorRegistro = true;
                    continue;
                }
                dvhCalculados.Add(dvhCalc);
            } // DEBUGGEAR ACÁ PARA LOS DV

            if (huboErrorRegistro) return; // el DVV va a fallar como consecuencia directa; reportarlo también sería ruido

            SER_DVV_064RB dvv = map_dv.ObtenerDVV_064RB(nombreTabla);
            if (dvv == null)
            {
                inconsistencias.Add(new SER_Inconsistencia_064RB(nombreTabla, "(tabla completa)", $"La tabla '{nombreTabla}' tiene registros con DVH válido, pero no existe un DVV generado para la tabla.{Environment.NewLine}Posible eliminación directa del DVV en la base de datos."));
                return;
            }
            string dvvCalc = CalcularHash_064RB(dvhCalculados);
            if (dvvCalc != dvv.Valor_064RB)
            {
                inconsistencias.Add(new SER_Inconsistencia_064RB(nombreTabla, "(tabla completa)", $"El DVV para la tabla '{nombreTabla}' no coincide con el valor calculado.{Environment.NewLine}..."));
            }
        }

        #endregion

        #region "Calculos"
        private string Concatenar_064RB(object obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (obj is object[] valores)
            {
                return string.Join(SEPARADOR_HASH, valores.Select(v => v?.ToString() ?? string.Empty));
            }
            if (obj is IEnumerable<string> listaStr)
            {
                return string.Join(SEPARADOR_HASH, listaStr);
            }
            PropertyInfo[] propiedades = obj.GetType().GetProperties().OrderBy(p => p.Name).ToArray();
            return string.Join(SEPARADOR_HASH, propiedades.Select(p => p.GetValue(obj)?.ToString() ?? string.Empty));
        }
        public string CalcularHash_064RB(object o) => SER_Cripto_064RB.Encriptar_064RB(Concatenar_064RB(o));
        #endregion

        #region "Utilidades"
        private string ObtenerIdCompuesto_064RB(object[] registro) => $"{registro[0]}{SEPARADOR_ID}{registro[1]}";
        private List<object[]> OrdenarCompuesta_064RB(List<object[]> lista) => lista.OrderBy(x => Convert.ToInt32(x[0])).ThenBy(x => Convert.ToInt32(x[1])).ToList();
        #endregion

        #region "Presentacion"
        private string MostrarDatos_064RB(object obj, string nombreTabla = null)
        {
            if (obj is object[] valores)
            {
                string[] nombresCampos = ObtenerNombresCampos_064RB(nombreTabla);
                var lineas = valores.Select((v, i) =>
                {
                    string nombreCampo = (nombresCampos != null && i < nombresCampos.Length) ? nombresCampos[i] : $"Campo{i}";
                    return $"{nombreCampo}: {v?.ToString() ?? "null"}";
                });
                return string.Join(Environment.NewLine, lineas);
            }
            StringBuilder sb = new StringBuilder();
            PropertyInfo[] propiedades = obj.GetType().GetProperties().Where(p => p.PropertyType == typeof(string) || !typeof(System.Collections.IEnumerable).IsAssignableFrom(p.PropertyType)).ToArray();
            foreach (PropertyInfo propiedad in propiedades)
            {
                sb.Append($"{propiedad.Name}: {propiedad.GetValue(obj)}").Append(Environment.NewLine);
            }
            return sb.ToString().TrimEnd();
        }
        private string[] ObtenerNombresCampos_064RB(string nombreTabla)
        {
            switch (nombreTabla)
            {
                case "Rol_familia_064RB": return new[] { "IdRol", "IdFamilia" };
                case "Rol_permiso_064RB": return new[] { "IdRol", "IdPermiso" };
                case "Familia_familia_064RB": return new[] { "IdFamiliaPadre", "IdFamiliaHija" };
                case "Permiso_familia_064RB": return new[] { "IdFamilia", "IdPermiso" };
                default: return null;
            }
        }
        #endregion
    }
}
