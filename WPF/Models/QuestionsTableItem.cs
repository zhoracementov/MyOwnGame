using System;
using System.Linq;
using System.Text.RegularExpressions;
using WPF.Exceptions;

namespace WPF.Models
{
    public class QuestionsTableItem
    {
        private readonly string[] _answers;

        public string Description { get; }
        public int Cost { get; }
        public bool IsEnabled { get; set; }

        public QuestionsTableItem(string description, int cost, bool isEnabled, params string[] answers)
        {
            _answers = answers.ToArray();

            if (_answers.Length == 0)
                throw new ZeroAnswerForQuestionException();

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException();

            Cost = cost;
            Description = description;
            IsEnabled = isEnabled;
        }

        public bool CheckAnswer(string answerTest)
        {
            return _answers
                .Any(answerTrue => answerTest
                .Equals(Regex.Replace(answerTrue, @"\s+", string.Empty),
                StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
