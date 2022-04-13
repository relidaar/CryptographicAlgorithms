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
        private readonly char _letterToRemove = 'J';
        private readonly char[] _alphabet;
        private readonly char[,] _keyMatrix;
        private readonly int _keyMatrixSize;
        private readonly IDictionary<char, (uint row, uint column)> _indicesCache;

        public PolybiusEncoder(char letterToRemove = 'J')
        {
            var alphabet = EncoderHelper.GenerateAlphabet();
            letterToRemove = char.ToUpper(letterToRemove);

            if (alphabet.Contains(letterToRemove) == false)
                throw new ArgumentException("The chosen letter has to be from Latin alphabet.");

            _letterToRemove = letterToRemove;
            _alphabet = alphabet.Where(c => c != letterToRemove).ToArray();
            _keyMatrix = _alphabet.GenerateMatrix();
            _keyMatrixSize = _keyMatrix.GetLength(0);
            _indicesCache = _keyMatrix.CreateIndicesCache();
        }

        public string Decode(string encryptedMessage)
        {
            encryptedMessage = encryptedMessage ?? string.Empty;
            var digits = encryptedMessage
                .Where(c => char.IsDigit(c))
                .Select(d => (d - '0') - 1)
                .Where(d => d >= 0 && d < _keyMatrixSize)
                .ToArray();

            var indices = Enumerable
                .Range(0, digits.Length / 2)
                .Select(i => 
                {
                    var pair = digits.Skip(i * 2).Take(2);
                    int row = pair.First();
                    int column = pair.Last();
                    return (row, column);
                });

            var output = indices.Select(i => _keyMatrix[i.row, i.column]);
            return string.Concat(output);
        }

        public string Encode(string message)
        {
            var output = message
                .Filter(_alphabet)
                .Where(c => c != _letterToRemove)
                .Select(c => _indicesCache[c])
                .Select(i => $"{i.row + 1}{i.column + 1}");
            return string.Concat(output);
        }
    }
}
