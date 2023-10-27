using System.Collections.Generic;
using System.Linq;
using WPF.Models;

namespace WPF.Services
{
    public class PlayerRouletteService
    {
        private readonly Queue<Player> players;

        public Player Current { get; set; }

        public PlayerRouletteService()// : this(Enumerable.Empty<Player>())
        {
            players = new Queue<Player>();

            var pl1 = new Player("First", "Red");
            var pl2 = new Player("Second", "Blue");
            var pl3 = new Player("First 2", "Red");
            var pl4 = new Player("Second 2", "Blue");

            //players = new Queue<Player>();
            players.Enqueue(pl1);
            players.Enqueue(pl2);
            players.Enqueue(pl3);
            players.Enqueue(pl4);

            Current = players.Peek();
        }

        public void Add(Player player)
        {
            players.Enqueue(player);
        }

        public void Move()
        {
            Current = players.Dequeue();
            players.Enqueue(Current);
        }

        public void AddScore(int cost)
        {
            Current.Score += cost;
        }

        public IEnumerable<Player> GetPlayersSortedByScore()
        {
            return players.OrderBy(x => x.Score).ThenBy(x => x.Name);
        }
    }
}
