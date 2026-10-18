using BE_064RB;
using BLL_064RB.Tecnico_064RB;
using MAP_064RB.Negocio_064RB;
using Servicio_064RB;

namespace BLL_064RB.Negocio_064RB
{
    public class BLL_Cliente_064RB
    {
        MAP_Cliente_064RB map_cliente;
        BLL_Bitacora_064RB bll_bitacora;
        BLL_DV_064RB bll_dv;
        public BLL_Cliente_064RB()
        {
            map_cliente = new();
            bll_bitacora = new();
            bll_dv = new();
        }
        public void Agregar_064RB(BE_Cliente_064RB cliente)
        {
            if (Consultar().Any(c => c.Dni_064RB == cliente.Dni_064RB)) throw new Exception("El cliente está repetido!");
            map_cliente.Agregar_064RB(cliente);

            SER_Usuario_064RB usu = SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB;
            bll_bitacora.RegistrarBitacora_064RB(new SER_Bitacora_064RB(usu, DateTime.Now, "Clientes", "Alta", 3));
            bll_dv.GenerarDVH_064RB(cliente, cliente.Dni_064RB, "Cliente_064RB");
            bll_dv.GenerarDVV_064RB("Cliente_064RB");
        }
        public void Borrar_064RB(BE_Cliente_064RB cliente)
        {
            map_cliente.Borrar_064RB(cliente);
            SER_Usuario_064RB usu = SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB;
            bll_bitacora.RegistrarBitacora_064RB(new SER_Bitacora_064RB(usu, DateTime.Now, "Clientes", "Baja", 3));
            bll_dv.BorrarDVH_064RB("Cliente_064RB", cliente.Dni_064RB);
            bll_dv.GenerarDVV_064RB("Cliente_064RB");
        }
        public void Modificar_064RB(BE_Cliente_064RB cliente)
        {
            map_cliente.Modificar_064RB(cliente);
            SER_Usuario_064RB usu = SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB;
            bll_bitacora.RegistrarBitacora_064RB(new SER_Bitacora_064RB(usu, DateTime.Now, "Clientes", "Modificación", 3));
            bll_dv.GenerarDVH_064RB(cliente, cliente.Dni_064RB, "Cliente_064RB");
            bll_dv.GenerarDVV_064RB("Cliente_064RB");
        }
        public List<BE_Cliente_064RB> Consultar() => map_cliente.Consultar();
        public BE_Cliente_064RB ConsultarPorDni_064RB(string dni) => map_cliente.ConsultarPorDni_064RB(dni);
    }
}
