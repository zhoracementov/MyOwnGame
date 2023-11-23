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
        private readonly GameViewModel gameViewModel;

        private AsyncTimer timer;

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

        public AnswerWaitViewModel(GameViewModel gameViewModel)
        {
            AnswerGivenCommand = new RelayCommand(x =>
            {
                timer?.Cancel();
            });

            this.gameViewModel = gameViewModel;
        }

        public async Task WaitAnswerAsync(QuestionItem questionItem)
        {
            gameViewModel.AnswerViewModel = this;

            var delayTime = AsyncTimer.DefaultDelay;
            var waitTime = AsyncTimer.DefaultWait;

            TimeBefore = waitTime;

            CurrentQuestionText = string.Concat(questionItem.Cost, Environment.NewLine, questionItem.Description);

            timer = new AsyncTimer(() => TimeBefore -= delayTime, delayTime, waitTime);
            await timer.Start();
        }
    }
}
