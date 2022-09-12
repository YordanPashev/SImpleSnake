namespace SimpleSnake.GameObjects.Foods
{
    using System;

    internal class MagentaFood : Food
    {
        private const ConsoleColor foodColor = ConsoleColor.Magenta;
        private const int foodPoints = 2;

        public MagentaFood(Field field)
            : base(field, foodPoints, foodColor)
        {
        }
    }
}
