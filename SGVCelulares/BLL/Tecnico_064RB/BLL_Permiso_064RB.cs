using MAP_064RB.Tecnico_064RB;
using Servicio_064RB;

namespace BLL_064RB.Tecnico_064RB
{
    public class BLL_Permiso_064RB
    {
        MAP_Permiso_064RB map_permiso_064RB;
        public BLL_Permiso_064RB() => map_permiso_064RB = new();
        public List<SER_Permiso_064RB> Consultar_064RB() => map_permiso_064RB.Consultar_064RB();
    }
}
