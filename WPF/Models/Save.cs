using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WPF.Services.Serialization;

namespace WPF.Models
{
    public class Save
    {
        public string Name { get; set; }
        public string FilePath { get; set; }

        public static IObjectSerializer SavesSerializer { get; set; } = new JsonObjectSerializer();

        public Save(string filePath)
        {
            FilePath = filePath;
            Name = Path.GetFileNameWithoutExtension(FilePath);
        }

        public async Task<QuestionsTable> GetQuestionsTableAsync(IObjectSerializer objectSerializer)
        {
            return await QuestionsTable.LoadAsync(FilePath, objectSerializer);
        }

        public static IEnumerable<Save> GetSaves(string dirPath, IObjectSerializer objectSerializer)
        {
            return Directory
                .GetFiles(dirPath, $"*{objectSerializer.FileFormat}", SearchOption.TopDirectoryOnly)
                .Select(file => new Save(file))
                .Where(save =>
                {
                    var res = true;

                    try
                    {
                        var table = objectSerializer.Deserialize<QuestionsTable>(save.FilePath);
                    }
                    catch
                    {
                        res = false;
                    }

                    return res;
                });
        }
    }
}
