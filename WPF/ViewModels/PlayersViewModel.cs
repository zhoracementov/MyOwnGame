using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WPF.Commands;
using WPF.Models;

namespace WPF.ViewModels
{
    public class PlayersViewModel : ViewModel
    {
        private ObservableCollection<Player> players;
        public ObservableCollection<Player> Players
        {
            get => players;
            set => Set(ref players, value);
        }

        private QuestionItem lastQuestion;
        public QuestionItem LastQuestion
        {
            get => lastQuestion;
            set => Set(ref lastQuestion, value);
        }

        private int currentIndex;
        public int CurrentIndex
        {
            get => currentIndex;
            set
            {
                var val = value % Players.Count;
                if (Set(ref currentIndex, val))
                {
                    //...
                }
            }
        }

        public ICommand AddNewPlayerCommand { get; }
        public ICommand RemovePlayerCommand { get; }
        public ICommand ResetPlayersCommand { get; }

        public PlayersViewModel()
        {
            ResetPlayersCommand = new RelayCommand(x => ResetPlayers());

            players = new ObservableCollection<Player>();

            var curr = new Player("Current", "Red");
            var prev = new Player("Previous", "Yellow");
            var next = new Player("Next", "Green");
            var loh = new Player("LOH", "Brown");

            players.Add(prev);
            players.Add(curr);
            players.Add(next);
            players.Add(loh);

            CurrentIndex = 1;
            //SwapPlayersQueue(CurrentIndex);
        }

        public void AddPlayer()
        {
            throw new NotImplementedException();
        }

        public void SuccessfullyAnswered(QuestionItem questionItem)
        {
            LastQuestion = questionItem;
            Players[CurrentIndex].Score += LastQuestion.Cost;

            //SwapPlayersQueue(CurrentIndex++);
            CurrentIndex++;
        }

        //public void SwapPlayersQueue(int index)
        //{
        //    PreviousPlayer = players[(index - 1) % players.Count];
        //    CurrentPlayer = players[index % players.Count];
        //    NextPlayer = players[(index + 1) % players.Count];
        //}

        public void ResetPlayers()
        {
            foreach (var player in Players)
            {
                player.Score = 0;
            }
        }
    }
}
