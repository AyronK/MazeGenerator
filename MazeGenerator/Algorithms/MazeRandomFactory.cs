using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGenerator.Algorithms
{
    public abstract class MazeRandomFactory : IMazeFactory
    {
        protected static Random random = new Random();

        public Maze generate(int size)
        {
            return generate(size, size);
        }

        public abstract Maze generate(int rowsCount, int columsCount);
    }
}
