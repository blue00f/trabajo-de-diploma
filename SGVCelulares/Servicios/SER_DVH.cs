using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio
{
    public class SER_DVH : SER_DV
    {
        public string IdRegistro { get; set; }

        public SER_DVH(string nombreTabla, string ID, string valor) : base(nombreTabla, valor)
        {
            IdRegistro = ID;
        }

        public SER_DVH(object[] datos) : base(datos[0].ToString(), datos[2].ToString())
        {
            IdRegistro = datos[1].ToString();
        }
    }
}
