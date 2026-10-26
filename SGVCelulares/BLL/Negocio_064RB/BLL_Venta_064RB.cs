using BE_064RB;
using BLL_064RB.Tecnico_064RB;
using MAP_064RB.Negocio_064RB;
using Servicio_064RB;

namespace BLL_064RB.Negocio_064RB
{
    public class BLL_Venta_064RB
    {
        public static readonly string[] FormasPago = { "Efectivo", "Débito", "Crédito", "Transferencia" };
        MAP_Venta_064RB map_venta;
        BLL_DetalleVenta_064RB bll_detalle;
        BLL_Cliente_064RB bll_cliente;
        BLL_Bitacora_064RB bll_bitacora;
        BLL_DV_064RB bll_dv;
        public BLL_Venta_064RB()
        {
            map_venta = new();
            bll_detalle = new();
            bll_cliente = new();
            bll_bitacora = new();
            bll_dv = new();
        }
        public BE_Venta_064RB Iniciar_064RB() => new() { Detalle_064RB = new List<BE_DetalleVenta_064RB>() };
        private static void ValidarEditable_064RB(BE_Venta_064RB venta)
        {
            if (!string.IsNullOrWhiteSpace(venta.FormaPago_064RB)) throw new Exception("El pago ya fue aprobado. Cancele la venta para modificar el detalle!");
        }
        public void AgregarProducto_064RB(BE_Venta_064RB venta, BE_Producto_064RB producto, int cantidad)
        {
            ValidarEditable_064RB(venta);
            bll_detalle.Agregar_064RB(venta.Detalle_064RB, producto, cantidad);
            venta.MontoTotal_064RB = venta.Detalle_064RB.Sum(d => d.Subtotal_064RB);
        }
        public void QuitarProducto_064RB(BE_Venta_064RB venta, string sku)
        {
            ValidarEditable_064RB(venta);
            bll_detalle.Quitar_064RB(venta.Detalle_064RB, sku);
            venta.MontoTotal_064RB = venta.Detalle_064RB.Sum(d => d.Subtotal_064RB);
        }
        public BE_Cliente_064RB AsociarCliente_064RB(BE_Venta_064RB venta, string dni)
        {
            venta.Cliente_064RB = null;
            var cliente = bll_cliente.ConsultarPorDni_064RB(dni) ?? throw new Exception("No existe un cliente registrado con el DNI ingresado!");
            venta.Cliente_064RB = cliente;
            return cliente;
        }
        private static bool EsElectronico_064RB(string formaPago) => formaPago != "Efectivo";
        public bool ProcesarPago_064RB(BE_Venta_064RB venta, string? formaPago, Func<decimal, bool> autorizarPagoElectronico)
        {
            if (venta.Detalle_064RB == null || venta.Detalle_064RB.Count == 0) throw new Exception("La venta no tiene productos en el detalle!");
            if (venta.Cliente_064RB == null) throw new Exception("Debe asociar un cliente a la venta!");
            if (string.IsNullOrEmpty(formaPago) || !FormasPago.Contains(formaPago)) throw new Exception("No hay medio de pago!");
            venta.MontoTotal_064RB = venta.Detalle_064RB.Sum(d => d.Subtotal_064RB);
            bool aprobado = !EsElectronico_064RB(formaPago) || autorizarPagoElectronico(venta.MontoTotal_064RB);
            venta.FormaPago_064RB = aprobado ? formaPago : null;
            return aprobado;
        }
        public int RegistrarVenta_064RB(BE_Venta_064RB venta)
        {
            if (venta.NumVenta_064RB != 0) throw new Exception("La venta ya fue registrada.");
            if (venta.Detalle_064RB == null || venta.Detalle_064RB.Count == 0) throw new Exception("La venta no tiene productos en el detalle.");
            if (venta.Cliente_064RB == null) throw new Exception("Debe asociar un cliente a la venta.");
            if (string.IsNullOrWhiteSpace(venta.FormaPago_064RB)) throw new Exception("El pago de la venta no fue aprobado.");

            venta.Fecha_064RB = DateTime.Now;
            venta.MontoTotal_064RB = venta.Detalle_064RB.Sum(d => d.Subtotal_064RB);
            venta.NumVenta_064RB = map_venta.Agregar_064RB(venta);

            SER_Usuario_064RB usu = SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB;
            bll_bitacora.RegistrarBitacora_064RB(new SER_Bitacora_064RB(usu, DateTime.Now, "Ventas", "Alta", 1));
            bll_dv.ActualizarDV_064RB("Venta_064RB", "DetalleVenta_064RB", "UnidadCelular_064RB", "DetalleVenta_Imeis_064RB", "Accesorio_064RB");
            return venta.NumVenta_064RB;
        }
        public BE_Comprobante_064RB ObtenerComprobante(int numVenta)
        {
            if (numVenta <= 0) throw new Exception("No hay una venta registrada para emitir!");
            return map_venta.ConsultarComprobante_064RB(numVenta) ?? throw new Exception("No se pudieron obtener los datos de la venta!");
        }
    }
}
