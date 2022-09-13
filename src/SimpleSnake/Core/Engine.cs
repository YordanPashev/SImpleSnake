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
        private readonly DifficultyLevel difficultyLevel;

        private Direction direction;
        private Random randomDirectionValue;

        private double sleepTime;
        private double sleepDecrement;

        private Engine()
        {
            this.pointOfDirection = new Point[GlobalConstants.DirectionsCount];
            this.sleepDecrement = GlobalConstants.SleepDecrement;
            this.randomDirectionValue = new Random();
        }

        public Engine(Snake snake, DifficultyLevel difficultyLevel)
            : this()
        {
            this.snake = snake;
            this.difficultyLevel = difficultyLevel;
        }

        public void Run()
        {
            InitializeDirections();
            SetDifficultyLevel(this.difficultyLevel);

            int initialDirectionIndex = randomDirectionValue.Next(0 , GlobalConstants.DirectionsCount);
            this.direction = (Direction)initialDirectionIndex;

            if (this.direction == Direction.Left)
            {
                this.snake.DrawSnakeForInitialDirectionLeft(this.pointOfDirection[(int)this.direction]);
            }

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
                    ConsoleRenderer.DisplayGameOver();
                    GameOver();
                }

                if (this.sleepTime - this.sleepDecrement > GlobalConstants.MinSleepTime)
                {
                    this.sleepTime -= this.sleepDecrement;
                }

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
                this.direction != Direction.Right)
            {
                this.direction = Direction.Left;
            }

            else if (userInput.Key == ConsoleKey.RightArrow &&
                     this.direction != Direction.Left)
            {
                this.direction = Direction.Right;
            }

            else if (userInput.Key == ConsoleKey.UpArrow &&
                     this.direction != Direction.Down)
            {
                this.direction = Direction.Up;
            }

            else if (userInput.Key == ConsoleKey.DownArrow &&
                     this.direction != Direction.Up)
            {
                this.direction = Direction.Down;
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

