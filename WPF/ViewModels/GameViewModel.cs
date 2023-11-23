using WPF.Services;

namespace WPF.ViewModels
{
    public class GameViewModel : ViewModel
    {
        public PlayerRouletteService PlayerRouletteService { get; set; }

        public GameViewModel(PlayerRouletteService playerRouletteService)
        {
            PlayerRouletteService = playerRouletteService;
        }
    }
}
