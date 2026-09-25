namespace BE_064RB
{
    abstract public class BE_Producto_064RB
    {
        public string Sku_064RB { get; set; }
        public string Marca_064RB { get; set; }
        public string Modelo_064RB { get; set; }
        public decimal Precio_064RB { get; set; }
        public string Descripcion_064RB { get; set; }
        public BE_Producto_064RB(string sku, string marca, string modelo, decimal precio, string descripcion)
        {
            Sku_064RB = sku;
            Marca_064RB = marca;
            Modelo_064RB = modelo;
            Precio_064RB = precio;
            Descripcion_064RB = descripcion;
        }
    }
}
