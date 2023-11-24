using System.Threading.Tasks;
using WPF.Models;

namespace WPF.ViewModels
{
    public class GameViewModel : ViewModel
    {
        private readonly AnswerWaitViewModel answerWaitWindowViewModel;
        private readonly MessageChooseViewModel messageWasteGameWindow;

        private ViewModel messageViewModel;
        public ViewModel MessageViewModel
        {
            get => messageViewModel;
            set => Set(ref messageViewModel, value);
        }

        public GameViewModel(AnswerWaitViewModel answerWaitWindowViewModel, MessageChooseViewModel messageWasteGameWindow)
        {
            this.answerWaitWindowViewModel = answerWaitWindowViewModel;
            this.messageWasteGameWindow = messageWasteGameWindow;
        }

        public async Task<bool> OpenWaitAsnwerWindow(QuestionItem questionItem)
        {
            MessageViewModel = answerWaitWindowViewModel;
            return await answerWaitWindowViewModel.WaitAnswerAsync(questionItem);
        }

        public async Task<bool> GetResponceFromGivenWindow(QuestionItem questionItem)
        {
            return await OpenMessageChooseWindow(questionItem.Answer);
        }

        public async Task<bool> OpenMessageWasteWindow()
        {
            return await OpenMessageChooseWindow("Want to escape? Progess will be wasted.");
        }

        public async Task<bool> OpenMessageChooseWindow(string messageText)
        {
            MessageViewModel = messageWasteGameWindow;
            return await messageWasteGameWindow.GetResponce(messageText);
        }

        public void CloseAnswerWindow()
        {
            MessageViewModel = null;
        }
    }
}
