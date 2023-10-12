using System.Windows;
using System.Windows.Input;
using WPF.Commands;
using WPF.Services;

namespace WPF.ViewModels
{
    public class MainMenuViewModel : ViewModel
    {
        public ICommand CloseAppCommand { get; }
        public ICommand StartNewGameCommand { get; }
        public ICommand GameEditCommand { get; }

        public MainMenuViewModel(INavigationService navigationService)
        {
            CloseAppCommand = new RelayCommand(x =>
            {
                Application.Current.Shutdown(Program.ExitCode);
            });

            StartNewGameCommand = new RelayCommand(x =>
            {
                navigationService.NavigateTo<NewGameViewModel>();
            });

            GameEditCommand = new RelayCommand(x =>
            {
                navigationService.NavigateTo<GameEditorViewModel>();
            });
        }
    }
}
