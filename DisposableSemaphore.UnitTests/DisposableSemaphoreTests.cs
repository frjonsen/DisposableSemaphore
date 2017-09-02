using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DisposableSemaphore.UnitTests
{
    public class DisposableSemaphoreTests
    {
        [Fact]
        public async Task DisposingTest()
        {
            var semaphore = new DisposableSemaphore(1, 1);

            using (await semaphore.WaitAsync())
            {
                Assert.Equal(semaphore.CurrentCount, 0);
            }

            Assert.Equal(semaphore.CurrentCount, 1);
        }

        [Fact]
        public async Task ThreadingTest()
        {
            var semaphore = new DisposableSemaphore(1, 1);
            var shared = 0;

            async Task TestFunc(int val)
            {
                using (await semaphore.WaitAsync())
                {
                    shared = val;
                }
            }

            var t1 = Task.Run(() => TestFunc(3));
            await t1;
            Assert.Equal(3, shared);

            var t2 = Task.Run(() => TestFunc(5));
            await t2;
            Assert.Equal(5, shared);

            var t3 = Task.Run(() => TestFunc(3));
            await t3;
            Assert.Equal(3, shared);


        }
    }
}
