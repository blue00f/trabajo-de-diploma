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
        public void AgregarCelular_064RB(string sku, string marca, string modelo, decimal precio, string descripcion, int stockMinimo, string almacenamiento, string color)
        {
            var comandos = new List<(string, List<ParametroBD_064RB>)>
            {
                ("INSERT INTO Producto_064RB(Sku_064RB,Marca_064RB,Modelo_064RB,Precio_064RB,Descripcion_064RB,StockMinimo_064RB) VALUES (@sku,@marca,@modelo,@precio,@descripcion,@stockMinimo)",
                    new List<ParametroBD_064RB> {
                        new("@sku", SqlDbType.VarChar, sku),
                        new("@marca", SqlDbType.VarChar, marca),
                        new("@modelo", SqlDbType.VarChar, modelo),
                        new("@precio", SqlDbType.Decimal, precio),
                        new("@descripcion", SqlDbType.VarChar, descripcion),
                        new("@stockMinimo", SqlDbType.Int, stockMinimo)
                    }),
                ("INSERT INTO Celular_064RB(Sku_064RB,Almacenamiento_064RB,Color_064RB) VALUES (@sku,@almacenamiento,@color)",
                    new List<ParametroBD_064RB> {
                        new("@sku", SqlDbType.VarChar, sku),
                        new("@almacenamiento", SqlDbType.VarChar, almacenamiento),
                        new("@color", SqlDbType.VarChar, color)
                    })
            };
            com.EjecutarTransaccion_064RB(comandos);
        }
        public void AgregarAccesorio_064RB(string sku, string marca, string modelo, decimal precio, string descripcion, int stockMinimo, string categoria, int stock)
        {
            var comandos = new List<(string, List<ParametroBD_064RB>)>
            {
                ("INSERT INTO Producto_064RB(Sku_064RB,Marca_064RB,Modelo_064RB,Precio_064RB,Descripcion_064RB,StockMinimo_064RB) VALUES (@sku,@marca,@modelo,@precio,@descripcion,@stockMinimo)",
                    new List<ParametroBD_064RB> {
                        new("@sku", SqlDbType.VarChar, sku),
                        new("@marca", SqlDbType.VarChar, marca),
                        new("@modelo", SqlDbType.VarChar, modelo),
                        new("@precio", SqlDbType.Decimal, precio),
                        new("@descripcion", SqlDbType.VarChar, descripcion),
                        new("@stockMinimo", SqlDbType.Int, stockMinimo)
                    }),
                ("INSERT INTO Accesorio_064RB(Sku_064RB,Categoria_064RB,Stock_064RB) VALUES (@sku,@categoria,@stock)",
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
            com.Ejecutar_064RB("delete from Producto_064RB where Sku_064RB=@sku", parametros);
        }
        public void ModificarCelular_064RB(string sku, string marca, string modelo, decimal precio, string descripcion, int stockMinimo, string almacenamiento, string color)
        {
            var comandos = new List<(string, List<ParametroBD_064RB>)>
            {
                ("update Producto_064RB set Marca_064RB=@marca, Modelo_064RB=@modelo, Precio_064RB=@precio, Descripcion_064RB=@descripcion, StockMinimo_064RB=@stockMinimo where Sku_064RB=@sku",
                    new List<ParametroBD_064RB> {
                        new("@sku", SqlDbType.VarChar, sku),
                        new("@marca", SqlDbType.VarChar, marca),
                        new("@modelo", SqlDbType.VarChar, modelo),
                        new("@precio", SqlDbType.Decimal, precio),
                        new("@descripcion", SqlDbType.VarChar, descripcion),
                        new("@stockMinimo", SqlDbType.Int, stockMinimo)
                    }),
                ("update Celular_064RB set Almacenamiento_064RB=@almacenamiento, Color_064RB=@color where Sku_064RB=@sku",
                    new List<ParametroBD_064RB> {
                        new("@sku", SqlDbType.VarChar, sku),
                        new("@almacenamiento", SqlDbType.VarChar, almacenamiento),
                        new("@color", SqlDbType.VarChar, color)
                    })
            };
            com.EjecutarTransaccion_064RB(comandos);
        }
        public void ModificarAccesorio_064RB(string sku, string marca, string modelo, decimal precio, string descripcion, int stockMinimo, string categoria, int stock)
        {
            var comandos = new List<(string, List<ParametroBD_064RB>)>
            {
                ("update Producto_064RB set Marca_064RB=@marca, Modelo_064RB=@modelo, Precio_064RB=@precio, Descripcion_064RB=@descripcion, StockMinimo_064RB=@stockMinimo where Sku_064RB=@sku",
                    new List<ParametroBD_064RB> {
                        new("@sku", SqlDbType.VarChar, sku),
                        new("@marca", SqlDbType.VarChar, marca),
                        new("@modelo", SqlDbType.VarChar, modelo),
                        new("@precio", SqlDbType.Decimal, precio),
                        new("@descripcion", SqlDbType.VarChar, descripcion),
                        new("@stockMinimo", SqlDbType.Int, stockMinimo)
                    }),
                ("update Accesorio_064RB set Categoria_064RB=@categoria, Stock_064RB=@stock where Sku_064RB=@sku",
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
            return com.Consultar_064RB("select p.Sku_064RB, p.Marca_064RB, p.Modelo_064RB, p.Precio_064RB, p.Descripcion_064RB, p.StockMinimo_064RB, c.Almacenamiento_064RB, c.Color_064RB from Producto_064RB p" +
                " inner join Celular_064RB c on p.Sku_064RB = c.Sku_064RB");
        }
        public DataTable ConsultarAccesorios_064RB()
        {
            return com.Consultar_064RB("select p.Sku_064RB, p.Marca_064RB, p.Modelo_064RB, p.Precio_064RB, p.Descripcion_064RB, p.StockMinimo_064RB, a.Categoria_064RB, a.Stock_064RB from Producto_064RB p" +
                " inner join Accesorio_064RB a on p.Sku_064RB = a.Sku_064RB");
        }
        public DataTable ConsultarCelularPorSku_064RB(string sku)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@sku", SqlDbType.VarChar, sku),
            };
            return com.Consultar_064RB("select p.Sku_064RB, p.Marca_064RB, p.Modelo_064RB, p.Precio_064RB, p.Descripcion_064RB, p.StockMinimo_064RB, c.Almacenamiento_064RB, c.Color_064RB from Producto_064RB p inner join Celular_064RB c ON p.Sku_064RB = c.Sku_064RB where c.Sku_064RB=@sku", parametros);
        }
        public DataTable ConsultarAccesorioPorSku_064RB(string sku)
        {
            var parametros = new List<ParametroBD_064RB>
            {
                new("@sku", SqlDbType.VarChar, sku),
            };
            return com.Consultar_064RB("select p.Sku_064RB, p.Marca_064RB, p.Modelo_064RB, p.Precio_064RB, p.Descripcion_064RB, p.StockMinimo_064RB, a.Categoria_064RB, a.Stock_064RB from Producto_064RB p inner join Accesorio_064RB a ON p.Sku_064RB = a.Sku_064RB where a.Sku_064RB=@sku", parametros);
        }
    }
}
