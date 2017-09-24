using System;
using Xunit;

namespace main.tests
{
    public class ScannerTests
    {
        [Fact]
        public void Scanner_ShouldReturnTokens_WithValidSource()
        {
            var input = "test";
            var scanner = new Scanner(input);

            var result = scanner.tokens();

            Assert.NotEmpty(result);
        }

        [Fact]
        public void Scanner_ShouldThrowExpection_WithNullSource()
        {
            var scanner = new Scanner(null);

            Assert.Throws<NullReferenceException>(() => scanner.tokens());
        }

        [Fact]
        public void Scanner_ShouldNotThrowExpection_WithEmptySource()
        {
            var scanner = new Scanner(string.Empty);

            var result = scanner.tokens();

            Assert.Empty(result);
        }

        [Fact]
        public void Scanner_ShouldReportError_WithInvalidLexeme()
        {
            var scanner = new Scanner("QWERTY");

            var result = scanner.tokens();

            Assert.Equal(true, ErrorLogger.hadError);
        }

        [Theory]
        [InlineData("(", TokenType.L_PARAM)]
        [InlineData(")", TokenType.R_PARAM)]
        [InlineData("{", TokenType.L_BRACE)]
        [InlineData("}", TokenType.R_BRACE)]
        [InlineData(",", TokenType.COMMA)]
        [InlineData(".", TokenType.DOT)]
        [InlineData("-", TokenType.MINUS)]
        [InlineData("+", TokenType.PLUS)]
        [InlineData(";", TokenType.SEMICOLON)]
        [InlineData("*", TokenType.STAR)]
        public void Scanner_ShouldReturnRightTokenType_WithValidData(string input, TokenType expectedTokenType)
        {
            var scanner = new Scanner(input);

            var result = scanner.tokens();

            Assert.Equal(expectedTokenType, result[0].TokenType);
            Assert.Equal(TokenType.EOF, result[1].TokenType);
        }
    }
}
