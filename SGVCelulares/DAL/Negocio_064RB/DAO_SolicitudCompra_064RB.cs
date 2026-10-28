using Microsoft.Data.SqlClient;
using System.Data;

namespace DAL_064RB.Negocio_064RB
{
    public class DAO_SolicitudCompra_064RB
    {
        ComandoBD_064RB com;
        public DAO_SolicitudCompra_064RB() => com = new();
        public DataTable ConsultarSkusConSolicitudEnCurso_064RB() => com.Consultar_064RB("select SkuProducto_064RB from SolicitudCompra_064RB where Estado_064RB <> 'Rechazada' and Estado_064RB <> 'Recibida'");

        public void Agregar_064RB(string sku, int cantidad, string estado, DateTime fechaSolicitud)
        {
            var parametros = new List<ParametroBD_064RB>
        {
            new("@sku", SqlDbType.VarChar, sku),
            new("@cantidad", SqlDbType.Int, cantidad),
            new("@estado", SqlDbType.VarChar, estado),
            new("@fecha", SqlDbType.DateTime, fechaSolicitud)
        };
            try
            {
                com.Ejecutar_064RB("insert into SolicitudCompra_064RB(SkuProducto_064RB,Cantidad_064RB,Estado_064RB,FechaSolicitud_064RB) values (@sku,@cantidad,@estado,@fecha)", parametros);
            }
            catch (SqlException ex) when (ex.Number == 2601 || ex.Number == 2627)
            {
                throw new Exception("El producto seleccionado ya tiene una solicitud de reposición en curso.");
            }
        }
        public DataTable ConsultarPorEstado_064RB(string estado)
        {
            return com.Consultar_064RB($"select IdSolicitud_064RB, SkuProducto_064RB, Cantidad_064RB, Estado_064RB, FechaSolicitud_064RB, CuitProveedor_064RB, PrecioUnitario_064RB, MontoTotal_064RB, FechaDecision_064RB, Motivo_064RB from SolicitudCompra_064RB where Estado_064RB = @estado order by FechaSolicitud_064RB", new List<ParametroBD_064RB> { new("@estado", SqlDbType.VarChar, estado) });
        }

        public DataTable ConsultarPorId_064RB(int idSolicitud)
        {
            return com.Consultar_064RB($"select IdSolicitud_064RB, SkuProducto_064RB, Cantidad_064RB, Estado_064RB, FechaSolicitud_064RB, CuitProveedor_064RB, PrecioUnitario_064RB, MontoTotal_064RB, FechaDecision_064RB, Motivo_064RB from SolicitudCompra_064RB where IdSolicitud_064RB = @id", new List<ParametroBD_064RB> { new("@id", SqlDbType.Int, idSolicitud) });
        }
        public int Cotizar_064RB(int idSolicitud, string cuitProveedor, decimal precioUnitario, decimal montoTotal, string estadoNuevo, string estadoEsperado)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@id", SqlDbType.Int, idSolicitud),
                new("@cuit", SqlDbType.VarChar, cuitProveedor),
                new("@precio", SqlDbType.Decimal, precioUnitario),
                new("@monto", SqlDbType.Decimal, montoTotal),
                new("@estadoNuevo", SqlDbType.VarChar, estadoNuevo),
                new("@estadoEsperado", SqlDbType.VarChar, estadoEsperado)
            };
            return com.Ejecutar_064RB(
                @"update SolicitudCompra_064RB
                    set CuitProveedor_064RB = @cuit, PrecioUnitario_064RB = @precio, MontoTotal_064RB = @monto, Estado_064RB = @estadoNuevo
                    where IdSolicitud_064RB = @id and Estado_064RB = @estadoEsperado", parametros);
        }
        public int Evaluar_064RB(int idSolicitud, string estadoNuevo, DateTime fechaDecision, string? motivo, string estadoEsperado)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@id", SqlDbType.Int, idSolicitud),
                new("@estadoNuevo", SqlDbType.VarChar, estadoNuevo),
                new("@fecha", SqlDbType.DateTime, fechaDecision),
                new("@motivo", SqlDbType.VarChar, (object?)motivo ?? DBNull.Value),
                new("@estadoEsperado", SqlDbType.VarChar, estadoEsperado)
            };
            return com.Ejecutar_064RB(
                @"update SolicitudCompra_064RB
                  set Estado_064RB = @estadoNuevo, FechaDecision_064RB = @fecha, Motivo_064RB = @motivo
                  where IdSolicitud_064RB = @id and Estado_064RB = @estadoEsperado", parametros);
        }
    }
}
