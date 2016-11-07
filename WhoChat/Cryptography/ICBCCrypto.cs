using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoChat.Cryptography
{
    public interface ICBCCrypto
    {
        string Decrypt(string CypherText, string Key, byte[] IV);

        string Encrypt(string PlainText, string Key, byte[] IV );
    }

}
