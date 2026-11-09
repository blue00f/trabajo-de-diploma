using System.Security.Cryptography;
using System.Text;

namespace Servicio_064RB
{
    public class SER_Cripto_064RB
    {
        public static string EncriptarSHA256_064RB(string texto)
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
        public static string EncriptarAES_064RB(string texto)
        {
            ArgumentNullException.ThrowIfNull(texto);
            using (Aes aes_064RB = Aes.Create())
            {
                aes_064RB.KeySize = 256;
                aes_064RB.Mode = CipherMode.CBC;
                aes_064RB.Padding = PaddingMode.PKCS7;
                aes_064RB.Key = ObtenerClaveAES_064RB();
                aes_064RB.GenerateIV();
                byte[] bytesTexto_064RB = Encoding.UTF8.GetBytes(texto);
                using (ICryptoTransform cifrador_064RB = aes_064RB.CreateEncryptor())
                {
                    byte[] bytesCifrados_064RB = cifrador_064RB.TransformFinalBlock(bytesTexto_064RB, 0, bytesTexto_064RB.Length);
                    byte[] resultado_064RB = aes_064RB.IV.Concat(bytesCifrados_064RB).ToArray();
                    return Convert.ToBase64String(resultado_064RB);
                }
            }
        }
        public static string DesencriptarAES_064RB(string textoCifrado)
        {
            ArgumentNullException.ThrowIfNull(textoCifrado);
            byte[] bytesCifrados_064RB = Convert.FromBase64String(textoCifrado);
            if (bytesCifrados_064RB.Length < 32 || (bytesCifrados_064RB.Length - 16) % 16 != 0) throw new CryptographicException("El contenido cifrado no tiene un formato válido.");
            byte[] iv_064RB = bytesCifrados_064RB.Take(16).ToArray();
            byte[] contenido_064RB = bytesCifrados_064RB.Skip(16).ToArray();
            using (Aes aes_064RB = Aes.Create())
            {
                aes_064RB.KeySize = 256;
                aes_064RB.Mode = CipherMode.CBC;
                aes_064RB.Padding = PaddingMode.PKCS7;
                aes_064RB.Key = ObtenerClaveAES_064RB();
                aes_064RB.IV = iv_064RB;
                using (ICryptoTransform descifrador_064RB = aes_064RB.CreateDecryptor())
                {
                    byte[] bytesOriginales_064RB = descifrador_064RB.TransformFinalBlock(contenido_064RB, 0, contenido_064RB.Length);
                    return Encoding.UTF8.GetString(bytesOriginales_064RB);
                }
            }
        }
        private static byte[] ObtenerClaveAES_064RB()
        {
            string? clave_064RB = Environment.GetEnvironmentVariable("AES_KEY_BASE64");
            if (string.IsNullOrWhiteSpace(clave_064RB)) throw new Exception("No se encontró la clave de cifrado AES.");
            byte[] bytesClave_064RB = Convert.FromBase64String(clave_064RB);
            if (bytesClave_064RB.Length != 32) throw new Exception("La clave AES debe tener exactamente 32 bytes.");
            return bytesClave_064RB;
        }
    }
}
