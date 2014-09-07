using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmc.BLL.Contract.Security
{
    public interface IEncryptionService
    {
        /// <summary>
        /// Create a salt key
        /// </summary>
        /// <param name="size">Size of salt key</param>
        /// <returns></returns>
        string CreateSaltKey(int size);
        string CreatePasswordHash(string password, string saltkey, string passwordFormat = "SHA1");
        string EncryptString(string plainText, string encryptionPrivateKey = "");
        string DecryptString(string cipherText, string encryptionPrivateKey = "");
    }
}
