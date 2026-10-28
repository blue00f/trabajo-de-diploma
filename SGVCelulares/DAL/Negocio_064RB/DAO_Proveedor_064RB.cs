using System.Data;

namespace DAL_064RB.Negocio_064RB
{
    public class DAO_Proveedor_064RB
    {
        ComandoBD_064RB com;
        public DAO_Proveedor_064RB() => com = new();
        public void Agregar_064B(string cuit, string razonSocial)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@cuit", SqlDbType.VarChar, cuit),
                new("@razonSocial", SqlDbType.VarChar, razonSocial),
            };
            com.Ejecutar_064RB("insert into Proveedor_064RB(Cuit_064RB,RazonSocial_064RB) values (@cuit,@razonSocial)", parametros);
        }
        public void Borrar_064RB(string cuit)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@cuit", SqlDbType.VarChar, cuit)
            };
            com.Ejecutar_064RB("UPDATE Proveedor_064RB SET Activo_064RB=0 WHERE Cuit_064RB=@cuit", parametros);
        }
        public void Modificar_064RB(string cuit, string razonSocial)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@cuit", SqlDbType.VarChar, cuit),
                new("@razonSocial", SqlDbType.VarChar, razonSocial)
            };
            com.Ejecutar_064RB("UPDATE Proveedor_064RB SET RazonSocial_064RB=@razonSocial WHERE Cuit_064RB=@cuit", parametros);
        }
        public DataTable Consultar_064RB()
        {
            return com.Consultar_064RB("select * from Proveedor_064RB where Activo_064RB=1");
        }
    }
}
