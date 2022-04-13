using CryptographicAlgorithms.Helpers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace CryptographicAlgorithms.Tests
{
    public class EncoderHelperTests
    {
        #region Filter
        [Theory]
        [InlineData("ABC", "ABC")]
        [InlineData("AbC", "ABC")]
        [InlineData("AbC", "abc", true)]
        [InlineData("AB%6C", "ABC")]
        [InlineData("$#Ab34C", "ABC")]
        [InlineData("@#%A545bC", "abc", true)]
        public void Filter_ShouldReturnFilteredString(string message, string expected, bool inLowerCase = false)
        {
            // Arrange
            var alphabet = new char[] { 'A', 'B', 'C' };

            // Act
            var actual = EncoderHelper.Filter(message, alphabet, inLowerCase);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Filter_ShouldReturnEmptyStringIfInvalidInput(string message)
        {
            // Arrange
            var alphabet = new char[] { 'A', 'B', 'C' };

            // Act
            var actual = EncoderHelper.Filter(message, alphabet);

            // Assert
            Assert.Equal(string.Empty, actual);
        }

        [Fact]
        public void Filter_ShouldThrowExceptionIfNullAlphabet()
        {
            // Arrange
            // Act
            // Assert
            Assert.ThrowsAny<ArgumentException>(() => "abc".Filter(null));
        }

        [Fact]
        public void Filter_ShouldNotChangeTheInputIfEmptyAlphabet()
        {
            // Arrange
            var alphabet = new char[] {};
            string message = "ABC";

            // Act
            var actual = EncoderHelper.Filter(message, alphabet);

            // Assert
            Assert.Equal(message, actual);
        }
        #endregion
    }
}
