using DAL_064RB.Tecnico_064RB;
using Servicio_064RB;
using System.Data;

namespace MAP_064RB.Tecnico_064RB
{
    public class MAP_Bitacora_064RB
    {
        DAO_Bitacora_064RB dao_bitacora;
        MAP_Usuario_064RB map_usuario;
        public MAP_Bitacora_064RB()
        {
            dao_bitacora = new();
            map_usuario = new();
        }
        public void RegistrarBitacora_064RB(SER_Bitacora_064RB bitacora) => dao_bitacora.Agregar_064RB(bitacora.Usuario_064RB.Dni_064RB, bitacora.Fecha_064RB, bitacora.Modulo_064RB, bitacora.Evento_064RB, bitacora.Criticidad_064RB);
        public List<SER_Bitacora_064RB> Consultar_064RB()
        {
            DataTable dt = dao_bitacora.Consultar_064RB();
            var lista = new List<SER_Bitacora_064RB>();
            foreach (DataRow fila in dt.Rows)
            {
                lista.Add(Mapear_064RB(fila));
            }
            return lista;
        }
        private SER_Bitacora_064RB Mapear_064RB(DataRow fila)
        {
            return new SER_Bitacora_064RB(
                map_usuario.ConsultarPorId_064RB(new SER_Usuario_064RB(fila["dni"].ToString())),
                Convert.ToDateTime(fila["fecha"]),
                Convert.ToString(fila["modulo"]),
                Convert.ToString(fila["evento"]),
                Convert.ToInt16(fila["criticidad"])
            );
        }
    }
}
