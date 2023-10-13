using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WPF.Models
{
    public class QuestionsTable
    {
        public ObservableCollection<QuestionsLine> Table { get; }
        public QuestionsTable(IEnumerable<QuestionsLine> table)
        {
            Table = new ObservableCollection<QuestionsLine>(table);
        }
    }
}
