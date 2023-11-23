using System;
using System.Threading;
using System.Threading.Tasks;

namespace WPF.Services
{
    public class AsyncTimer
    {
        public static readonly TimeSpan DefaultWait = TimeSpan.FromSeconds(60);
        public static readonly TimeSpan DefaultDelay = TimeSpan.FromSeconds(1);

        private readonly Action callbackAction;
        private CancellationTokenSource cancellationTokenSource;

        private readonly TimeSpan delay;
        private readonly TimeSpan wait;

        public AsyncTimer(Action callbackAction, TimeSpan delay, TimeSpan wait)
        {
            if (callbackAction is null)
                throw new NullReferenceException(nameof(callbackAction));

            this.callbackAction = callbackAction;
            this.delay = delay;
            this.wait = wait;
        }

        public async Task Start()
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
