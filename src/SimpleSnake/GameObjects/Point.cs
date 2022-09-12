namespace SimpleSnake.GameObjects
{
    using Renderer;

    public class Point
    {
        public Point(int leftX, int topY)
        {
            this.LeftX = leftX;
            this.TopY = topY;
        }

        public int LeftX { get; set; }

        public int TopY { get; set; }

        public void Draw(char symbol)
        {
            ConsoleRenderer.VisualizePoint(this.LeftX, this.TopY, symbol);
        }

        public void Draw(int leftX, int topY, char symbol)
        {
            ConsoleRenderer.VisualizePoint(leftX, topY, symbol);
        }
    }
}
