using System;

namespace WPF.Models
{
    public class QuestionItem
    {
        public int Cost { get; set; }
        public string Description { get; set; }
        public string Answer { get; set; }
        public bool? IsClosed { get; set; }

        public QuestionItem()
        {
            //...
        }

        public QuestionItem(int cost, string description, string answer)
        {
            Answer = answer;

            if (string.IsNullOrWhiteSpace(answer))
                throw new ArgumentException();

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException();

            Cost = cost;
            Description = description;
        }
    }
}
