using System;
using Xunit;

namespace DisposableSemaphore.UnitTests
{
    public class DisposableSemaphoreTests
    {
        [Fact]
        public void SimpleTest()
        {
            Assert.Equal(3, DisposableSemaphore.SomethingTestable);
        }
    }
}
