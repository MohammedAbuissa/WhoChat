using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace WhoChat.Cryptography
{
    public class DES_CBC : ICBCCrypto
    {
        DESCryptoServiceProvider Crypto = new DESCryptoServiceProvider();

        public DES_CBC()
        {
            Crypto.Padding = PaddingMode.Zeros;
            Crypto.Mode = CipherMode.CBC;
        }

        public string Decrypt(string CypherText, string Key , byte[] IV )
        {
            #region Declaration
            //Key will be converted to array of chars
            //IV will be used as it is
            byte[] key = null, iV = null;
            string plainText = null;
            #endregion
            #region KeySelection
            key = Encoding.ASCII.GetBytes(Key);
            #endregion
            #region IVSelection
            iV = IV;
            #endregion
            #region DecryptingText
            var dec = Crypto.CreateDecryptor(key, iV);
            var cypherChars = Enumerable.Range(0, CypherText.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(CypherText.Substring(x, 2), 16))
                .ToArray();
            var plainChars = dec.TransformFinalBlock(cypherChars, 0, cypherChars.Length);
            plainText = Encoding.ASCII.GetString(plainChars, 0, plainChars.Where(x => x > 0).Count());
            #endregion
            return plainText;
        }

        public string Encrypt(string PlainText, string Key, byte[] IV )
        {
            #region Decalartion
            byte[] key = null, iV = null;
            string cypherText;
            #endregion

            #region KeySelection
            key =  Encoding.ASCII.GetBytes(Key);
            #endregion

            #region IVSelection
            iV = IV;
            #endregion

            #region EncryptingText
            var enc = Crypto.CreateEncryptor(key, iV);

            #region PlainTextPadding
            int blockSizeBytes = Crypto.BlockSize / 8;
            int noOfBlocks = (int)Math.Ceiling(PlainText.Length / (double)blockSizeBytes);
            var plainChars = new byte[noOfBlocks * blockSizeBytes];
            for (int i = 0; i < PlainText.Length; i++)
            {
                plainChars[i] = (byte)PlainText[i];
            }
            Console.WriteLine(Encoding.ASCII.GetString(plainChars));
            #endregion

            var cypherChars = enc.TransformFinalBlock(plainChars, 0, plainChars.Length);
            cypherText = cypherChars.Select(x => x.ToString("x2"))
                .Aggregate((x, y) => x + y);
            #endregion
            return cypherText;
        }
    }
}