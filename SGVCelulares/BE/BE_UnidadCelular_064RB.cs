
namespace BE_064RB
{
    public class BE_UnidadCelular_064RB
    {
        public string Imei_064RB { get; set; }
        public string Estado_064RB { get; set; }
        public BE_Celular_064RB Celular_064RB { get; set; }

        public BE_UnidadCelular_064RB(string imei)
        {
            Imei_064RB = imei;
        }
        public BE_UnidadCelular_064RB(string imei, string estado, BE_Celular_064RB celular)
        {
            Imei_064RB = imei;
            Estado_064RB = estado;
            Celular_064RB = celular;
        }
    }
}
