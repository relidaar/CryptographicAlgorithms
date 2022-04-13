using CryptographicAlgorithms.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace CryptographicAlgorithms.Tests
{
    using Encoder = CaesarEncoder;
    public class CaesarEncoderTests
    {
        #region Encode
        [Theory]
        [InlineData("AbC", 1, "BCD")]
        [InlineData("AbC", -1, "ZAB")]
        [InlineData("AbC", 0, "ABC")]
        public void Encode_ShouldReturnEncodedMessage(string message, int shift, string expected)
        {
            // Arrange
            IEncoder encoder = new Encoder(shift);

            // Act
            string actual = encoder.Encode(message);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Encode_ShouldIgnoreNonLetters()
        {
            // Arrange
            IEncoder encoder = new Encoder(0);
            string message = "A9B%34C";
            string expected = "ABC";

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
            IEncoder encoder = new Encoder(0);
            string expected = "";

            // Act
            string actual = encoder.Encode(message);

            // Assert
            Assert.Equal(expected, actual);
        }
        #endregion
     
        #region Decode
        [Theory]
        [InlineData("BcD", 1, "ABC")]
        [InlineData("zAb", -1, "ABC")]
        [InlineData("AbC", 0, "ABC")]
        public void Decode_ShouldReturnDecodedMessage(string message, int shift, string expected)
        {
            // Arrange
            IEncoder encoder = new Encoder(shift);

            // Act
            string actual = encoder.Decode(message);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Decode_ShouldIgnoreNonLetters()
        {
            // Arrange
            IEncoder encoder = new Encoder(0);
            string message = "A9B%34C";
            string expected = "ABC";

            // Act
            string actual = encoder.Decode(message);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Decode_ShouldReturnEmptyStringForInvalidInput(string message)
        {
            // Arrange
            IEncoder encoder = new Encoder(0);
            string expected = "";

            // Act
            string actual = encoder.Decode(message);

            // Assert
            Assert.Equal(expected, actual);
        }
        #endregion
    }
}
