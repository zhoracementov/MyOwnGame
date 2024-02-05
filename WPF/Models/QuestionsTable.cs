using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WPF.Services.Serialization;

namespace WPF.Models
{
    public class QuestionsTable
    {
        public string Name { get; set; }
        public ObservableCollection<QuestionsLine> TableRows { get; set; }

        public static async Task<QuestionsTable> LoadAsync(string filePath, IObjectSerializer objectSerializer)
        {
            return await objectSerializer.DeserializeAsync<QuestionsTable>(filePath);
        }

        public async Task SaveAsync(string filePath, IObjectSerializer objectSerializer)
        {
            await objectSerializer.SerializeAsync(this, filePath);
        }

        public bool IsCompleted()
        {
            return TableRows.SelectMany(x => x.RowItems).All(x => x.IsClosed == true);
        }
    }
}
