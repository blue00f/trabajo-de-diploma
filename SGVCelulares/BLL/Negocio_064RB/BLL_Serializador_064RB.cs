using BE_064RB;
using System.Xml.Serialization;

namespace BLL_064RB.Negocio_064RB
{
    public class BLL_Serializador_064RB
    {
        public void SerializarXml_064RB(List<BE_Cliente_064RB> clientes, string ruta)
        {
            using(FileStream fs = new FileStream(ruta, FileMode.Create))
            {
                XmlSerializer serializador = new XmlSerializer(typeof(List<BE_Cliente_064RB>));
                serializador.Serialize(fs, clientes);
            }
        }
    }
}
