using System;
using System.Collections.Generic;

namespace MazeGenerator.Algorithms
{
    public class PrimsAlgorithm : MazeFactory
    {
        private Maze maze = null;
        private Cell selected = null;
        private List<Cell> touched = null;
        private List<Cell> connected = null;

        public override Maze generate(int rowCount, int columnCount)
        {
            maze = new Maze(rowCount, columnCount); maze.Entrance = selected;
            touched = new List<Cell>();
            connected = new List<Cell>();

            MakeEntrance();
            MakeExit();

            while (touched.Count > 0)
            {
                selected = GetRandomFrontier();
                Cell neighbour = GetRandomConnectedNeighbour(selected);
                RemoveWallsBetween(selected, neighbour);
                ConnectTouchedToMaze(selected);
                UpdateTouched(selected);
            }
            return maze;
        }

        private void MakeEntrance()
        {
            Cell entrance = maze[0, 0];
            maze.Entrance = entrance;
            maze.Entrance.RemoveWall(Direction.West);
            connected.Add(entrance);
            UpdateTouched(entrance);
        }

        private void MakeExit()
        {
            int rowsCount = maze.RowsCount;
            int columnsCount = maze.ColumnsCount;
            maze.Exit = maze[rowsCount - 1, columnsCount - 1];
            maze.Exit.RemoveWall(Direction.East);
        }

        private Cell GetRandomFrontier()
        {
            int rnd = random.Next(0, touched.Count);
            return touched[rnd];
        }

        private Cell GetRandomConnectedNeighbour(Cell cell)
        {
            List<Cell> neighbours = GetConnectedNeighbours(cell);
            int rnd = random.Next(0, neighbours.Count);
            return neighbours[rnd];
        }

        private List<Cell> GetConnectedNeighbours(Cell cell)
        {
            List<Cell> neighbours = new List<Cell>();

            int row = maze.RowOf(cell);
            int column = maze.ColumnOf(cell);

            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                if (HasNeighbour(cell, direction))
                {
                    Cell neighbour = GetNeighbour(cell, direction);
                    if (connected.Contains(neighbour))
                        neighbours.Add(neighbour);
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

        private void RemoveWallsBetween(Cell cell, Cell neightbour)
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

        private void ConnectTouchedToMaze(Cell cell)
        {
            touched.Remove(cell);
            connected.Add(cell);
        }

        private void UpdateTouched(Cell cell)
        {
            int row = maze.RowOf(cell);
            int column = maze.ColumnOf(cell);
            if (row > 0)
                AddToFrontier(maze[row - 1, column]);
            if (row < maze.RowsCount - 1)
                AddToFrontier(maze[row + 1, column]);
            if (column > 0)
                AddToFrontier(maze[row, column-1]);
            if (column < maze.ColumnsCount - 1)
                AddToFrontier(maze[row, column+1]);
        }

        private void AddToFrontier(Cell cell)
        {
            if (IsNotUsed(cell))
            {
                touched.Add(cell);
            }
        }

        private bool IsNotUsed(Cell cell)
        {
            return !touched.Contains(cell) && !connected.Contains(cell);
        }
    }
}
