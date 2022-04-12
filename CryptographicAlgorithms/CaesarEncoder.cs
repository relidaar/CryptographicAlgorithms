using CryptographicAlgorithms.Helpers;
using CryptographicAlgorithms.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CryptographicAlgorithms
{
    public class CaesarEncoder : IEncoder
    {
        private readonly int _shift;
        private readonly char[] _alphabet;

        public CaesarEncoder(int shift)
        {
            _shift = shift;
            _alphabet = EncoderHelper.GenerateAlphabet();
        }

        public string Encode(string message)
        {
            var filteredMessage = message.Filter(_alphabet);
            var output = GetShiftedChars(filteredMessage);
            return string.Concat(output);
        }

        public string Decode(string encryptedMessage)
        {
            var filteredMessage = encryptedMessage.Filter(_alphabet);
            var output = GetShiftedChars(filteredMessage, true);
            return string.Concat(output);
        }

        private IEnumerable<char> GetShiftedChars(IEnumerable<char> message, bool decoding = false) =>
            message.Select(c =>
            {
                int shift = decoding ? -_shift : _shift;
                var index = (Array.IndexOf(_alphabet, c) + shift).Mod(_alphabet.Length);
                return _alphabet[index];
            });
    }
}
