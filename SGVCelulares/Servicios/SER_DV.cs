using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio
{
    public abstract class SER_DV
    {
        public string NombreTabla { get; set; }
        public string Valor { get; set; }

        public SER_DV(string nombreTabla, string valor)
        {
            NombreTabla = nombreTabla;
            Valor = valor;
        }
    }

}
