using System.Runtime.InteropServices;

namespace BE_064RB
{
    public class BE_Cliente_064RB
    {
        public string Dni_064RB { get; set; }
        public string Nombre_064RB{ get; set; }
        public string Apellido_064RB { get; set; }
        public string Telefono_064RB { get; set; }
        public string Email_064RB { get; set; }
        public DateTime FechaNacimiento_064RB { get; set; }

        public BE_Cliente_064RB(string dni, string nombre, string apellido, string telefono, string email, DateTime fechaNacimiento)
        {
            Dni_064RB = dni;
            Nombre_064RB = nombre;
            Apellido_064RB = apellido;
            Telefono_064RB = telefono;
            Email_064RB = email;
            FechaNacimiento_064RB = fechaNacimiento;
        }
        public override string ToString()
        {
            return $"{Dni_064RB} - {Nombre_064RB} {Apellido_064RB} {Telefono_064RB} {Email_064RB} {FechaNacimiento_064RB.ToShortDateString()}";
        }
    }
}
