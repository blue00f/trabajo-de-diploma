using System.Data;

namespace DAL_064RB.Tecnico_064RB
{
    public class DAO_Backup_064RB
    {
        ComandoBD_064RB com;
        public DAO_Backup_064RB() => com = new("master");
        public void Backup_064RB(string ruta)
        {
            var parametros = new List<ParametroBD_064RB> { new("@ruta", SqlDbType.VarChar, ruta) };
            com.Ejecutar_064RB("BACKUP DATABASE bd_santsung_064RB TO DISK = @ruta WITH INIT", parametros);
        }

        public void Restore_064RB(string ruta)
        {
            var parametros = new List<ParametroBD_064RB> { new("@ruta", SqlDbType.VarChar, ruta) };
            com.Ejecutar_064RB("ALTER DATABASE bd_santsung_064RB SET SINGLE_USER WITH ROLLBACK IMMEDIATE; RESTORE DATABASE bd_santsung_064RB FROM DISK = @ruta WITH REPLACE; ALTER DATABASE bd_santsung_064RB SET MULTI_USER", parametros);
        }
    }
}
