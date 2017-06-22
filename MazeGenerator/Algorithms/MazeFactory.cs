using System;

namespace MazeGenerator.Algorithms
{
    public abstract class MazeFactory
    {
        protected static Random random = new Random();
        public abstract Maze generate(int rowsCount, int columsCount);
        public Maze generate(int size)
        {
            return generate(size, size);
        }
    }
}
