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
        private readonly DifficultyLevel difficultyLevel;

        private double sleepTime;
        private double sleepDecrement;
        private Direction direction;
        private Random randomDirectionValue;

        private Engine()
        {
            this.pointOfDirection = new Point[GlobalConstants.DirectionsCount];
            this.sleepDecrement = GlobalConstants.SleepDecrement;
            this.randomDirectionValue = new Random();
        }

        public Engine(Field field, Snake snake, DifficultyLevel difficultyLevel)
            : this()
        {
            this.field = field;
            this.snake = snake;
            this.difficultyLevel = difficultyLevel;
        }

        public void Run()
        {
            InitializeDirections();
            SetDifficultyLevel(this.difficultyLevel);

            int initialDirection = randomDirectionValue.Next(0 , GlobalConstants.DirectionsCount);

            if (initialDirection == 1)
            {
                initialDirection = 0;
            }
            this.direction = (Direction)initialDirection;

            bool isSnakeAlive = true;

            while (isSnakeAlive)
            {
                if (Console.KeyAvailable)
                {
                    this.GetNextDirection();
                }

                Point direction = this.pointOfDirection[(int)this.direction];
                bool canSnakeMove = this.snake.TryToMakeAMove(direction);

                if (!canSnakeMove)
                {
                    ConsoleRenderer.DisplayGameOver();
                    GameOver();
                }

                if (sleepTime - sleepDecrement > GlobalConstants.MinSleepTime)
                {
                    sleepTime -= sleepDecrement;
                }

                Thread.Sleep((int)sleepTime);
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

            else if (userAnswer == "2")
            {
                Environment.Exit(0);
            }

            else
            {
                Console.Clear();
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

        private void SetDifficultyLevel(DifficultyLevel difficultyLevel)
        {
            if (difficultyLevel == DifficultyLevel.Easy)
            {
                this.sleepTime = GlobalConstants.EasyDiffinitialSleepTime;
            }

            else if (difficultyLevel == DifficultyLevel.Meadium)
            {
                this.sleepTime = GlobalConstants.MidiumDiffinitialSleepTime;
            }

            else if (difficultyLevel == DifficultyLevel.Hard)
            {
                this.sleepTime = GlobalConstants.HardDiffinitialSleepTime;
            }
        }
    }
}

