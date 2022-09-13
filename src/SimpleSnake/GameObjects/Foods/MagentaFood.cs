namespace SimpleSnake.GameObjects.Foods
{
    using System;

    public class MagentaFood : Food
    {
        private const ConsoleColor foodColor = ConsoleColor.Magenta;
        private const int foodPoints = 3;

        public MagentaFood(Field field)
            : base(field, foodPoints, foodColor)
        {
        }
    }
}
