using System;
using Xunit;
using Moq;

namespace main.tests
{
    public class LoxTests
    {
        [Fact]
        public void ErrorLogger_ShouldFlagError_WhenErrorIsCalled()
        {    
            ErrorLogger.Error(1, "class bla");
            Assert.Equal(ErrorLogger.hadError, true);
        }
    }
}
