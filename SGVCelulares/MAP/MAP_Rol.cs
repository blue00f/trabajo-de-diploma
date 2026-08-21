using DAL;
using Microsoft.Data.SqlClient;
using Servicio;

namespace MAP
{
    public class MAP_Rol
    {
        DAO_Rol dao_rol;
        MAP_Familia map_familia;
        public MAP_Rol()
        {
            dao_rol = new DAO_Rol();
            map_familia = new MAP_Familia();
        }

        public List<SER_Rol> Consultar()
        {
            List<SER_Rol> listaAux = new List<SER_Rol>();
            List<SER_Rol> lista = new List<SER_Rol>();
            SqlDataReader dr = dao_rol.Consultar();

            while (dr.Read())
            {
                object[] datos = new object[dr.FieldCount];
                dr.GetValues(datos);
                listaAux.Add(new SER_Rol(datos));
            }
            dr.Close();

            foreach (SER_Rol rol in listaAux)
            {
                lista.Add(ObtenerArbol(rol.Id));
            }
            return lista;
        }
        public List<object[]> ConsultarRolFamilia()
        {
            List<object[]> listaAux = new List<object[]>();
            SqlDataReader dr = dao_rol.ConsultarRolFamilia();
            while (dr.Read())
            {
                object[] datos = new object[dr.FieldCount];
                dr.GetValues(datos);
                listaAux.Add(datos); // datos[0] = IdRol, datos[1] = IdFamilia
            }
            dr.Close();
            return listaAux;
        }
        public List<object[]> ConsultarRolPermiso()
        {
            List<object[]> listaAux = new List<object[]>();
            SqlDataReader dr = dao_rol.ConsultarRolPermiso();
            while (dr.Read())
            {
                object[] datos = new object[dr.FieldCount];
                dr.GetValues(datos);
                listaAux.Add(datos); // datos[0] = IdRol, datos[1] = IdPermiso
            }
            dr.Close();
            return listaAux;
        }
        public SER_Rol ObtenerArbol(int idRol)
        {
            SER_Rol rol = ConsultarRol(idRol);

            foreach (SER_Permiso permiso in ConsultarPermisos(idRol))
            {
                rol.Componentes.Add(permiso);
            }
            foreach (SER_Familia familia in ConsultarFamilia(idRol))
            {
                rol.Componentes.Add(map_familia.ObtenerArbol(familia.Id));
            }
            return rol;
        }

        private SER_Rol ConsultarRol(int idRol)
        {
            SER_Rol rol = null;
            SqlDataReader dr = dao_rol.ConsultarRol(idRol);
            while (dr.Read())
            {
                object[] datos = new object[dr.FieldCount];
                dr.GetValues(datos);
                rol = new SER_Rol(datos);
            }
            dr.Close();
            return rol;
        }
        private List<SER_Permiso> ConsultarPermisos(int idRol)
        {
            List<SER_Permiso> lista = new List<SER_Permiso>();
            SqlDataReader dr = dao_rol.ConsultarPermisos(idRol);

            while (dr.Read())
            {
                object[] datos = new object[dr.FieldCount];
                dr.GetValues(datos);
                lista.Add(new SER_Permiso(datos));
            }
            dr.Close();
            return lista;
        }
        private List<SER_Familia> ConsultarFamilia(int idRol)
        {
            List<SER_Familia> lista = new List<SER_Familia>();
            SqlDataReader dr = dao_rol.ConsultarFamilia(idRol);
            while (dr.Read())
            {
                object[] datos = new object[dr.FieldCount];
                dr.GetValues(datos);
                lista.Add(new SER_Familia(datos));
            }
            dr.Close();
            return lista;
        }
        public bool ExisteRol(SER_Rol rol) => dao_rol.ExisteRol(rol.Nombre);
        public void Agregar(SER_Rol rol) => dao_rol.Agregar(rol.Nombre);
        public void AgregarPermisoFamilia(SER_Rol rol, SER_Componente componente)
        {
            if (componente is SER_Permiso p)
            {
                dao_rol.AgregarPermiso(rol.Id, p.Id);
            }
            else if (componente is SER_Familia f)
            {
                dao_rol.AgregarFamilia(rol.Id, f.Id);
            }
        }
        public SER_Rol ObtenerIdPorNombre(SER_Rol rol)
        {
            SER_Rol rolAux = null;
            SqlDataReader dr = dao_rol.ObtenerIdPorNombre(rol.Nombre);
            while (dr.Read())
            {
                object[] datos = new object[dr.FieldCount];
                dr.GetValues(datos);
                rolAux = new SER_Rol(datos);
            }
            dr.Close();
            return rolAux;
        }
        public SER_Rol ObtenerRolPorId(int idRol)
        {
            SER_Rol rol = null;
            SqlDataReader dr = dao_rol.ConsultarRol(idRol);
            while (dr.Read())
            {
                object[] datos = new object[dr.FieldCount];
                dr.GetValues(datos);
                rol = new SER_Rol(datos);
            }
            dr.Close();
            return rol;
        }
        public void Borrar(SER_Rol rol) => dao_rol.Borrar(rol.Id);
        public bool EstaEnUso(SER_Rol rol) => dao_rol.EstaEnUso(rol.Id);
    }
}
