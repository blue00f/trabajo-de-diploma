using BE_064RB;
using BLL_064RB.Tecnico_064RB;
using MAP_064RB.Negocio_064RB;
using Servicio_064RB;

namespace BLL_064RB.Negocio_064RB
{
    public class BLL_UnidadCelular_064RB
    {
        MAP_UnidadCelular_064RB map_unidadCelular;
        BLL_Bitacora_064RB bll_bitacora;
        BLL_DV_064RB bll_dv;
        public BLL_UnidadCelular_064RB()
        {
            map_unidadCelular = new();
            bll_bitacora = new();
            bll_dv = new();
        }
        public void Agregar_064RB(BE_UnidadCelular_064RB unidadC)
        {
            map_unidadCelular.Agregar_064RB(unidadC);
            SER_Usuario_064RB usu = SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB;
            bll_bitacora.RegistrarBitacora_064RB(new SER_Bitacora_064RB(usu, DateTime.Now, "Unidad Celulares", "Alta", 2));
            bll_dv.GenerarDVH_064RB(unidadC, unidadC.Imei_064RB, "UnidadCelular_064RB");
            bll_dv.GenerarDVV_064RB("UnidadCelular_064RB");
        }
        public void Borrar_064RB(BE_UnidadCelular_064RB unidadC)
        {
            map_unidadCelular.Borrar_064RB(unidadC);
            SER_Usuario_064RB usu = SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB;
            bll_bitacora.RegistrarBitacora_064RB(new SER_Bitacora_064RB(usu, DateTime.Now, "Unidad Celulares", "Alta", 2));
        }
        public void Modificar_064RB(BE_UnidadCelular_064RB unidadC)
        {
            map_unidadCelular.Modificar_064RB(unidadC);
            SER_Usuario_064RB usu = SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB;
            bll_bitacora.RegistrarBitacora_064RB(new SER_Bitacora_064RB(usu, DateTime.Now, "Unidad Celulares", "Alta", 2));
        }
        public List<BE_UnidadCelular_064RB> Consultar()
        {
            return map_unidadCelular.Consultar_064RB();
        }
        public List<object> ConsultarGrilla()
        {
            var c = from u in Consultar()
                    select new
                    {
                        IMEI = u.Imei_064RB,
                        Estado = u.Estado_064RB,
                        SKU = u.Celular_064RB.Sku_064RB,
                        Marca = u.Celular_064RB.Marca_064RB,
                        Modelo = u.Celular_064RB.Modelo_064RB,
                        Precio = u.Celular_064RB.Precio_064RB
                    };
            return c.ToList<object>();
        }
    }
}
