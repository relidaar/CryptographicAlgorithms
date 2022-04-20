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
        throw new System.NotImplementedException();
    }
    public string Decode(string encryptedMessage)
    {
        throw new System.NotImplementedException();
    }
}
