using System.Collections.Generic;
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

        public QuestionsTableViewModel()
        {
            QuestionsTable = new QuestionsTable(new List<QuestionsTableItem>
            {
                new QuestionsTableItem("1", "1", "1"), new QuestionsTableItem("2", "2", "2"),
                new QuestionsTableItem("3", "3", "3"), new QuestionsTableItem("4", "4", "4"),
                new QuestionsTableItem("5", "5", "5"), new QuestionsTableItem("6", "6", "6"),
            }, 2, 3);
        }
    }
}
