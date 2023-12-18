using System;
using System.Collections.Generic;
using System.Windows.Input;
using WPF.Commands;
using WPF.Models;

namespace WPF.ViewModels
{
    public class PlayersViewModel : ViewModel
    {
        private readonly List<Player> players;
        private int currentPlayerIndex;

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

        public IEnumerable<Player> Players => players;

        public ICommand AddNewPlayerCommand { get; }
        public ICommand RemovePlayerCommand { get; }
        public ICommand ResetPlayersCommand { get; }

        public PlayersViewModel()
        {
            ResetPlayersCommand = new RelayCommand(x => ResetPlayers());

            players = new List<Player>();

            var curr = new Player("Current", "Red");
            var prev = new Player("Previous", "Yellow");
            var next = new Player("Next", "Green");
            var loh = new Player("LOH", "Brown");

            players.Add(prev);
            players.Add(curr);
            players.Add(next);
            players.Add(loh);

            currentPlayerIndex = 1;
            SwapPlayersQueue(currentPlayerIndex);
        }

        public void AddPlayer()
        {
            throw new NotImplementedException();
        }

        public void SuccessfullyAnswered(QuestionItem questionItem)
        {
            LastQuestion = questionItem;
            CurrentPlayer.Score += LastQuestion.Cost;

            SwapPlayersQueue(currentPlayerIndex++);
        }

        public void SwapPlayersQueue(int index)
        {
            PreviousPlayer = players[(index - 1) % players.Count];
            CurrentPlayer = players[index % players.Count];
            NextPlayer = players[(index + 1) % players.Count];
        }

        public void ResetPlayers()
        {
            foreach (var player in players)
            {
                player.Score = 0;
            }
        }
    }
}
