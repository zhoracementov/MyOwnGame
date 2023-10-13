using System;

namespace WPF.Exceptions
{
    public class ZeroAnswerForQuestionException : Exception
    {
        public override string Message => "Zero Answer for Question Exception";
    }
}
