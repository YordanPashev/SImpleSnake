namespace SimpleSnake
{
    using System;

    using Core;
    using GameObjects;
    using Renderer;
    using SimpleSnake.Common;
    using SimpleSnake.Enums;
    using Utilities;

    public class StartUp
    {
        public static void Main()
        {
            ConsoleWindow.CustomizeConsole();
            DifficultyLevel difficultyLevel = GetDifficultyLevel();

            Field field = CreateField();
            Snake snake = new Snake(field);

            ConsoleRenderer.VisualizeGameName();
            ConsoleRenderer.VisualizeDifficultyLevel(difficultyLevel);
            ConsoleRenderer.VisualizePlayerResult(GlobalConstants.InitialPlayerPoints);
            ConsoleRenderer.VisualizeFoodInfo(field);

            IEngine engine = new Engine(field, snake, difficultyLevel);
            engine.Run();
        }

        private static Field CreateField()
            => new Field(GlobalConstants.FieldWidth, GlobalConstants.FieldHeight);

        public static DifficultyLevel GetDifficultyLevel()
        {
            ConsoleRenderer.VisualizeGameName();
            ConsoleRenderer.AskUserForDifficultyLeve();
            bool isInputValid = int.TryParse(Console.ReadLine(), out int levelValue) &&
                                Enum.IsDefined(typeof(DifficultyLevel), levelValue);

            if (!isInputValid)
            {
                Console.Clear();
                GetDifficultyLevel();
            }

            Console.Clear();
            DifficultyLevel level = (DifficultyLevel)levelValue;
            return level;
        }
    }
}
