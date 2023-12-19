using System.Threading.Tasks;
using System.Windows.Input;
using WPF.Commands;
using WPF.Services;

namespace WPF.ViewModels
{
    public class AddPlayerViewModel : ViewModel
    {
        private readonly AsyncCancelWaiter<bool> waiter = new AsyncCancelWaiter<bool>();

        public ICommand NavigateBackCommand { get; }

        private string playerName;
        public string PlayerName
        {
            get => playerName;
            set => Set(ref playerName, value);
        }

        public AddPlayerViewModel()
        {
            NavigateBackCommand = new RelayCommand(x =>
            {
                waiter.Cancel();
            });
        }

        public async Task<string> Wait()
        {
            await waiter.Wait();
            var res = PlayerName;
            PlayerName = string.Empty;
            return res;
        }
    }
}
