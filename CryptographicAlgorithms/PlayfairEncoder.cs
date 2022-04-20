using CryptographicAlgorithms.Helpers;
using CryptographicAlgorithms.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;

public class PlayfairEncoder : IEncoder
{
    private readonly char[] _alphabet;
    private readonly char[,] _keyMatrix;
    private readonly int _keyMatrixSize;
    private readonly IDictionary<char, (uint row, uint column)> _indicesCache;
    private const char _letterToRemove = 'J';

    public PlayfairEncoder()
    {
        _alphabet = EncoderHelper
            .GenerateAlphabet()
            .Where(c => c != _letterToRemove)
            .ToArray();
        _keyMatrix = _alphabet.GenerateMatrix();
        _keyMatrixSize = _keyMatrix.GetLength(0);
        _indicesCache = _keyMatrix.CreateIndicesCache();
    }

    public string Encode(string message)
    {
        var letters = message
            ?.Filter(_alphabet)
            ?.Where(c => c != _letterToRemove)
            ?.ToList()
            ?? new List<char>();

        if (letters.Count <= 0) return string.Empty;

        if (letters.Count % 2 != 0)
        {
            letters.Add('X');
        }

        var bigrams = Enumerable
            .Range(0, letters.Count / 2)
            .Select(i =>
            {
                var pair = letters.Skip(i * 2).Take(2);
                char first = pair.First();
                char second = pair.Last();
                return (first, second);
            });

        var output = bigrams.Select(b => ChangeBigrams(b));

        return string.Concat(output);
    }

    public string Decode(string encryptedMessage)
    {
        var letters = encryptedMessage
            ?.Filter(_alphabet)
            ?.Where(c => c != _letterToRemove)
            ?.ToList()
            ?? new List<char>();

        if (letters.Count <= 0) return string.Empty;

        if (letters.Count % 2 != 0)
        {
            letters.Add('X');
        }

        var bigrams = Enumerable
            .Range(0, letters.Count / 2)
            .Select(i =>
            {
                var pair = letters.Skip(i * 2).Take(2);
                char first = pair.First();
                char second = pair.Last();
                return (first, second);
            });

        var output = bigrams.Select(b => ChangeBigrams(b, true));

        return string.Concat(output);
    }

    private string ChangeBigrams((char first, char second) bigram, bool decoding = false)
    {
        uint ChangePosition(uint value) => (decoding ? value - 1 : value + 1).Mod(_keyMatrixSize);
        var firstIndices = _indicesCache[bigram.first];
        var secondIndices = _indicesCache[bigram.second];

        if (firstIndices.row == secondIndices.row)
        {
            uint newFirstColumn = ChangePosition(firstIndices.column);
            uint newSecondColumn = ChangePosition(secondIndices.column);

            char firstLetter = _keyMatrix[firstIndices.row, newFirstColumn];
            char secondLetter = _keyMatrix[secondIndices.row, newSecondColumn];
            return $"{firstLetter}{secondLetter}";
        }
        else if (firstIndices.column == secondIndices.column)
        {
            uint newFirstRow = ChangePosition(firstIndices.row);
            uint newSecondRow = ChangePosition(secondIndices.row);

            char firstLetter = _keyMatrix[newFirstRow, firstIndices.column];
            char secondLetter = _keyMatrix[newSecondRow, secondIndices.column];
            return $"{firstLetter}{secondLetter}";
        }
        else
        {
            char firstLetter = _keyMatrix[firstIndices.row, secondIndices.column];
            char secondLetter = _keyMatrix[secondIndices.row, firstIndices.column];
            return $"{firstLetter}{secondLetter}";
        }
    }
}
