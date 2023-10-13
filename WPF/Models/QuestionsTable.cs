using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using WPF.Services.Serialization;

namespace WPF.Models
{
    public class QuestionsTable
    {
        public string Name { get; }
        public ObservableCollection<QuestionsLine> Table { get; }

        public QuestionsTable(string name, params QuestionsLine[] table) : this(name, (IEnumerable<QuestionsLine>)table)
        {
            //...
        }

        public QuestionsTable(string name, IEnumerable<QuestionsLine> table)
        {
            Name = name;
            Table = new ObservableCollection<QuestionsLine>(table);

            new JsonObjectSerializer().Serialize(this, Path.Combine(App.UserDataDirectory, $"{Name}.json"));
        }
    }
}
