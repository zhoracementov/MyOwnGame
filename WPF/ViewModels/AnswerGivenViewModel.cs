using System;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF.Commands;
using WPF.Models;
using WPF.Services;

namespace WPF.ViewModels
{
    public class AnswerGivenViewModel : ViewModel
    {
        private readonly GameViewModel gameViewModel;
        private AsyncTimer timer;

        private string answerText;
        public string AnswerText
        {
            get => answerText;
            set => Set(ref answerText, value);
        }

        public ICommand IsTrueAnswerCommand { get; }
        public ICommand IsFalseAnswerCommand { get; }

        private bool result;

        public AnswerGivenViewModel(GameViewModel gameViewModel)
        {
            IsTrueAnswerCommand = new RelayCommand(x =>
            {
                result = true;
                timer?.Cancel();
            });

            IsFalseAnswerCommand = new RelayCommand(x =>
            {
                result = false;
                timer?.Cancel();
            });

            this.gameViewModel = gameViewModel;
        }

        public async Task<bool> GetResult(QuestionItem questionItem)
        {
            gameViewModel.AnswerViewModel = this;

            AnswerText = questionItem.Answer;

            timer = new AsyncTimer(() => { }, AsyncTimer.DefaultDelay, new TimeSpan(0, 0, 0, -1, -1));
            await timer.Start();

            return result;
        }
    }
}
