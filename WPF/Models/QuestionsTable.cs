using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WPF.Services.Serialization;

namespace WPF.Models
{
    public class QuestionsTable
    {
        public string Name { get; set; }
        public ObservableCollection<QuestionsRow> TableRows { get; set; }

        public static async Task<QuestionsTable> LoadAsync(string filePath, IObjectSerializer objectSerializer)
        {
            return await objectSerializer.DeserializeAsync<QuestionsTable>(filePath);
        }

        public async Task SaveAsync(string filePath, IObjectSerializer objectSerializer)
        {
            await objectSerializer.SerializeAsync(this, filePath);
        }

        public void Save(string filePath, IObjectSerializer objectSerializer)
        {
            objectSerializer.Serialize(this, filePath);
        }

        public bool IsCompleted()
        {
            return TableRows.SelectMany(x => x.RowItems).All(x => x.IsClosed == true);
        }

        public static QuestionsTable CreateEmpty(string name, int rowsCount, int rowLength)
        {
            return new QuestionsTable
            {
                Name = name,
                TableRows = new ObservableCollection<QuestionsRow>(Enumerable
                .Range(1, rowsCount)
                .Select(rowNumber => new QuestionsRow
                {
                    RowTitle = $"Row number {rowNumber}",
                    RowItems = new ObservableCollection<QuestionItem>(Enumerable
                    .Range(1, rowLength)
                    .Select(i => new QuestionItem
                    {
                        Cost = i * 100,
                        Answer = "Answer text",
                        Description = "Description",
                        PicturePath = "none",
                    }))
                }))
            };
        }
    }
}
