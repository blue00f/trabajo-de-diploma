using DAL_064RB.Tecnico_064RB;
using Servicio_064RB;
using System.Data;

namespace MAP_064RB.Tecnico_064RB
{
    public class MAP_Rol_064RB : IProtegidoPorDV_064RB
    {
        DAO_Rol_064RB dao_rol;
        MAP_Familia_064RB map_familia;
        public MAP_Rol_064RB()
        {
            dao_rol = new();
            map_familia = new();
        }
        public string NombreTabla_064RB => "Rol_064RB";
        public List<object> ObtenerRegistrosParaDV_064RB() => Consultar_064RB().Cast<object>().ToList();
        public string ObtenerIdParaDV_064RB(object registro) => Convert.ToString(((SER_Rol_064RB)registro).Id_064RB);

        public List<SER_Rol_064RB> Consultar_064RB()
        {
            var listaAux = new List<SER_Rol_064RB>();
            var lista = new List<SER_Rol_064RB>();
            DataTable tabla = dao_rol.Consultar_064RB();
            foreach (DataRow fila in tabla.Rows)
            {
                listaAux.Add(new SER_Rol_064RB(fila.ItemArray));
            }
            foreach (SER_Rol_064RB rol in listaAux)
            {
                lista.Add(ObtenerArbol_064RB(rol.Id_064RB));
            }
            return lista;
        }
        public List<object[]> ConsultarRolFamilia_064RB()
        {
            var lista = new List<object[]>();
            DataTable tabla = dao_rol.ConsultarRolFamilia_064RB();
            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(fila.ItemArray); // datos[0] = IdRol, datos[1] = IdFamilia
            }
            return lista;
        }
        public List<object[]> ConsultarRolPermiso_064RB()
        {
            var lista = new List<object[]>();
            DataTable tabla = dao_rol.ConsultarRolPermiso_064RB();
            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(fila.ItemArray); // datos[0] = IdRol, datos[1] = IdPermiso
            }
            return lista;
        }
        public SER_Rol_064RB ObtenerArbol_064RB(int idRol)
        {
            SER_Rol_064RB rol = ConsultarRol_064RB(idRol);
            foreach (SER_Permiso_064RB permiso in ConsultarPermisos_064RB(idRol))
            {
                rol.Componentes_064RB.Add(permiso);
            }
            foreach (SER_Familia_064RB familia in ConsultarFamilia_064RB(idRol))
            {
                rol.Componentes_064RB.Add(map_familia.ObtenerArbol_064RB(familia.Id_064RB));
            }
            return rol;
        }

        private SER_Rol_064RB ConsultarRol_064RB(int idRol)
        {
            DataTable tabla = dao_rol.ConsultarRol_064RB(idRol);
            return tabla.Rows.Count > 0 ? new SER_Rol_064RB(tabla.Rows[0].ItemArray) : null;
        }
        private List<SER_Permiso_064RB> ConsultarPermisos_064RB(int idRol)
        {
            var lista = new List<SER_Permiso_064RB>();
            DataTable tabla = dao_rol.ConsultarPermisos_064RB(idRol);
            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(new SER_Permiso_064RB(fila.ItemArray));
            }
            return lista;
        }
        private List<SER_Familia_064RB> ConsultarFamilia_064RB(int idRol)
        {
            var lista = new List<SER_Familia_064RB>();
            DataTable tabla = dao_rol.ConsultarFamilia_064RB(idRol);
            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(new SER_Familia_064RB(fila.ItemArray));
            }
            return lista;
        }
        public bool ExisteRol_064RB(SER_Rol_064RB rol) => dao_rol.ExisteRol_064RB(rol.Nombre_064RB);
        public void Agregar_064RB(SER_Rol_064RB rol) => dao_rol.Agregar_064RB(rol.Nombre_064RB);
        public void AgregarPermisoFamilia_064RB(SER_Rol_064RB rol, SER_Componente_064RB componente)
        {
            if (componente is SER_Permiso_064RB p)
            {
                dao_rol.AgregarPermiso_064RB(rol.Id_064RB, p.Id_064RB);
            }
            else if (componente is SER_Familia_064RB f)
            {
                dao_rol.AgregarFamilia_064RB(rol.Id_064RB, f.Id_064RB);
            }
        }
        public SER_Rol_064RB ObtenerIdPorNombre_064RB(SER_Rol_064RB rol)
        {
            DataTable tabla = dao_rol.ObtenerIdPorNombre_064RB(rol.Nombre_064RB);
            return tabla.Rows.Count > 0 ? new SER_Rol_064RB(tabla.Rows[0].ItemArray) : null;
        }
        public SER_Rol_064RB ObtenerRolPorId_064RB(int idRol)
        {
            DataTable tabla = dao_rol.ConsultarRol_064RB(idRol);
            return tabla.Rows.Count > 0 ? new SER_Rol_064RB(tabla.Rows[0].ItemArray) : null;
        }
        public void Borrar_064RB(SER_Rol_064RB rol) => dao_rol.Borrar_064RB(rol.Id_064RB);
        public bool EstaEnUso_064RB(SER_Rol_064RB rol) => dao_rol.EstaEnUso_064RB(rol.Id_064RB);
    }
}
