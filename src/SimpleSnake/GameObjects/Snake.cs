namespace SimpleSnake.GameObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Common;
    using Enums;
    using GameObjects.Foods;
    using Renderer;

    public class Snake
    {
        private readonly Field field;
        private readonly List<Food> foods;
        private readonly Queue<Point> elements;
        private readonly Random random;
        private readonly DifficultyLevel difficultyLevel;

        private const char snakeBodySymbol = '\u2588';
        private const char snakeHeadSymbol = ':';
        private const char empytSpace = ' ';
        private const int initialSnakePoints = 6;
        private const ConsoleColor snakeBoddyColor = ConsoleColor.DarkGreen;
        private const ConsoleColor snakeHeadColor = ConsoleColor.Green;

        private int totalPoints;
        private int nextLeftX;
        private int nextTopY;
        private int foodIndex;

        private Snake()
        {
            this.elements = new Queue<Point>();
            this.random = new Random();
            this.totalPoints = GlobalConstants.InitialPlayerPoints;
        }

        public Snake(Field field, DifficultyLevel difficultyLevel)
            : this()
        {
            this.field = field;
            this.foods = field.GetAllTypeOfFoods();
            this.difficultyLevel = difficultyLevel;
            this.CreateSnake();
        }

        public int TotalPoints => this.totalPoints;

        public bool TryToMakeAMove(Point direction)
        {
            Point currentSnakeHeadPossition = this.elements.Last();
            this.GetNextPoint(direction, currentSnakeHeadPossition);
            Point nextPositionOfSnakeHead = new Point(this.nextLeftX, this.nextTopY);

            if (!IsNextMovePossible(nextPositionOfSnakeHead))
            {
                return false;
            }

            DrawSnakeNextPosition(this.elements, currentSnakeHeadPossition, nextPositionOfSnakeHead, direction);

            return true;
        }

        public void DrawSnakeForInitialDirectionLeft(Point direction)
        {
            Point currentSnakeHeadPossition = this.elements.Last();
            this.GetNextPoint(direction, currentSnakeHeadPossition);
            Point nextPositionOfSnakeHead = new Point(this.nextLeftX, this.nextTopY);

            for (int i = 0; i < initialSnakePoints; i++)
            {
                DrawSnakeNextPosition(this.elements, currentSnakeHeadPossition, nextPositionOfSnakeHead, direction);
            }
        }

        private void CreateSnake()
        {
            int snakeSpawnLeftX = this.random.Next(GlobalConstants.InitialSnakeMinLeftX, GlobalConstants.InitialSnakeMaxLeftX);
            int snakeSpawnTopY = this.random.Next(GlobalConstants.InitialSnakeMinTopY, GlobalConstants.InitialSnakeMaxTopY);

            for (int currLeftX = snakeSpawnLeftX; currLeftX < snakeSpawnLeftX + initialSnakePoints; currLeftX++)
            {
                this.elements.Enqueue(new Point(currLeftX, snakeSpawnTopY));
            }

            this.foodIndex = this.field.CreateFood(this.elements);
        }

        private void GetNextPoint(Point direction, Point snakeHead)
        {
            this.nextLeftX = snakeHead.LeftX + direction.LeftX;
            this.nextTopY = snakeHead.TopY + direction.TopY;
        }

        public void Eat(Point direction, Point currentSnakeHead)
        {
            int foodPoints = this.foods[this.foodIndex].Points;

            for (int i = 0; i < foodPoints; i++)
            {
                Point newPoint = new Point(this.nextLeftX, this.nextTopY);
                this.elements.Enqueue(newPoint);
                newPoint.Draw(snakeBodySymbol);

                this.GetNextPoint(direction, currentSnakeHead);
            }

            if (this.difficultyLevel == DifficultyLevel.Easy)
            {
                this.totalPoints += foodPoints;
            }

            else
            {
                this.totalPoints = CalculateTotalPoints(foodPoints);
            }

            ConsoleRenderer.VisualizePlayerResult(this.TotalPoints);

            this.foodIndex = this.field.CreateFood(this.elements);
        }

        private int CalculateTotalPoints(int foodPoints)
        {
            if (this.difficultyLevel == DifficultyLevel.Medium)
            {
                return this.totalPoints += (foodPoints * 2);
            }

            if (this.difficultyLevel == DifficultyLevel.Hard)
            {
                return this.totalPoints += (foodPoints * 3);
            }

            return this.totalPoints += foodPoints;
        }

        private void DrawSnakeNextPosition(Queue<Point> snake, Point currentSnakeHeadPossition, Point nextPositionOfSnakeHead, Point direction)
        {
            bool isNextPointFood = nextPositionOfSnakeHead.LeftX == this.foods[foodIndex].LeftX &&
                                      nextPositionOfSnakeHead.TopY == this.foods[foodIndex].TopY;

            if (isNextPointFood)
            {
                this.Eat(direction, currentSnakeHeadPossition);
            }

            this.elements.Enqueue(nextPositionOfSnakeHead);

            Console.ForegroundColor = snakeBoddyColor;
            currentSnakeHeadPossition.Draw(snakeBodySymbol);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = snakeHeadColor;

            nextPositionOfSnakeHead.Draw(snakeHeadSymbol);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;

            Point snakeTail = this.elements.Dequeue();
            snakeTail.Draw(empytSpace);
        }

        private bool IsNextMovePossible(Point nextPositionOfSnakeHead)
        {
            if (this.elements.Any(p => p.LeftX == this.nextLeftX && p.TopY == nextTopY))
            {
                return false;
            }

            if (this.field.IsPointOfWall(nextPositionOfSnakeHead))
            {
                return false;
            }

            return true;
        }
    }
}
