using System.Data;

namespace DAL_064RB.Tecnico_064RB
{
    public class DAO_Permiso_064RB
    {
        ComandoBD_064RB com;
        public DAO_Permiso_064RB() => com = new();
        public DataTable Consultar_064RB() => com.Consultar_064RB("SELECT * FROM Permiso_064RB");
    }
}
