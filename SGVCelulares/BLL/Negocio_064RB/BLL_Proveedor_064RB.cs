using BE_064RB;
using BLL_064RB.Tecnico_064RB;
using MAP_064RB.Negocio_064RB;
using Servicio_064RB;

namespace BLL_064RB.Negocio_064RB
{
    public class BLL_Proveedor_064RB
    {
        MAP_Proveedor_064RB map_proveedor;
        BLL_DV_064RB bll_dv;
        BLL_Bitacora_064RB bll_bitacora;
        public BLL_Proveedor_064RB()
        {
            map_proveedor = new();
            bll_bitacora = new();
            bll_dv = new();
        }
        public void Agregar_064RB(BE_Proveedor_064RB proveedor)
        { 
            if (map_proveedor.ConsultarTodos_064RB().Any(p => p.Cuit_064RB == proveedor.Cuit_064RB)) throw new Exception("El proveedor está repetido!");
            map_proveedor.Agregar_064RB(proveedor);
            bll_bitacora.RegistrarBitacora_064RB(new SER_Bitacora_064RB(SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB, DateTime.Now, "ABM proveedores", "Crear proveedor", (int)Criticidad_064RB.Media));
            bll_dv.ActualizarDV_064RB("Proveedor_064RB");
        }
        public void Borrar_064RB(BE_Proveedor_064RB proveedor)
        {
            map_proveedor.Borrar_064RB(proveedor);
            bll_bitacora.RegistrarBitacora_064RB(new SER_Bitacora_064RB(SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB, DateTime.Now, "ABM proveedores", "Borrar proveedor", (int)Criticidad_064RB.Media));
            bll_dv.ActualizarDV_064RB("Proveedor_064RB");
        }
        public void Modificar_064RB(BE_Proveedor_064RB proveedor)
        {
            map_proveedor.Modificar_064RB(proveedor);
            bll_bitacora.RegistrarBitacora_064RB(new SER_Bitacora_064RB(SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB, DateTime.Now, "ABM proveedores", "Modificar proveedor", (int)Criticidad_064RB.Media));
            bll_dv.ActualizarDV_064RB("Proveedor_064RB");
        }
        public List<BE_Proveedor_064RB> Consultar_064RB() => map_proveedor.Consultar_064RB();
        public List<object> ConsultarGrilla_064RB()
        {
            var consulta = from s in Consultar_064RB()
                           select new
                           {
                               Cuit = s.Cuit_064RB,
                               RazonSocial = s.RazonSocial_064RB
                           };
            return consulta.ToList<object>();
        }
    }
}
