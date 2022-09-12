namespace SimpleSnake.Core
{
    using System;
    using System.Threading;

    using GameObjects;
    using Common;
    using Enums;
    using Renderer;

    public class Engine : IEngine
    {
        private readonly Point[] pointOfDirection;
        private readonly Snake snake;
        private readonly Field field;

        private Direction direction;
        private double sleepTime;

        private Engine()
        {
            this.sleepTime = GlobalConstants.InitialSleepTime;
            this.pointOfDirection = new Point[GlobalConstants.DirectionsCount];
        }

        public Engine(Field field, Snake snake)
            : this()
        {
            this.field = field;
            this.snake = snake;
        }

        public void Run()
        {
            InitializeDirections();
            bool isSnakeAlive = true;

            while (isSnakeAlive)
            {
                if (Console.KeyAvailable)
                {
                    this.GetNextDirection();
                }

                Point direction = this.pointOfDirection[(int)this.direction];       
                bool isSnakeMoved = this.snake.TryToMakeAMove(direction);

                if (!isSnakeMoved)
                {
                    GameOver();
                }

                sleepTime -= 0.01;
                Thread.Sleep((int)this.sleepTime);
            }
        }

        private void GameOver()
        {
            ConsoleRenderer.DisplayQuestionForOneMoreGame();
            string userAnswer = Console.ReadLine();

            if (userAnswer == "1")
            {
                Console.Clear();
                StartUp.Main();
            }

            else if(userAnswer == "2")
            {
                ConsoleRenderer.DisplayGoodByeMessage();
                Environment.Exit(0);
            }

            else
            {
                GameOver();
            }
        }

        private void GetNextDirection()
        {
            ConsoleKeyInfo userInput = Console.ReadKey();

            if (userInput.Key == ConsoleKey.LeftArrow && 
                direction != Direction.Right)
            {
                direction = Direction.Left;
            }

            else if (userInput.Key == ConsoleKey.RightArrow &&
                     direction != Direction.Left)
            {
                direction = Direction.Right;
            }

            else if (userInput.Key == ConsoleKey.UpArrow &&
                     direction != Direction.Down)
            {
                direction = Direction.Up;
            }

            else if (userInput.Key == ConsoleKey.DownArrow &&
                     direction != Direction.Up)
            {
                direction = Direction.Down;
            }

            Console.CursorVisible = false;
        }

        private void InitializeDirections()
        {
            this.pointOfDirection[0] = new Point(1, 0);
            this.pointOfDirection[1] = new Point(-1, 0);
            this.pointOfDirection[2] = new Point(0, 1);
            this.pointOfDirection[3] = new Point(0, -1);
        }
    }
}
