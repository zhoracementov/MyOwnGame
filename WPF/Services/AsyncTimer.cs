using System;
using System.Threading;
using System.Threading.Tasks;

namespace WPF.Services
{
    public class AsyncTimer
    {
        public static readonly TimeSpan DefaultWait = TimeSpan.FromSeconds(60);
        public static readonly TimeSpan DefaultDelay = TimeSpan.FromSeconds(1);

        private Action callbackAction;
        private CancellationTokenSource cancellationTokenSource;
        private AsyncCancelWaiter<object> asyncCancelWaiter;

        private bool exitCode;

        private readonly TimeSpan delay;
        private readonly TimeSpan wait;

        public TimeSpan ActuallyWaited { get; set; }

        public AsyncTimer(Action callbackAction) : this(DefaultDelay, DefaultWait, callbackAction)
        {
            //...
        }

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
            ActuallyWaited = TimeSpan.Zero;

            using (cancellationTokenSource = new CancellationTokenSource(Timeout.InfiniteTimeSpan))
            {
                while (!cancellationTokenSource.IsCancellationRequested)
                {
                    if (asyncCancelWaiter != null)
                        await asyncCancelWaiter.Wait();

                    if (ActuallyWaited >= wait + delay)
                        cancellationTokenSource.Cancel();

                    try
                    {
                        await Task.Delay(delay, cancellationTokenSource.Token);
                        ActuallyWaited += delay;
                    }
                    catch (TaskCanceledException)
                    {
                        break;
                    }
                    finally
                    {
                        callbackAction?.Invoke();
                    }
                }
            }

            cancellationTokenSource = null;
            callbackAction = null;

            return exitCode;
        }

        public void Abort()
        {
            asyncCancelWaiter = new AsyncCancelWaiter<object>();
        }

        public void Continue()
        {
            asyncCancelWaiter?.Cancel(null);
            asyncCancelWaiter = null;
        }

        public void Restart()
        {
            ActuallyWaited = TimeSpan.Zero;
        }

        public void Cancel(bool exitCode = false)
        {
            this.exitCode = exitCode;
            cancellationTokenSource?.Cancel();
        }
    }
}
