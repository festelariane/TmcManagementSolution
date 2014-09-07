using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Tmc.BLL.Contract.Security;

namespace Tmc.BLL.Impl.Security
{
    public class EncryptionService : IEncryptionService
    {
        private static readonly string EncryptKey = "TmcDev";
        public EncryptionService()
        {
        }
        public string CreateSaltKey(int size)
        {
            var key = string.Empty;
            using(var rng = new RNGCryptoServiceProvider())
            {
                var dataBuff = new byte[size];
                rng.GetBytes(dataBuff);
                key = Convert.ToBase64String(dataBuff);
            }

            return key;
        }

        public string CreatePasswordHash(string password, string saltkey, string passwordFormat = "SHA1")
        {
            if (String.IsNullOrEmpty(passwordFormat))
                passwordFormat = "SHA1";
            string saltAndPassword = String.Concat(password, saltkey);
            var hashAlgorithm = HashAlgorithm.Create(passwordFormat);
            if (hashAlgorithm == null)
                throw new ArgumentException("Unrecognized hash algorithm name", "hashName");
            var hashByteArray = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(saltAndPassword));
            return BitConverter.ToString(hashByteArray).Replace("-", "");
        }

        public string EncryptString(string plainText, string encryptionPrivateKey = "")
        {
            if (string.IsNullOrEmpty(plainText))
                return plainText;

            byte[] encryptedData = null;
            if (String.IsNullOrEmpty(encryptionPrivateKey))
                encryptionPrivateKey = EncryptKey;
            using(var encryptionProvider = new TripleDESCryptoServiceProvider())
            {
                encryptionProvider.Key = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(0, 16));
                encryptionProvider.IV = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(8, 8));

                var encryptor = encryptionProvider.CreateEncryptor();
                using(var ms = new MemoryStream())
                {
                    using(var cs = new CryptoStream(ms, encryptor,CryptoStreamMode.Write))
                    {
                        byte[] data = new UnicodeEncoding().GetBytes(plainText);
                        cs.Write(data, 0, data.Length);
                        cs.FlushFinalBlock();
                    }
                    encryptedData = ms.ToArray();
                }
            }
            return Convert.ToBase64String(encryptedData);

        }

        public string DecryptString(string cipherText, string encryptionPrivateKey = "")
        {
            if (string.IsNullOrEmpty(cipherText))
                return cipherText;

            if (String.IsNullOrEmpty(encryptionPrivateKey))
                encryptionPrivateKey = EncryptKey;
            using (var encryptionProvider = new TripleDESCryptoServiceProvider())
            {
                encryptionProvider.Key = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(0, 16));
                encryptionProvider.IV = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(8, 8));

                var decryptor = encryptionProvider.CreateDecryptor();
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        var sr = new StreamReader(cs, new UnicodeEncoding());
                        return sr.ReadLine();
                    }
                }
            }
            
        }
    }
}
