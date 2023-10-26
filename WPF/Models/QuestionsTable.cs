using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WPF.Services.Serialization;

namespace WPF.Models
{
    public class QuestionsTable
    {
        public string Name { get; set; }
        public ObservableCollection<QuestionsLine> TableItems { get; set; }

        public QuestionsTable(string name, IEnumerable<QuestionsLine> table)
        {
            Name = name;
            TableItems = new ObservableCollection<QuestionsLine>(table);

            //var ser = new JsonObjectSerializer();
            //ser.Serialize(this, Path.Combine(App.UserDataDirectory, $"{Name}{ser.FileFormat}"));
        }

        public QuestionsTable(string name, params QuestionsLine[] table) : this(name, (IEnumerable<QuestionsLine>)table)
        {
            //...
        }

        //public QuestionsTable(string name, ObservableCollection<QuestionsLine> tableItems)
        //{
        //    this.Name = name;
        //    this.TableItems = tableItems;
        //}

        public QuestionsTable()
        {

        }

        public static QuestionsTable LoadFromFile(string filePath, IObjectSerializer objectSerializer)
        {
            return objectSerializer.Deserialize<QuestionsTable>(filePath);
        }

        public static void SaveToFile()
        {
            throw new NotImplementedException();
        }

        public bool IsCompleted()
        {
            return TableItems.SelectMany(x => x.LineItems).All(x => x.IsClosed == true);
        }
    }
}
