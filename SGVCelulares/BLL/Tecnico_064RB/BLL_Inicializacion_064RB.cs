using DAL_064RB;
using MAP_064RB.Tecnico_064RB;

namespace BLL_064RB.Tecnico_064RB
{
    public class BLL_Inicializacion_064RB
    {
        private readonly MAP_Inicializacion_064RB map;
        public BLL_Inicializacion_064RB() => map = new MAP_Inicializacion_064RB();
        public void InicializarBaseDatos_064RB()
        {
            string rutaBak = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BD", "bd_santsung_064RB.bak");
            map.RestaurarBaseDatosSiNoExiste_064RB(rutaBak);
        }
    }
}
