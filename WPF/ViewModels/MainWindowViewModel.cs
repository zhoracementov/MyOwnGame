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
            AnswerWaitViewModel answerWaitWindowViewModel, MessageChooseViewModel messageWasteGameWindow)
        {
            NavigationService = navigationService;

            this.answerWaitWindowViewModel = answerWaitWindowViewModel;
            this.messageWasteGameWindow = messageWasteGameWindow;

            NavigateBackCommand = new RelayCommand(x =>
            {
                if (navigationService.CurrentViewModel != gameViewModel)
                {
                    NavigationService.NavigateTo<MainMenuViewModel>();
                }
                else
                {
                    //var choose = await OpenMessageChooseWindow("Want to escape? Progess will be wasted.");
                    //CloseMessageWindow();

                    //if (choose)
                    //{
                    //    NavigationService.NavigateTo<MainMenuViewModel>();
                    //}
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
