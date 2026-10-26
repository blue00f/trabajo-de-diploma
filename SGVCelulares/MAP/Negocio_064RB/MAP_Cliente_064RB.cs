using BE_064RB;
using DAL_064RB.Negocio_064RB;
using System.Data;

namespace MAP_064RB.Negocio_064RB
{
    public class MAP_Cliente_064RB
    {
        DAO_Cliente_064RB dao_cliente;
        public MAP_Cliente_064RB()
        {
            dao_cliente = new();
        }
        public void Agregar_064RB(BE_Cliente_064RB cliente)
        {
            dao_cliente.Agregar_064RB(cliente.Dni_064RB, cliente.Nombre_064RB, cliente.Apellido_064RB, cliente.Telefono_064RB, cliente.Email_064RB, cliente.FechaNacimiento_064RB);
        }
        public void Borrar_064RB(BE_Cliente_064RB cliente)
        {
            dao_cliente.Borrar_064RB(cliente.Dni_064RB);
        }
        public void Modificar_064RB(BE_Cliente_064RB cliente)
        {
            dao_cliente.Modificar_064RB(cliente.Dni_064RB, cliente.Nombre_064RB, cliente.Apellido_064RB, cliente.Telefono_064RB, cliente.Email_064RB, cliente.FechaNacimiento_064RB);
        }
        public List<BE_Cliente_064RB> Consultar()
        {
            DataTable dt = dao_cliente.Consultar_064RB();
            List<BE_Cliente_064RB> lista = new();
            foreach (DataRow fila in dt.Rows)
            {
                lista.Add(Mapear_064RB(fila));
            }
            return lista;
        }
        public BE_Cliente_064RB ConsultarPorDni_064RB(string dni)
        {
            DataTable dt = dao_cliente.ConsultarPorDni_064RB(dni);
            return Mapear_064RB(dt.Rows[0]);
        }
        private BE_Cliente_064RB Mapear_064RB(DataRow fila)
        {
            return new BE_Cliente_064RB(
                Convert.ToString(fila[0]),
                Convert.ToString(fila[1]),
                Convert.ToString(fila[2]),
                Convert.ToString(fila[3]),
                Convert.ToString(fila[4]),
                Convert.ToDateTime(fila[5])
            );
        }
    }
}
