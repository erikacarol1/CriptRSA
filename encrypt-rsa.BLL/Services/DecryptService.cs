using encrypt_rsa.BLL.Infra.Services.Interfaces;
using encrypt_rsa.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace encrypt_rsa.BLL.Services
{
    public class DecryptService : IDecryptService
    {
        private readonly IEncryptService encryptService;
        public DecryptService(IEncryptService _encryptService)
        {
            encryptService = _encryptService;
        }
        public EncryptMessageDto DecryptMessage(ConfigRSADto config)
        {
            ConfigRSADto configKeys = config;

            configKeys = encryptService.GeneratePublicKey(config);

            configKeys = GeneratePrivateKey(configKeys);

            EncryptMessageDto DecryptedMessage = new EncryptMessageDto(DecryptText(configKeys));
            return DecryptedMessage;
        }

        public ConfigRSADto GeneratePrivateKey(ConfigRSADto configs)
        {
            int d = 0;
            while ((d * configs.e) % configs.totiente != 1)
            {
                d += 1;
            }
            configs.d = d;
            return configs;
        }

        public string DecryptText(ConfigRSADto configs)
        {
            string decryptText = "";

            List<char> listDecrypt = new List<char>();

            for (int i = 0; i < configs.message.Length; i++)
            {
                listDecrypt.Add(configs.message[i]);
            }
            foreach (char obj in listDecrypt)
            {
                double ascii = (int)obj;
                double k = Math.Pow(ascii, (double)configs.d) % configs.n;
                decryptText += (char)k;
            }
            return decryptText;
        }
    }
}
