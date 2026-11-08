using BE_064RB;
using System.Xml.Serialization;

namespace BLL_064RB.Negocio_064RB
{
    public class BLL_Deserializador_064RB
    {
        public List<BE_Cliente_064RB> DeserealizarXml_064RB(string ruta)
        {
            using (FileStream fs = new FileStream(ruta, FileMode.Open))
            {
                XmlSerializer deserializador = new XmlSerializer(typeof(List<BE_Cliente_064RB>));
                return (List<BE_Cliente_064RB>)deserializador.Deserialize(fs)!;
            }
        }
    }
}
