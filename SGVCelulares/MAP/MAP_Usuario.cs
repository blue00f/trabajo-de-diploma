using DAL;
using Microsoft.Data.SqlClient;
using Servicio;

namespace MAP
{
    public class MAP_Usuario : IABMC<SER_Usuario>
    {
        DAO_Usuario dao_usuario;
        MAP_Rol map_rol;
        public MAP_Usuario()
        {
            dao_usuario = new DAO_Usuario();
            map_rol = new MAP_Rol();
        }

        public void Agregar(SER_Usuario usuario)
        {
            dao_usuario.Agregar(usuario.Dni, usuario.Nombre, usuario.Apellido, usuario.Correo, usuario.NombreUsuario, usuario.Password, usuario.Bloqueo, usuario.Activo, usuario.CantIntentos, usuario.Rol.Id, usuario.IdIdioma);
        }

        public void Borrar(SER_Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public void Modificar(SER_Usuario usuario)
        {
            dao_usuario.Modificar(usuario.Dni, usuario.Nombre, usuario.Apellido, usuario.NombreUsuario);
        }
        public void ModificarPassword(SER_Usuario usuario, string claveNueva)
        {
            dao_usuario.ModificarPassword(usuario.Dni, claveNueva);
        }
        public List<SER_Usuario> Consultar()
        {
            List<SER_Usuario> lista = new List<SER_Usuario>();
            SqlDataReader dr = dao_usuario.Consultar();

            while (dr.Read())
            {
                object[] datos = new object[dr.FieldCount];
                dr.GetValues(datos);
                lista.Add(
                        new SER_Usuario(
                                Convert.ToString(datos[0]),
                                Convert.ToString(datos[1]),
                                Convert.ToString(datos[2]),
                                Convert.ToString(datos[3]),
                                Convert.ToString(datos[4]),
                                Convert.ToString(datos[5]),
                                Convert.ToBoolean(datos[6]),
                                Convert.ToBoolean(datos[7]),
                                Convert.ToInt32(datos[8]),
                                map_rol.ObtenerRolPorId(Convert.ToInt16(datos[9])),
                                Convert.ToString(datos[10])
                            )
                    );
            }
            dr.Close();
            return lista;
        }
        public SER_Usuario? ConsultarPorId(SER_Usuario usuario)
        {
            SER_Usuario usuarioAux = null;
            SqlDataReader dr = dao_usuario.ConsultarPorId(usuario.Dni);
            if (dr.Read())
            {
                object[] datos = new object[dr.FieldCount];
                dr.GetValues(datos);
                usuarioAux = new SER_Usuario(
                               Convert.ToString(datos[0]),
                               Convert.ToString(datos[1]),
                               Convert.ToString(datos[2]),
                               Convert.ToString(datos[3]),
                               Convert.ToString(datos[4]),
                               Convert.ToString(datos[5]),
                               Convert.ToBoolean(datos[6]),
                               Convert.ToBoolean(datos[7]),
                               Convert.ToInt16(datos[8]),
                               map_rol.ObtenerRolPorId(Convert.ToInt16(datos[9])),
                               Convert.ToString(datos[10])
                           );
            }
            dr.Close();
            return usuarioAux;
        }
        public SER_Usuario? ConsultarPorNombreUsuario(SER_Usuario usuario)
        {
            SER_Usuario usuarioAux = null;
            SqlDataReader dr = dao_usuario.ConsultarPorNombreUsuario(usuario.NombreUsuario);
            if (dr.Read())
            {
                object[] datos = new object[dr.FieldCount];
                dr.GetValues(datos);
                usuarioAux = new SER_Usuario(
                                Convert.ToString(datos[0]),
                                Convert.ToString(datos[1]),
                                Convert.ToString(datos[2]),
                                Convert.ToString(datos[3]),
                                Convert.ToString(datos[4]),
                                Convert.ToString(datos[5]),
                                Convert.ToBoolean(datos[6]),
                                Convert.ToBoolean(datos[7]),
                                Convert.ToInt16(datos[8]),
                                map_rol.ObtenerRolPorId(Convert.ToInt16(datos[9])),
                                Convert.ToString(datos[10])
                            );
            }
            dr.Close();
            return usuarioAux;
        }
      
        public bool ValidarDniRepetido(SER_Usuario usuario) => dao_usuario.ValidarRepetido(usuario.Dni);
        public bool ExisteNombreUsuario(SER_Usuario usuario) => dao_usuario.ExisteNombreUsuario(usuario.NombreUsuario);
        public bool ValidarCorreoRepetido(SER_Usuario usuario) => dao_usuario.ValidarCorreoRepetido(usuario.Correo);
        public void SumarCantidadIntento(SER_Usuario usuario) => dao_usuario.SumarCantidadIntento(usuario.Dni);
        public void Bloquear(SER_Usuario usuario) => dao_usuario.Bloquear(usuario.Dni);
        public void ReiniciarIntentos(SER_Usuario usuario) => dao_usuario.ReiniciarIntentos(usuario.Dni);
        public void Desbloquear(SER_Usuario usuario) => dao_usuario.Desbloquear(usuario.Dni);
        public void CambiarEstadoActivo(SER_Usuario usuario) => dao_usuario.CambiarEstadoActivo(usuario.Dni, usuario.Activo);
        public void ModificarIdioma(SER_Usuario usuario)
        {
            dao_usuario.ModificarIdioma(usuario.Dni, usuario.IdIdioma);
        }
    }
}
