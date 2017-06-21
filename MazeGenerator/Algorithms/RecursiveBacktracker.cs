using System;
using System.Collections.Generic;

namespace MazeGenerator.Algorithms
{
    public class RecursiveBacktracker : MazeFactory
    {
        private Cell selected = null;
        private Maze maze = null;
        private Stack<Cell> stack = new Stack<Cell>();
        private List<Cell> visitedCells = new List<Cell>();
        private Random random = new Random();

        public override Maze generate(int rowCount, int columnCount)
        {
            if (!AreDimentionsCorrect(rowCount, columnCount))
                throw new ArgumentException("Dimentions cannot be zero.");
            
            maze = new Maze(rowCount, columnCount);
            SelectCell(maze[0, 0]);
            maze.Entrance = selected;

            while (HasUnvisitedCells())
            {
                if (HasSelectedCellUnvisitedNeighbours())
                {
                    Cell neighbour = GetRandomNotVisitedNeighbour(selected);
                    stack.Push(selected);
                    RemoveWallsBetween(selected, neighbour);
                    SelectCell(neighbour);
                }
                else if (stack.Count > 0)
                {
                    SelectCell(stack.Pop());
                }
            }
            maze.Exit = null;
            return maze;
        }

        private static bool AreDimentionsCorrect(int length, int heigth)
        {
            return length * heigth != 0;
        }

        private void SelectCell(Cell cell)
        {
            selected = cell;
            SetAsVisited(cell);
        }

        private void SetAsVisited(Cell cell)
        {
            visitedCells.Add(cell);
        }

        private bool HasUnvisitedCells()
        {
            foreach (Cell cell in maze)
            {
                if (!IsVisited(cell))
                    return true;
            }
            return false;
        }

        private bool IsVisited(Cell cell)
        {
            return visitedCells.Contains(cell);
        }

        private bool HasSelectedCellUnvisitedNeighbours()
        {
            return GetNotVisitedNeighbours(selected).Count > 0;
        }

        private Cell GetRandomNotVisitedNeighbour(Cell cell)
        {
            List<Cell> neighbours = new List<Cell>(GetNotVisitedNeighbours(cell).Values);
            int index = random.Next(0, neighbours.Count);
            return neighbours[index];
        }

        private Dictionary<Direction, Cell> GetNotVisitedNeighbours(Cell cell)
        {
            Dictionary<Direction, Cell> neighbours = new Dictionary<Direction, Cell>();

            int row = maze.RowOf(cell);
            int column = maze.ColumnOf(cell);

            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                if (HasNeighbour(cell, direction))
                {
                    Cell neighbour = GetNeighbour(cell, direction);
                    if (!IsVisited(neighbour))
                        neighbours.Add(direction, neighbour);
                }
            }

            return neighbours;
        }

        private bool HasNeighbour(Cell cell, Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    return maze.RowOf(cell) < maze.RowsCount - 1;
                case Direction.South:
                    return maze.RowOf(cell) > 0;
                case Direction.East:
                    return maze.ColumnOf(cell) < maze.ColumnsCount - 1;
                case Direction.West:
                    return maze.ColumnOf(cell) > 0;
                default:
                    return false;
            }
        }

        private Cell GetNeighbour(Cell cell, Direction direction)
        {
            int row = maze.RowOf(cell);
            int column = maze.ColumnOf(cell);

            switch (direction)
            {
                case Direction.North:
                    return maze[row + 1, column];
                case Direction.South:
                    return maze[row - 1, column];
                case Direction.East:
                    return maze[row, column + 1];
                case Direction.West:
                    return maze[row, column - 1];
            }

            throw new ArgumentException("Unsupported Direction.");
        }

        public void RemoveWallsBetween(Cell cell, Cell neightbour)
        {
            Direction direction = DirectionOfNeighbour(cell, neightbour);
            cell.RemoveWall(direction);
            neightbour.RemoveWall(direction.Opposite());
        }

        private Direction DirectionOfNeighbour(Cell cell, Cell neighbour)
        {
            if (maze.RowOf(cell) == maze.RowOf(neighbour))
            {
                if (maze.ColumnOf(cell) > maze.ColumnOf(neighbour))
                    return Direction.West;
                else if (maze.ColumnOf(cell) < maze.ColumnOf(neighbour))
                    return Direction.East;
            }
            else if (maze.ColumnOf(cell) == maze.ColumnOf(neighbour))
            {
                if (maze.RowOf(cell) > maze.RowOf(neighbour))
                    return Direction.South;
                else if (maze.RowOf(cell) < maze.RowOf(neighbour))
                    return Direction.North;
            }
            
           throw new ArgumentException("Cells are not neighbours");
        }        
    }
}
