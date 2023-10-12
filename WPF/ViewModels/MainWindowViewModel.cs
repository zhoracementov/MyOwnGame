using System.Windows.Input;
using WPF.Commands;
using WPF.Services;

namespace WPF.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        public string Title => App.CurrentDirectory;

        private INavigationService navigationService;
        public INavigationService NavigationService
        {
            get => navigationService;
            set => Set(ref navigationService, value);
        }
        public ICommand NavigateToMenuCommand { get; }

        public MainWindowViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService
                ;
            NavigationService.NavigateTo<MainMenuViewModel>();
            NavigateToMenuCommand = new RelayCommand(x =>
            {
                NavigationService.NavigateTo<MainMenuViewModel>();
            });
        }
    }
}
