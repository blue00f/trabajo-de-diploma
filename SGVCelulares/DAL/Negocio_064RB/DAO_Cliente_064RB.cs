using System.Data;

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
            com.Ejecutar_064RB("INSERT INTO Cliente_064RB(Dni_064RB,Nombre_064RB,Apellido_064RB,Telefono_064RB,Email_064RB,FechaNacimiento_064RB) values (@dni,@nombre,@apellido,@telefono,@email,@fechaNacimiento)", parametros);
        }
        public void Borrar_064RB(string dni)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@dni", SqlDbType.VarChar, dni)
            };
            com.Ejecutar_064RB("DELETE FROM Cliente_064RB WHERE Dni_064RB=@dni;", parametros);
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
            com.Ejecutar_064RB("UPDATE Cliente_064RB SET Nombre_064RB=@nombre,Apellido_064RB=@apellido,Telefono_064RB=@telefono,Email_064RB=@email,FechaNacimiento_064RB=@fechaNacimiento WHERE Dni_064RB=@dni", parametros);
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
            return com.Consultar_064RB("SELECT * FROM Cliente_064RB WHERE Dni_064RB=@dni", parametros);
        }
    }
}
