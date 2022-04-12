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
            _descendingShift = descendingShift;
            _alphabet = EncoderHelper.GenerateAlphabet();
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
            int shift = (int)_shift;
            foreach (var c in message)
            {
                var index = (Array.IndexOf(_alphabet, c) + shift).Mod(_alphabet.Length);

                int shiftChange = _descendingShift ? -1 : 1;
                shift = decoding ? shift - shiftChange : shift + shiftChange;

                yield return _alphabet[index];
            }
        }
    }
}
