using System.Collections.ObjectModel;

namespace WPF.Models
{
    public class QuestionsRow
    {
        private ObservableCollection<QuestionItem> rowItems;
        public ObservableCollection<QuestionItem> RowItems
        {
            get => rowItems;
            set
            {
                foreach (var item in value)
                {
                    item.RowTitle = RowTitle;
                }
                rowItems = value;
            }
        }
        public string RowTitle { get; set; }
    }
}
