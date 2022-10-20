using encrypt_rsa.BLL.Infra.Services.Interfaces;
using encrypt_rsa.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace encrypt_rsa.BLL.Services
{
    public class EncryptService : IEncryptService
    {
        Random numRandom = new Random();

        public EncryptService()
        {
            
        }
        public EncryptMessageDto EncryptMessage(ConfigRSADto config)
        {
            ConfigRSADto configKeys = config;
            
            configKeys = GeneratePublicKey(config);

            configKeys = GeneratePrivateKey(configKeys);

            EncryptMessageDto encryptedMessage = new EncryptMessageDto(Encrypt(configKeys));
            
            return encryptedMessage;
        }

        public ConfigRSADto GeneratePublicKey(ConfigRSADto config)
        {
            config.totiente = (config.q - 1) * (config.p - 1);

            while(true){
                int e = numRandom.Next(2, config.n);
                if (Mdc(config.n, e) == 1)
                {
                    config.e = e;
                    return config;
                }
            }
            throw new ArgumentException("Valor de totiente inválido");
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

        public string Encrypt(ConfigRSADto configs)
        {
            string msgCrypt = "";
            List<char> ASCIIList = new List<char>();

            for (int i = 0; i < configs.message.Length; i++)
            {
                ASCIIList.Add(configs.message[i]);
            }
            foreach (char obj in ASCIIList)
            {
                double ascii = (int)obj;
                double k = Math.Pow(ascii, (double)configs.d) % configs.n;
                msgCrypt += (char)k;
            }
            configs.message = msgCrypt;
            return configs.message;
        }

        public int Mdc(int n1, int n2)
        {
            while (n2 != 0)
            {
                int r = n1 % n2;
                n1 = n2;
                n2 = r;
            }
            return n1;
        }
    }
}
