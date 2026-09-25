using MAP_064RB.Tecnico_064RB;
using Servicio_064RB;

namespace BLL_064RB.Tecnico_064RB
{
    public class BLL_Backup_064RB : IGestorBackup_064RB
    {
        MAP_Backup_064RB map_backup;
        BLL_Bitacora_064RB bll_bitacora;
        public BLL_Backup_064RB() { map_backup = new(); bll_bitacora = new(); }
        public void Backup_064RB(string ruta)
        {
            map_backup.Backup_064RB(ruta);
            SER_Bitacora_064RB bitacora = new SER_Bitacora_064RB(SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB, DateTime.Now, "Respaldo", "Generar backup", 1);
            bll_bitacora.RegistrarBitacora_064RB(bitacora);
        }
        public void Restore_064RB(string ruta)
        {
            map_backup.Restore_064RB(ruta);
            SER_Bitacora_064RB bitacora = new SER_Bitacora_064RB(SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB, DateTime.Now, "Respaldo", "Generar restore", 1);
            bll_bitacora.RegistrarBitacora_064RB(bitacora);
        }
    }
}
