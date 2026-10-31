using Microsoft.Data.SqlClient;
using System.Data;

namespace DAL_064RB.Negocio_064RB
{
    public class DAO_RemitoCompra_064RB
    {
        ComandoBD_064RB com;
        public DAO_RemitoCompra_064RB() => com = new();
        public int ContarImei_064RB(string imei)
        {
            var parametros = new List<ParametroBD_064RB> { new("@imei", SqlDbType.VarChar, imei) };
            return Convert.ToInt32(com.Escalar_064RB("select count(*) from UnidadCelular_064RB where Imei_064RB = @imei", parametros));
        }

        public int Agregar_064RB(int numOrden, int idSolicitud, int numRemito, string estadoRemito, DateTime fechaRecepcion,
            bool hayDiscrepancia, int cantidadFaltante, int cantidadDanada, string observaciones,
            string sku, bool esCelular, int cantidadConforme, List<string> imeis,
            string estadoSolicitudNuevo, string estadoSolicitudEsperado)
        {
            try
            {
                return com.EjecutarEnTransaccion_064RB(tx =>
                {
                    int filas = tx.Ejecutar_064RB(
                        @"update SolicitudCompra_064RB set Estado_064RB = @estadoNuevo
                      where IdSolicitud_064RB = @id and Estado_064RB = @estadoEsperado",
                        new List<ParametroBD_064RB>
                        {
                        new("@estadoNuevo", SqlDbType.VarChar, estadoSolicitudNuevo),
                        new("@id", SqlDbType.Int, idSolicitud),
                        new("@estadoEsperado", SqlDbType.VarChar, estadoSolicitudEsperado)
                        });
                    if (filas != 1) return 0;

                    int idRemito = Convert.ToInt32(tx.Escalar_064RB(
                        @"insert into RemitoCompra_064RB(NumOrden_064RB, NumRemito_064RB, Estado_064RB, FechaRecepcion_064RB)
                      values (@numOrden, @numRemito, @estado, @fecha);
                      select cast(scope_identity() as int);",
                        new List<ParametroBD_064RB>
                        {
                        new("@numOrden", SqlDbType.Int, numOrden),
                        new("@numRemito", SqlDbType.Int, numRemito),
                        new("@estado", SqlDbType.VarChar, estadoRemito),
                        new("@fecha", SqlDbType.DateTime, fechaRecepcion)
                        }));

                    if (hayDiscrepancia)
                    {
                        tx.Ejecutar_064RB(
                            @"insert into Discrepancia_064RB(IdRemito_064RB, CantidadFaltante_064RB, CantidadDanada_064RB, Observaciones_064RB)
                          values (@idRemito, @faltante, @danada, @observaciones)",
                            new List<ParametroBD_064RB>
                            {
                            new("@idRemito", SqlDbType.Int, idRemito),
                            new("@faltante", SqlDbType.Int, cantidadFaltante),
                            new("@danada", SqlDbType.Int, cantidadDanada),
                            new("@observaciones", SqlDbType.VarChar, observaciones)
                            });
                    }

                    if (esCelular)
                    {
                        foreach (string imei in imeis)
                        {
                            tx.Ejecutar_064RB(
                                "insert into UnidadCelular_064RB(Imei_064RB, Estado_064RB, SkuCelular_064RB) values (@imei, 'Disponible', @sku)",
                                new List<ParametroBD_064RB>
                                {
                                new("@imei", SqlDbType.VarChar, imei),
                                new("@sku", SqlDbType.VarChar, sku)
                                });
                        }
                    }
                    else if (cantidadConforme > 0)
                    {
                        int afectadas = tx.Ejecutar_064RB(
                            "update Accesorio_064RB set Stock_064RB = Stock_064RB + @cantidad where Sku_064RB = @sku",
                            new List<ParametroBD_064RB>
                            {
                            new("@cantidad", SqlDbType.Int, cantidadConforme),
                            new("@sku", SqlDbType.VarChar, sku)
                            });
                        if (afectadas != 1) throw new Exception("No se pudo actualizar el stock del producto " + sku + ".");
                    }
                    return idRemito;
                });
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                throw new Exception("Alguno de los IMEI ingresados ya está registrado.");
            }
        }
    }
}
