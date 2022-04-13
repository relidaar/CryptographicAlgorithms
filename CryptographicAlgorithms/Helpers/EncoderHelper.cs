using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CryptographicAlgorithms.Helpers
{
    public static class EncoderHelper
    {
        public static IEnumerable<char> Filter(this string message, char[] alphabet, bool inLowerCase = false)
        {
            string value = inLowerCase 
                ? message?.ToLower()
                : message?.ToUpper();

            return value
                ?.Where(alphabet.Contains)
                ?? Enumerable.Empty<char>();
        }

        public static char[] GetCharacters(uint min, uint max) => 
            Enumerable
            .Range((int)min, (int)(max + 1 - min))
            .Select(x => (char)x).ToArray();

        public static char[] GenerateAlphabet() => GetCharacters(65, 90);

        public static char[,] GenerateMatrix(this char[] alphabet)
        {
            int matrixSize = (int)Math.Floor(Math.Sqrt(alphabet.Length));
            var matrix = new char[matrixSize, matrixSize];
            
            for (uint i = 0, row = 0; row < matrixSize; row++)
            {
                for (var column = 0; column < matrixSize; column++)
                {
                    matrix[row, column] = alphabet[i];
                    i++;
                }
            }

            return matrix;
        }

        public static IDictionary<char, (uint row, uint column)> CreateIndicesCache(this char[,] alphabetMatrix)
        {
            var dict = new Dictionary<char, (uint, uint)>();
            int size = alphabetMatrix.GetLength(0);
            for (uint row = 0; row < size; row++)
            {
                for (uint column = 0; column < size; column++)
                {
                    char letter = alphabetMatrix[row, column];
                    dict[letter] = (row, column);
                }
            }
            return dict;
        }


        public static string ReplaceAt(this string value, uint index, char newChar)
        {
            if (value == null || index >= value.Length) return value;

            var builder = new StringBuilder(value);
            builder[(int)index] = newChar;

            return builder.ToString();
        }

        public static string GetBinary(this uint value)
        {
            if (value == 0) return null;

            var output = new StringBuilder();
            while (value != 0)
            {
                var remainder = value % 2;
                value /= 2;
                output.Append(remainder);
            }

            return output.ToString();
        }

        public static string GetBinary(this string data, uint width = 8, string separator = "")
        {
            if (data == null) return data;

            var output = data.Select(c => Convert.ToString(c, 2).PadLeft((int)width, '0'));
            return string.Join(separator, output);
        }

        public static string GetFractionalBinary(this double value)
        {
            if (value == 0) return "0";

            var fractional = new StringBuilder();
            while (fractional.Length < 8)
            {
                value *= 2;
                fractional.Append((int)value);
                value = value < 1 ? value : value - (int)value;
            }

            return fractional.ToString();
        }

        public static string Xor(this string first, string second)
        {
            if (first == null) return second;
            if (second == null) return first;

            var output = first.Zip(second, (c1, c2) => 
                c1 == '1' && c2 == '0' || c1 == '0' && c1 == '1' ? '1' : '0');

            return string.Concat(output);
        }
    }
}
