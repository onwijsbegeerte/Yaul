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
    }
}
