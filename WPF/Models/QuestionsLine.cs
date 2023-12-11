using System.Collections.ObjectModel;

namespace WPF.Models
{
    public class QuestionsLine
    {
        public string LineTitle { get; set; }
        public ObservableCollection<QuestionItem> LineItems { get; set; }
    }
}
