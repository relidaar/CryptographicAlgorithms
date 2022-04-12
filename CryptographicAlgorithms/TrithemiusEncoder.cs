using CryptographicAlgorithms.Helpers;
using CryptographicAlgorithms.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CryptographicAlgorithms
{
    public class TrithemiusEncoder : IEncoder
    {
        private readonly uint _shift;
        private readonly char[] _alphabet;
        private readonly bool _descendingShift;

        public TrithemiusEncoder(uint shift, bool descendingShift = false)
        {
            _shift = shift;
            _alphabet = EncoderHelper.GenerateAlphabet();
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
