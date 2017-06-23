using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGenerator.Algorithms
{
    public interface IMazeFactory
    {
        Maze generate(int rowsCount, int columsCount);
        Maze generate(int size);
    }
}
