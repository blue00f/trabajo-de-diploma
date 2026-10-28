using System.Data;

namespace DAL_064RB.Negocio_064RB
{
    public class DAO_OrdenCompra_064RB
    {
        ComandoBD_064RB com;
        public DAO_OrdenCompra_064RB() => com = new();
        public int Agregar_064RB(int idSolicitud, DateTime fecha, decimal montoPagado, string estadoNuevo, string estadoEsperado)
        {
            return com.EjecutarEnTransaccion_064RB(tx =>
            {
                int filas = tx.Ejecutar_064RB(
                    @"update SolicitudCompra_064RB set Estado_064RB = @estadoNuevo
                  where IdSolicitud_064RB = @id and Estado_064RB = @estadoEsperado",
                    new List<ParametroBD_064RB>
                    {
                    new("@estadoNuevo", SqlDbType.VarChar, estadoNuevo),
                    new("@id", SqlDbType.Int, idSolicitud),
                    new("@estadoEsperado", SqlDbType.VarChar, estadoEsperado)
                    });
                if (filas != 1) return 0;

                return Convert.ToInt32(tx.Escalar_064RB(
                    @"insert into OrdenCompra_064RB(IdSolicitud_064RB, Fecha_064RB, MontoPagado_064RB)
                  values (@id, @fecha, @monto);
                  select cast(scope_identity() as int);",
                    new List<ParametroBD_064RB>
                    {
                    new("@id", SqlDbType.Int, idSolicitud),
                    new("@fecha", SqlDbType.DateTime, fecha),
                    new("@monto", SqlDbType.Decimal, montoPagado)
                    }));
            });
        }
    }
}
