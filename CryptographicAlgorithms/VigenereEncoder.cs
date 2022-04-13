using CryptographicAlgorithms.Interfaces;

using System;
using System.Collections.Generic;
using System.Text;

namespace CryptographicAlgorithms
{
    public class VigenereEncoder : IEncoder
    {
        public string Key { get; }

        public VigenereEncoder(string key)
        {
            Key = key;
        }

        public string Decode(string encryptedMessage)
        {
            throw new NotImplementedException();
        }

        public string Encode(string message)
        {
            throw new NotImplementedException();
        }
    }
}
