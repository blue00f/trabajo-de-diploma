using MAP_064RB.Tecnico_064RB;
using Servicio_064RB;

namespace BLL_064RB.Tecnico_064RB
{
    public class BLL_Usuario_064RB : IABMC_064RB<SER_Usuario_064RB>
    {
        MAP_Usuario_064RB map_usuario;
        MAP_Rol_064RB map_rol;
        BLL_Bitacora_064RB bll_bitacora;
        BLL_Idioma_064RB bll_idioma;
        BLL_DV_064RB bll_dv;
        public BLL_Usuario_064RB()
        {
            map_usuario = new();
            bll_bitacora = new();
            bll_idioma = new();
            map_rol = new();
            bll_dv = new();
        }

        public void Agregar_064RB(SER_Usuario_064RB usuario)
        {
            if (map_usuario.ValidarDniRepetido_064RB(usuario)) throw new Exception("El DNI está repetido!");
            if (map_usuario.ValidarCorreoRepetido_064RB(usuario)) throw new Exception("El correo electrónico está repetido!");
            string passwordHasheado = SER_Cripto_064RB.Encriptar_064RB(usuario.Password_064RB);
            usuario.Password_064RB = passwordHasheado;
            map_usuario.Agregar_064RB(usuario);
            ActualizarDV_064RB(usuario);
            SER_Bitacora_064RB bitacora = new SER_Bitacora_064RB(SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB, DateTime.Now, "Usuarios", "Crear usuario", 2);
            bll_bitacora.RegistrarBitacora_064RB(bitacora);
        }
        public void Borrar_064RB(SER_Usuario_064RB usuario) => throw new NotImplementedException();
        public void Modificar_064RB(SER_Usuario_064RB usuario)
        {
            SER_Usuario_064RB usuAux = map_usuario.ConsultarPorNombreUsuario_064RB(usuario);
            if (usuAux != null && usuAux.Dni_064RB != usuario.Dni_064RB) throw new Exception("Ya existe usuario con este nombre de usuario");
            map_usuario.Modificar_064RB(usuario);
            ActualizarDV_064RB(usuario);

            SER_Bitacora_064RB bitacora = new SER_Bitacora_064RB(SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB, DateTime.Now, "Usuarios", "Modificar usuario", 2);
            bll_bitacora.RegistrarBitacora_064RB(bitacora);
        }
        public void ModificarPassword_064RB(string claveActual, string claveNueva)
        {
            if (claveActual == claveNueva) throw new Exception("No puede usar la misma contraseña");

            SER_Usuario_064RB usuario = SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB;
            if (SER_Cripto_064RB.Encriptar_064RB(claveActual) != usuario.Password_064RB) throw new Exception("Contraseña incorrecta");
            string claveNuevaHasheada = SER_Cripto_064RB.Encriptar_064RB(claveNueva);
            map_usuario.ModificarPassword_064RB(usuario, claveNuevaHasheada);

            usuario.Password_064RB = claveNuevaHasheada;
            ActualizarDV_064RB(usuario);

            SER_Bitacora_064RB bitacora = new SER_Bitacora_064RB(usuario, DateTime.Now, "Usuarios", "Cambiar clave", 1);
            bll_bitacora.RegistrarBitacora_064RB(bitacora);
        }
        public List<SER_Usuario_064RB> Consultar_064RB() => map_usuario.Consultar_064RB();
        public List<object> ConsultarParaGrilla_064RB()
        {
            var consulta = from u in Consultar_064RB()
                           select new
                           {
                               DNI = u.Dni_064RB,
                               Nombre = u.Nombre_064RB,
                               Apellido = u.Apellido_064RB,
                               Usuario = u.NombreUsuario_064RB,
                               Correo = u.Correo_064RB,
                               Activo = u.Activo_064RB,
                               Bloqueado = u.Bloqueo_064RB,
                               Rol = u.Rol_064RB.Nombre_064RB
                           };
            return consulta.ToList<object>();
        }

