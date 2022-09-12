namespace SimpleSnake.GameObjects.Foods
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Common;

    public abstract class Food : Point
    {
        private readonly Random random;
        private readonly Field field;

        protected Food(Field field, int foodPoints, ConsoleColor foodColor) 
            : base(field.LeftX, field.TopY)
        {
            this.field = field;
            this.Points = foodPoints;
            this.Color = foodColor;

            this.random = new Random();
        }

        public int Points { get; private set; }

        public ConsoleColor Color { get; private set; }

        public void SetRandomPosition(Queue<Point> snake)
        {
            do
            {
                this.LeftX = random.Next(GlobalConstants.InitialFieldLeftXConsoleCursor + 1, field.LeftX + GlobalConstants.InitialFieldLeftXConsoleCursor - 1);
                this.TopY = random.Next(GlobalConstants.InitiaFieldlTopYConsoleCursor  + 1, field.TopY + GlobalConstants.InitiaFieldlTopYConsoleCursor  - 1);
            } while(snake.Any(s => s.LeftX == this.LeftX && s.TopY == this.TopY));

            Console.ForegroundColor = this.Color;
            this.Draw(GlobalConstants.FoodSymbol);
            Console.ForegroundColor = ConsoleColor.Black;
        }

        public bool IsFoodPoint(Point sneakHead)
            => this.LeftX == sneakHead.LeftX && this.TopY == sneakHead.TopY;
    }
}
