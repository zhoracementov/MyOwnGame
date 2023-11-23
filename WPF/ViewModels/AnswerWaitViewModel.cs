using System;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF.Commands;
using WPF.Models;
using WPF.Services;

namespace WPF.ViewModels
{
    public class AnswerWaitViewModel : ViewModel
    {
        private AsyncTimer timer;

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

        private string currentQuestionText;
        public string CurrentQuestionText
        {
            get => currentQuestionText;
            set => Set(ref currentQuestionText, value);
        }

        public ICommand AnswerGivenCommand { get; }

        public AnswerWaitViewModel()
        {
            AnswerGivenCommand = new RelayCommand(x =>
            {
                IsAvailable = false;
                timer?.Cancel();
            });
        }

        public async Task WaitAnswerAsync(QuestionItem questionItem)
        {
            var delayTime = AsyncTimer.DefaultDelay;
            var waitTime = AsyncTimer.DefaultWait;

            IsAvailable = true;
            TimeBefore = waitTime;

            CurrentQuestionText = string.Concat(questionItem.Cost, Environment.NewLine, questionItem.Description);

            timer = new AsyncTimer(() => TimeBefore -= delayTime, delayTime, waitTime);
            await timer.Start();

            IsAvailable = false;
        }
    }
}
