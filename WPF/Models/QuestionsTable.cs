using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WPF.Models
{
    public class QuestionsTable
    {
        public ObservableCollection<QuestionsTableItem> GameItemsLinear { get; }

        public int SizeX { get; }
        public int SizeY { get; }

        public QuestionsTable(IEnumerable<QuestionsTableItem> items, int x, int y)
        {
            GameItemsLinear = new ObservableCollection<QuestionsTableItem>(items);
            SizeX = x;
            SizeY = y;
        }

        public static QuestionsTable GetFromFile(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
