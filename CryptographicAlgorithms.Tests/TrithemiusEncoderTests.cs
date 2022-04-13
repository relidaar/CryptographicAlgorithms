using CryptographicAlgorithms.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace CryptographicAlgorithms.Tests
{
    using Encoder = TrithemiusEncoder;
    public class TrithemiusEncoderTests
    {
        #region Encode
        [Theory]
        [InlineData("AbC", 1, false, "BDF")]
        [InlineData("AbC", 0, false, "ACE")]
        [InlineData("AbC", 1, true, "BBB")]
        [InlineData("AbC", 0, true, "AAA")]
        public void Encode_ShouldReturnEncodedMessage(
            string message,
            uint shift, 
            bool descendingShift,
            string expected)
        {
            // Arrange
            IEncoder encoder = new Encoder(shift, descendingShift);

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
            string expected = "ACE";

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
        [InlineData("AbC", 1, false, "BBB")]
        [InlineData("AbC", 0, false, "AAA")]
        [InlineData("AbC", 1, true, "BDF")]
        [InlineData("AbC", 0, true, "ACE")]
        public void Decode_ShouldReturnDecodedMessage(
            string message,
            uint shift,
            bool descendingShift,
            string expected)
        {
            // Arrange
            IEncoder encoder = new Encoder(shift, descendingShift);

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
            string expected = "AAA";

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
