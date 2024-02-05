using System.Collections.ObjectModel;

namespace WPF.Models
{
    public class QuestionsLine
    {
        public string RowTitle { get; set; }
        public ObservableCollection<QuestionItem> RowItems { get; set; }
    }
}
