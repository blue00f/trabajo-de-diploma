using System.Data;
using System.Drawing;
using System.Reflection;

namespace DAL_064RB.Negocio_064RB
{
    public class DAO_Producto_064RB
    {
        ComandoBD_064RB com;
        public DAO_Producto_064RB()
        {
            com = new();
        }
        public void AgregarCelular_064RB(string sku, string marca, string modelo, decimal precio, string descripcion, string almacenamiento, string color)
        {
            var comandos = new List<(string, List<ParametroBD_064RB>)>
            {
                ("INSERT INTO Producto_064RB(sku,marca,modelo,precio,descripcion) VALUES (@sku,@marca,@modelo,@precio,@descripcion)",
                    new List<ParametroBD_064RB> {
                        new("@sku", SqlDbType.VarChar, sku),
                        new("@marca", SqlDbType.VarChar, marca),
                        new("@modelo", SqlDbType.VarChar, modelo),
                        new("@precio", SqlDbType.Decimal, precio),
                        new("@descripcion", SqlDbType.VarChar, descripcion)
                    }),
                ("INSERT INTO Celular_064RB(sku,almacenamiento,color) VALUES (@sku,@almacenamiento,@color)",
                    new List<ParametroBD_064RB> {
                        new("@sku", SqlDbType.VarChar, sku),
                        new("@almacenamiento", SqlDbType.VarChar, almacenamiento),
                        new("@color", SqlDbType.VarChar, color)
                    })
            };
            com.EjecutarTransaccion_064RB(comandos);
        }
        public void AgregarAccesorio_064RB(string sku, string marca, string modelo, decimal precio, string descripcion, string categoria, int stock)
        {
            var comandos = new List<(string, List<ParametroBD_064RB>)>
            {
                ("INSERT INTO Producto_064RB(sku,marca,modelo,precio,descripcion) VALUES (@sku,@marca,@modelo,@precio,@descripcion)",
                    new List<ParametroBD_064RB> {
                        new("@sku", SqlDbType.VarChar, sku),
                        new("@marca", SqlDbType.VarChar, marca),
                        new("@modelo", SqlDbType.VarChar, modelo),
                        new("@precio", SqlDbType.Decimal, precio),
                        new("@descripcion", SqlDbType.VarChar, descripcion)
                    }),
                ("INSERT INTO Accesorio_064RB(sku,categoria,stock) VALUES (@sku,@categoria,@stock)",
                    new List<ParametroBD_064RB> {
                        new("@sku", SqlDbType.VarChar, sku),
                        new("@categoria", SqlDbType.VarChar, categoria),
                        new("@stock", SqlDbType.Int, stock)
                    })
            };
            com.EjecutarTransaccion_064RB(comandos);
        }
        public void Borrar_064RB(string sku)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@sku", SqlDbType.VarChar, sku)
            };
            com.Ejecutar_064RB("delete from Producto_064RB where sku=@sku", parametros);
        }
        public void ModificarCelular_064RB(string sku, string marca, string modelo, decimal precio, string descripcion, string almacenamiento, string color)
        {
            var comandos = new List<(string, List<ParametroBD_064RB>)>
            {
                ("update Producto_064RB set marca=@marca, modelo=@modelo, precio=@precio, descripcion=@descripcion where sku=@sku",
                    new List<ParametroBD_064RB> {
                        new("@sku", SqlDbType.VarChar, sku),
                        new("@marca", SqlDbType.VarChar, marca),
                        new("@modelo", SqlDbType.VarChar, modelo),
                        new("@precio", SqlDbType.Decimal, precio),
                        new("@descripcion", SqlDbType.VarChar, descripcion)
                    }),
                ("update Celular_064RB set almacenamiento=@almacenamiento, color=@color where sku=@sku",
                    new List<ParametroBD_064RB> {
                        new("@sku", SqlDbType.VarChar, sku),
                        new("@almacenamiento", SqlDbType.VarChar, almacenamiento),
                        new("@color", SqlDbType.VarChar, color)
                    })
            };
            com.EjecutarTransaccion_064RB(comandos);
        }
        public void ModificarAccesorio_064RB(string sku, string marca, string modelo, decimal precio, string descripcion, string categoria, int stock)
        {
            var comandos = new List<(string, List<ParametroBD_064RB>)>
            {
                ("update Producto_064RB set marca=@marca, modelo=@modelo, precio=@precio, descripcion=@descripcion where sku=@sku",
                    new List<ParametroBD_064RB> {
                        new("@sku", SqlDbType.VarChar, sku),
                        new("@marca", SqlDbType.VarChar, marca),
                        new("@modelo", SqlDbType.VarChar, modelo),
                        new("@precio", SqlDbType.Decimal, precio),
                        new("@descripcion", SqlDbType.VarChar, descripcion)
                    }),
                ("update Accesorio_064RB set categoria=@categoria, stock=@stock where sku=@sku",
                    new List<ParametroBD_064RB> {
                        new("@sku", SqlDbType.VarChar, sku),
                        new("@categoria", SqlDbType.VarChar, categoria),
                        new("@stock", SqlDbType.VarChar, stock)
                    })
            };
            com.EjecutarTransaccion_064RB(comandos);
        }
        public DataTable ConsultarCelulares_064RB()
        {
            return com.Consultar_064RB("select p.sku, p.marca, p.modelo, p.precio, p.descripcion, c.almacenamiento, c.color from Producto_064RB p" +
                " inner join Celular_064RB c on p.sku = c.sku");
        }
        public DataTable ConsultarAccesorios_064RB()
        {
            return com.Consultar_064RB("select p.sku, p.marca, p.modelo, p.precio, p.descripcion, a.categoria, a.stock from Producto_064RB p" +
                " inner join Accesorio_064RB a on p.sku = a.sku");
        }
        public DataTable ConsultarCelularPorSku_064RB(string sku)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@sku", SqlDbType.VarChar, sku),
            };
            return com.Consultar_064RB("select p.sku, p.marca, p.modelo, p.precio, p.descripcion, c.almacenamiento, c.color from Producto_064RB p inner join Celular_064RB c ON p.sku = c.sku where c.sku=@sku", parametros);
        }
        public DataTable ConsultarAccesorioPorSku_064RB(string sku)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@sku", SqlDbType.VarChar, sku),
            };
            return com.Consultar_064RB("select p.sku, p.marca, p.modelo, p.precio, p.descripcion, a.categoria, a.stock from Producto_064RB p inner join Accesorio_064RB a ON p.sku = a.sku where a.sku=@sku", parametros);
        }
    }
}
