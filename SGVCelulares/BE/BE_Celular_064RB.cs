namespace BE_064RB
{
    public class BE_Celular_064RB : BE_Producto_064RB
    {
        public string Almacenamiento_064RB { get; set; }
        public string Color_064RB { get; set; }
        public List<BE_UnidadCelular_064RB> UnidadCelulares_064RB { get; set; }
        public BE_Celular_064RB(string sku, string marca, string modelo, decimal precio, string descripcion, string almacenamiento, string color) : base(sku, marca, modelo, precio, descripcion)
        {
            Almacenamiento_064RB = almacenamiento;
            Color_064RB = color;
        }
        public BE_Celular_064RB(string sku, string marca, string modelo, decimal precio, string descripcion, string almacenamiento, string color, List<BE_UnidadCelular_064RB> unidadCelulares) : base(sku, marca, modelo, precio, descripcion)
        {
            Almacenamiento_064RB = almacenamiento;
            Color_064RB = color;
            UnidadCelulares_064RB = unidadCelulares;
        }
    }
}
