using CryptographicAlgorithms.Interfaces;

using System;
using System.Collections.Generic;
using System.Text;

namespace CryptographicAlgorithms
{
    public class CaesarEncoder : IEncoder
    {
        private readonly int _shift;

        public CaesarEncoder(int shift)
        {
            _shift = shift;
        }

        public string Encode(string message)
        {
            throw new NotImplementedException();
        }

        public string Decode(string encryptedMessage)
        {
            throw new NotImplementedException();
        }
    }
}
