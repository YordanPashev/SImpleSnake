namespace SimpleSnake.GameObjects.Foods
{
    using System;

    internal class DarkYellowFood : Food
    {
        private const ConsoleColor foodColor = ConsoleColor.DarkYellow;
        private const int foodPoints = 2;

        public DarkYellowFood(Field field)
            : base(field, foodPoints, foodColor)
        {
        }
    }
}