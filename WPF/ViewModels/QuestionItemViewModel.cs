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

        private bool isActive;
        public bool IsActive
        {
            get => isActive;
            set => Set(ref isActive, value);
        }

        public ICommand TapToAnswerCommand { get; }

        public QuestionItemViewModel(INavigationService navigationService, GameViewModel gameViewModel)
        {
            TapToAnswerCommand = new RelayCommand(async x =>
            {
                if (navigationService.CurrentViewModel == gameViewModel)
                {
                    IsActive = true;
                    await gameViewModel.OpenTiming(GameViewModel.DefaultTiming);
                }
            });
        }
    }
}
