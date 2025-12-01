namespace AOC.Day01.Models
{
    public class TurnAction
    {
        public TurnAction(char direction, int distance)
        {
            Direction = direction;
            Distance = distance;
        }
        public char Direction { get; set; }
        public int Distance { get; set; }
    }
}
