using System;
using System.Collections;
using System.Collections.Generic;

namespace MazeGenerator
{
    public class Maze : IEnumerable<Cell>
    {
        #region Fields/Properties
        public Cell Entrance
        {
            get { return entrance; }
            set
            {
                if (entrance != null)
                    throw new FieldAccessException("This maze has already an entrance.");
                entrance = value;
                CreatePassageOutOfMaze(entrance);
            }
        }
        public Cell Exit
        {
            get { return exit; }
            set
            {
                if (exit != null)
                    throw new FieldAccessException("This maze has already an exit.");
                exit = value;
                CreatePassageOutOfMaze(exit);
            }
        }
        public int RowsCount { get; private set; }
        public int ColumnsCount { get; private set; }
        public int Count { get { return RowsCount * ColumnsCount; } }
        private List<Cell> grid;
        private Cell entrance = null;
        private Cell exit = null;
        #endregion

        private void CreatePassageOutOfMaze(Cell cell)
        {
            List<Cell> neighbours = new List<Cell>(GetNeighbours(cell));

            List<Direction> outOfMazeDirections = new List<Direction>();
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                if (!HasNeighbour(cell, direction))
                {
                    outOfMazeDirections.Add(direction);
                }
            }

            if(outOfMazeDirections.Count > 0)
            {
                cell.RemoveWall(outOfMazeDirections.RandomElement());
            }
        }

        #region Constructors
        public Maze(int size) : this(size, size) { }

        public Maze(int rowsCount, int columnsCount)
        {
            CheckDimentions(rowsCount, columnsCount);
            RowsCount = rowsCount;
            ColumnsCount = columnsCount;
            CreateGrid();
        }

        private static void CheckDimentions(int rowsCount, int columnsCount)
        {
            if (rowsCount * columnsCount == 0)
                throw new ArgumentException("Dimentions cannot be zero.");
        }
        #endregion

        private void CreateGrid()
        {
            grid = new List<Cell>();
            FillGrid();
        }

        private void FillGrid()
        {
            for (int rowsCount = 0; rowsCount < Count; rowsCount++)
            {
                grid.Add(new Cell());
            }
        }

        public Cell this[int row, int column]
        {
            get
            {
                return GetValue(row,column);
            }
            private set
            {
                SetValue(row, column, value);
            }
        }

        public Cell GetValue(int row, int column)
        {
            if (row > RowsCount || column > ColumnsCount)
                throw new IndexOutOfRangeException();
            int position = ConvertPosition(row, column);
            return grid[position];
        }       

        private void SetValue(int row, int column, Cell value)
        {
            if (row > RowsCount || column > ColumnsCount)
                throw new IndexOutOfRangeException();
            int position = ConvertPosition(row, column);
            grid[position] = value;
        }

        private int ConvertPosition(int row, int column)
        {
            return row * ColumnsCount + column;
        }
        
        public int RowOf(Cell cell)
        {
            if (!Contains(cell))
                throw new ArgumentException("That cell is not an element of grid");
            return grid.IndexOf(cell) / ColumnsCount;
        }

        public int ColumnOf(Cell cell)
        {
            if (!Contains(cell))
                throw new ArgumentException("That cell is not an element of grid");
            return grid.IndexOf(cell) % ColumnsCount;
        }

        public bool Contains(Cell cell)
        {
            return grid.Contains(cell);
        }
        
        public bool IsWallBetween(Cell cell, Cell neighbour)
        {
            if (!IsNeighbour(cell, neighbour))
                return false;
            Direction neighbourDirection = DirectionOfNeighbour(cell, neighbour);
            bool cellHasProperWall = cell.ContainsWall(neighbourDirection);
            bool neighbourHasProperWall = neighbour.ContainsWall(neighbourDirection.Opposite());
            return cellHasProperWall & neighbourHasProperWall;

        }

        public void RemoveWallsBetween(Cell cell, Cell neighbour)
        {
            if (!IsNeighbour(cell, neighbour))
                return;
            Direction direction = DirectionOfNeighbour(cell, neighbour);
            cell.RemoveWall(direction);
            neighbour.RemoveWall(direction.Opposite());
        }

        public bool IsNeighbour(Cell cell, Cell neighbour)
        {
            if (RowOf(cell) == RowOf(neighbour))
            {
                if (Math.Abs(ColumnOf(cell) - ColumnOf(neighbour)) == 1)
                    return true;
            }
            else if (ColumnOf(cell) == ColumnOf(neighbour))
            {
                if (Math.Abs(RowOf(cell) - RowOf(neighbour)) == 1)
                    return true;
            }
            return false;
        }

        private Direction DirectionOfNeighbour(Cell cell, Cell neighbour)
        {
            if (RowOf(cell) == RowOf(neighbour))
            {
                if (ColumnOf(cell) > ColumnOf(neighbour))
                    return Direction.West;
                else if (ColumnOf(cell) < ColumnOf(neighbour))
                    return Direction.East;
            }
            else if (ColumnOf(cell) == ColumnOf(neighbour))
            {
                if (RowOf(cell) > RowOf(neighbour))
                    return Direction.South;
                else if (RowOf(cell) < RowOf(neighbour))
                    return Direction.North;
            }

            throw new ArgumentException("Cells are not neighbours");
        }

        public IEnumerable<Cell> GetNeighbours(Cell cell)
        {
            List<Cell> neighbours = new List<Cell>();

            int row = RowOf(cell);
            int column = ColumnOf(cell);

            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                if (HasNeighbour(cell, direction))
                {
                    Cell neighbour = GetNeighbour(cell, direction);
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
                    return RowOf(cell) < RowsCount - 1;
                case Direction.South:
                    return RowOf(cell) > 0;
                case Direction.East:
                    return ColumnOf(cell) < ColumnsCount - 1;
                case Direction.West:
                    return ColumnOf(cell) > 0;
                default:
                    return false;
            }
        }

        private Cell GetNeighbour(Cell cell, Direction direction)
        {
            int row = RowOf(cell);
            int column = ColumnOf(cell);

            switch (direction)
            {
                case Direction.North:
                    return this[row + 1, column];
                case Direction.South:
                    return this[row - 1, column];
                case Direction.East:
                    return this[row, column + 1];
                case Direction.West:
                    return this[row, column - 1];
            }

            throw new ArgumentException("Unsupported Direction.");
        }
        #region IEnumerable implementation
        public IEnumerator<Cell> GetEnumerator()
        {
            return grid.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        } 
        #endregion
    }
}
