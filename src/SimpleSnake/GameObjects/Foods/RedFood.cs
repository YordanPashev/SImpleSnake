namespace SimpleSnake.GameObjects.Foods
{
    using System;

    public class RedFood : Food
    {
        private const ConsoleColor foodColor = ConsoleColor.Red;
        private const char symbol = '\u25AA';
        private const int points = 1;


        public RedFood(Field field) 
            : base(field, points, foodColor, symbol) 
        { 
        }

        public char FoodSymbol => symbol;
    }
}
