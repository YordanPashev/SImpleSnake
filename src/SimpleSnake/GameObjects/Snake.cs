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

        private int nextLeftX;
        private int nextTopY;
        private int foodIndex;
        private int totalPoints;

        private const char snakeSymbol = '\u25CF';
        private const char empytSpace = ' ';
        private const ConsoleColor snakeColor = ConsoleColor.DarkGreen;
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

            if (!isNextMovePossible(nextPositionOfSnakeHead))
            {
                return false;
            }

            if (nextPositionOfSnakeHead.LeftX == this.foods[foodIndex].LeftX &&
                nextPositionOfSnakeHead.TopY == this.foods[foodIndex].TopY)
            {
                this.Eat(direction, currentSnakeHeadPossition);
            }

            this.snake.Enqueue(nextPositionOfSnakeHead);
            Console.ForegroundColor = snakeColor;
            nextPositionOfSnakeHead.Draw(snakeSymbol);
            Console.ForegroundColor = ConsoleColor.Black;
            Point snakeTail = this.snake.Dequeue();
            snakeTail.Draw(empytSpace);

            return true;
        }

        public void DrawSnakeForInitialDirectionLeft(Point direction)
        {
            for (int i = 0; i < 6; i++)
            {
                Point currentSnakeHeadPossition = this.snake.Last();
                this.GetNextPoint(direction, currentSnakeHeadPossition);
                Point nextPositionOfSnakeHead = new Point(this.nextLeftX, this.nextTopY);

                if (nextPositionOfSnakeHead.LeftX == this.foods[foodIndex].LeftX &&
                    nextPositionOfSnakeHead.TopY == this.foods[foodIndex].TopY)
                {
                    this.Eat(direction, currentSnakeHeadPossition);
                }

                this.snake.Enqueue(nextPositionOfSnakeHead);
                Console.ForegroundColor = snakeColor;
                nextPositionOfSnakeHead.Draw(snakeSymbol);
                Console.BackgroundColor = ConsoleColor.White;
                Point snakeTail = this.snake.Dequeue();
                snakeTail.Draw(empytSpace);
            }
        }

        private void CreateSnake()
        {
            int snakeSpawnTopY = this.random.Next(GlobalConstants.InitiaFieldlTopYConsoleCursor + 5, GlobalConstants.InitiaFieldlTopYConsoleCursor + 10);
            int snakeSpawnLeftX = this.random.Next(GlobalConstants.InitialFieldLeftXConsoleCursor + 25, GlobalConstants.InitialFieldLeftXConsoleCursor + 35);

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

        private bool isNextMovePossible(Point nextPositionOfSnakeHead)
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
