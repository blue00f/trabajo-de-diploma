using System.Security.Cryptography;
using System.Text;

namespace Servicio_064RB
{
    public class SER_Cripto_064RB
    {
        public static string Encriptar_064RB(string texto)
        {
            byte[] bytesTexto_064RB = Encoding.UTF8.GetBytes(texto);
            using (SHA256 sha_064RB = SHA256.Create())
            {
                byte[] bytesHash_064RB = sha_064RB.ComputeHash(bytesTexto_064RB);
                StringBuilder sb_064RB = new();
                foreach(byte b_064RB in bytesHash_064RB)
                {
                    sb_064RB.Append(b_064RB.ToString("x2"));
                }
                return sb_064RB.ToString();
            }
        }
    }
}
