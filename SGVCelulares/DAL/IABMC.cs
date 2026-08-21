using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IABMC
    {
        void Agregar(params object[] datos);
        void Borrar(string id);
        void Modificar(string id, params object[] datos);
        SqlDataReader Consultar();
        SqlDataReader ConsultarPorId(string id);
        bool ValidarRepetido(string id);
    }
}
