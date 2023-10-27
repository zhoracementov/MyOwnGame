namespace WPF.Models
{
    public class Player
    {
        public string Name { get; set; }
        public string BrushColor { get; set; }
        public int Score { get; set; }

        public Player(string name, string brushColor)
        {
            Name = name;
            BrushColor = brushColor;
        }
    }
}
