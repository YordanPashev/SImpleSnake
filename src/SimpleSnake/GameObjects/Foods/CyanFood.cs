namespace SimpleSnake.GameObjects.Foods
{
    using System;

    public class CyanFood : Food
    {
        private const ConsoleColor foodColor = ConsoleColor.Cyan;
        private const int foodPoints = 2;

        public CyanFood(Field field)
            : base(field, foodPoints, foodColor)
        {
        }
    }
}