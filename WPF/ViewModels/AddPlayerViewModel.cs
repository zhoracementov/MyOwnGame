using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF.Commands;
using WPF.Models;
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

        private int maxPlayerNameLength;
        public int MaxPlayerNameLength
        {
            get => maxPlayerNameLength;
            set => Set(ref maxPlayerNameLength, value);
        }

        public AddPlayerViewModel(IOptions<GameSettings> options)
        {
            MaxPlayerNameLength = options.Value.MaxPlayerNameLength;

            NavigateBackCommand = new RelayCommand(x =>
            {
                waiter?.Cancel(true);
            });
        }

        public async Task<string> Wait() //после закрытия возвращает строчку
        {
            await waiter.Wait();
            var res = PlayerName;
            PlayerName = string.Empty;
            return res;
        }
    }
}
