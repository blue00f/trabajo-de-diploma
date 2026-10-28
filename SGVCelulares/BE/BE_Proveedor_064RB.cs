namespace BE_064RB
{
    public class BE_Proveedor_064RB
    {
        public string Cuit_064RB { get; set; }
        public string RazonSocial_064RB { get; set; }
        public BE_Proveedor_064RB(string cuit, string razonSocial)
        {
            Cuit_064RB = cuit;
            RazonSocial_064RB = razonSocial;
        }
        public BE_Proveedor_064RB(string cuit)
        {
            Cuit_064RB = cuit;
        }
    }
}
