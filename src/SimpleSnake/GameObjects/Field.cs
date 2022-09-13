namespace SimpleSnake.GameObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Common;
    using GameObjects.Foods;

    public class Field : Point
    {
        private List<Food> foods;

        public Field(int leftX, int topY)
            : base(leftX, topY)
        {
            InitializeWallBorders();
            this.foods = CreateListOfAllFoodTypes();
        }

        public bool IsPointOfWall(Point snakeHead)
            => snakeHead.LeftX == GlobalConstants.WallStartLeftXIndex + 1 ||
               snakeHead.LeftX == GlobalConstants.WallEndLeftXIndex - 1 ||
               snakeHead.TopY == GlobalConstants.WallStartTopYIndex ||
               snakeHead.TopY == GlobalConstants.WallEndTopYIndex;

        public List<Food> GetAllTypeOfFoods() => this.foods;

        private List<Food> CreateListOfAllFoodTypes()
        {
           this.foods = new List<Food>();
           Type[] foodTypes = Assembly.GetExecutingAssembly()
                                       .GetTypes()
                                       .Where(t => t.Namespace == "SimpleSnake.GameObjects.Foods" &&
                                              t.IsClass && !t.IsAbstract)
                                       .ToArray();

            foreach (Type type in foodTypes)
            {
                Food currFood = (Food)Activator.CreateInstance(type, new object[] { this });
                this.foods.Add(currFood);
            }

            return this.foods;
        }

        public int CreateFood(Queue<Point> snake)
        {
            int foodIndex = new Random().Next(0, foods.Count);
            this.foods[foodIndex].SetRandomPosition(snake);
            return foodIndex;
        }

        private void SetHorizontalLine(int topY)
        {
            for (int currLeftXpossition = 0; currLeftXpossition < this.LeftX; currLeftXpossition++)
            {
                this.Draw(currLeftXpossition + GlobalConstants.InitialFieldLeftXConsoleCursor, 
                          topY + GlobalConstants.InitiaFieldlTopYConsoleCursor ,
                          GlobalConstants.HorizontalWallSymbol);
            }
        }

        private void SetVerticalLine(int leftX)
        {
            for (int currTopYPossition = 1; currTopYPossition < this.TopY + 1; currTopYPossition++)
            {
                this.Draw(leftX + GlobalConstants.InitialFieldLeftXConsoleCursor, 
                          currTopYPossition + GlobalConstants.InitiaFieldlTopYConsoleCursor , 
                          GlobalConstants.VerticalWallSymbol);
            }
        }

        private void InitializeWallBorders()
        {
            SetHorizontalLine(0);
            SetHorizontalLine(this.TopY);

            SetVerticalLine(0);
            SetVerticalLine(this.LeftX - 1);
        }
    }
}
