namespace Pract9
{
    public class Snake
    {
        private List<Position> body = new List<Position>();
        private Position fruit;
        private int napravlenie = 0;
        private bool isAlive = true;
        private bool isWin = false;
        private int maxLength;

        public Snake()
        {
            Console.Clear();
            maxLength = ((int)Polya.Width - 2) * ((int)Polya.Height - 2);
            int head_x = (int)Polya.Width / 2;
            int head_y = (int)Polya.Height / 2;
            body.Add(new Position(head_x, head_y));
            body.Add(new Position(head_x, head_y + 1));
            GenerateFruit();
            DrawPolya();
            DrawSnake(body);
            DrawFruit();
        }

        public void StartGame()
        {
            isAlive = true;
            isWin = false;
            ConsoleKeyInfo key = Console.ReadKey();
            Thread thread = new Thread(new ThreadStart(StartDrawing));
            thread.Start();
            while (isAlive && !isWin)
            {
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (napravlenie != 2)
                        {
                            napravlenie = 0;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (napravlenie != 3)
                        {
                            napravlenie = 1;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (napravlenie != 0)
                        {
                            napravlenie = 2;
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (napravlenie != 1)
                        {
                            napravlenie = 3;
                        }
                        break;
                }

                key = Console.ReadKey();
            }
        }

        private void DrawPolya()
        {
            for (int i = 0; i < (int)Polya.Height; i++)
            {
                for (int j = 0; j < (int)Polya.Width; j++)
                {
                    if (i == 0 || j == 0 || i == (int)Polya.Height - 1 || j == (int)Polya.Width - 1)
                    {
                        Console.SetCursorPosition(j, i);
                        Console.Write("#");
                    }
                }
            }
        }

        private void DrawSnake(List<Position> old)
        {
            foreach (var elem in old)
            {
                Console.SetCursorPosition(elem.x, elem.y);
                Console.Write(" ");
            }
            foreach (var elem in body)
            {
                Console.SetCursorPosition(elem.x, elem.y);
                Console.Write("*");
            }
        }

        private void StartDrawing()
        {
            var oldBody = CopyBody();
            while (isAlive && !isWin)
            {
                DrawSnake(oldBody);
                oldBody = CopyBody();
                Move();
                Thread.Sleep(100);
            }
        }

        private void DrawFruit()
        {
            Console.SetCursorPosition(fruit.x, fruit.y);
            Console.Write("+");
        }

        private void Move()
        {
            var head = body[0];
            Position new_head;
            switch (napravlenie)
            {
                case 0:
                    new_head = new Position(head.x, head.y - 1);
                    break;
                case 1:
                    new_head = new Position(head.x + 1, head.y);
                    break;
                case 2:
                    new_head = new Position(head.x, head.y + 1);
                    break;
                case 3:
                    new_head = new Position(head.x - 1, head.y);
                    break;
                default:
                    new_head = new Position(head.x, head.y);
                    break;
            }
            if (new_head.x < 1 || new_head.x > (int)Polya.Width - 2 || new_head.y < 1 || new_head.y > (int)Polya.Height - 2 || PositionInSnake(new_head))
            {
                isAlive = false;
                return;
            }
            body.Insert(0, new_head);
            if (new_head.Ravni(fruit))
            {
                GenerateFruit();
                DrawFruit();
                if (body.Count == maxLength)
                {
                    isWin = true;
                    return;
                }
            } else
            {
                body.RemoveAt(body.Count - 1);
            }
        }

        private void GenerateFruit()
        {
            Position fuit_position = Position.Random();
            while (PositionInSnake(fuit_position)) {
                fuit_position = Position.Random();
            }
            fruit = fuit_position;
        }

        private bool PositionInSnake(Position position)
        {
            foreach (var elem in body)
            {
                if (elem.Ravni(position))
                {
                    return true;
                }
            }

            return false;
        }

        private List<Position> CopyBody()
        {
            List<Position> copy = new List<Position>();
            foreach (var elem in body)
            {
                copy.Add(new Position(elem.x, elem.y));
            }
            return copy;
        }
    }
}
