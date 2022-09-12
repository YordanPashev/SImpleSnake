namespace SimpleSnake.GameObjects.Foods
{
    using System;

    public class RedFood : Food
    {
        private const ConsoleColor foodColor = ConsoleColor.Red;
        private const int foodPoints = 1;

        public RedFood(Field field) 
            : base(field, foodPoints, foodColor) 
        { 
        }
    }
}
