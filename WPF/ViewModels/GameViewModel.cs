namespace WPF.ViewModels
{
    public class GameViewModel : ViewModel
    {
        private int costSum;
        public int CostSum
        {
            get => costSum;
            set => Set(ref costSum, value);
        }

        private string lastAnswer;
        public string LastAnswer
        {
            get => lastAnswer;
            set => Set(ref lastAnswer, value);
        }

        public GameViewModel()
        {

        }
    }
}
