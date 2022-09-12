namespace SimpleSnake.Renderer
{
    using System;
    using System.Collections.Generic;

    using Common;
    using GameObjects;
    using GameObjects.Foods;

    public static class ConsoleRenderer
    {
        public static void VisualizeGameName()
        {
            Console.SetCursorPosition(GlobalConstants.NameLefXCursorPossition, GlobalConstants.NameTopYCursorPossition);
            Console.WriteLine("Simple Snake");
        }

        public static void VisualizePoint(int leftX, int topY, char symbol)
        {
            Console.SetCursorPosition(leftX, topY);
            Console.WriteLine(symbol);
        }

        public static void VisualizePlayerResult(int totalPoints)
        {
            Console.SetCursorPosition(GlobalConstants.ResultLeftX, GlobalConstants.ResultTopY);
            Console.Write($"Player points: {totalPoints}");
        }

        public static void VisualizeFoodInfo(Field field)
        {
            List<Food> foods = field.GetFoods();
            int leftX = GlobalConstants.FoodInfoLeftX;
            int topY = GlobalConstants.FoodInfoTopY;
            int foodInfoLine = 2;

            for (int i = 0; i < foods.Count; i++)
            {
                Console.SetCursorPosition(leftX, topY + foodInfoLine);
                Console.BackgroundColor = foods[i].Color;
                Console.Write(' ');
                Console.BackgroundColor = ConsoleColor.White;
                Console.Write($" -> {foods[i].Points} Points");
                foodInfoLine += 2;
            }
        }

        public static void DisplayQuestionForOneMoreGame()
        {
            Console.SetCursorPosition(GlobalConstants.GameOverLeftX, GlobalConstants.GameOverTopY);
            Console.WriteLine("Game Over");
            Console.SetCursorPosition(GlobalConstants.AskUserForOneMoreGameLeftX, GlobalConstants.AskUserForOneMoreGameTopY);
            Console.WriteLine("Do you want to play one more game?");
            Console.SetCursorPosition(GlobalConstants.AnswerLeftX, GlobalConstants.AnswerTopY);
            Console.WriteLine("1. Yes");
            Console.SetCursorPosition(GlobalConstants.AnswerLeftX, GlobalConstants.AnswerTopY + 1);
            Console.WriteLine("2. No");
            Console.SetCursorPosition(GlobalConstants.AnswerLeftX + 3, GlobalConstants.AnswerTopY + 3);
        }

        public static void DisplayGoodByeMessage()
        {
            Console.SetCursorPosition(GlobalConstants.GoodbyeMessageLeftX, GlobalConstants.GoodbyeMessageTopY);
            Console.WriteLine("Good Bye!");
            Console.SetCursorPosition(GlobalConstants.GoodbyeMessageLeftX, GlobalConstants.GoodbyeMessageTopY + 10);
        }
    }
}
