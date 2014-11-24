using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MFramework.Common.Core.Crypto.Hashing;

namespace MFramework.Common.Core.Security
{
    public static class MessageAutenticator<THash,TEncoding> 
        where THash: HashAlgorithm, new()
        where TEncoding:Encoding,new()
    {
        public static string Authenticate(string message)
        {
            return message + Hash.HashOf<THash>(message, new TEncoding());

        }

        public static bool Verify(string authenticatedMessage)
        {

            return authenticatedMessage == Authenticate(StripAuthentication(authenticatedMessage));
        }

        public  static string StripAuthentication(string authenticatedMessage)
        {
            return authenticatedMessage.Substring(0, (authenticatedMessage.Length - GetAuthCharSize()));
            
        }

        static private int GetAuthCharSize()
        {
            var algoritm = new THash();
            return (algoritm.HashSize/8)*2;
        }

    }
}
