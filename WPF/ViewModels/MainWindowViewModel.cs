using System.Threading.Tasks;
using System.Windows.Input;
using WPF.Commands;
using WPF.Models;
using WPF.Services.Navigation;

namespace WPF.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private readonly AnswerWaitViewModel answerWaitWindowViewModel;
        private readonly MessageChooseViewModel messageChooseGameWindow;
        private readonly CancelWaitViewModel cancelWaitViewModel;
        private readonly AddPlayerViewModel addPlayerViewModel;

        private ViewModel messageViewModel;
        public ViewModel MessageViewModel
        {
            get => messageViewModel;
            set => Set(ref messageViewModel, value);
        }

        private INavigationService navigationService;
        public INavigationService NavigationService
        {
            get => navigationService;
            set => Set(ref navigationService, value);
        }

        public ICommand NavigateBackCommand { get; }

        public MainWindowViewModel(INavigationService navigationService, GameViewModel gameViewModel,
            AnswerWaitViewModel answerWaitWindowViewModel, MessageChooseViewModel messageChooseGameWindow,
            /*NewGameViewModel newGameViewModel, */CancelWaitViewModel cancelWaitViewModel,
            PlayersViewModel playersViewModel, AddPlayerViewModel addPlayerViewModel)
        {
            NavigationService = navigationService;

            this.answerWaitWindowViewModel = answerWaitWindowViewModel;
            this.messageChooseGameWindow = messageChooseGameWindow;
            this.cancelWaitViewModel = cancelWaitViewModel;
            this.addPlayerViewModel = addPlayerViewModel;

            NavigateBackCommand = new RelayCommand(async x =>
            {
                if (navigationService.CurrentViewModel != gameViewModel)
                {
                    if (messageViewModel != addPlayerViewModel)
                    {
                        NavigationService.NavigateTo<MainMenuViewModel>();
                    }
                    else
                    {
                        CloseMessageWindow();
                    }
                }
                else
                {
                    if (messageViewModel != messageChooseGameWindow)
                    {
                        var responce = await OpenMessageChooseWindow("Escape from this game? Progress will be lost.");

                        CloseMessageWindow();

                        if (responce)
                        {
                            NavigationService.NavigateTo<MainMenuViewModel>();
                            playersViewModel.GameEnds();
                        }
                    }
                }
            });

            NavigationService.NavigateTo<MainMenuViewModel>();
        }

        public async Task<bool> OpenWaitAnswerWindow(QuestionItem questionItem)
        {
            MessageViewModel = answerWaitWindowViewModel;
            return await answerWaitWindowViewModel.WaitAnswerAsync(questionItem);
        }

        public async Task<bool> OpenMessageChooseWindow(string messageText)
        {
            MessageViewModel = messageChooseGameWindow;
            return await messageChooseGameWindow.GetResponce(messageText);
        }

        public async Task<bool> OpenCancelWaitWindow(string messageText)
        {
            MessageViewModel = cancelWaitViewModel;
            return await cancelWaitViewModel.Wait(messageText);
        }

        public async Task<string> OpenAddPlayerWindow()
        {
            MessageViewModel = addPlayerViewModel;
            return await addPlayerViewModel.Wait();
        }

        public void CloseMessageWindow()
        {
            MessageViewModel = null;
        }
    }
}
