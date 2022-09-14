namespace SimpleSnake.GameObjects.Foods
{
    using System;

    public class MagentaFood : Food
    {
        private const ConsoleColor foodColor = ConsoleColor.Magenta;
        private const char symbol = '\u25A0';
        private const int points = 3;

        public MagentaFood(Field field)
            : base(field, points, foodColor, symbol)
        {
        }

        public char FoodSymbol => symbol;
    }
}
