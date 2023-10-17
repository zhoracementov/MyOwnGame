using System.Windows.Input;
using WPF.Commands;
using WPF.Navigation.Services;

namespace WPF.ViewModels
{
    public class NewGameViewModel : ViewModel
    {
        public ICommand MoveToGameCommand { get; }

        public NewGameViewModel(INavigationService navigationService)
        {
            MoveToGameCommand = new RelayCommand(x =>
            {
                navigationService.NavigateTo<GameViewModel>();
            });
        }
    }
}
