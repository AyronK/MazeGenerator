using MazeGenerator;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGeneratorTest.Algorithms
{
    public static class MazeSolver
    {
        private static Maze _maze = null;
        private static Cell selected = null;
        private static List<Cell> visited = null;
        private static Stack<Cell> stack = null;
        private static Random random = new Random();

        /// <summary>
        /// Checks if it is possible to reach maze's exit starting from its entry.
        /// Uses recursive backtracker algorithm to solve the maze.
        /// </summary>
        /// <param name="maze"></param>
        /// <returns></returns>
        public static bool IsSolvable(Maze maze)
        {
            _maze = maze;
            visited = new List<Cell>();
            stack = new Stack<Cell>();
            selected = _maze.Entrance;
            visited.Add(selected);

            while(visited.Count <= maze.ColumnsCount * maze.RowsCount)
            {
                if (selected == _maze.Exit)
                    return true;
                if (GetPassages(selected).Count > 0)
                {
                    stack.Push(selected);
                    selected = GetRandomPassage(selected);
                    visited.Add(selected);
                }
                else if (stack.Count > 0)
                {
                    selected = stack.Pop();
                }
            }
            return false;
        }

        private static Cell GetRandomPassage(Cell cell)
        {
            List<Cell> passages = GetPassages(cell);
            int r = random.Next(0, passages.Count);
            return passages[r];
        }

        private static List<Cell> GetPassages(Cell cell)
        {
            List<Cell> passages = new List<Cell>();
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                if (!cell.ContainsWall(direction))
                {
                    if(HasPassage(cell, direction))
                    {
                        Cell passage = GetPassage(cell, direction);
                        if (!visited.Contains(passage))
                            passages.Add(GetPassage(cell, direction));
                    }
                }
            }
            return passages;
        }

        private static bool HasPassage(Cell cell, Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    return _maze.RowOf(cell) < _maze.RowsCount - 1;
                case Direction.South:
                    return _maze.RowOf(cell) > 0;
                case Direction.East:
                    return _maze.ColumnOf(cell) < _maze.ColumnsCount - 1;
                case Direction.West:
                    return _maze.ColumnOf(cell) > 0;
                default:
                    return false;
            }
        }

        private static Cell GetPassage(Cell cell, Direction direction)
        {
            int row = _maze.RowOf(cell);
            int column = _maze.ColumnOf(cell);

            switch (direction)
            {
                case Direction.North:
                    return _maze[row + 1, column];
                case Direction.South:
                    return _maze[row - 1, column];
                case Direction.East:
                    return _maze[row, column + 1];
                case Direction.West:
                    return _maze[row, column - 1];
            }

            throw new ArgumentException("Unsupported Direction.");
        }

    }
}
