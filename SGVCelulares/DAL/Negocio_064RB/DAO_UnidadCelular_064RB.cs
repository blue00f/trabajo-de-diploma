using System.Data;

namespace DAL_064RB.Negocio_064RB
{
    public class DAO_UnidadCelular_064RB
    {
        ComandoBD_064RB com;
        public DAO_UnidadCelular_064RB()
        {
            com = new();
        }
        public void Agregar_064RB(string imei, string estado, string sku)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@imei", SqlDbType.VarChar, imei),
                new("@estado", SqlDbType.VarChar, estado),
                new("@sku", SqlDbType.VarChar, sku),
            };
            com.Ejecutar_064RB("INSERT INTO UnidadCelular_064RB(imei,estado,skuCelular) values (@imei,@estado,@sku)", parametros);
        }
        public void Borrar_064RB(string imei)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@imei", SqlDbType.VarChar, imei)
            };
            com.Ejecutar_064RB("DELETE FROM UnidadCelular_064RB WHERE imei=@imei;", parametros);
        }
        public void Modificar_064RB(string estado, string sku, string imei)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@estado", SqlDbType.VarChar, estado),
                new("@sku", SqlDbType.VarChar, sku),
                new("@imei", SqlDbType.VarChar, imei),
            };
            com.Ejecutar_064RB("UPDATE UnidadCelular_064RB SET estado=@estado, skuCelular=@sku WHERE imei=@imei", parametros);
        }
        public DataTable Consultar_064RB()
        {
            return com.Consultar_064RB("select * from UnidadCelular_064RB");
        }
    }
}
