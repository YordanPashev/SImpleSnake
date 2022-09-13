namespace SimpleSnake.Renderer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Common;
    using GameObjects;
    using GameObjects.Foods;
    using SimpleSnake.Enums;

    public static class ConsoleRenderer
    {
        public static void AskUserForDifficultyLeve()
        {
            Console.SetCursorPosition(GlobalConstants.DifficultyLevelQuestionLeftX, GlobalConstants.DifficultyLevelQuestioTopY);
            Console.WriteLine("Select difficulty:");
            Console.SetCursorPosition(GlobalConstants.LevelsLeftX, GlobalConstants.LevelsTopY);
            Console.WriteLine("1. Easy");
            Console.SetCursorPosition(GlobalConstants.LevelsLeftX, GlobalConstants.LevelsTopY + 1);
            Console.WriteLine("2. Medium");
            Console.SetCursorPosition(GlobalConstants.LevelsLeftX, GlobalConstants.LevelsTopY + 2);
            Console.WriteLine("3. Hard");
            Console.SetCursorPosition(GlobalConstants.LevelsLeftX, GlobalConstants.LevelsTopY + 4);
        }

        public static void DisplayGameOver()
        {
            Console.SetCursorPosition(GlobalConstants.GameOverLeftX, GlobalConstants.GameOverTopY);
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Game Over");
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
        }

        public static void DisplayQuestionForOneMoreGame()
        {
            Console.SetCursorPosition(GlobalConstants.AskUserForOneMoreGameLeftX, GlobalConstants.AskUserForOneMoreGameTopY);
            Console.WriteLine("Do you want to play one more game?");
            Console.SetCursorPosition(GlobalConstants.AnswerLeftX, GlobalConstants.AnswerTopY);
            Console.WriteLine("1. Yes");
            Console.SetCursorPosition(GlobalConstants.AnswerLeftX, GlobalConstants.AnswerTopY + 1);
            Console.WriteLine("2. No");
            Console.SetCursorPosition(GlobalConstants.AnswerLeftX + 3, GlobalConstants.AnswerTopY + 3);
        }

        public static void VisualizeGameName()
        {
            Console.SetCursorPosition(GlobalConstants.NameLefXCursorPossition, GlobalConstants.NameTopYCursorPossition);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Simple Snake");
            Console.ForegroundColor = ConsoleColor.Black;


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

        public static void VisualizeDifficultyLevel(DifficultyLevel diffuciltyLevel)
        {
            Console.SetCursorPosition(GlobalConstants.DifficultyLevelLeftX, GlobalConstants.DifficultyLevelTopY);
            Console.Write($"Difficulty Level: {diffuciltyLevel}");
        }

        public static void VisualizeFoodInfo(Field field)
        {
            List<Food> foods = field.GetAllTypeOfFoods().OrderByDescending(f => f.Points).ToList();
            int leftX = GlobalConstants.FoodInfoLeftX;
            int topY = GlobalConstants.FoodInfoTopY;
            int foodInfoLine = 2;

            for (int i = 0; i < foods.Count; i++)
            {
                Console.SetCursorPosition(leftX, topY + foodInfoLine);
                Console.ForegroundColor = foods[i].Color;
                Console.Write(GlobalConstants.FoodSymbol);
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write($" -> {foods[i].Points} Points");
                foodInfoLine += 2;
            }
        }
    }
}
