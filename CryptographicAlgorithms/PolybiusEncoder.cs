using CryptographicAlgorithms.Helpers;
using CryptographicAlgorithms.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CryptographicAlgorithms
{
    public class PolybiusEncoder : IEncoder
    {
        public PolybiusEncoder(char letterToRemove = 'J')
        {
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
