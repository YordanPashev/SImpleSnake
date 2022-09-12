namespace SimpleSnake.Common
{
    public static class GlobalConstants
    {
        // Field
        public const int InitialFieldLeftXConsoleCursor = 80;
        public const int InitiaFieldlTopYConsoleCursor  = 3;

        public const int FieldWidth= 60;
        public const int FieldHeight = 20;

        // Walls Indices
        public const int WallStartLeftXIndex = InitialFieldLeftXConsoleCursor - 1;
        public const int WallEndLeftXIndex = 80 + FieldWidth;
        public const int WallStartTopYIndex = InitiaFieldlTopYConsoleCursor;
        public const int WallEndTopYIndex = FieldHeight + InitiaFieldlTopYConsoleCursor;

        // Wall Symbols
        public const char HorizontalWallSymbol = '\u2588';
        public const char VerticalWallSymbol = '\u2588';

        // Directions
        public const int DirectionsCount = 4;

        // Sleep Time
        public const double InitialSleepTime = 100;

        // Game Name
        public const int NameLefXCursorPossition = 104;
        public const int NameTopYCursorPossition = 1;

        // Player Info
        public const int ResultLeftX = 150;
        public const int ResultTopY = 5;

        // Food Info
        public const int FoodInfoLeftX = 150;
        public const int FoodInfoTopY = 7;

        // Ask User For One More Game
        public const int AskUserForOneMoreGameLeftX = 93;
        public const int AskUserForOneMoreGameTopY = 27;
        public const int AnswerLeftX = 105;
        public const int AnswerTopY = 29;

        // Goodbye Message
        public const int GoodbyeMessageLeftX = 104;
        public const int GoodbyeMessageTopY = 34;

        // Game Over Message
        public const int GameOverLeftX = 105;
        public const int GameOverTopY = 13;
    }
}
