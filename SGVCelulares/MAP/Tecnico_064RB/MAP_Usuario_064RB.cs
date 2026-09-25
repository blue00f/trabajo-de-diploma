using DAL_064RB;
using Servicio_064RB;
using System.Data;

namespace MAP_064RB.Tecnico_064RB
{
    public class MAP_Usuario_064RB : IABMC_064RB<SER_Usuario_064RB>, IProtegidoPorDV_064RB
    {
        DAO_Usuario_064RB dao_usuario;
        MAP_Rol_064RB map_rol;
        public MAP_Usuario_064RB()
        {
            dao_usuario = new DAO_Usuario_064RB();
            map_rol = new MAP_Rol_064RB();
        }
        public string NombreTabla_064RB => "Usuario_064RB";
        public List<object> ObtenerRegistrosParaDV_064RB() => Consultar_064RB().Cast<object>().ToList();
        public string ObtenerIdParaDV_064RB(object registro) => ((SER_Usuario_064RB)registro).Dni_064RB;

        public void Agregar_064RB(SER_Usuario_064RB usuario) => dao_usuario.Agregar_064RB(usuario.Dni_064RB, usuario.Nombre_064RB, usuario.Apellido_064RB, usuario.Correo_064RB, usuario.NombreUsuario_064RB, usuario.Password_064RB, usuario.Bloqueo_064RB, usuario.Activo_064RB, usuario.CantIntentos_064RB, usuario.Rol_064RB.Id_064RB, usuario.IdIdioma_064RB);
        public void Borrar_064RB(SER_Usuario_064RB usuario) => throw new NotImplementedException();
        public void Modificar_064RB(SER_Usuario_064RB usuario) => dao_usuario.Modificar_064RB(usuario.Dni_064RB, usuario.Nombre_064RB, usuario.Apellido_064RB, usuario.NombreUsuario_064RB);
        public List<SER_Usuario_064RB> Consultar_064RB()
        {
            DataTable tabla = dao_usuario.Consultar_064RB();
            var lista = new List<SER_Usuario_064RB>();
            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(Mapear_064RB(fila));
            }
            return lista;
        }
        public SER_Usuario_064RB? ConsultarPorId_064RB(SER_Usuario_064RB usuario)
        {
            DataTable tabla = dao_usuario.ConsultarPorId_064RB(usuario.Dni_064RB);
            return tabla.Rows.Count > 0 ? Mapear_064RB(tabla.Rows[0]) : null;
        }
        public SER_Usuario_064RB? ConsultarPorNombreUsuario_064RB(SER_Usuario_064RB usuario)
        {
            DataTable tabla = dao_usuario.ConsultarPorNombreUsuario_064RB(usuario.NombreUsuario_064RB);
            return tabla.Rows.Count > 0 ? Mapear_064RB(tabla.Rows[0]) : null;
        }
        public bool ValidarDniRepetido_064RB(SER_Usuario_064RB usuario) => dao_usuario.ValidarRepetido_064RB(usuario.Dni_064RB);
        public bool ValidarCorreoRepetido_064RB(SER_Usuario_064RB usuario) => dao_usuario.ValidarCorreoRepetido_064RB(usuario.Correo_064RB);
        public bool ExisteNombreUsuario_064RB(SER_Usuario_064RB usuario) => dao_usuario.ExisteNombreUsuario_064RB(usuario.NombreUsuario_064RB);
        public void SumarCantidadIntento_064RB(SER_Usuario_064RB usuario) => dao_usuario.SumarCantidadIntento_064RB(usuario.Dni_064RB);
        public void ReiniciarIntentos_064RB(SER_Usuario_064RB usuario) => dao_usuario.ReiniciarIntentos_064RB(usuario.Dni_064RB);
        public void Bloquear_064RB(SER_Usuario_064RB usuario) => dao_usuario.Bloquear_064RB(usuario.Dni_064RB);
        public void Desbloquear_064RB(SER_Usuario_064RB usuario) => dao_usuario.Desbloquear_064RB(usuario.Dni_064RB);
        public void CambiarEstadoActivo_064RB(SER_Usuario_064RB usuario) => dao_usuario.CambiarEstadoActivo_064RB(usuario.Dni_064RB, usuario.Activo_064RB);
        public void ModificarIdioma_064RB(SER_Usuario_064RB usuario) => dao_usuario.ModificarIdioma_064RB(usuario.Dni_064RB, usuario.IdIdioma_064RB);
        public void ModificarPassword_064RB(SER_Usuario_064RB usuario, string claveNueva) => dao_usuario.ModificarPassword_064RB(usuario.Dni_064RB, claveNueva);
        private SER_Usuario_064RB Mapear_064RB(DataRow fila)
        {
            return new SER_Usuario_064RB(
                Convert.ToString(fila[0]),
                Convert.ToString(fila[1]),
                Convert.ToString(fila[2]),
                Convert.ToString(fila[3]),
                Convert.ToString(fila[4]),
                Convert.ToString(fila[5]),
                Convert.ToBoolean(fila[6]),
                Convert.ToBoolean(fila[7]),
                Convert.ToInt16(fila[8]),
                map_rol.ObtenerRolPorId_064RB(Convert.ToInt16(fila[9])),
                Convert.ToString(fila[10])
            );
        }
    }
}
