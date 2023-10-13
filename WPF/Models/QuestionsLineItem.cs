using System;
using System.Linq;
using System.Text.RegularExpressions;
using WPF.Exceptions;

namespace WPF.Models
{
    public class QuestionsLineItem
    {
        public int Cost { get; }
        public string Description { get; }
        public string[] Answers { get; }
        public bool IsSolved { get; set; }

        public QuestionsLineItem(string description, int cost, bool isSolved, params string[] answers)
        {
            Answers = answers.ToArray();

            if (Answers.Length == 0)
                throw new ZeroAnswerForQuestionException();

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException();

            Cost = cost;
            Description = description;
            IsSolved = isSolved;
        }

        public bool CheckAnswer(string answerTest)
        {
            return Answers
                .Any(answerTrue => answerTest
                .Equals(Regex.Replace(answerTrue, @"\s+", string.Empty),
                StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
