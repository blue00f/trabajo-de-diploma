using MAP_064RB.Tecnico_064RB;
using Servicio_064RB;

namespace BLL_064RB.Tecnico_064RB
{
    public class BLL_Bitacora_064RB
    {
        MAP_Bitacora_064RB map_bitacora;
        List<SER_Bitacora_064RB> bitacoraCache;
        public BLL_Bitacora_064RB()
        {
            map_bitacora = new MAP_Bitacora_064RB();
        }
        private List<SER_Bitacora_064RB> Consultar_064RB()
        {
            if (bitacoraCache == null) bitacoraCache = map_bitacora.Consultar_064RB();
            return bitacoraCache;
        }
        public void RegistrarBitacora_064RB(SER_Bitacora_064RB bitacora)
        {
            map_bitacora.RegistrarBitacora_064RB(bitacora);
        }
        public List<object> ConsultarParaGrilla_064RB()
        {
            var consulta = from b in Consultar_064RB()
                           select new
                           {
                               Usuario = b.Usuario_064RB.NombreUsuario_064RB,
                               Fecha = b.Fecha_064RB.ToShortDateString(),
                               Hora = b.Fecha_064RB.ToString("HH:mm"),
                               Modulo = b.Modulo_064RB,
                               Evento = b.Evento_064RB,
                               Criticidad = b.Criticidad_064RB
                           };
            return consulta.ToList<object>();
        }
        public List<object> ConsultarParaGrilla3Dias_064RB()
        {
            var consulta = from b in Consultar_064RB() where b.Fecha_064RB >= (DateTime.Now.AddDays(-3))
                           select new
                           {
                               Usuario = b.Usuario_064RB.NombreUsuario_064RB,
                               Fecha = b.Fecha_064RB.ToShortDateString(),
                               Hora = b.Fecha_064RB.ToString("HH:mm"),
                               Modulo = b.Modulo_064RB,
                               Evento = b.Evento_064RB,
                               Criticidad = b.Criticidad_064RB
                           };
            return consulta.ToList<object>();
        }
        public SER_Usuario_064RB ConsultarPorNombreUsuario_064RB(string nombreUsuario) 
        {
            var cta = (from b in Consultar_064RB()
                      where b.Usuario_064RB.NombreUsuario_064RB == nombreUsuario
                      select new
                      {
                          Nombre = b.Usuario_064RB.Nombre_064RB,
                          Apellido = b.Usuario_064RB.Apellido_064RB
                      }).ToList();
            SER_Usuario_064RB us = new SER_Usuario_064RB();
            us.Nombre_064RB = cta[0].Nombre;
            us.Apellido_064RB = cta[0].Apellido;
            return us;
        }
        public List<object> ConsultarFiltradoBitacora_064RB(string login,string modulo,string evento,int? criticidad,DateTime? fechaInicio,DateTime? fechaFin)
        {
            var consulta = from bit in Consultar_064RB()
                           where
                           (string.IsNullOrWhiteSpace(login)
                           || bit.Usuario_064RB.NombreUsuario_064RB.Contains(login))
                           &&
                           (string.IsNullOrWhiteSpace(modulo)
                           || bit.Modulo_064RB.Contains(modulo))
                           &&
                           (string.IsNullOrWhiteSpace(evento)
                           || bit.Evento_064RB.Contains(evento))
                           &&
                           (!criticidad.HasValue
                            || bit.Criticidad_064RB == criticidad.Value)
                           &&
                           (!fechaInicio.HasValue
                           || bit.Fecha_064RB >= fechaInicio.Value)
                           &&
                           (!fechaFin.HasValue
                           || bit.Fecha_064RB <= fechaFin.Value)
                            select new
                           {
                                Usuario = bit.Usuario_064RB.NombreUsuario_064RB,
                                Fecha = bit.Fecha_064RB.ToShortDateString(),
                                Hora = bit.Fecha_064RB.ToString("HH:mm"),
                                Modulo = bit.Modulo_064RB,
                                Evento = bit.Evento_064RB,
                                Criticidad = bit.Criticidad_064RB
                           };

            return consulta.ToList<object>();
        }
    }
}
