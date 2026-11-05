using System.Data;

namespace DAL_064RB.Negocio_064RB
{
    public class DAO_Accesorio_C_064RB
    {
        ComandoBD_064RB com;
        public DAO_Accesorio_C_064RB() => com = new();

        public DataTable Consultar_064RB() => com.Consultar_064RB("select Id_064RB,Sku_064RB,Marca_064RB,Modelo_064RB,Precio_064RB,Descripcion_064RB,StockMinimo_064RB,Categoria_064RB,Stock_064RB,ActivoProducto_064RB,FechaHora_064RB,Act_064RB from Accesorio_C_064RB");
        public void Activar_064RB(int id)
        {
            string sql = @"
                UPDATE Accesorio_C_064RB SET Act_064RB = 0 WHERE Sku_064RB = (SELECT Sku_064RB FROM Accesorio_C_064RB WHERE Id_064RB = @id) AND Act_064RB = 1;
                UPDATE Accesorio_C_064RB SET Act_064RB = 1 WHERE Id_064RB = @id AND Act_064RB = 0;
            ";

            var comandos = new List<(string, List<ParametroBD_064RB>)>
            {
                (sql, new List<ParametroBD_064RB>
                {
                    new("@id", SqlDbType.Int, id)
                })
            };
            com.EjecutarTransaccion_064RB(comandos);
        }
    }
}
