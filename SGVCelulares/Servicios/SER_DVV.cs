using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio
{
    public class SER_DVV : SER_DV
    {
        public SER_DVV(string nombreTabla, string valor) : base(nombreTabla, valor)
        {

        }
        public SER_DVV(object[] datos) : base(datos[0].ToString(), datos[1].ToString())
        {

        }
    }

}
