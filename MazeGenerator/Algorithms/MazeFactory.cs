namespace MazeGenerator.Algorithms
{
    public abstract class MazeFactory
    {
        public abstract Maze generate(int length, int heigth);
        public Maze generate(int size)
        {
            return generate(size, size);
        }
    }
}
