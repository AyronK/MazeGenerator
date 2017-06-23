using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGenerator.Algorithms
{
    public interface ISolver
    {
        bool IsSolvable(Maze maze);
        List<Cell> FindSolution(Maze maze);
    }
}
