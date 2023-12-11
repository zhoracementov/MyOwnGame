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
        private readonly MessageChooseViewModel messageWasteGameWindow;

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
            NewGameViewModel newGameViewModel)
        {
            NavigationService = navigationService;

            this.answerWaitWindowViewModel = answerWaitWindowViewModel;
            messageWasteGameWindow = messageChooseGameWindow;

            NavigateBackCommand = new RelayCommand(async x =>
            {
                if (navigationService.CurrentViewModel != gameViewModel)
                {
                    NavigationService.NavigateTo<MainMenuViewModel>();
                }
                else
                {
                    if (messageViewModel != messageChooseGameWindow)
                    {
                        var responce = await OpenMessageChooseWindow("Escape from this game? Progress will be wasted.");
                        
                        CloseMessageWindow();

                        if (responce)
                        {
                            NavigationService.NavigateTo<MainMenuViewModel>();
                            newGameViewModel.UpdateTable();
                        }
                    }
                }
            });

            NavigationService.NavigateTo<MainMenuViewModel>();
        }

        public async Task<bool> OpenWaitAsnwerWindow(QuestionItem questionItem)
        {
            MessageViewModel = answerWaitWindowViewModel;
            return await answerWaitWindowViewModel.WaitAnswerAsync(questionItem);
        }

        public async Task<bool> OpenMessageChooseWindow(string messageText)
        {
            MessageViewModel = messageWasteGameWindow;
            return await messageWasteGameWindow.GetResponce(messageText);
        }

        public void CloseMessageWindow()
        {
            MessageViewModel = null;
        }
    }
}
