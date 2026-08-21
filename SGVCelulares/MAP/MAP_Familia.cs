using DAL;
using Microsoft.Data.SqlClient;
using Servicio;

namespace MAP
{
    public class MAP_Familia : IABMC<SER_Familia>
    {
        DAO_Familia dao_familia;
        public MAP_Familia()
        {
            dao_familia = new DAO_Familia();
        }
        public void Agregar(SER_Familia familia)
        {
            dao_familia.Agregar(familia.Nombre);
        }
        public void Borrar(SER_Familia familia) => dao_familia.Borrar(familia.Id);

        public bool EstaEnUso(SER_Familia familia) => dao_familia.EstaEnUso(familia.Id);

        public List<SER_Familia> Consultar()
        {
            List<SER_Familia> listaAux = new List<SER_Familia>();
            List<SER_Familia> lista = new List<SER_Familia>();

            SqlDataReader dr = dao_familia.Consultar();

            while (dr.Read())
            {
                object[] datos = new object[dr.FieldCount];
                dr.GetValues(datos);
                listaAux.Add(new SER_Familia(datos));
            }
            dr.Close();

            foreach (SER_Familia familia in listaAux)
            {
                lista.Add(ObtenerArbol(familia.Id));
            }
            return lista;
        }
        public List<object[]> ConsultarFamiliaFamilia()
        {
            List<object[]> listaAux = new List<object[]>();
            SqlDataReader dr = dao_familia.ConsultarFamiliaFamilia();
            while (dr.Read())
            {
                object[] datos = new object[dr.FieldCount];
                dr.GetValues(datos);
                listaAux.Add(datos); // datos[0] = IdFamilia, datos[1] = IdFamilia
            }
            dr.Close();
            return listaAux;
        }
        public List<object[]> ConsultarFamiliaPermiso()
        {
            List<object[]> listaAux = new List<object[]>();
            SqlDataReader dr = dao_familia.ConsultarFamiliaPermiso();
            while (dr.Read())
            {
                object[] datos = new object[dr.FieldCount];
                dr.GetValues(datos);
                listaAux.Add(datos); // datos[0] = IdFamilia, datos[1] = IdPermiso
            }
            dr.Close();
            return listaAux;
        }

        public SER_Familia ConsultarPorId(SER_Familia familia)
        {
            throw new NotImplementedException();
        }

        public void Modificar(SER_Familia familia)
        {
            throw new NotImplementedException();
        }
        public bool ExisteFamilia(SER_Familia familia)
        {
            return dao_familia.ExisteFamilia(familia.Nombre);
        }
        public void AgregarPermisoFamilia(SER_Familia familia, SER_Componente componente)
        {
            if (componente is SER_Permiso p)
            {
                dao_familia.AgregarPermiso(familia.Id, p.Id);
            }
            else if (componente is SER_Familia f)
            {
                dao_familia.AgregarFamilia(familia.Id, f.Id);
            }
        }
        public SER_Familia ObtenerIdPorNombre(SER_Familia familia)
        {
            SER_Familia familiaAux = null;
            SqlDataReader dr = dao_familia.ObtenerIdPorNombre(familia.Nombre);
            while (dr.Read())
            {
                object[] datos = new object[dr.FieldCount];
                dr.GetValues(datos);
                familiaAux = new SER_Familia(datos);
            }
            dr.Close();
            return familiaAux;
        }

        public SER_Familia ObtenerArbol(int idFamilia)
        {
            SER_Familia familia = ConsultarFamilia(idFamilia);

            foreach (SER_Permiso permiso in ConsultarPermisos(idFamilia))
            {
                familia.Componentes.Add(permiso);
            }
            foreach (SER_Familia hija in ConsultarFamiliasHijas(idFamilia))
            {
                familia.Componentes.Add(ObtenerArbol(hija.Id));
            }
            return familia;
        }

        private SER_Familia ConsultarFamilia(int idFamilia)
        {
            SER_Familia familia = null;
            SqlDataReader dr = dao_familia.ConsultarFamilia(idFamilia);
            while (dr.Read())
            {
                object[] datos = new object[dr.FieldCount];
                dr.GetValues(datos);
                familia = new SER_Familia(datos);
            }
            dr.Close();
            return familia;
        }
        private List<SER_Permiso> ConsultarPermisos(int idFamilia)
        {
            List<SER_Permiso> lista = new List<SER_Permiso>();
            SqlDataReader dr = dao_familia.ConsultarPermisos(idFamilia);

            while (dr.Read())
            {
                object[] datos = new object[dr.FieldCount];
                dr.GetValues(datos);
                lista.Add(new SER_Permiso(datos));
            }
            dr.Close();
            return lista;
        }
        private List<SER_Familia> ConsultarFamiliasHijas(int idFamilia)
        {
            List<SER_Familia> lista = new List<SER_Familia>();
            SqlDataReader dr = dao_familia.ConsultarFamiliasHijas(idFamilia);

            while (dr.Read())
            {
                object[] datos = new object[dr.FieldCount];
                dr.GetValues(datos);
                lista.Add(new SER_Familia(datos));
            }
            dr.Close();
            return lista;
        }
    }
}
