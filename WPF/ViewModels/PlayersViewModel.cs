using System.Collections.ObjectModel;
using WPF.Extenstions.EnumerablePickExtentions;
using WPF.Models;

namespace WPF.ViewModels
{
    public class PlayersViewModel : ViewModel
    {
        private ObservableCollection<Player> players;
        public ObservableCollection<Player> Players //все игроки
        {
            get => players;
            set => Set(ref players, value);
        }

        private Player currentPlayer;
        public Player CurrentPlayer //текущий игрок, который отвечает
        {
            get => currentPlayer;
            set => Set(ref currentPlayer, value);
        }

        public PlayersViewModel()
        {
            players = new ObservableCollection<Player>();  //создана пустая коллекция
        }

        public void GameStarts()
        {
            Players = new ObservableCollection<Player>(Players.ShakeAll());
            CurrentPlayer = Players[0];
            ResetScores();
            players.RemoveAt(0);  //смещение очереди после назначения текущего
        }

        public void GameEnds()
        {
            players.Add(CurrentPlayer);
            CurrentPlayer = null;
        }

        public void SuccessfullyAnswered(QuestionItem questionItem = null)
        {
            if (questionItem != null)
                CurrentPlayer.Score += questionItem.Cost;

            Players.Add(CurrentPlayer);
            CurrentPlayer = Players[0];
            Players.RemoveAt(0);
        }


        public void ResetScores()
        {
            foreach (var player in Players)
            {
                player.Score = 0;
            }
        }
    }
}
