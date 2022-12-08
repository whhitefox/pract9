namespace Pract9
{
    public class Position
    {
        public int x;
        public int y;

        public Position(int xParam, int yParam)
        {
            x = xParam;
            y = yParam;
        }

        public bool Ravni(Position other)
        {
            return x == other.x && y == other.y;
        }

        public static Position Random()
        {
            Random rand = new Random();
            int x = rand.Next(1, (int)Polya.Width - 1);
            int y = rand.Next(1, (int)Polya.Height - 1);
            return new Position(x, y);
        }
    }
}
