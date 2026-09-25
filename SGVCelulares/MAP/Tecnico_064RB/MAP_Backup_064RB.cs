using DAL_064RB.Tecnico_064RB;
using Servicio_064RB;

namespace MAP_064RB.Tecnico_064RB
{
    public class MAP_Backup_064RB : IGestorBackup_064RB
    {
        DAO_Backup_064RB dao_backup;
        public MAP_Backup_064RB() => dao_backup = new();
        public void Backup_064RB(string ruta) => dao_backup.Backup_064RB(ruta);
        public void Restore_064RB(string ruta) => dao_backup.Restore_064RB(ruta);
    }
}
