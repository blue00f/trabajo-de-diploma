using DAL_064RB;

namespace MAP_064RB.Tecnico_064RB
{
    public class MAP_Inicializacion_064RB
    {
        private readonly ComandoBD_064RB com;
        public MAP_Inicializacion_064RB() => com = new ComandoBD_064RB("master");
        public void RestaurarBaseDatosSiNoExiste_064RB(string rutaBak) => com.RestaurarBaseDatosSiNoExiste_064RB(rutaBak);
    }
}
