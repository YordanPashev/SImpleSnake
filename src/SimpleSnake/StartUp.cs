namespace SimpleSnake
{
    using Core;
    using GameObjects;
    using Renderer;
    using SimpleSnake.Common;
    using Utilities;

    public class StartUp
    {
        public static void Main()
        {
            ConsoleWindow.CustomizeConsole();

            Field field = CreateField();
            Snake snake = new Snake(field);

            ConsoleRenderer.VisualizeGameName();
            ConsoleRenderer.VisualizePlayerResult(0);
            ConsoleRenderer.VisualizeFoodInfo(field);

            IEngine engine = new Engine(field, snake);
            engine.Run();
        }

        private static Field CreateField()
        {
            return new Field(GlobalConstants.FieldWidth, GlobalConstants.FieldHeight);
        }
    }
}
