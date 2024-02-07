using System.Collections.ObjectModel;

namespace WPF.Models
{
    public class QuestionsRow
    {
        public string RowTitle { get; set; }

        private ObservableCollection<QuestionItem> rowItems;
        public ObservableCollection<QuestionItem> RowItems
        {
            get => rowItems;
            set
            {
                foreach (var item in value)
                {
                    item.Row = this;
                }

                rowItems = value;
            }
        }
    }
}
