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

        public MainWindowViewModel(INavigationService navigationService, GameViewModel gameViewModel,
            MessageChooseViewModel messageChooseGameWindow,
            MessageBoxViewModel messageBoxViewModel,
            PlayersViewModel playersViewModel, AddPlayerViewModel addPlayerViewModel)
        {
            NavigationService = navigationService;

            NavigateBackCommand = new RelayCommand(async x =>
            {
                if (navigationService.CurrentViewModel != gameViewModel)
                {
                    if (messageBoxViewModel.AttachedViewModel != addPlayerViewModel)
                    {
                        NavigationService.NavigateTo<MainMenuViewModel>();
                    }
                    else
                    {
                        messageBoxViewModel.CloseMessageWindow();
                    }
                }
                else
                {
                    if (messageBoxViewModel.AttachedViewModel != messageChooseGameWindow)
                    {
                        var currMessageVM = messageBoxViewModel.AttachedViewModel;
                        var responce = await messageBoxViewModel.OpenMessageChooseWindow("Escape from this game? Progress will be lost.");

                        messageBoxViewModel.CloseMessageWindow();

                        if (responce)
                        {
                            NavigationService.NavigateTo<MainMenuViewModel>();
                            playersViewModel.GameEnds();
                        }
                        else
                        {
                            messageBoxViewModel.AttachedViewModel = currMessageVM;
                        }
                    }
                }
            });

            NavigationService.NavigateTo<MainMenuViewModel>();
        }
    }
}
