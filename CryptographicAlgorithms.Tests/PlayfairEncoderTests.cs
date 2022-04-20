using CryptographicAlgorithms.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace CryptographicAlgorithms.Tests
{
    using Encoder = PlayfairEncoder;
    public class PlayfairEncoderTests
    {
        #region Encode
        [Theory]
        [InlineData("AbC", "BCHC")]
        [InlineData("EncryptedKeyword", "CPBSZOUDEIDZYMTB")]
        public void Encode_ShouldReturnEncodedMessage(
            string message,
            string expected)
        {
            // Arrange
            IEncoder encoder = new Encoder();

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
            string expected = "BCHC";

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
        [InlineData("ABCX", "BCHC")]
        [InlineData("ENCRYPTEDKEYWORD", "CPBSZOUDEIDZYMTB")]
        public void Decode_ShouldReturnDecodedMessage(
            string expected,
            string message)
        {
            // Arrange
            IEncoder encoder = new Encoder();

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
            string message = "B%2C13HC";
            string expected = "ABCX";

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
