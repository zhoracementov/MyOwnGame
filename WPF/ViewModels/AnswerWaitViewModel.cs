using Microsoft.Extensions.Options;
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

        private TimeSpan timeBefore;
        public TimeSpan TimeBefore
        {
            get => timeBefore;
            set => Set(ref timeBefore, value);
        }

        private string currentQuestionText;
        private readonly IOptions<GameSettings> gameOptions;

        public string CurrentQuestionText
        {
            get => currentQuestionText;
            set => Set(ref currentQuestionText, value);
        }

        public ICommand AnswerGivenCommand { get; }

        public AnswerWaitViewModel(IOptions<GameSettings> gameOptions)
        {
            AnswerGivenCommand = new RelayCommand(x => timer.Cancel());
            this.gameOptions = gameOptions;
        }

        public async Task<bool> WaitAnswerAsync(QuestionItem questionItem)
        {
            var delayTime = AsyncTimer.DefaultDelay;
            var waitTime = gameOptions.Value.AnswerWaitingTimeSpan;

            TimeBefore = waitTime;

            CurrentQuestionText = string.Concat(
                questionItem.Cost,
                Environment.NewLine,
                Environment.NewLine,
                questionItem.Description);

            timer ??= new AsyncTimer(delayTime, waitTime, () => TimeBefore -= delayTime);
            return await timer.Start();
        }
    }
}
