using BE_064RB;

namespace BLL_064RB.Negocio_064RB
{
    public class BLL_Discrepancia_064RB
    {
        public BE_Discrepancia_064RB Registrar_064RB(BE_RemitoCompra_064RB remito, string faltanteTexto, string danadaTexto, string observaciones)
        {
            int cantidadPedida = remito.OrdenCompra_064RB.SolicitudCompra_064RB.Cantidad_064RB;
            int faltante = ConvertirCantidad_064RB(faltanteTexto);
            int danada = ConvertirCantidad_064RB(danadaTexto);
            if (faltante == 0 && danada == 0) throw new Exception("Se debe ingresar al menos una cantidad faltante o dañada!!");
            if (string.IsNullOrWhiteSpace(observaciones)) throw new Exception("Las observaciones son obligatorias!");
            if (faltante + danada > cantidadPedida) throw new Exception("La suma de las cantidades faltante y dañada supera la cantidad pedida (" + cantidadPedida + ").");

            BE_Discrepancia_064RB discrepancia = new BE_Discrepancia_064RB();
            discrepancia.CantidadFaltante_064RB = faltante;
            discrepancia.CantidadDanada_064RB = danada;
            discrepancia.Observaciones_064RB = observaciones.Trim();
            discrepancia.Remito_064RB = remito;
            return discrepancia;
        }
        public int CalcularCantidadConforme_064RB(BE_RemitoCompra_064RB remito, BE_Discrepancia_064RB? discrepancia)
        {
            int cantidadPedida = remito.OrdenCompra_064RB.SolicitudCompra_064RB.Cantidad_064RB;
            if (discrepancia == null) return cantidadPedida;
            return cantidadPedida - discrepancia.CantidadFaltante_064RB - discrepancia.CantidadDanada_064RB;
        }

        private int ConvertirCantidad_064RB(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto)) return 0;
            int cantidad;
            bool esNumero = int.TryParse(texto.Trim(), out cantidad);
            if (!esNumero || cantidad < 0) throw new Exception("Las cantidades ingresadas deben ser números enteros mayores o iguales a cero.");
            return cantidad;
        }
    }
}
