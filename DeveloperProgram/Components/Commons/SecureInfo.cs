using CSUI_Teams_Sync.Components.Configurations;
using Microsoft.Extensions.Options;

namespace CSUI_Teams_Sync.Components.Commons
{
    public class SecureInfo
    {
        private readonly ConnectionConfig _config;
        private readonly CryptographyCore _cryptography;
        public SecureInfo(IOptions<ConnectionConfig> config, CryptographyCore cryptography)
        {
            _config = config.Value;
            _cryptography = cryptography;
        }

        public string GetSensitiveInfo(string encryptedString)
        {
            var fileName = _config.SecureInfo.FileName;

            string credentialsDirectory = _config.SecureInfo.Path;
            string AESKeyFilePath = Path.Combine(credentialsDirectory, _config.SecureInfo.FileName.Master_Name);

            return _cryptography.ReadSensitiveData(encryptedString, AESKeyFilePath);
        }
    }
}
