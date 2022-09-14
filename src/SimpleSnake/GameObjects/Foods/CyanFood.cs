namespace SimpleSnake.GameObjects.Foods
{
    using System;

    public class CyanFood : Food
    {
        private const ConsoleColor foodColor = ConsoleColor.Cyan;
        private const char symbol = '\u25CF';
        private const int points = 2;

        public CyanFood(Field field)
            : base(field, points, foodColor, symbol)
        {
        }

        public char FoodSymbol => symbol;
    }
}