using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using WPF.Exceptions;

namespace WPF.Models
{
    public class QuestionItem
    {
        public int Cost { get; }
        public string Description { get; }
        public string[] Answers { get; }

        public QuestionItem(int cost, string description, IEnumerable<string> answers)
        {
            Answers = answers.ToArray();

            if (Answers.Length == 0)
                throw new ZeroAnswerForQuestionException();

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException();

            Cost = cost;
            Description = description;
        }

        public QuestionItem(int cost, string description, params string[] answers)
            : this(cost, description, (IEnumerable<string>)answers)
        {
            //...
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
