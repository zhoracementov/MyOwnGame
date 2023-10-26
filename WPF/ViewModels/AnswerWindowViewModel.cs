using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF.Commands;
using WPF.Models;

namespace WPF.ViewModels
{
    public class AnswerWindowViewModel : ViewModel
    {
        public static readonly TimeSpan DefaultTiming = TimeSpan.FromSeconds(60);
        public static readonly TimeSpan DefaultWaitDelay = TimeSpan.FromSeconds(1);
        private static CancellationTokenSource cancellationTokenSource;

        private bool iAvailable;
        public bool IsAvailable
        {
            get => iAvailable;
            set => Set(ref iAvailable, value);
        }

        private TimeSpan timeBefore;
        public TimeSpan TimeBefore
        {
            get => timeBefore;
            set => Set(ref timeBefore, value);
        }

        private string enteredAnswerText;
        public string EnteredAnswerText
        {
            get => enteredAnswerText;
            set => Set(ref enteredAnswerText, value);
        }

        private string currentQuestionText;
        public string CurrentQuestionText
        {
            get => currentQuestionText;
            set => Set(ref currentQuestionText, value);
        }

        public ICommand CloseCommand { get; }

        public AnswerWindowViewModel()
        {
            CloseCommand = new RelayCommand(x =>
            {
                IsAvailable = false;
                cancellationTokenSource?.Cancel();
            });
        }

        public async Task<string> OpenTiming(QuestionItem questionItem)
        {
            return await OpenTiming(questionItem, DefaultTiming, DefaultWaitDelay);
        }

        public async Task<string> OpenTiming(QuestionItem questionItem, TimeSpan timer, TimeSpan delayTime)
        {
            IsAvailable = true;
            TimeBefore = timer;

            EnteredAnswerText = string.Empty;
            CurrentQuestionText = string.Concat(questionItem.Cost, Environment.NewLine, questionItem.Description);

            using (cancellationTokenSource = new CancellationTokenSource(timer))
            {
                cancellationTokenSource.CancelAfter(timer + delayTime);
                while (TimeBefore >= delayTime && !cancellationTokenSource.IsCancellationRequested)
                {
                    try
                    {
                        await Task.Delay(delayTime, cancellationTokenSource.Token);
                    }
                    catch (TaskCanceledException)
                    {
                        break;
                    }

                    TimeBefore -= delayTime;
                }
            }

            cancellationTokenSource = null;

            return EnteredAnswerText;
        }
    }
}
