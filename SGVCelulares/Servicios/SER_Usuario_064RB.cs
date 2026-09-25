namespace Servicio_064RB
{
    public class SER_Usuario_064RB
    {
        public string Dni_064RB { get; set; }
        public string Nombre_064RB { get; set; }
        public string Apellido_064RB { get; set; }
        public string Correo_064RB { get; set; }
        public string NombreUsuario_064RB { get; set; }
        public string Password_064RB { get; set; }
        public bool Bloqueo_064RB { get; set; }
        public bool Activo_064RB { get; set; }
        public int CantIntentos_064RB { get; set; }
        public SER_Rol_064RB Rol_064RB { get; set; }
        public string IdIdioma_064RB { get; set; }
        public SER_Usuario_064RB() { }
        public SER_Usuario_064RB(string dni)
        {
            Dni_064RB = dni;
        }
        public SER_Usuario_064RB(string dni, string nombre, string apellido, string correo,string nombreUsuario)
        {
            Dni_064RB = dni;
            Nombre_064RB = nombre;
            Apellido_064RB = apellido;
            Correo_064RB = correo;
            NombreUsuario_064RB = nombreUsuario;
            
        }
        public SER_Usuario_064RB(string nombreUsuario, string password)
        {
            NombreUsuario_064RB = nombreUsuario;
            Password_064RB = password;
        }
        public SER_Usuario_064RB(string dni, string nombre, string apellido, string correo, string nombreUsuario, string password, bool bloqueo, bool activo)
        {
            Dni_064RB = dni;
            Nombre_064RB = nombre;
            Apellido_064RB = apellido;
            Correo_064RB = correo;
            NombreUsuario_064RB = nombreUsuario;
            Password_064RB = password;
            Bloqueo_064RB = bloqueo;
            Activo_064RB = activo;
        }
        public SER_Usuario_064RB(string dni, string nombre, string apellido, string correo, string nombreUsuario, string password, bool bloqueo, bool activo, int cantIntentos, SER_Rol_064RB rol, string idIdioma)
        {
            Dni_064RB = dni;
            Nombre_064RB = nombre;
            Apellido_064RB = apellido;
            Correo_064RB = correo;
            NombreUsuario_064RB = nombreUsuario;
            Password_064RB = password;
            Bloqueo_064RB = bloqueo;
            Activo_064RB = activo;
            CantIntentos_064RB = cantIntentos;
            Rol_064RB = rol;
            IdIdioma_064RB = idIdioma;
        }
        public SER_Usuario_064RB(string dni, string nombre, string apellido, string correo, string nombreUsuario, bool bloqueo, bool activo, SER_Rol_064RB rol)
        {
            Dni_064RB = dni;
            Nombre_064RB = nombre;
            Apellido_064RB = apellido;
            Correo_064RB = correo;
            NombreUsuario_064RB = nombreUsuario;
            Bloqueo_064RB = bloqueo;
            Activo_064RB = activo;
            Rol_064RB = rol;
        }
        public SER_Usuario_064RB(string dni, string nombre, string apellido, string correo, bool bloqueo, bool activo, SER_Rol_064RB rol, string idIdioma)
        {
            Dni_064RB = dni;
            Nombre_064RB = nombre;
            Apellido_064RB = apellido;
            Correo_064RB = correo;
            NombreUsuario_064RB = apellido.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries)[0] + dni;
            Password_064RB = dni + apellido.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries)[0];
            Bloqueo_064RB = bloqueo;
            Activo_064RB = activo;
            CantIntentos_064RB = 0;
            Rol_064RB = rol;
            IdIdioma_064RB = idIdioma;
        }
        public SER_Usuario_064RB(object[] datos)
        {
            Dni_064RB = Convert.ToString(datos[0]);
            Nombre_064RB = Convert.ToString(datos[1]);
            Apellido_064RB = Convert.ToString(datos[2]);
            Correo_064RB = Convert.ToString(datos[3]);
            NombreUsuario_064RB = Convert.ToString(datos[4]);
            Password_064RB = Convert.ToString(datos[5]);
            Bloqueo_064RB = Convert.ToBoolean(datos[6]);
            Activo_064RB = Convert.ToBoolean(datos[7]);
            CantIntentos_064RB = Convert.ToInt16(datos[8]);
            IdIdioma_064RB = Convert.ToString(datos[10]);
        }
    }
}
