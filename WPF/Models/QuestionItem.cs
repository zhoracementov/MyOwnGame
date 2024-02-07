using System.Text.Json.Serialization;

namespace WPF.Models
{
    public class QuestionItem
    {
        public int Cost { get; set; }
        public string Description { get; set; }
        public string PicturePath { get; set; }
        public string Answer { get; set; }
        public bool? IsClosed { get; set; }
        [JsonIgnore]
        public QuestionsRow Row { get; set; }
    }
}
