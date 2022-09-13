namespace SimpleSnake.Core
{
    using System;
    using System.IO;
    using System.Threading;

    using Newtonsoft.Json;

    using Common;
    using DataProcessor;
    using Enums;
    using GameObjects;
    using Renderer;

    public class Engine : IEngine
    {
        private readonly Point[] pointOfDirection;
        private readonly Snake snake;
        private readonly DifficultyLevel difficultyLevel;

        private Direction direction;
        private Random randomDirectionValue;
        private PlayerDto[] topThreePlayers;
        private int surpassedPlayerIndex;
        private double sleepTime;
        private double sleepDecrement;
        private string playerName;


        private Engine()
        {
            this.pointOfDirection = new Point[GlobalConstants.DirectionsCount];
            this.sleepDecrement = GlobalConstants.SleepDecrement;
            this.randomDirectionValue = new Random();
        }

        public Engine(Snake snake, DifficultyLevel difficultyLevel, PlayerDto[] topThreePlayers)
            : this()
        {
            this.snake = snake;
            this.difficultyLevel = difficultyLevel;
            this.topThreePlayers = topThreePlayers;
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

        private void AskPlayerForOneMoreGame()
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
                AskPlayerForOneMoreGame();
            }
        }

        private void GameOver()
        {
            if (IsPlayerSetARecord(this.snake.TotalPoints, this.topThreePlayers))
            {
                GetPlayerName();

                int playerRank = topThreePlayers[surpassedPlayerIndex].Rank;
                int playerScore = this.snake.TotalPoints;
                this.topThreePlayers[surpassedPlayerIndex] = new PlayerDto
                {
                    Rank = playerRank,
                    Name = this.playerName,
                    Score = playerScore,
                };

                string newJsonHighScoreList = JsonConvert.SerializeObject(this.topThreePlayers, Formatting.Indented);
                File.WriteAllText(GlobalConstants.filePathOfHighScoreList, newJsonHighScoreList);
            }

            AskPlayerForOneMoreGame();
        }

        private void GetPlayerName()
        {
            ConsoleRenderer.AskPlayerToFillHisName();
            string playerName = Console.ReadLine();

            if (IsPlayerNameValid(playerName))
            {
                 this.playerName = playerName;           
            }

            else
            {
                ConsoleRenderer.InvalidNameMessage();
                GetPlayerName();
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

        private bool IsPlayerNameValid(string playerName)
            => !string.IsNullOrWhiteSpace(playerName) && playerName.Length < 20;

        private bool IsPlayerSetARecord(int totalScore, PlayerDto[] topThreePlayers)
        {
            if (totalScore == 0)
            {
                return false;
            }

            for(int currPlayerIndex = 0; currPlayerIndex < topThreePlayers.Length; currPlayerIndex++)
            {
                if (topThreePlayers[currPlayerIndex].Score < totalScore)
                {
                    this.surpassedPlayerIndex = currPlayerIndex;
                    return true;
                }
            }

            return false;
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

            else if (difficultyLevel == DifficultyLevel.Medium)
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

