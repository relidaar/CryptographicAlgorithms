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
            var filteredMessage = message?.ToUpper()?.Where(_alphabet.Contains) ?? Enumerable.Empty<char>();

            var output = filteredMessage.Select(c =>
            {
                var index = (Array.IndexOf(_alphabet, c) + _shift).Mod(_alphabet.Length);
                return _alphabet[index];
            });
            return string.Concat(output);
        }

        public string Decode(string encryptedMessage)
        {
            var filteredMessage = encryptedMessage?.ToUpper()?.Where(_alphabet.Contains) ?? Enumerable.Empty<char>();

            var output = filteredMessage.Select(c =>
            {
                var index = (Array.IndexOf(_alphabet, c) - _shift).Mod(_alphabet.Length);
                return _alphabet[index];
            });
            return string.Concat(output);
        }
    }
}
