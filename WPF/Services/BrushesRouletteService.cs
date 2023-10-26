using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media;
using WPF.Extenstions;
using WPF.Services.Serialization;

namespace WPF.Services
{
    public class BrushesRouletteService
    {
        public const string FileName = "Brushes.json";

        private readonly string filePath;
        private readonly Queue<string> brushes;

        public string Next
        {
            get
            {
                var next = brushes.Dequeue();
                brushes.Enqueue(next);
                return next;
            }
        }

        public BrushesRouletteService()
        {
            filePath = Path.Combine(App.UserDataDirectory, FileName);
            brushes = new Queue<string>(LoadBasicColors(new JsonObjectSerializer()));
        }

        public IEnumerable<string> LoadBasicColors(IObjectSerializer objectSerializer)
        {
            return objectSerializer.Deserialize<string[]>(filePath).ShakeAll();
        }

        public static IEnumerable<string> GetAllColors()
        {
            return typeof(Brushes)
                .GetProperties()
                .Select(x => x.Name);
        }
    }
}
