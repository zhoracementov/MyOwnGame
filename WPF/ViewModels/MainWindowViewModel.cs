using System.Windows.Input;
using WPF.Commands;
using WPF.Services.Navigation;

namespace WPF.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private INavigationService navigationService;
        public INavigationService NavigationService
        {
            get => navigationService;
            set => Set(ref navigationService, value);
        }

        public ICommand NavigateToMenuCommand { get; }

        public MainWindowViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;

            NavigateToMenuCommand = new RelayCommand(x =>
            {
                NavigationService.NavigateTo<MainMenuViewModel>();
            });

            //main menu as based window
            if (NavigateToMenuCommand.CanExecute(null))
            {
                NavigateToMenuCommand.Execute(null);
            }
        }
    }
}
