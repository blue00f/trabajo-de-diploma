using MAP_064RB.Tecnico_064RB;
using Servicio_064RB;

namespace BLL_064RB.Tecnico_064RB
{
    public class BLL_Rol_064RB
    {
        MAP_Rol_064RB map_rol;
        BLL_Bitacora_064RB bll_bitacora;
        BLL_DV_064RB bll_dv;
        public BLL_Rol_064RB()
        {
            map_rol = new();
            bll_bitacora = new();
            bll_dv = new();
        }
        public List<SER_Rol_064RB> Consultar_064RB() => map_rol.Consultar_064RB();
        public List<object[]> ConsultarRolFamilia_064RB() => map_rol.ConsultarRolFamilia_064RB();
        public List<object[]> ConsultarRolPermiso_064RB() => map_rol.ConsultarRolPermiso_064RB();
        public void Agregar_064RB(SER_Rol_064RB rol, List<SER_Componente_064RB> componentes)
        {
            if (componentes.Count == 0) throw new Exception("No hay permisos o familias cargadas!");
            if (map_rol.ExisteRol_064RB(rol)) throw new Exception("El nombre del rol está repetido!");
            if (ValidarPermisosRepetidos_064RB(componentes)) throw new Exception("El permiso repite con otro");
            map_rol.Agregar_064RB(rol);
            rol.Id_064RB = map_rol.ObtenerIdPorNombre_064RB(rol).Id_064RB;
            bll_dv.GenerarDVH_064RB(rol, rol.Id_064RB.ToString(),"rol_064RB");
            bll_dv.GenerarDVV_064RB("rol_064RB");
            
            foreach (SER_Componente_064RB c in componentes)
            {
                map_rol.AgregarPermisoFamilia_064RB(rol, c);
                if(c is SER_Familia_064RB familia)
                {
                    bll_dv.GenerarDVH_064RB(new List<string>() { rol.Id_064RB.ToString(), c.Id_064RB.ToString() }, "rol_familia_064RB");
                    bll_dv.GenerarDVV_064RB("rol_familia_064RB");
                }
                else if (c is SER_Permiso_064RB permiso)
                {
                    bll_dv.GenerarDVH_064RB(new List<string>() { rol.Id_064RB.ToString(), c.Id_064RB.ToString() }, "rol_permiso_064RB");
                    bll_dv.GenerarDVV_064RB("rol_permiso_064RB");
                }
            }
            bll_bitacora.RegistrarBitacora_064RB(new SER_Bitacora_064RB(SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB, DateTime.Now, "Roles", "Crear rol", 1));
        }
        private bool ValidarPermisosRepetidos_064RB(List<SER_Componente_064RB> componentes)
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
        public void Borrar_064RB(SER_Rol_064RB rol)
        {
            if (map_rol.EstaEnUso_064RB(rol))
                throw new Exception("No se puede borrar: el rol está asignado a uno o más usuarios.");

            map_rol.Borrar_064RB(rol);

            bll_dv.BorrarDVH_064RB("rol", rol.Id_064RB.ToString());

            foreach (var componente in rol.Componentes_064RB)
            {
                string tabla = componente is SER_Familia_064RB ? "rol_familia_064RB" : "rol_permiso_064RB";
                string id = $"{rol.Id_064RB}-{componente.Id_064RB}";
                bll_dv.BorrarDVH_064RB(tabla, id);
            }

            bll_dv.GenerarDVV_064RB("rol_064RB");
            bll_dv.GenerarDVV_064RB("rol_familia_064RB");
            bll_dv.GenerarDVV_064RB("rol_permiso_064RB");

            bll_bitacora.RegistrarBitacora_064RB(new SER_Bitacora_064RB(SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB, DateTime.Now, "Roles", "Borrar rol", 1));
        }
    }
}
