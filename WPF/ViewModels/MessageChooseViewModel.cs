using System.Threading.Tasks;
using System.Windows.Input;
using WPF.Commands;
using WPF.Services;

namespace WPF.ViewModels
{
    public class MessageChooseViewModel : ViewModel
    {
        private readonly AsyncCancelWaiter<bool> waiter = new AsyncCancelWaiter<bool>();

        private string message;
        public string Message
        {
            get => message;
            set => Set(ref message, value);
        }

        public ICommand AnswerCommand { get; }

        public MessageChooseViewModel()
        {
            AnswerCommand = new RelayCommand(boolStr => waiter.Cancel(bool.Parse((string)boolStr)));
        }

        public async Task<bool> GetResponce(string messageText)
        {
            Message = messageText;
            return await waiter.Wait();
        }
    }
}
