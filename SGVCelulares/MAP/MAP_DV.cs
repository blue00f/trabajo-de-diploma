using DAL;
using Microsoft.Data.SqlClient;
using Servicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP
{
    public class MAP_DV
    {

        DAO_DV dao_dv;
        public MAP_DV() 
        { 
            dao_dv = new DAO_DV(); 
        }
        public void AgregarDVH(SER_DVH dvh) => dao_dv.AgregarDVH(dvh.NombreTabla, dvh.IdRegistro, dvh.Valor);
        public void BorrarDVH(string nombreTabla, string idRegistro) => dao_dv.BorrarDVH(nombreTabla, idRegistro);

        public void ModificarDVH(SER_DVH dvh) => dao_dv.ModificarDVH(dvh.NombreTabla, dvh.IdRegistro, dvh.Valor);

        public void AgregarDVV(SER_DVV dvv) => dao_dv.AgregarDVV(dvv.NombreTabla, dvv.Valor);
        public void BorrarDVV(string nombreTabla) => dao_dv.BorrarDVV(nombreTabla);
        public void ModificarDVV(SER_DVV dvv) => dao_dv.ModificarDVV(dvv.NombreTabla, dvv.Valor);
        public bool ExisteDVH(string nombreTabla, string id) => dao_dv.ExisteDVH(nombreTabla, id);
        public bool ExisteDVV(string nombreTabla) => dao_dv.ExisteDVV(nombreTabla);
        public SER_DVH ObtenerDVH(string nombreTabla, string idRegistro)
        {
            SER_DVH dvh = null;
            SqlDataReader dr = dao_dv.ObtenerDVH(nombreTabla, idRegistro);
            if (dr.Read())
            {
                object[] datos = new object[dr.FieldCount];
                dr.GetValues(datos);

                dvh = new SER_DVH(datos);
            }
            dr.Close();
            return dvh;
        }
        public SER_DVV ObtenerDVV(string nombreTabla)
        {
            SER_DVV dvv = null;
            SqlDataReader dr = dao_dv.ObtenerDVV(nombreTabla);
            if (dr.Read())
            {
                object[] datos = new object[dr.FieldCount];
                dr.GetValues(datos);
                dvv = new SER_DVV(datos);
            }
            dr.Close();
            return dvv;
        }
        public List<string> ConsultarDVHValores(string nombreTabla)
        {
            List<string> valores = new List<string>();
            SqlDataReader dr = dao_dv.ConsultarDVH(nombreTabla);

            while (dr.Read())
            {
                string valor = dr["Valor"].ToString();
                valores.Add(valor);
            }
            dr.Close();
            return valores;
        }
        public List<string> ConsultarDVHIds(string nombreTabla)
        {
            List<string> ids = new List<string>();
            SqlDataReader dr = dao_dv.ConsultarDVH(nombreTabla);
            while (dr.Read())
            {
                ids.Add(dr["IdRegistro"].ToString());
            }
            dr.Close();
            return ids;
        }
    }
}
