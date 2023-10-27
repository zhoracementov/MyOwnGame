using WPF.Services;

namespace WPF.ViewModels
{
    public class GameViewModel : ViewModel
    {
        public PlayerRouletteService PlayerRouletteService { get; set; }

        private string lastAnswer;
        public string LastAnswer
        {
            get => lastAnswer;
            set => Set(ref lastAnswer, value);
        }

        public GameViewModel(PlayerRouletteService playerRouletteService)
        {
            PlayerRouletteService = playerRouletteService;
        }
    }
}
