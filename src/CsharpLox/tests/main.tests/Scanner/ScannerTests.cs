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
            var scanner = new Scanner("^^^");

            var result = scanner.tokens();

            Assert.Equal(true, ErrorLogger.hadError);
            Assert.Equal(TokenType.EOF, result[0].TokenType);
        }

        [Theory]
        [InlineData("\n")]
        [InlineData("\r")]
        [InlineData("     ")]
        public void Scanner_ShouldNotCreateToken_WithNonTokenTypes(string input)
        {
            var scanner = new Scanner(input);

            var result = scanner.tokens();

            Assert.Equal(TokenType.EOF, result[0].TokenType);
        }

        [Theory]
        [InlineData("\"hello lox\"")]
        public void Scanner_ShouldCreateToken_WithValidString(string input)
        {
            var scanner = new Scanner(input);
            //Check if this works with encoded data
            var result = scanner.tokens();

            Assert.Equal(TokenType.STRING, result[0].TokenType);
            Assert.Equal("hello lox", result[0].Literal);
        }
        
        [Theory]
        [InlineData("5")]
        [InlineData("12")]
        [InlineData("12.1")]
        [InlineData("0.12")]
        public void Scanner_ShouldCreateToken_WithValidNumber(string input)
        {
            var scanner = new Scanner(input);
            var result = scanner.tokens();

            Assert.Equal(TokenType.NUMBER, result[0].TokenType);
            Assert.Equal(double.Parse(input), double.Parse(result[0].Literal.ToString()));
        }

        [Fact]
        public void Scanner_ShouldExtractComment_WhenGivenCommentToken()
        {
            var scanner = new Scanner("/*this is a comment*/");

            var result = scanner.tokens();
            
            Assert.Equal("this is a comment", result[0].Literal.ToString());
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
        [InlineData("!", TokenType.BANG)]
        [InlineData("!=", TokenType.BANG_EQUAL)]
        [InlineData("=", TokenType.EQUAL)]
        [InlineData("==", TokenType.EQUAL_EQUAL)]
        [InlineData("<=", TokenType.LESS_EQUAL)]
        [InlineData(">", TokenType.GREATER)]
        [InlineData("<", TokenType.LESS)]
        [InlineData(">=", TokenType.GREATER_EQUAL)]
        [InlineData("/", TokenType.SLASH)]
        [InlineData("2", TokenType.NUMBER)]
        [InlineData("and", TokenType.AND)]
        [InlineData("identi", TokenType.IDENTIFIER)]
        [InlineData("/*this is a comment*/", TokenType.COMMENT)]
        public void Scanner_ShouldReturnRightTokenType_WithValidData(string input, TokenType expectedTokenType)
        {
            var scanner = new Scanner(input);

            var result = scanner.tokens();

            Assert.Equal(expectedTokenType, result[0].TokenType);
            Assert.Equal(TokenType.EOF, result[1].TokenType);
        }
    }
}
