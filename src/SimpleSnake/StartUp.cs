namespace SimpleSnake
{
    using System;
    using System.IO;
    using System.Linq;

    using Newtonsoft.Json;

    using Common;
    using Core;
    using DataProcessor;
    using Enums;
    using GameObjects;
    using Renderer;
    using Utilities;

    public class StartUp
    {
        public static void Main()
        {
            ConsoleWindow.CustomizeConsole();
            DifficultyLevel difficultyLevel = GetDifficultyLevel();
            PlayerDto[] topThreePlayers = GetHighScoreList();

            Field field = CreateField();
            Snake snake = new Snake(field, difficultyLevel);

            ConsoleRenderer.VisualizeGameName();
            ConsoleRenderer.VisualizeDifficultyLevel(difficultyLevel);
            ConsoleRenderer.VisualizeHighScoreList(topThreePlayers);
            ConsoleRenderer.VisualizePlayerResult(GlobalConstants.InitialPlayerPoints);
            ConsoleRenderer.VisualizeFoodInfo(field);

            IEngine engine = new Engine(snake, difficultyLevel, topThreePlayers);
            engine.Run();
        }

        private static Field CreateField()
            => new Field(GlobalConstants.FieldWidth, GlobalConstants.FieldHeight);

        private static PlayerDto[] GetHighScoreList()
        {
            string jsonHighScoreList = File.ReadAllText(GlobalConstants.filePathOfHighScoreList);
            return JsonConvert.DeserializeObject<PlayerDto[]>(jsonHighScoreList)
                                                    .OrderByDescending(p => p.Score).ToArray();
        }

        private static DifficultyLevel GetDifficultyLevel()
        {
            ConsoleRenderer.VisualizeGameName();
            ConsoleRenderer.AskPlayerForDifficultyLeve();
            bool isInputValid = int.TryParse(Console.ReadLine(), out int levelValue) &&
                                Enum.IsDefined(typeof(DifficultyLevel), levelValue);

            if (!isInputValid)
            {
                StartUp.Main();
            }

            Console.Clear();
            DifficultyLevel level = (DifficultyLevel)levelValue;
            return level;
        }
    }
}
