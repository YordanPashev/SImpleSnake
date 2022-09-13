namespace SimpleSnake.GameObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Common;
    using GameObjects.Foods;
    using Renderer;

    public class Snake
    {
        private readonly Field field;
        private readonly List<Food> foods;
        private readonly Queue<Point> snake;
        private readonly Random random;

        private const char snakeSymbol = '\u2588';
        private const char snakeHeadSymbol = ':';
        private const char empytSpace = ' ';
        private const ConsoleColor snakeBoddyColor = ConsoleColor.DarkGreen;
        private const ConsoleColor snakeHeadColor = ConsoleColor.Green;

        private int nextLeftX;
        private int nextTopY;
        private int foodIndex;
        private int totalPoints;
        private const int initialSnakePoints = 6;

        private Snake()
        {
            this.snake = new Queue<Point>();
            this.random = new Random();
        }

        public Snake(Field field)
            : this()
        {
            this.field = field;
            this.foods = field.GetAllTypeOfFoods();
            this.CreateSnake();
        }

        public bool TryToMakeAMove(Point direction)
        {
            Point currentSnakeHeadPossition = this.snake.Last();
            this.GetNextPoint(direction, currentSnakeHeadPossition);
            Point nextPositionOfSnakeHead = new Point(this.nextLeftX, this.nextTopY);

            if (!IsNextMovePossible(nextPositionOfSnakeHead))
            {
                return false;
            }

            DrawSnakeNextPosition(snake, currentSnakeHeadPossition, nextPositionOfSnakeHead, direction);

            return true;
        }

        private void DrawSnakeNextPosition(Queue<Point> snake, Point currentSnakeHeadPossition, Point nextPositionOfSnakeHead, Point direction)
        {
            bool isNextPositionFood = nextPositionOfSnakeHead.LeftX == this.foods[foodIndex].LeftX &&
                                      nextPositionOfSnakeHead.TopY == this.foods[foodIndex].TopY;

            if (isNextPositionFood)
            {
                this.Eat(direction, currentSnakeHeadPossition);
            }

            this.snake.Enqueue(nextPositionOfSnakeHead);

            Console.ForegroundColor = snakeBoddyColor;
            currentSnakeHeadPossition.Draw(snakeSymbol);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = snakeHeadColor;

            nextPositionOfSnakeHead.Draw(snakeHeadSymbol);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;

            Point snakeTail = this.snake.Dequeue();
            snakeTail.Draw(empytSpace);
        }

        public void DrawSnakeForInitialDirectionLeft(Point direction)
        {
            Point currentSnakeHeadPossition = this.snake.Last();
            this.GetNextPoint(direction, currentSnakeHeadPossition);
            Point nextPositionOfSnakeHead = new Point(this.nextLeftX, this.nextTopY);

            for (int i = 0; i < initialSnakePoints; i++)
            {
                DrawSnakeNextPosition(snake, currentSnakeHeadPossition, nextPositionOfSnakeHead, direction);
            }
        }

        private void CreateSnake()
        {
            int snakeSpawnLeftX = this.random.Next(GlobalConstants.InitialSnakeMinLeftX, GlobalConstants.InitialSnakeMaxLeftX);
            int snakeSpawnTopY = this.random.Next(GlobalConstants.InitialSnakeMinTopY, GlobalConstants.InitialSnakeMaxTopY);

            for (int currLeftX = snakeSpawnLeftX; currLeftX < snakeSpawnLeftX + initialSnakePoints; currLeftX++)
            {
                this.snake.Enqueue(new Point(currLeftX, snakeSpawnTopY));
            }

            this.foodIndex = this.field.CreateFood(this.snake);
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
                this.snake.Enqueue(newPoint);
                newPoint.Draw(snakeSymbol);

                this.GetNextPoint(direction, currentSnakeHead);
            }

            this.totalPoints += foodPoints;
            ConsoleRenderer.VisualizePlayerResult(this.totalPoints);

            this.foodIndex = this.field.CreateFood(this.snake);
        }

        private bool IsNextMovePossible(Point nextPositionOfSnakeHead)
        {
            if (this.snake.Any(p => p.LeftX == this.nextLeftX && p.TopY == nextTopY))
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
