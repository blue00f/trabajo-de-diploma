using System.Data;

namespace DAL_064RB
{
    public class ParametroBD_064RB
    {
        public string Nombre_064RB { get; set; }
        public SqlDbType Tipo_064RB { get; set; }
        public object Valor_064RB { get; set; }
        public ParametroBD_064RB(string nombre, SqlDbType tipo, object valor)
        {
            Nombre_064RB = nombre;
            Tipo_064RB = tipo;
            Valor_064RB = valor;
        }
    }
}
