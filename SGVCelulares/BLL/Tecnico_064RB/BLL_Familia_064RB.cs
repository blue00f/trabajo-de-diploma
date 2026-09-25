using MAP_064RB.Tecnico_064RB;
using Servicio_064RB;

namespace BLL_064RB.Tecnico_064RB
{
    public class BLL_Familia_064RB : IABMC_064RB<SER_Familia_064RB>
    {
        MAP_Familia_064RB map_familia;
        BLL_Bitacora_064RB bll_bitacora;
        BLL_DV_064RB bll_dv;
        public BLL_Familia_064RB()
        {
            map_familia = new MAP_Familia_064RB();
            bll_bitacora = new BLL_Bitacora_064RB();
            bll_dv = new BLL_DV_064RB();
        }
        public void Agregar_064RB(SER_Familia_064RB familia) => map_familia.Agregar_064RB(familia);
        public void Borrar_064RB(SER_Familia_064RB familia)
        {
            if (map_familia.EstaEnUso_064RB(familia))
                throw new Exception("No se puede borrar: la familia está asignada a un rol u otra familia.");

            map_familia.Borrar_064RB(familia);

            bll_dv.BorrarDVH_064RB("familia", familia.Id_064RB.ToString());

            foreach (var componente in familia.Componentes_064RB)
            {
                string tabla = componente is SER_Familia_064RB ? "familia_familia" : "permiso_familia";
                string id = $"{familia.Id_064RB}-{componente.Id_064RB}";
                bll_dv.BorrarDVH_064RB(tabla, id);
            }

            bll_dv.GenerarDVV_064RB("familia");
            bll_dv.GenerarDVV_064RB("familia_familia");
            bll_dv.GenerarDVV_064RB("permiso_familia");

            bll_bitacora.RegistrarBitacora_064RB(new SER_Bitacora_064RB(SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB, DateTime.Now, "Roles", "Borrar familia", 1));
        }

        public List<SER_Familia_064RB> Consultar_064RB() => map_familia.Consultar_064RB();
        public List<object[]> ConsultarFamiliaFamilia_064RB() => map_familia.ConsultarFamiliaFamilia_064RB();
        public List<object[]> ConsultarFamiliaPermiso_064RB() => map_familia.ConsultarFamiliaPermiso_064RB();
        public SER_Familia_064RB ConsultarPorId_064RB(SER_Familia_064RB familia) => map_familia.ObtenerArbol_064RB(familia.Id_064RB);
        public void Modificar_064RB(SER_Familia_064RB familia) => throw new NotImplementedException();
        public void Agregar_064RB(SER_Familia_064RB familia, List<SER_Componente_064RB> componentes)
        {
            if (componentes.Count == 0) throw new Exception("No hay permisos o familias cargadas!");
            if (map_familia.ExisteFamilia_064RB(familia)) throw new Exception("Ya existe la familia!");
            if (ValidarPermisosRepetidos_064RB(componentes)) throw new Exception("Existe un permiso repetido!");
            map_familia.Agregar_064RB(familia);
            familia.Id_064RB = map_familia.ObtenerIdPorNombre_064RB(familia).Id_064RB;
            
            bll_dv.GenerarDVH_064RB(familia, familia.Id_064RB.ToString(),"familia");
            bll_dv.GenerarDVV_064RB("familia");
            
            foreach (SER_Componente_064RB c in componentes)
            {
                map_familia.AgregarPermisoFamilia_064RB(familia, c);
                if (c is SER_Familia_064RB familiaHijo)
                {
                    bll_dv.GenerarDVH_064RB(new List<string>() { familia.Id_064RB.ToString(), c.Id_064RB.ToString() }, "familia_familia");
                    bll_dv.GenerarDVV_064RB("familia_familia");
                }
                else if (c is SER_Permiso_064RB permiso)
                {
                    bll_dv.GenerarDVH_064RB(new List<string>() { familia.Id_064RB.ToString(), c.Id_064RB.ToString() }, "permiso_familia");
                    bll_dv.GenerarDVV_064RB("permiso_familia");
                }
            }
            bll_bitacora.RegistrarBitacora_064RB(new SER_Bitacora_064RB(SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB, DateTime.Now, "Roles", "Crear familia", 1));
        }
        public bool ValidarPermisosRepetidos_064RB(List<SER_Componente_064RB> componentes)
        {
            bool rdo = false;
            HashSet<int> permisos = new HashSet<int>();
            foreach (SER_Componente_064RB componente in componentes)
            {
                if (!AgregarPermisos_064RB(componente, permisos)) rdo = true;
            }
            return rdo;
        }
        private bool AgregarPermisos_064RB(SER_Componente_064RB componente, HashSet<int> permisos)
        {
            if (componente is SER_Permiso_064RB permiso)
            {
                return permisos.Add(permiso.Id_064RB);
            }

            if (componente is SER_Familia_064RB familia)
            {
                foreach (SER_Componente_064RB hijo in familia.Componentes_064RB)
                {
                    if (!AgregarPermisos_064RB(hijo, permisos))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
