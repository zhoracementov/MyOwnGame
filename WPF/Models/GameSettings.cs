using System;

namespace WPF.Models
{
    public class GameSettings
    {
        public TimeSpan AnswerWaitingTimeSpan { get; set; }
        public int MaxPlayerNameLength { get; set; }
        public int MaxPlayersCount { get; set; }
    }
}