        public SER_Usuario_064RB ConsultarPorId_064RB(SER_Usuario_064RB usuario)
        {
            return map_usuario.ConsultarPorId_064RB(usuario);
        }
        public List<object> ConsultarFiltrado_064RB(SER_Usuario_064RB us)
        {
            var consulta = from user in Consultar_064RB()
                           where
                           (string.IsNullOrWhiteSpace(us.Dni_064RB) || user.Dni_064RB.Contains(us.Dni_064RB))
                           &&
                           (string.IsNullOrWhiteSpace(us.Nombre_064RB) || user.Nombre_064RB.Contains(us.Nombre_064RB))
                           &&
                           (string.IsNullOrWhiteSpace(us.Apellido_064RB) || user.Apellido_064RB.Contains(us.Apellido_064RB))
                           &&
                           (string.IsNullOrWhiteSpace(us.Correo_064RB) || user.Correo_064RB.Contains(us.Correo_064RB))
                           &&
                           (string.IsNullOrWhiteSpace(us.NombreUsuario_064RB) || user.NombreUsuario_064RB.Contains(us.NombreUsuario_064RB))
                           &&
                           (string.IsNullOrWhiteSpace(us.Rol_064RB.Nombre_064RB) || user.Rol_064RB.Nombre_064RB.Contains(us.Rol_064RB.Nombre_064RB))
                           select new
                           {
                               DNI = user.Dni_064RB,
                               Nombre = user.Nombre_064RB,
                               Apellido = user.Apellido_064RB,
                               Usuario = user.NombreUsuario_064RB,
                               Correo = user.Correo_064RB,
                               Activo = user.Activo_064RB,
                               Bloqueado = user.Bloqueo_064RB,
                               Rol = user.Rol_064RB.Nombre_064RB
                           };

            return consulta.ToList<object>();
        }
        public List<object> ConsultarActivos_064RB() 
        {
            var consulta = from u in Consultar_064RB()
                           where u.Activo_064RB
                           select new
                           {
                               DNI = u.Dni_064RB,
                               Nombre = u.Nombre_064RB,
                               Apellido = u.Apellido_064RB,
                               Usuario = u.NombreUsuario_064RB,
                               Correo = u.Correo_064RB,
                               Activo = u.Activo_064RB,
                               Bloqueado = u.Bloqueo_064RB,
                               Rol = u.Rol_064RB.Nombre_064RB
                           };
            return consulta.ToList<object>();
        }

        public bool Login_064RB(SER_Usuario_064RB usuario, out string mensajeInconsistencia, bool esRelogin = false)
        {
            mensajeInconsistencia = null;
            bool rdo = false;
            SER_Usuario_064RB obj = map_usuario.ConsultarPorNombreUsuario_064RB(usuario);
            if (obj == null) throw new Exception("Datos erróneos!");
            if (EstaBloqueado_064RB(obj)) throw new Exception("La cuenta está bloqueada!");
            if (!EstaActivo_064RB(obj)) throw new Exception("El usuario se encuentra desactivado!");

            if (!CompararPassword_064RB(obj, usuario.Password_064RB))
            {
                map_usuario.SumarCantidadIntento_064RB(obj);
                obj.CantIntentos_064RB += 1;
                ActualizarDV_064RB(obj);
                if (map_usuario.ConsultarPorNombreUsuario_064RB(usuario).CantIntentos_064RB >= 3)
                {
                    map_usuario.Bloquear_064RB(obj);
                    obj.Bloqueo_064RB = true;
                    ActualizarDV_064RB(obj);
                    throw new Exception("Se bloqueó el usuario por motivos de seguridad!");
                }
                throw new Exception("No se pudo iniciar sesión");
            }
            bool integro = true;

            try
            {
                bll_dv.VerificarIntegridad_064RB();
            }
            catch (Exception ex)
            {
                integro = false;
                mensajeInconsistencia = ex.Message;
            }
            bool esAdmin = obj.Rol_064RB.Id_064RB == 1;
            if (!integro && !esAdmin) throw new Exception("El sistema no se encuentra disponible. Contacte con un administrador si el problema persiste.");

            if (!esRelogin)
            {
                try
                {
                    bll_idioma.CambiarIdioma_064RB(obj.IdIdioma_064RB);
                }
                catch
                {
                    try { bll_idioma.CambiarIdioma_064RB("es-AR"); } catch { }
                }
            }
            rdo = true;
            SER_SessionManager_064RB sesion = SER_SessionManager_064RB.ObtenerSesion_064RB();
            map_usuario.ReiniciarIntentos_064RB(obj);
            obj.CantIntentos_064RB = 0;
            if (integro)
            {
                bll_dv.GenerarDVH_064RB(obj, obj.Dni_064RB, "usuario");
                bll_dv.GenerarDVV_064RB("usuario");
            }
            obj.Rol_064RB = map_rol.ObtenerArbol_064RB(obj.Rol_064RB.Id_064RB);
            sesion.Usuario_064RB = obj;
            string evento = esRelogin ? "Relogin" : "Login";
            SER_Bitacora_064RB bitacora = new SER_Bitacora_064RB(obj, DateTime.Now, "Usuarios", evento, 1);
            bll_bitacora.RegistrarBitacora_064RB(bitacora);
            return rdo;
        }

