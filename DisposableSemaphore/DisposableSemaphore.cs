using System;
using System.Threading;
using System.Threading.Tasks;

namespace DisposableSemaphore
{
    public class DisposableSemaphore
    {
        private readonly SemaphoreSlim _semaphore;

        public int CurrentCount => this._semaphore.CurrentCount;

        public DisposableSemaphore(int initialCount, int maxCount)
        {
            this._semaphore = new SemaphoreSlim(initialCount, maxCount);
        }

        public async Task<DisposableSemaphoreHandle> WaitAsync()
        {
            var handle = new DisposableSemaphoreHandle(this._semaphore);
            await handle.WaitAsync();
            return handle;
        }

        public class DisposableSemaphoreHandle : IDisposable
        {
            private readonly SemaphoreSlim _semaphore;

            internal DisposableSemaphoreHandle(SemaphoreSlim instance)
            {
                this._semaphore = instance;
            }

            public void Dispose()
            {
                this._semaphore.Release();
            }

            internal async Task WaitAsync()
            {
                await this._semaphore.WaitAsync();
            }
        }
    }
}
