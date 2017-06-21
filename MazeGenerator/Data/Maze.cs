using System;
using System.Collections;
using System.Collections.Generic;

namespace MazeGenerator
{
    public class Maze : IEnumerable<Cell>
    {
        #region Fields/Properties
        public Cell Entrance { get; set; }
        public Cell Exit { get; set; }
        public int RowsCount { get; private set; }
        public int ColumnsCount { get; private set; }
        private List<Cell> grid;
        #endregion

        #region Constructors
        public Maze(int size) : this(size, size) { }

        public Maze(int rowsCount, int columnsCount)
        {
            RowsCount = rowsCount;
            ColumnsCount = columnsCount;
            CreateGrid();
        }
        #endregion

        private void CreateGrid()
        {
            grid = new List<Cell>();
            FillGrid();
        }

        private void FillGrid()
        {
            int totalAmountOfCells = RowsCount * ColumnsCount;
            for (int rowsCount = 0; rowsCount < totalAmountOfCells; rowsCount++)
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
