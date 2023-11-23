using System.Windows;
using System.Windows.Input;
using WPF.Commands;
using WPF.Services.Navigation;

namespace WPF.ViewModels
{
    public class MainMenuViewModel : ViewModel
    {
        public ICommand CloseAppCommand { get; }
        public ICommand MoveToNewGameCommand { get; }
        public ICommand MoveToGameEditCommand { get; }

        public MainMenuViewModel(INavigationService navigationService)
        {
            CloseAppCommand = new RelayCommand(x =>
            {
                Application.Current.Shutdown();
            });

            MoveToNewGameCommand = new RelayCommand(x =>
            {
                navigationService.NavigateTo<NewGameViewModel>();
            });

            MoveToGameEditCommand = new RelayCommand(x =>
            {
                navigationService.NavigateTo<GameEditorViewModel>();
            });
        }
    }
}
