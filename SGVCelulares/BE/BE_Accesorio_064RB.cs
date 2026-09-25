namespace BE_064RB
{
    public class BE_Accesorio_064RB : BE_Producto_064RB
    {
        public string Categoria_064RB { get; set; }
        public int Stock_064RB { get; set; }
        public BE_Accesorio_064RB(string sku, string marca, string modelo, decimal precio, string descripcion, string categoria, int stock) : base(sku, marca, modelo, precio, descripcion)
        {
            Categoria_064RB = categoria;
            Stock_064RB = stock;
        }
        
    }
}
