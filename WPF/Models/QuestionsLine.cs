using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WPF.Models
{
    public class QuestionsLine
    {
        public string LineTitle { get; }
        public ObservableCollection<QuestionItem> LineItems { get; }

        public QuestionsLine(string lineTitle, IEnumerable<QuestionItem> items)
        {
            LineItems = new ObservableCollection<QuestionItem>(items);
            LineTitle = lineTitle;
        }

        public QuestionsLine(string lineTitle, params QuestionItem[] items) : this(lineTitle, (IEnumerable<QuestionItem>)items)
        {
            //...
        }
    }
}
