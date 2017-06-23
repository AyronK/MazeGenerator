using MazeGenerator;
using MazeGenerator.Algorithms;
using MazeGeneratorMVVM.MVVMLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MazeGeneratorMVVM.ViewModel
{
    internal class MainViewModel : ViewModelBase
    {
        public ObservableCollection<CellItem> ViewCells { get; private set; } = new ObservableCollection<CellItem>();
        public ObservableCollection<CellItem> ViewSolution { get; private set; } = new ObservableCollection<CellItem>();

        public int Size
        {
            get
            {
                return size;
            }
            set
            {
                if (value > 0)
                {
                    size = value;
                    RaisePropertyChanged();
                }
            }
        }

        private int size = 20;

        private CellItem viewEntrance;
        public CellItem ViewEntrance
        {
            get
            {
                return viewEntrance;
            }
            private set
            {
                viewEntrance = value;
                RaisePropertyChanged();
            }
        }

        private CellItem viewExit;
        public CellItem ViewExit
        {
            get
            {
                return viewExit;
            }
            private set
            {
                viewExit = value;
                RaisePropertyChanged();
            }
        }

        private Maze maze;

        public bool ShowEntranceExit
        {
            get
            {
                return _showEntranceExit;
            }
            set
            {
                _showEntranceExit = value;
                RaisePropertyChanged();
            }
        }
        private bool _showEntranceExit;
        public bool ShowSolution
        {
            get
            {
                return showSolution;
            }
            set
            {
                showSolution = value;
                RaisePropertyChanged();
            }
        }
        private bool showSolution;
        public ObservableCollection<IMazeFactory> MazeGenerators { get; private set; }
        public IMazeFactory MazeGenerator { get { return mazeGenerator; } set { mazeGenerator = value; RaisePropertyChanged(); } }
        private IMazeFactory mazeGenerator;
        private ISolver mazeSolver;
        private double graphicsSize;

        public MainViewModel()
        {
            GenerateMazeCommand = new RelayCommand(GenerateMaze);
            MazeGenerators = new ObservableCollection<IMazeFactory>()
                {
                    new RecursiveBacktracker(),
                    new PrimsAlgorithm()
                };
            mazeSolver = new RecursiveBacktracker();
        }

        private void GenerateMaze()
        {
            graphicsSize = 400 / Size;
            maze = mazeGenerator.generate(Size);
            CreateViewCells();
            CreateViewSolution();
            CreateViewEntrance();
            CreateViewExit();
        }

        private void CreateViewCells()
        {
            ViewCells.Clear();
            List<Cell> solution = mazeSolver.FindSolution(maze);
            for (int i = 0; i < maze.RowsCount; i++)
            {
                for (int j = 0; j < maze.ColumnsCount; j++)
                {
                    Cell cell = maze[i, j];

                    CellItem cellItem = new CellItem(cell)
                    {
                        Top = i * graphicsSize,
                        Left = j * graphicsSize,
                        Width = graphicsSize,
                    };

                    ViewCells.Add(cellItem);
                }
            }
        }

        private void CreateViewEntrance()
        {
            foreach (CellItem cellItem in ViewCells)
            {
                if (cellItem.Cell == maze.Entrance)
                {
                    ViewEntrance = cellItem;
                    return;
                }
            }
        }

        private void CreateViewExit()
        {
            foreach (CellItem cellItem in ViewCells)
            {
                if (cellItem.Cell == maze.Exit)
                {
                    ViewExit = cellItem;
                    return;
                }
            }
        }

        private void CreateViewSolution()
        {
            ViewSolution.Clear();
            List<Cell> solution = mazeSolver.FindSolution(maze);
            foreach (CellItem cellItem in ViewCells)
            {
                if (solution.Contains(cellItem.Cell))
                {
                    ViewSolution.Add(cellItem);
                }
            }
        }

        public RelayCommand GenerateMazeCommand
        {
            get;
            private set;
        }
    }
}
