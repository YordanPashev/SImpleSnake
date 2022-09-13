namespace SimpleSnake.Renderer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Common;
    using DataProcessor;
    using Enums;
    using GameObjects;
    using GameObjects.Foods;

    public static class ConsoleRenderer
    {
        public static void AskPlayerToFillHisName()
        {
            Console.SetCursorPosition(GlobalConstants.NewHighScoreLeftX, GlobalConstants.NewHighScoreTopY);
            Console.WriteLine("You set a new record! Please fill your name (20 symbols max):");
            Console.SetCursorPosition(GlobalConstants.NewHighScoreLeftX, GlobalConstants.NewHighScoreTopY + 1);
            Console.WriteLine(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(GlobalConstants.NewHighScoreLeftX, GlobalConstants.NewHighScoreTopY + 1);
        }

        public static void AskPlayerForDifficultyLeve()
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
            Console.WriteLine(new string(' ', Console.WindowWidth));
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
            Console.WriteLine("Press 1 to play");
            Console.SetCursorPosition(GlobalConstants.AnswerLeftX, GlobalConstants.AnswerTopY + 1);
            Console.WriteLine("Press 2 for Exit");
            Console.SetCursorPosition(GlobalConstants.AnswerLeftX + 3, GlobalConstants.AnswerTopY + 3);
            Console.WriteLine(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(GlobalConstants.AnswerLeftX + 3, GlobalConstants.AnswerTopY + 3);
        }

        public static void InvalidNameMessage()
        {
            Console.SetCursorPosition(GlobalConstants.NewHighScoreLeftX + 10, GlobalConstants.NewHighScoreTopY - 1);
            Console.WriteLine("Invalid name! (must be 20 characters max)");
        }

        public static void VisualizeGameName()
        {
            Console.SetCursorPosition(GlobalConstants.NameLefXCursorPossition, GlobalConstants.NameTopYCursorPossition);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Simple Snake");
            Console.ForegroundColor = ConsoleColor.Black;
        }

        public static void VisualizeHighScoreList(PlayerDto[] highScoreList)
        {
            int lineCounter = 0;
            Console.SetCursorPosition(GlobalConstants.Top3HighScoreLeftX + 5, GlobalConstants.Top3HighScoreTopY + lineCounter++);
            Console.WriteLine("TOP 3");

            foreach (var player in highScoreList)
            {
                if (player.Name == null || player.Score == 0)
                {
                    continue;
                }

                Console.SetCursorPosition(GlobalConstants.Top3HighScoreLeftX, GlobalConstants.Top3HighScoreTopY + ++lineCounter);
                Console.WriteLine($"{player.Rank}. {player.Name} - {player.Score}");
            }
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
            if (diffuciltyLevel == DifficultyLevel.Easy)
            {
                Console.Write($"Difficulty Level: {diffuciltyLevel}");
            }
            else
            {
                Console.Write(@$"Difficulty Level: {diffuciltyLevel} - Points x{(diffuciltyLevel == DifficultyLevel.Medium 
                                                                                ? GlobalConstants.MidiumDiffPointsMultiplier
                                                                                : GlobalConstants.HardDiffPointsMultiplier)}");
            }
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
