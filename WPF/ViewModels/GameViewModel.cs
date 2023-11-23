using WPF.Services;

namespace WPF.ViewModels
{
    public class GameViewModel : ViewModel
    {

        private ViewModel answerViewModel;
        public ViewModel AnswerViewModel
        {
            get => answerViewModel;
            set => Set(ref answerViewModel, value);
        }

        public GameViewModel()
        {

        }
    }
}
