using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using System.Text;

namespace CSUI_Teams_Sync.Components.Commons
{
    public class CryptographyCore
    {
        /* private ConnectionConfig _config;
         public CryptographyCore(IOptions<ConnectionConfig> config)
         {
             _config = config.Value;
         }*/

        private readonly int _MacBitSize = 128;
        private int MacBitSize { get => _MacBitSize; }

        private string Decrypt(string cipherText, string passPhrase)
        {
            string sR = string.Empty;
            byte[] iv = Convert.FromBase64String(new string(cipherText.Take(16).ToArray())).ToArray();
            byte[] encryptedBytes = Convert.FromBase64String(new string(cipherText.Skip(16).ToArray())).ToArray();
            byte[] key = Convert.FromBase64String(passPhrase);


            GcmBlockCipher cipher = new GcmBlockCipher(new AesEngine());
            AeadParameters parameters = new AeadParameters(new KeyParameter(key), MacBitSize, iv, null);
            //ParametersWithIV parameters = new ParametersWithIV(new KeyParameter(key), iv);

            cipher.Init(false, parameters);
            byte[] plainBytes = new byte[cipher.GetOutputSize(encryptedBytes.Length)];
            Int32 retLen = cipher.ProcessBytes
                            (encryptedBytes, 0, encryptedBytes.Length, plainBytes, 0);
            cipher.DoFinal(plainBytes, retLen);

            sR = Encoding.UTF8.GetString(plainBytes).TrimEnd("\r\n\0".ToCharArray());


            return sR;
        }
        public string ReadSensitiveData(string input, string keyFilePath)
        {
            string passPhrase = File.ReadAllText(keyFilePath);
            return Decrypt(input, passPhrase);
        }
    }
}
