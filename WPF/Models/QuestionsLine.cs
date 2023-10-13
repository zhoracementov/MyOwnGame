using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WPF.Models
{
    public class QuestionsLine
    {
        public string Title { get; }
        public ObservableCollection<QuestionsLineItem> GameItems { get; }

        public QuestionsLine(string lineTitle, IEnumerable<QuestionsLineItem> items)
        {
            GameItems = new ObservableCollection<QuestionsLineItem>(items);
            Title = lineTitle;
        }
    }
}
