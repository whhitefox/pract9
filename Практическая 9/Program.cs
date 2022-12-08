namespace Pract9
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            while (true)
            {
                Snake snake = new Snake();
                snake.StartGame();
            }
        }
    }
}