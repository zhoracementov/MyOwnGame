using System;
using System.Threading;
using System.Threading.Tasks;

namespace WPF.Services
{
    public class AsyncTimer
    {
        public static readonly TimeSpan DefaultWait = TimeSpan.FromSeconds(60);
        public static readonly TimeSpan DefaultDelay = TimeSpan.FromSeconds(1);

        private CancellationTokenSource cancellationTokenSource;

        private readonly TimeSpan delay;
        private readonly TimeSpan wait;

        public AsyncTimer(TimeSpan delay, TimeSpan wait)
        {
            this.delay = delay;
            this.wait = wait;
        }

        public AsyncTimer() : this(DefaultDelay, DefaultWait)
        {
            //...
        }

        public async Task Start(Action callbackAction)
        {
            using (cancellationTokenSource = new CancellationTokenSource(wait + delay))
            {
                while (!cancellationTokenSource.IsCancellationRequested)
                {
                    try
                    {
                        await Task.Delay(delay, cancellationTokenSource.Token);
                    }
                    catch (TaskCanceledException)
                    {
                        break;
                    }

                    callbackAction.Invoke();
                }
            }

            cancellationTokenSource = null;
        }

        public void Cancel()
        {
            cancellationTokenSource?.Cancel();
        }
    }
}
