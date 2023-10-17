using System;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF.Commands;
using WPF.Models;

namespace WPF.ViewModels
{
    public class AnswerWindowViewModel : ViewModel
    {
        public static readonly TimeSpan DefaultTiming = new TimeSpan(0, 0, 60);
        public static readonly TimeSpan DefaultStep = TimeSpan.FromSeconds(1);

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
            });
        }

        public async Task<string> OpenTiming(TimeSpan timer, QuestionItem questionItem)
        {
            IsAvailable = true;
            TimeBefore = timer;

            EnteredAnswerText = string.Empty;
            CurrentQuestionText = string.Concat(questionItem.Cost, Environment.NewLine, questionItem.Description);

            while (TimeBefore >= DefaultStep && IsAvailable)
            {
                await Task.Delay(DefaultStep);
                TimeBefore -= DefaultStep;
            }

            return EnteredAnswerText;
        }
    }
}
