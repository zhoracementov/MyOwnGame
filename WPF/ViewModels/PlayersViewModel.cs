using System;
using System.Collections.Generic;
using System.Windows.Input;
using WPF.Commands;
using WPF.Models;

namespace WPF.ViewModels
{
    public class PlayersViewModel : ViewModel
    {
        private readonly Queue<Player> players;

        private Player previousPlayer;
        public Player PreviousPlayer
        {
            get => previousPlayer;
            set => Set(ref previousPlayer, value);
        }

        private Player currentPlayer;
        public Player CurrentPlayer
        {
            get => currentPlayer;
            set => Set(ref currentPlayer, value);
        }

        private Player nextPlayer;
        public Player NextPlayer
        {
            get => nextPlayer;
            set => Set(ref nextPlayer, value);
        }

        private QuestionItem lastQuestion;
        public QuestionItem LastQuestion
        {
            get => lastQuestion;
            set => Set(ref lastQuestion, value);
        }

        public ICommand AddNewPlayerCommand { get; }
        public ICommand ResetPlayersCommand { get; }

        public PlayersViewModel()
        {
            ResetPlayersCommand = new RelayCommand(x => ResetPlayers());

            players = new Queue<Player>();
            
            var curr = new Player("Current", "Red");
            var prev = new Player("Previous", "Yellow");
            var next = new Player("Next", "Green");
            var loh = new Player("LOH", "Brown");

            players.Enqueue(prev);
            players.Enqueue(curr);
            players.Enqueue(next);
            players.Enqueue(loh);

            PreviousPlayer = players.Dequeue();
            CurrentPlayer = players.Dequeue();
            NextPlayer = players.Dequeue();

        }

        public void AddPlayer()
        {
            throw new NotImplementedException();
        }

        public void SuccessfullyAnswered(QuestionItem questionItem)
        {
            LastQuestion = questionItem;
            CurrentPlayer.Score += LastQuestion.Cost;

            SwapPlayersQueue();
        }

        public void SwapPlayersQueue()
        {
            players.Enqueue(PreviousPlayer);
            PreviousPlayer = CurrentPlayer;
            CurrentPlayer = NextPlayer;
            NextPlayer = players.Dequeue();
        }

        public void ResetPlayers()
        {
            var count = players.Count;
            for (int i = 0; i < count; i++)
            {
                var pop = players.Dequeue();
                pop.Score = 0;
                players.Enqueue(pop);
            }
        }
    }
}
