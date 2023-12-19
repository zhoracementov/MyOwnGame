using System.Collections.ObjectModel;
using WPF.Extenstions.EnumerablePickExtentions;
using WPF.Models;

namespace WPF.ViewModels
{
    public class PlayersViewModel : ViewModel
    {
        private ObservableCollection<Player> players;
        public ObservableCollection<Player> Players
        {
            get => players;
            set
            {
                var list = new ObservableCollection<Player>(value.ShakeAll());
                if (Set(ref players, list))
                {

                }
            }
        }

        public PlayersViewModel()
        {
            Players = new ObservableCollection<Player>();
        }

        public void GameStart()
        {
            Players = new ObservableCollection<Player>(Players.ShakeAll());
            ResetScores();
        }

        public void SuccessfullyAnswered(QuestionItem questionItem = null)
        {
            var pop = players[0];

            if (questionItem != null)
                pop.Score += questionItem.Cost;

            Players.RemoveAt(0);
            players.Add(pop);
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
