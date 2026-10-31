using BE_064RB;
using BLL_064RB.Tecnico_064RB;
using MAP_064RB.Negocio_064RB;
using Servicio_064RB;

namespace BLL_064RB.Negocio_064RB
{
    public class BLL_RemitoCompra_064RB
    {
        MAP_RemitoCompra_064RB map_remito;
        BLL_SolicitudCompra_064RB bll_solicitud;
        BLL_Discrepancia_064RB bll_discrepancia;
        BLL_Bitacora_064RB bll_bitacora;
        BLL_DV_064RB bll_dv;
        public BLL_RemitoCompra_064RB()
        {
            map_remito = new();
            bll_solicitud = new();
            bll_discrepancia = new();
            bll_bitacora = new();
            bll_dv = new();
        }

        public void ValidarImeiIngresado_064RB(string imei, List<string> yaIngresados, int cantidadConforme)
        {
            ValidarFormatoImei_064RB(imei);
            foreach (string ingresado in yaIngresados)
            {
                if (ingresado == imei) throw new Exception("El IMEI " + imei + " ya fue ingresado.");
            }
            if (yaIngresados.Count >= cantidadConforme) throw new Exception("Ya se ingresaron los IMEI de todas las unidades recibidas en condiciones conformes.");
        }

        public void Registrar_064RB(BE_RemitoCompra_064RB remito, string numRemitoTexto, BE_Discrepancia_064RB? discrepancia, List<string> imeis)
        {
            const string yaRecibida = "La solicitud seleccionada ya no está en estado pendiente de recepción.";

            BE_SolicitudCompra_064RB solicitud = remito.OrdenCompra_064RB.SolicitudCompra_064RB;
            bool esCelular = solicitud.Producto_064RB is BE_Celular_064RB;
            int numRemito;
            bool esNumero = int.TryParse(numRemitoTexto.Trim(), out numRemito);
            if (!esNumero || numRemito <= 0) throw new Exception("Se debe ingresar el número de remito (solo números).");

            int cantidadConforme = bll_discrepancia.CalcularCantidadConforme_064RB(remito, discrepancia);

            if (esCelular)
            {
                if (imeis.Count != cantidadConforme) throw new Exception("La cantidad de IMEI ingresados (" + imeis.Count + ") no coincide con la cantidad recibida en condiciones conformes (" + cantidadConforme + ").");
                ValidarImeis_064RB(imeis);
            }
            BE_SolicitudCompra_064RB? actual = bll_solicitud.ObtenerPorId_064RB(solicitud.IdSolicitud_064RB);
            if (actual == null || actual.Estado_064RB != EstadoSolicitud_064RB.PendienteDeRecepcion_064RB) throw new Exception(yaRecibida);

            remito.NumRemito_064RB = numRemito;
            remito.FechaRecepcion_064RB = DateTime.Now;
            if (discrepancia == null)
            {
                remito.Estado_064RB = EstadoRemito_064RB.Conforme_064RB;
            }
            else
            {
                remito.Estado_064RB = EstadoRemito_064RB.NoConforme_064RB;
                discrepancia.Remito_064RB = remito;
            }
            int idRemito = map_remito.Agregar_064RB(remito, discrepancia, imeis, cantidadConforme);
            if (idRemito == 0) throw new Exception(yaRecibida);
            SER_Usuario_064RB usu = SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB;
            bll_bitacora.RegistrarBitacora_064RB(new SER_Bitacora_064RB(usu, DateTime.Now, "Remitos de compra", "Alta", 3));
            bll_dv.ActualizarDV_064RB("SolicitudCompra_064RB", "RemitoCompra_064RB", "Discrepancia_064RB", "UnidadCelular_064RB", "Accesorio_064RB");
        }

        private void ValidarImeis_064RB(List<string> imeis)
        {
            for (int i = 0; i < imeis.Count; i++)
            {
                ValidarFormatoImei_064RB(imeis[i]);
                for (int j = i + 1; j < imeis.Count; j++)
                {
                    if (imeis[i] == imeis[j]) throw new Exception("El IMEI " + imeis[i] + " está repetido.");
                }
                if (map_remito.ExisteImei_064RB(imeis[i])) throw new Exception("El IMEI " + imeis[i] + " ya está registrado.");
            }
        }

        private void ValidarFormatoImei_064RB(string imei)
        {
            bool valido = imei.Length == 15;
            foreach (char c in imei)
            {
                if (c < '0' || c > '9') valido = false;
            }
            if (!valido) throw new Exception("El IMEI " + imei + " no es válido: debe tener 15 dígitos.");
        }
    }
}
