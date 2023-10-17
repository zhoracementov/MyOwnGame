using System.Windows.Input;
using WPF.Commands;
using WPF.Models;
using WPF.Navigation.Services;

namespace WPF.ViewModels
{
    public class QuestionItemViewModel : ViewModel
    {
        private QuestionItem questionItem;
        public QuestionItem QuestionItem
        {
            get => questionItem;
            set => Set(ref questionItem, value);
        }

        private bool isActive = true;
        public bool IsActive
        {
            get => isActive;
            set => Set(ref isActive, value);
        }

        public ICommand TapToAnswerCommand { get; }

        public QuestionItemViewModel(INavigationService navigationService, GameViewModel gameViewModel, AnswerWindowViewModel answerWindowViewModel)
        {
            TapToAnswerCommand = new RelayCommand(async x =>
            {
                if (navigationService.CurrentViewModel == gameViewModel)
                {
                    IsActive = false;
                    var answer = await answerWindowViewModel.OpenTiming(AnswerWindowViewModel.DefaultTiming, QuestionItem);

                    if (QuestionItem.CheckAnswer(answer))
                    {
                        gameViewModel.CostSum += QuestionItem.Cost;
                    }

                    gameViewModel.LastAnswer = answer;
                }
            });
        }
    }
}
