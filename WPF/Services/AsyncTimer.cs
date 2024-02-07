using System;
using System.Threading;
using System.Threading.Tasks;

namespace WPF.Services
{
    public class AsyncTimer
    {
        public static readonly TimeSpan DefaultWait = TimeSpan.FromSeconds(60);
        public static readonly TimeSpan DefaultDelay = TimeSpan.FromMilliseconds(500);

        private readonly Action callbackAction;
        private CancellationTokenSource cancellationTokenSource;
        private bool exitCode;

        private readonly TimeSpan delay;
        private readonly TimeSpan wait;

        public AsyncTimer(TimeSpan delay, TimeSpan wait, Action callbackAction)
        {
            if (callbackAction is null)
                throw new NullReferenceException(nameof(callbackAction));

            this.callbackAction = callbackAction;
            this.delay = delay;
            this.wait = wait;
        }

        public async Task<bool> Start()
        {
            exitCode = true;

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
                    finally
                    {
                        callbackAction.Invoke();
                    }
                }
            }

            cancellationTokenSource = null;
            return exitCode;
        }

        public void Cancel(bool exitCode = false)
        {
            this.exitCode = exitCode;
            cancellationTokenSource?.Cancel();
        }
    }
}
