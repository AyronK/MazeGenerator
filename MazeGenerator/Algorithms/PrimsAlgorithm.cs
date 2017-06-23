using System;
using System.Collections.Generic;

namespace MazeGenerator.Algorithms
{
    public sealed class PrimsAlgorithm : MazeRandomFactory
    {
        private Maze maze = null;
        private Cell selected = null;
        private List<Cell> inMazeNeighbour = null; 
        private List<Cell> inMaze = null; 

        public override Maze generate(int rowsCount, int columnsCount)
        {
            maze = new Maze(rowsCount, columnsCount);
            InitializeFields();

            maze.Entrance = maze[0, 0];
            maze.Exit = maze[rowsCount - 1, columnsCount - 1];

            inMaze.Add(maze.Entrance);
            UpdateInMazeNeighbours(maze.Entrance);
            while (inMazeNeighbour.Count > 0)
            {
                selected = inMazeNeighbour.RandomElement();
                List<Cell> neighbours = GetInMazeNeighbours(selected);
                Cell neighbour = neighbours.RandomElement();
                maze.RemoveWallsBetween(selected, neighbour);
                AddToMaze(selected);
                UpdateInMazeNeighbours(selected);
            }
            return maze;
        }

        private void InitializeFields()
        {
            inMazeNeighbour = new List<Cell>();
            inMaze = new List<Cell>();
        }

        private List<Cell> GetInMazeNeighbours(Cell cell)
        {
            IEnumerable<Cell> neighbours = maze.GetNeighbours(cell);
            List<Cell> connectedNeighbours = new List<Cell>();

            foreach (Cell neighbour in neighbours)
            {
                if (inMaze.Contains(neighbour))
                    connectedNeighbours.Add(neighbour);
            }

            return connectedNeighbours;
        }

        private void AddToMaze(Cell cell)
        {
            inMazeNeighbour.Remove(cell);
            inMaze.Add(cell);
        }

        private void UpdateInMazeNeighbours(Cell cell)
        {
            IEnumerable<Cell> neighbours = maze.GetNeighbours(cell);
            foreach (Cell neighbour in neighbours)
            {
                AddToInMazeNeighbours(neighbour);
            }
        }

        private void AddToInMazeNeighbours(Cell cell)
        {
            if (IsNeverUsed(cell))
            {
                inMazeNeighbour.Add(cell);
            }
        }

        private bool IsNeverUsed(Cell cell)
        {
            return !inMazeNeighbour.Contains(cell) && !inMaze.Contains(cell);
        }
    }
}
