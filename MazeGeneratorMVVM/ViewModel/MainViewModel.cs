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
        #region Construtors
        public MainViewModel()
        {
            Size = 20;
            MazeCanvasSize = 400;
            GenerateMazeCommand = new RelayCommand(GenerateMaze);
            MazeGeneratorChoices = new ObservableCollection<IMazeFactory>()
                {
                    new RecursiveBacktracker(),
                    new PrimsAlgorithm()
                };
            MazeSolver = new RecursiveBacktracker();
        }
        #endregion

        #region API Code Behind
        private void GenerateMaze()
        {
            Maze = SelectedMazeGenerator.generate(Size);

            CreateViewCells();
            CreateViewSolution();
            CreateViewEntrance();
            CreateViewExit();
        }

        private void CreateViewCells()
        {
            ViewCells = new ObservableCollection<CellItem>();

            double graphicsSize = MazeCanvasSize / (double)Size;
            List<Cell> solution = MazeSolver.FindSolution(Maze);

            for (int row = 0; row < Maze.RowsCount; row++)
            {
                for (int column = 0; column < Maze.ColumnsCount; column++)
                {
                    Cell cell = Maze[row, column];

                    CellItem cellItem = new CellItem(cell)
                    {
                        TopLocation = row * graphicsSize,
                        LeftLocation = column * graphicsSize,
                        Width = graphicsSize,
                    };

                    ViewCells.Add(cellItem);
                }
            }
        }

        private void CreateViewSolution()
        {
            ViewSolution = new ObservableCollection<CellItem>();

            List<Cell> solution = MazeSolver.FindSolution(Maze);

            foreach (CellItem cellItem in ViewCells)
            {
                if (solution.Contains(cellItem.Cell))
                {
                    ViewSolution.Add(cellItem);
                }
            }
        }

        private void CreateViewEntrance()
        {
            foreach (CellItem cellItem in ViewCells)
            {
                if (cellItem.Cell == Maze.Entrance)
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
                if (cellItem.Cell == Maze.Exit)
                {
                    ViewExit = cellItem;
                    return;
                }
            }
        }
        #endregion

        #region API
        public int Size
        {
            get
            {
                return _size;
            }
            set
            {
                if (value > 0 && value <= 60)
                {
                    _size = value;
                    RaisePropertyChanged();
                }
            }
        }

        public int MazeCanvasSize
        {
            get
            {
                return _mazeCanvasSize;
            }
            set
            {
                if (value > 0)
                {
                    _mazeCanvasSize = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<CellItem> ViewCells
        {
            get
            {
                return _viewCells;
            }
            private set
            {
                _viewCells = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<CellItem> ViewSolution
        {
            get
            {
                return _viewSolution;
            }
            private set
            {
                _viewSolution = value;
                RaisePropertyChanged();
            }
        }

        public CellItem ViewEntrance
        {
            get
            {
                return _viewEntrance;
            }
            private set
            {
                _viewEntrance = value;
                RaisePropertyChanged();
            }
        }

        public CellItem ViewExit
        {
            get
            {
                return _viewExit;
            }
            private set
            {
                _viewExit = value;
                RaisePropertyChanged();
            }
        }

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

        public ObservableCollection<IMazeFactory> MazeGeneratorChoices { get; private set; }

        public IMazeFactory SelectedMazeGenerator
        {
            get
            {
                return _selectedMazeGenerator;
            }
            set
            {
                _selectedMazeGenerator = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand GenerateMazeCommand
        {
            get;
            private set;
        }
        #endregion

        #region Privates
        private Maze Maze { get; set; }
        private int _size;

        private int _mazeCanvasSize;
        private bool _showEntranceExit;
        private bool showSolution;
        private CellItem _viewEntrance;
        private CellItem _viewExit;
        private ObservableCollection<CellItem> _viewCells;
        private ObservableCollection<CellItem> _viewSolution;
        private IMazeFactory _selectedMazeGenerator;
        private ISolver MazeSolver { get; set; } 
        #endregion
    }
}
