using DAL;
using Microsoft.Data.SqlClient;
using Servicio;

namespace MAP
{
    public class MAP_Bitacora
    {
        DAO_Bitacora dao_bitacora;
        MAP_Usuario map_usuario;
        public MAP_Bitacora()
        {
            dao_bitacora = new DAO_Bitacora();
            map_usuario = new MAP_Usuario();
        }
        public void RegistrarBitacora(SER_Bitacora bitacora) => dao_bitacora.Agregar(bitacora.Usuario.Dni, bitacora.Fecha, bitacora.Modulo, bitacora.Evento, bitacora.Criticidad);
        public List<SER_Bitacora> Consultar()
        {
            List<SER_Bitacora> lista = new List<SER_Bitacora>();
            SqlDataReader dr = dao_bitacora.Consultar();

            while (dr.Read())
            {
                object[] datos = new object[dr.FieldCount];
                dr.GetValues(datos);

                SER_Bitacora bitacora = new SER_Bitacora(
                    map_usuario.ConsultarPorId(new SER_Usuario(datos[1].ToString())),
                    Convert.ToDateTime(datos[2]),
                    Convert.ToString(datos[3]),
                    Convert.ToString(datos[4]),
                    Convert.ToInt16(datos[5])
                );
                lista.Add(bitacora);
            }
            dr.Close();
            return lista;
        }
    }
}
