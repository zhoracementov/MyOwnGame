using System.Threading.Tasks;
using WPF.Models;

namespace WPF.ViewModels
{
    public class MessageBoxViewModel : ViewModel
    {
        private readonly AnswerWaitViewModel answerWaitWindowViewModel;
        private readonly MessageChooseViewModel messageChooseViewModel;
        private readonly CancelWaitViewModel cancelWaitViewModel;
        private readonly AddPlayerViewModel addPlayerViewModel;

        private bool isEnabled;
        public bool IsEnabled
        {
            get => isEnabled;
            set => Set(ref isEnabled, value);
        }

        private ViewModel attachedViewModel;
        public ViewModel AttachedViewModel
        {
            get => attachedViewModel;
            set => Set(ref attachedViewModel, value);
        }

        public MessageBoxViewModel(AnswerWaitViewModel answerWaitWindowViewModel, MessageChooseViewModel messageChooseViewModel,
            CancelWaitViewModel cancelWaitViewModel, AddPlayerViewModel addPlayerViewModel)
        {
            this.answerWaitWindowViewModel = answerWaitWindowViewModel;
            this.messageChooseViewModel = messageChooseViewModel;
            this.cancelWaitViewModel = cancelWaitViewModel;
            this.addPlayerViewModel = addPlayerViewModel;
        }

        public async Task<bool> OpenWaitAnswerWindow(QuestionItem questionItem)
        {
            AttachedViewModel = answerWaitWindowViewModel;
            IsEnabled = true;
            return await answerWaitWindowViewModel.WaitAnswerAsync(questionItem);
        }

        public async Task<bool> OpenMessageChooseWindow(string messageText)
        {
            AttachedViewModel = messageChooseViewModel;
            IsEnabled = true;
            return await messageChooseViewModel.GetResponce(messageText);
        }

        public async Task<bool> OpenCancelWaitWindow(string messageText)
        {
            AttachedViewModel = cancelWaitViewModel;
            IsEnabled = true;
            return await cancelWaitViewModel.Wait(messageText);
        }

        public async Task<string> OpenAddPlayerWindow()
        {
            AttachedViewModel = addPlayerViewModel;
            IsEnabled = true;
            return await addPlayerViewModel.Wait();
        }

        public void CloseMessageWindow()
        {
            IsEnabled = false;
            AttachedViewModel = null;
        }
    }
}
