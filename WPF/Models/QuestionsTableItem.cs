namespace WPF.Models
{
    public class QuestionsTableItem
    {
        public string Title { get; }
        public string Description { get; }
        public string Answer { get; }
        public bool IsSolved { get; set; }

        public QuestionsTableItem(string title, string description, string answer, bool isSolved = false)
        {
            Title = title;
            Description = description;
            Answer = answer;
            IsSolved = isSolved;
        }
    }
}
