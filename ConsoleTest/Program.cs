using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConsoleTest
{
    class Program
    {
        static public bool CheckAnswer(string answerTest, params string[] Answers)
        {
            return Answers
                .Any(answerTrue => answerTest
                .Equals(Regex.Replace(answerTrue, @"\s+", string.Empty),
                StringComparison.InvariantCultureIgnoreCase));
        }
        static void Main(string[] args)
        {
            Console.WriteLine(CheckAnswer("Цикл", "Цикл"));
            Console.ReadKey();
        }
    }
}
