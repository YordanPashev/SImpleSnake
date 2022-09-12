namespace SimpleSnake.GameObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Common;
    using GameObjects.Foods;
    using SimpleSnake.Renderer;

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
        private const int initialSnakePoints = 6;    

        private Snake()
        {
            snake = new Queue<Point>();
            this.random = new Random();
        }

        public Snake(Field field)
            : this()   
        {
            this.field = field;
            this.foods = field.GetFoods();
            this.CreateSnake();
        }

        public bool TryToMakeAMove(Point direction)
        {
            Point currentSnakeHeadPossition = this.snake.Last();
            this.GetNextPoint(direction, currentSnakeHeadPossition);
            Point nextPositionOfSnakeHead = new Point(this.nextLeftX, this.nextTopY);

            if (!isNextMovePossible(nextPositionOfSnakeHead))
            {
                field.SetVerticalLine(0);
                field.SetVerticalLine(GlobalConstants.FieldWidth - 1);

                return false;
            }

            if (nextPositionOfSnakeHead.LeftX == foods[foodIndex].LeftX && 
                nextPositionOfSnakeHead.TopY == foods[foodIndex].TopY)
            {
                this.Eat(direction, currentSnakeHeadPossition);
            }

            this.snake.Enqueue(nextPositionOfSnakeHead);
            nextPositionOfSnakeHead.Draw(snakeSymbol);
            Point snakeTail = this.snake.Dequeue();
            snakeTail.Draw(empytSpace);


            return true;
        }

        private bool isNextMovePossible(Point nextPositionOfSnakeHead)
        {
            if(this.snake.Any(p => p.LeftX == this.nextLeftX && p.TopY == nextTopY))
            {
                return false;
            }

            if (this.field.IsPointOfWall(nextPositionOfSnakeHead))
            {
                return false;
            }

            return true;
        }

        private void CreateSnake()
        {
            int snakeSpawnTopY = random.Next(GlobalConstants.InitiaFieldlTopYConsoleCursor  + 5, GlobalConstants.InitiaFieldlTopYConsoleCursor  + 10);
            int snakeSpawnLeftX = random.Next(GlobalConstants.InitialFieldLeftXConsoleCursor + 25, GlobalConstants.InitialFieldLeftXConsoleCursor + 35);

            for (int currLeftX = snakeSpawnLeftX; currLeftX < snakeSpawnLeftX + initialSnakePoints; currLeftX++)
            {
                this.snake.Enqueue(new Point(currLeftX, snakeSpawnTopY));
            }

            foodIndex = this.field.CreateFood(this.snake, this.foodIndex, this.foods);
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

            foodIndex = this.field.CreateFood(this.snake, this.foodIndex, this.foods);
        }
    }
}
