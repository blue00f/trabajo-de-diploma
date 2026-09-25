using DAL_064RB.Tecnico_064RB;
using Servicio_064RB;
using System.Data;

namespace MAP_064RB.Tecnico_064RB
{
    public class MAP_Familia_064RB : IABMC_064RB<SER_Familia_064RB>, IProtegidoPorDV_064RB
    {
        DAO_Familia_064RB dao_familia;
        public MAP_Familia_064RB() => dao_familia = new();
        public string NombreTabla_064RB => "Familia_064RB";
        public List<object> ObtenerRegistrosParaDV_064RB() => Consultar_064RB().Cast<object>().ToList();
        public string ObtenerIdParaDV_064RB(object registro) => Convert.ToString(((SER_Familia_064RB)registro).Id_064RB);

        public void Agregar_064RB(SER_Familia_064RB familia) => dao_familia.Agregar_064RB(familia.Nombre_064RB);
        public void Borrar_064RB(SER_Familia_064RB familia) => dao_familia.Borrar_064RB(familia.Id_064RB);
        public List<SER_Familia_064RB> Consultar_064RB()
        {
            var listaAux = new List<SER_Familia_064RB>();
            var lista = new List<SER_Familia_064RB>();
            DataTable tabla = dao_familia.Consultar_064RB();
            foreach (DataRow fila in tabla.Rows)
            {
                listaAux.Add(new SER_Familia_064RB(fila.ItemArray));
            }
            foreach (SER_Familia_064RB familia in listaAux)
            {
                lista.Add(ObtenerArbol_064RB(familia.Id_064RB));
            }
            return lista;
        }
        public List<object[]> ConsultarFamiliaFamilia_064RB()
        {
            var lista = new List<object[]>();
            DataTable tabla = dao_familia.ConsultarFamiliaFamilia_064RB();
            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(fila.ItemArray); // datos[0] = IdFamilia, datos[1] = IdFamilia
            }
            return lista;
        }
        public List<object[]> ConsultarFamiliaPermiso_064RB()
        {
            var lista = new List<object[]>();
            DataTable tabla = dao_familia.ConsultarFamiliaPermiso_064RB();
            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(fila.ItemArray); // datos[0] = IdFamilia, datos[1] = IdPermiso
            }
            return lista;
        }
        public SER_Familia_064RB ConsultarPorId_064RB(SER_Familia_064RB familia) => throw new NotImplementedException();

        public void Modificar_064RB(SER_Familia_064RB familia) => throw new NotImplementedException();
        public bool ExisteFamilia_064RB(SER_Familia_064RB familia) => dao_familia.ExisteFamilia_064RB(familia.Nombre_064RB);
        public void AgregarPermisoFamilia_064RB(SER_Familia_064RB familia, SER_Componente_064RB componente)
        {
            if (componente is SER_Permiso_064RB p)
            {
                dao_familia.AgregarPermiso_064RB(familia.Id_064RB, p.Id_064RB);
            }
            else if (componente is SER_Familia_064RB f)
            {
                dao_familia.AgregarFamilia_064RB(familia.Id_064RB, f.Id_064RB);
            }
        }
        public SER_Familia_064RB ObtenerArbol_064RB(int idFamilia)
        {
            SER_Familia_064RB familia = ConsultarFamilia_064RB(idFamilia);

            foreach (SER_Permiso_064RB permiso in ConsultarPermisos_064RB(idFamilia))
            {
                familia.Componentes_064RB.Add(permiso);
            }
            foreach (SER_Familia_064RB hija in ConsultarFamiliasHijas_064RB(idFamilia))
            {
                familia.Componentes_064RB.Add(ObtenerArbol_064RB(hija.Id_064RB));
            }
            return familia;
        }

        private SER_Familia_064RB ConsultarFamilia_064RB(int idFamilia)
        {
            DataTable tabla = dao_familia.ConsultarFamilia_064RB(idFamilia);
            return tabla.Rows.Count > 0 ? new SER_Familia_064RB(tabla.Rows[0].ItemArray) : null;
        }
        private List<SER_Permiso_064RB> ConsultarPermisos_064RB(int idFamilia)
        {
            var lista = new List<SER_Permiso_064RB>();
            DataTable tabla = dao_familia.ConsultarPermisos_064RB(idFamilia);
            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(new SER_Permiso_064RB(fila.ItemArray));
            }
            return lista;
        }
        private List<SER_Familia_064RB> ConsultarFamiliasHijas_064RB(int idFamilia)
        {
            var lista = new List<SER_Familia_064RB>();
            DataTable tabla = dao_familia.ConsultarFamiliasHijas_064RB(idFamilia);
            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(new SER_Familia_064RB(fila.ItemArray));
            }
            return lista;
        }
        public SER_Familia_064RB ObtenerIdPorNombre_064RB(SER_Familia_064RB familia)
        {
            DataTable tabla = dao_familia.ObtenerIdPorNombre_064RB(familia.Nombre_064RB);
            return tabla.Rows.Count > 0 ? new SER_Familia_064RB(tabla.Rows[0].ItemArray) : null;
        }
        public bool EstaEnUso_064RB(SER_Familia_064RB familia) => dao_familia.EstaEnUso_064RB(familia.Id_064RB);
    }
}
