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
            MessageChooseViewModel messageChooseGameWindow, MessageBoxViewModel messageBoxViewModel, AnswerWaitViewModel answerWaitViewModel,
            PlayersViewModel playersViewModel, AddPlayerViewModel addPlayerViewModel, CancelWaitViewModel cancelWaitViewModel)
        {
            NavigationService = navigationService;

            NavigateBackCommand = new RelayCommand(async x =>
            {
                if (navigationService.CurrentViewModel != gameViewModel)
                {
                    if (messageBoxViewModel.AttachedViewModel != addPlayerViewModel && messageBoxViewModel.AttachedViewModel != cancelWaitViewModel)
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
                        var isEnabled = messageBoxViewModel.IsEnabled;
                        var responce = await messageBoxViewModel.OpenMessageChooseWindow("Escape from this game? Progress will be lost.");

                        if (responce)
                        {
                            messageBoxViewModel.CloseMessageWindow();

                            answerWaitViewModel.AnswerGivenCommand.Execute(bool.FalseString);

                            NavigationService.NavigateTo<NewGameViewModel>();
                            playersViewModel.GameEnds();
                        }
                        else
                        {
                            if (isEnabled)
                                messageBoxViewModel.AttachedViewModel = currMessageVM;
                            else
                                messageBoxViewModel.CloseMessageWindow();
                        }
                    }
                }
            });

            NavigationService.NavigateTo<MainMenuViewModel>();
        }
    }
}
