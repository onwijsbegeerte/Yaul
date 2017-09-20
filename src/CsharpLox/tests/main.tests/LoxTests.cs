using System;
using Xunit;
using Moq;

namespace main.tests
{
    public class LoxTests
    {
        [Fact]
        public void Lox_ShouldFlagError_WhenErrorIsCalled()
        {
            var mockScanner = new Mock<IScanner>();
            var lox = new Lox(mockScanner.Object);
            lox.error(1, "class bla");
            Assert.Equal(lox.hadError, true);
        }
    }
}
