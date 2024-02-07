using System.Threading.Tasks;
using System.Windows.Input;
using WPF.Commands;
using WPF.Services;

namespace WPF.ViewModels
{
    public class CancelWaitViewModel : ViewModel
    {
        private readonly AsyncCancelWaiter<bool> waiter = new AsyncCancelWaiter<bool>();

        private string messageText;
        public string MessageText
        {
            get => messageText;
            set => Set(ref messageText, value);
        }

        public ICommand NavigateBackCommand { get; }

        public CancelWaitViewModel()
        {
            NavigateBackCommand = new RelayCommand(x => waiter?.Cancel(true));
        }

        public async Task<bool> Wait(string messageText)
        {
            MessageText = messageText;
            return await waiter.Wait();
        }
    }
}