using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF.Commands;
using WPF.Models;
using WPF.Services;

namespace WPF.ViewModels
{
    public class AnswerGivenViewModel : ViewModel
    {
        private AsyncTimer timer;

        private bool iAvailable;
        public bool IsAvailable
        {
            get => iAvailable;
            set => Set(ref iAvailable, value);
        }

        private string answerText;
        public string AnswerText
        {
            get => answerText;
            set => Set(ref answerText, value);
        }

        public ICommand IsTrueAnswerCommand { get; }
        public ICommand IsFalseAnswerCommand { get; }

        private bool result;

        public AnswerGivenViewModel()
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
        }

        public async Task<bool> GetResult(QuestionItem questionItem)
        {
            AnswerText = questionItem.Answer;
            IsAvailable = true;

            timer = new AsyncTimer(() => { }, AsyncTimer.DefaultDelay, new TimeSpan(0, 0, 0, -1, -1));
            await timer.Start();

            IsAvailable = false;

            return result;
        }
    }
}