        public void Logout_064RB()
        {
            SER_Usuario_064RB usuario = SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB;
            if (usuario != null)
            {
                usuario.IdIdioma_064RB = bll_idioma.IdiomaActual;
                map_usuario.ModificarIdioma_064RB(usuario);
                ActualizarDV_064RB(usuario);
                bll_bitacora.RegistrarBitacora_064RB(new SER_Bitacora_064RB(usuario, DateTime.Now, "Usuarios", "Logout", 2));
                bll_bitacora.RegistrarBitacora_064RB(new SER_Bitacora_064RB(usuario, DateTime.Now, "Usuarios", "Cambiar Idioma", 3));
                SER_SessionManager_064RB.CerrarSesion_064RB();
            }
            bll_idioma.CambiarIdioma_064RB("es-AR");
        }

        private bool CompararPassword_064RB(SER_Usuario_064RB usuario, string password)
        {
            bool rdo = false;
            string passwordHasheada = SER_Cripto_064RB.Encriptar_064RB(password);
            if (passwordHasheada == usuario.Password_064RB) rdo = true;
            return rdo;
        }
        private bool EstaBloqueado_064RB(SER_Usuario_064RB usuario)
        {
            bool rdo = false;
            if (usuario.Bloqueo_064RB) rdo = true;
            return rdo;
        }
        private bool EstaActivo_064RB(SER_Usuario_064RB usuario)
        {
            bool rdo = false;
            if (usuario.Activo_064RB) rdo = true;
            return rdo;
        }
        public void Desbloquear_064RB(SER_Usuario_064RB usuario)
        {
            SER_Usuario_064RB usuarioAux = ConsultarPorId_064RB(usuario);
            if (!EstaBloqueado_064RB(usuarioAux)) throw new Exception("El usuario no está bloqueado!");

            map_usuario.Desbloquear_064RB(usuarioAux);
            usuarioAux.Bloqueo_064RB = false;

            string nuevaPasswordHasheada = SER_Cripto_064RB.Encriptar_064RB(usuarioAux.Dni_064RB + usuarioAux.Apellido_064RB);
            map_usuario.ModificarPassword_064RB(usuarioAux, nuevaPasswordHasheada);
            usuarioAux.Password_064RB = nuevaPasswordHasheada;

            map_usuario.ReiniciarIntentos_064RB(usuarioAux);
            usuarioAux.CantIntentos_064RB = 0;

            ActualizarDV_064RB(usuarioAux);

            SER_Bitacora_064RB bitacora = new SER_Bitacora_064RB(SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB, DateTime.Now, "Usuarios", "Desbloquear usuario", 2);
            bll_bitacora.RegistrarBitacora_064RB(bitacora);
        }
        public void CambiarEstadoActivo_064RB(SER_Usuario_064RB usuario)
        {
            SER_Usuario_064RB usuarioAux = map_usuario.ConsultarPorId_064RB(usuario);
            if (usuarioAux != null)
            {
                usuarioAux.Activo_064RB = !usuarioAux.Activo_064RB;
                map_usuario.CambiarEstadoActivo_064RB(usuarioAux);
                ActualizarDV_064RB(usuarioAux);
                if (usuarioAux.Activo_064RB)
                {
                    SER_Bitacora_064RB bitacora = new SER_Bitacora_064RB(SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB, DateTime.Now, "Usuarios", "Activar usuario", 3);
                    
                    bll_bitacora.RegistrarBitacora_064RB(bitacora);
                }
                else
                {
                    SER_Bitacora_064RB bitacora = new SER_Bitacora_064RB(SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB, DateTime.Now, "Usuarios", "Desactivar usuario", 3);
                    bll_bitacora.RegistrarBitacora_064RB(bitacora);
                }
            }
        }
        private void ActualizarDV_064RB(SER_Usuario_064RB usuario)
        {
            bll_dv.GenerarDVH_064RB(usuario, usuario.Dni_064RB, "Usuario_064RB");
            bll_dv.GenerarDVV_064RB("Usuario_064RB");
        }
    }
}
