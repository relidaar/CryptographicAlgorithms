﻿namespace CryptographicAlgorithms.Interfaces
{
    public interface IEncoder
    {
        string Encode(string message);
        string Decode(string encryptedMessage);
    }
}
