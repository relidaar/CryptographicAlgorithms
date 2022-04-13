using CryptographicAlgorithms.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace CryptographicAlgorithms.Tests
{
    using Encoder = PolybiusEncoder;
    public class PolybiusEncoderTests
    {
        #region Constructor
        [Theory]
        [InlineData('1')]
        [InlineData('%')]
        public void Constructor_ShouldRaiseExceptionForInvalidLetter(char letterToRemove)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => new Encoder(letterToRemove));
        }
        #endregion

        #region Encode
        [Theory]
        [InlineData("AbC", "111213")]
        [InlineData("EncryptedKeyword", "15331342543544151425155452344214")]
        [InlineData("EncryptedKeyword", "153313425435441514155452344214", 'K')]
        public void Encode_ShouldReturnEncodedMessage(
            string message,
            string expected,
            char letterToRemove = 'J')
        {
            // Arrange
            IEncoder encoder = new Encoder(letterToRemove);

            // Act
            string actual = encoder.Encode(message);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Encode_ShouldIgnoreNonLetters()
        {
            // Arrange
            IEncoder encoder = new Encoder();
            string message = "A9B%34C";
            string expected = "111213";

            // Act
            string actual = encoder.Encode(message);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Encode_ShouldReturnEmptyStringForInvalidInput(string message)
        {
            // Arrange
            IEncoder encoder = new Encoder();
            string expected = "";

            // Act
            string actual = encoder.Encode(message);

            // Assert
            Assert.Equal(expected, actual);
        }
        #endregion

        #region Decode
        [Theory]
        [InlineData("ABC", "111213")]
        [InlineData("ENCRYPTEDKEYWORD", "15331342543544151425155452344214")]
        [InlineData("ENCRYPTEDEYWORD", "153313425435441514155452344214", 'K')]
        public void Decode_ShouldReturnEncodedMessage(
            string expected,
            string message,
            char letterToRemove = 'J')
        {
            // Arrange
            IEncoder encoder = new Encoder(letterToRemove);

            // Act
            string actual = encoder.Decode(message);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Decode_ShouldIgnoreNonLetters()
        {
            // Arrange
            IEncoder encoder = new Encoder();
            string message = "111%213C";
            string expected = "ABC";

            // Act
            string actual = encoder.Decode(message);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Decode_ShouldReturnEmptyStringForInvalidInput(string message)
        {
            // Arrange
            IEncoder encoder = new Encoder();
            string expected = "";

            // Act
            string actual = encoder.Decode(message);

            // Assert
            Assert.Equal(expected, actual);
        }
        #endregion
    }
}
