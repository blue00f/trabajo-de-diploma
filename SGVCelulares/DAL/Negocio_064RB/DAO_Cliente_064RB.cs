using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DAL_064RB.Negocio_064RB
{
    public class DAO_Cliente_064RB
    {
        ComandoBD_064RB com;
        public DAO_Cliente_064RB()
        {
            com = new();
        }
        public void Agregar_064RB(string dni, string nombre, string apellido, string telefono, string email, DateTime fechaNacimiento)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@dni", SqlDbType.VarChar, dni),
                new("@nombre", SqlDbType.VarChar, nombre),
                new("@apellido", SqlDbType.VarChar, apellido),
                new("@telefono", SqlDbType.VarChar, telefono),
                new("@email", SqlDbType.VarChar, email),
                new("@fechaNacimiento", SqlDbType.Date, fechaNacimiento)
            };
            com.Ejecutar_064RB("INSERT INTO Cliente_064RB(dni,nombre,apellido,telefono,email,fechaNacimiento) values (@dni,@nombre,@apellido,@telefono,@email,@fechaNacimiento)", parametros);
        }
        public void Borrar_064RB(string dni)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@dni", SqlDbType.VarChar, dni)
            };
            com.Ejecutar_064RB("DELETE FROM Cliente_064RB WHERE dni=@dni;", parametros);
        }
        public void Modificar_064RB(string dni, string nombre, string apellido, string telefono, string email, DateTime fechaNacimiento)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@dni", SqlDbType.VarChar, dni),
                new("@nombre", SqlDbType.VarChar, nombre),
                new("@apellido", SqlDbType.VarChar, apellido),
                new("@telefono", SqlDbType.VarChar, telefono),
                new("@email", SqlDbType.VarChar, email),
                new("@fechaNacimiento", SqlDbType.Date, fechaNacimiento)
            };
            com.Ejecutar_064RB("UPDATE Cliente_064RB SET nombre=@nombre,apellido=@apellido,telefono=@telefono,email=@email,fechaNacimiento=@fechaNacimiento WHERE dni=@dni", parametros);
        }
        public DataTable Consultar_064RB()
        {
            return com.Consultar_064RB("select * from Cliente_064RB");
        }
        public DataTable ConsultarPorDni_064RB(string dni)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@dni", SqlDbType.VarChar, dni)
            };
            return com.Consultar_064RB("SELECT * FROM Cliente_064RB WHERE dni=@dni", parametros);
        }
    }
}
