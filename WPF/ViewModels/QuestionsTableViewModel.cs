using WPF.Models;

namespace WPF.ViewModels
{
    public class QuestionsTableViewModel : ViewModel
    {
        private QuestionsTable questionsTable;
        public QuestionsTable QuestionsTable
        {
            get => questionsTable;
            set => Set(ref questionsTable, value);
        }
    }
}
