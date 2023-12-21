using System;
using System.Threading;
using System.Threading.Tasks;

namespace WPF.Services
{
    public class AsyncCancelWaiter<T>
    {
        private CancellationTokenSource cancellationTokenSource;
        private T waitResult;

        public async Task<T> Wait(TimeSpan timeOutSpan)
        {
            using (cancellationTokenSource = new CancellationTokenSource(timeOutSpan))
            {
                try
                {
                    await Task.Delay(timeOutSpan, cancellationTokenSource.Token);
                }
                catch (TaskCanceledException)
                {
                    //...
                }
            }

            cancellationTokenSource = null;

            return waitResult;
        }

        public async Task<T> Wait()
        {
            return await Wait(Timeout.InfiniteTimeSpan);
        }

        public void Cancel(T result)
        {
            waitResult = result;
            cancellationTokenSource?.Cancel();
        }
    }
}
