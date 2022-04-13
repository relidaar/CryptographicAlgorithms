using CryptographicAlgorithms.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace CryptographicAlgorithms.Tests
{
    using Encoder = VigenereEncoder;
    public class VigenereEncoderTests
    {
        #region Constructor
        [Theory]
        [InlineData("AbCd", "ABCD")]
        [InlineData("A9%bCd", "ABCD")]
        public void Constructor_ShouldFilterKeyAndSetToUppercase(string key, string expected)
        {
            // Arrange
            var encoder = new Encoder(key);

            // Act
            // Assert
            Assert.Equal(expected, encoder.Key);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("43847943%34")]
        [InlineData(null)]
        public void Constructor_ShouldRaiseExceptionForInvalidKey(string key)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => new Encoder(key));
        }
        #endregion

        #region Encode
        [Theory]
        [InlineData("AbcD", "KEY", "KFAN")]
        [InlineData("Vigenere", "KeY", "FMEORCBI")]
        [InlineData("Some text", "SoMEkEY", "KCYIDIVL")]
        public void Encode_ShouldReturnEncodedMessage(
            string message,
            string key,
            string expected)
        {
            // Arrange
            IEncoder encoder = new Encoder(key);

            // Act
            string actual = encoder.Encode(message);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Encode_ShouldIgnoreNonLetters()
        {
            // Arrange
            IEncoder encoder = new Encoder("KEY");
            string message = "A9B%34C";
            string expected = "KFA";

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
            IEncoder encoder = new Encoder("KEY");
            string expected = "";

            // Act
            string actual = encoder.Encode(message);

            // Assert
            Assert.Equal(expected, actual);
        }
        #endregion

        #region Decode
        [Theory]
        [InlineData("AbcD", "KEY", "QXET")]
        [InlineData("Vigenere", "KeY", "LEIUJGHA")]
        [InlineData("Some text", "SoMEkEY", "AAAAJAZB")]
        public void Decode_ShouldReturnDecodedMessage(
            string message,
            string key,
            string expected)
        {
            // Arrange
            IEncoder encoder = new Encoder(key);

            // Act
            string actual = encoder.Decode(message);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Decode_ShouldIgnoreNonLetters()
        {
            // Arrange
            IEncoder encoder = new Encoder("KEY");
            string message = "A9B%34C";
            string expected = "QXE";

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
            IEncoder encoder = new Encoder("KEY");
            string expected = "";

            // Act
            string actual = encoder.Decode(message);

            // Assert
            Assert.Equal(expected, actual);
        }
        #endregion
    }
}
