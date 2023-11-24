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

        public ICommand NavigateBackCommand { get; }

        public MainWindowViewModel(INavigationService navigationService, GameViewModel gameViewModel)
        {
            NavigationService = navigationService;

            NavigateBackCommand = new RelayCommand(async x =>
            {
                if (navigationService.CurrentViewModel != gameViewModel)
                {
                    NavigationService.NavigateTo<MainMenuViewModel>();
                }
                else
                {
                    var choose = await gameViewModel.OpenMessageWasteWindow();
                }
            });

            NavigationService.NavigateTo<MainMenuViewModel>();
        }
    }
}
