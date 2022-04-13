using CryptographicAlgorithms.Helpers;
using CryptographicAlgorithms.Interfaces;

using System;
using System.Collections.Generic;
using System.Text;

namespace CryptographicAlgorithms
{
    public class VigenereEncoder : IEncoder
    {
        private readonly char[] _alphabet;
        public string Key { get; }

        public VigenereEncoder(string key)
        {
            _alphabet = EncoderHelper.GenerateAlphabet();

            var filteredKey = string.Concat(key.Filter(_alphabet));
            if (string.IsNullOrWhiteSpace(filteredKey)) 
                throw new ArgumentException("Key cannot be null, empty or whitespace.");

            Key = filteredKey;
        }

        public string Decode(string encryptedMessage)
        {
            var filteredMessage = encryptedMessage.Filter(_alphabet);
            var output = GetShiftedChars(filteredMessage, true);
            return string.Concat(output);
        }

        public string Encode(string message)
        {
            var filteredMessage = message.Filter(_alphabet);
            var output = GetShiftedChars(filteredMessage);
            return string.Concat(output);
        }

        private IEnumerable<char> GetShiftedChars(IEnumerable<char> message, bool decoding = false)
        {
            int keyLetter = 0;
            foreach (var c in message)
            {
                int keyLetterIndex = Array.IndexOf(_alphabet, Key[keyLetter]);
                int shift = decoding ? -keyLetterIndex : keyLetterIndex;
                var index = (Array.IndexOf(_alphabet, c) + shift).Mod(_alphabet.Length);

                keyLetter = (keyLetter + 1).Mod(Key.Length);
                yield return _alphabet[index];
            }
        }
    }
}
