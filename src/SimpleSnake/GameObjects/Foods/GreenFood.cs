namespace SimpleSnake.GameObjects.Foods
{
    using System;

    internal class GreenFood : Food
    {
        private const ConsoleColor foodColor = ConsoleColor.Green;
        private const int foodPoints = 3;

        public GreenFood(Field field)
            : base(field, foodPoints, foodColor)
        {
        }
    }
}