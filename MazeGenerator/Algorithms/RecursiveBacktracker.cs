using System;
using System.Collections.Generic;

namespace MazeGenerator.Algorithms
{    
    public sealed class RecursiveBacktracker : MazeRandomFactory, ISolver
    {
        private Cell selected = null;
        private Maze _maze = null;
        private Stack<Cell> path = null;
        private List<Cell> visited = null;

        public bool IsSolvable(Maze maze)
        {
            _maze = maze;
            InitializeFields();
            selected = _maze.Entrance;
            Visit(selected);

            while (visited.Count < maze.Count)
            {
                if (selected == _maze.Exit)
                    break;

                List<Cell> passages = GetPassages(selected);
                if (passages.Count > 0)
                {
                    Cell passage = passages.RandomElement();
                    path.Push(selected);
                    SelectCell(passage);
                    Visit(selected);
                }
                else if (path.Count > 0)
                {
                    SelectCell(path.Pop());
                }
            }
            return selected == _maze.Exit;
        }

        public List<Cell> FindSolution(Maze maze)
        {
            _maze = maze;
            InitializeFields();
            List<Cell> solution = new List<Cell>();
            selected = _maze.Entrance;
            Visit(selected);

            while (visited.Count < maze.Count)
            {
                if (selected == _maze.Exit)
                    break;

                List<Cell> passages = GetPassages(selected);
                if (passages.Count > 0)
                {
                    solution.Add(selected);
                    Cell passage = passages.RandomElement();
                    path.Push(selected);
                    SelectCell(passage);
                    Visit(selected);
                }
                else if (path.Count > 0)
                {
                    Cell pathTop = path.Pop();
                    SelectCell(pathTop);
                    solution.Remove(pathTop);
                }
            }

            return solution;
        }

        public override Maze generate(int rowsCount, int columnsCount)
        {
            _maze = new Maze(rowsCount, columnsCount);
            InitializeFields();

            _maze.Entrance = _maze[0, 0];      
            _maze.Exit = _maze[rowsCount - 1, columnsCount - 1];

            SelectCell(_maze.Entrance);
            while (visited.Count < _maze.Count)
            {
                List<Cell> notVisitedNeighbours = GetNotVisitedNeighbours(selected);
                if (notVisitedNeighbours.Count > 0)
                {
                    Cell neighbour = notVisitedNeighbours.RandomElement();
                    _maze.RemoveWallsBetween(selected, neighbour);
                    path.Push(selected);
                    SelectCell(neighbour);
                    Visit(selected);
                }
                else if (path.Count > 0)
                {
                    SelectCell(path.Pop());
                }
            }
            return _maze;
        }

        private void InitializeFields()
        {
            path = new Stack<Cell>();
            visited = new List<Cell>();
        }        

        private void SelectCell(Cell cell)
        {
            selected = cell;
        }

        private void Visit(Cell cell)
        {
            visited.Add(cell);
        }

        private List<Cell> GetNotVisitedNeighbours(Cell cell)
        {
            IEnumerable<Cell> neighbours = _maze.GetNeighbours(cell);
            List<Cell> notVisitedNeighbours = new List<Cell>();

            foreach (Cell neighbour in neighbours)
            {
                if (!visited.Contains(neighbour))
                    notVisitedNeighbours.Add(neighbour);
            }

            return notVisitedNeighbours;
        }

        private List<Cell> GetPassages(Cell cell)
        {
            IEnumerable<Cell> neighbours = _maze.GetNeighbours(cell);
            List<Cell> passages = new List<Cell>();

            foreach (Cell neighbour in neighbours)
            {
                if (!_maze.IsWallBetween(cell, neighbour) && !visited.Contains(neighbour))
                {
                    passages.Add(neighbour);
                }
            }

            return passages;
        }
    }
}
