using System.IO;

namespace WPF.Models
{
    public class Save
    {
        public string name;
        public string Name => name ??= Path.GetFileNameWithoutExtension(FilePath);

        public string FilePath { get; set; }
    }
}
