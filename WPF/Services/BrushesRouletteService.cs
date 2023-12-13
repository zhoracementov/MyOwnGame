using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media;
using WPF.Extenstions.EnumerablePickExtentions;
using WPF.Services.Serialization;

namespace WPF.Services
{
    public class BrushesRouletteService
    {
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
            filePath = App.BrushesFile;

            IEnumerable<string> colors;
            var json = new JsonObjectSerializer();

            if (!File.Exists(filePath))
            {
                colors = GetAllColors();
                json.Serialize(colors, filePath);
            }
            else
            {
                colors = LoadBasicColors(json);
            }

            brushes = new Queue<string>(colors);
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
