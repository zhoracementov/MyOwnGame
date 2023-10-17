using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using WPF.Services.Serialization;

namespace WPF.Models
{
    public class QuestionsTable
    {
        public string Name { get; }
        public ObservableCollection<QuestionsLine> TableItems { get; }

        public QuestionsTable(string name, IEnumerable<QuestionsLine> table)
        {
            Name = name;
            TableItems = new ObservableCollection<QuestionsLine>(table);

            var ser = new JsonObjectSerializer();
            ser.Serialize(this, Path.Combine(App.UserDataDirectory, $"{Name}{ser.FileFormat}"));
        }

        public QuestionsTable(string name, params QuestionsLine[] table) : this(name, (IEnumerable<QuestionsLine>)table)
        {
            //...
        }
    }
}
