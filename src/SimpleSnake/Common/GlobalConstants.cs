namespace SimpleSnake.Common
{
    public static class GlobalConstants
    {
        // Ask User For One More Game
        public const int AnswerLeftX = 105;
        public const int AnswerTopY = 29;
        public const int AskUserForOneMoreGameLeftX = 93;
        public const int AskUserForOneMoreGameTopY = 27;  

        // Difficulty Level
        public const int DifficultyLevelQuestionLeftX = 101;
        public const int DifficultyLevelQuestioTopY = 4;
        public const int LevelsLeftX = 106;
        public const int LevelsTopY = 6;

        // Directions
        public const int DirectionsCount = 4;

        // Field
        public const int FieldWidth = 60;
        public const int FieldHeight = 20;
        public const int InitialFieldLeftXConsoleCursor = 80;
        public const int InitiaFieldlTopYConsoleCursor  = 3;

        // Game Name
        public const int NameLefXCursorPossition = 104;
        public const int NameTopYCursorPossition = 1;

        // Game Over Message
        public const int GameOverLeftX = 105;
        public const int GameOverTopY = 13;

        // Player Info
        public const int DifficultyLevelLeftX = 150;
        public const int DifficultyLevelTopY = 5;
        public const int FoodInfoLeftX = 150;
        public const int FoodInfoTopY = 11;
        public const int InitialPlayerPoints = 0;
        public const int ResultLeftX = 150;
        public const int ResultTopY = 9;

        // SleepTime
        public const int EasyDiffinitialSleepTime = 200;
        public const int HardDiffinitialSleepTime = 100;
        public const int MidiumDiffinitialSleepTime = 150;
        public const double MinSleepTime = 20;
        public const double SleepDecrement = 0.1;

        // Walls Indices
        public const int WallStartLeftXIndex = InitialFieldLeftXConsoleCursor - 1;
        public const int WallEndLeftXIndex = InitialFieldLeftXConsoleCursor + FieldWidth;
        public const int WallStartTopYIndex = InitiaFieldlTopYConsoleCursor;
        public const int WallEndTopYIndex = InitiaFieldlTopYConsoleCursor + FieldHeight;

        // Welcome Message
        public const int WelcomeMessageLeftX = 104;
        public const int WelcomeMessageTopY = 9;

        // Snake
        public const int InitialSnakeMinLeftX = InitialFieldLeftXConsoleCursor + 25;
        public const int InitialSnakeMaxLeftX = InitialFieldLeftXConsoleCursor + 35;
        public const int InitialSnakeMinTopY = InitiaFieldlTopYConsoleCursor + 10;
        public const int InitialSnakeMaxTopY = InitiaFieldlTopYConsoleCursor + 12;

        // Symbols
        public const char FoodSymbol = '\u25CF';
        public const char HorizontalWallSymbol = '\u2588';
        public const char VerticalWallSymbol = '\u2588';
    }
}
