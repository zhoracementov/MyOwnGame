using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WPF.Models
{
    public class QuestionsLine
    {
        public string Title { get; }
        public ObservableCollection<QuestionsTableItem> GameItems { get; }

        public QuestionsLine(string lineTitle, IEnumerable<QuestionsTableItem> items)
        {
            GameItems = new ObservableCollection<QuestionsTableItem>(items);
            Title = lineTitle;
        }
    }
}
