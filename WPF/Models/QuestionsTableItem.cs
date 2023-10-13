namespace WPF.Models
{
    public class QuestionsTableItem
    {
        public string Title { get; }
        public string Description { get; }
        public string Answer { get; }

        public QuestionsTableItem(string title, string description, string answer)
        {
            Title = title;
            Description = description;
            Answer = answer;
        }
    }
}
