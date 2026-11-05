namespace BE_064RB
{
    public class BE_Accesorio_C_064RB : BE_Producto_064RB
    {
        public int Id_064RB { get; set; }
        public string Categoria_064RB { get; set; }
        public int Stock_064RB { get; set; }
        public DateTime FechaHora_064RB { get; set; }
        public bool ActivoHistorico_064RB { get; set; }
        public BE_Accesorio_C_064RB(int id, string sku, string marca, string modelo, decimal precio, string descripcion, int stockMinimo, string categoria, int stock, bool activo, DateTime fechaHora, bool activoHistorico) : base(sku, marca, modelo, precio, descripcion, stockMinimo, activo)
        {
            Id_064RB = id;
            Categoria_064RB = categoria;
            Stock_064RB = stock;
            FechaHora_064RB = fechaHora;
            ActivoHistorico_064RB = activoHistorico;
        }
    }
}
