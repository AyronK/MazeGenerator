using MazeGenerator;
using MazeGenerator.Algorithms;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MazeGeneratorWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Maze maze;
        private const int MAZE_SIZE = 10;
        private double graphicsSize;

        public MainWindow()
        {
            InitializeComponent();
            graphicsSize = 400/MAZE_SIZE;
        }

        private void button_Recursive_Click(object sender, RoutedEventArgs e)
        {
            maze = new RecursiveBacktracker().generate(MAZE_SIZE);
            DrawMaze();
        }

        private void button_Prisms_Click(object sender, RoutedEventArgs e)
        {
            maze = new PrimsAlgorithm().generate(MAZE_SIZE);
            DrawMaze();
        }

        private void DrawMaze()
        {
            MainCanvas.Children.Clear();
            for (int i = 0; i < maze.RowsCount; i++)
            {
                for (int j = 0; j < maze.ColumnsCount; j++)
                {
                    Cell cell = maze[i, j];

                    Brush color = Brushes.DarkSlateBlue;

                    if(cell == maze.Entrance)
                    {
                        Rectangle rect = new Rectangle();
                        rect.Width = graphicsSize;
                        rect.Height = graphicsSize;
                        rect.Fill = Brushes.LightGreen;
                        MainCanvas.Children.Add(rect);
                        Canvas.SetTop(rect, i * graphicsSize);
                        Canvas.SetLeft(rect, j * graphicsSize);
                    }
                    else if (cell == maze.Exit)
                    {
                        Rectangle rect = new Rectangle();
                        rect.Width = graphicsSize;
                        rect.Height = graphicsSize;
                        rect.Fill = Brushes.LightPink;
                        MainCanvas.Children.Add(rect);
                        Canvas.SetTop(rect, i * graphicsSize);
                        Canvas.SetLeft(rect, j * graphicsSize);
                    }

                    if (cell.ContainsWall(Direction.North))
                    {
                        Line line = new Line();
                        line.Stroke = color;

                        line.X1 = 0;
                        line.X2 = graphicsSize;
                        line.Y1 = graphicsSize;
                        line.Y2 = graphicsSize;

                        line.StrokeThickness = 2;
                        MainCanvas.Children.Add(line);
                        Canvas.SetTop(line, i * graphicsSize);
                        Canvas.SetLeft(line, j * graphicsSize);
                    }
                    if (cell.ContainsWall(Direction.East))
                    {
                        Line line = new Line();
                        line.Stroke = color;

                        line.X1 = graphicsSize;
                        line.X2 = graphicsSize;
                        line.Y1 = 0;
                        line.Y2 = graphicsSize;

                        line.StrokeThickness = 2;
                        MainCanvas.Children.Add(line);
                        Canvas.SetTop(line, i * graphicsSize);
                        Canvas.SetLeft(line, j * graphicsSize);
                    }
                    if (cell.ContainsWall(Direction.South))
                    {
                        Line line = new Line();
                        line.Stroke = color;

                        line.X1 = 0;
                        line.X2 = graphicsSize;
                        line.Y1 = 0;
                        line.Y2 = 0;

                        line.StrokeThickness = 2;
                        MainCanvas.Children.Add(line);
                        Canvas.SetTop(line, i * graphicsSize);
                        Canvas.SetLeft(line, j * graphicsSize);
                    }
                    if (cell.ContainsWall(Direction.West))
                    {
                        Line line = new Line();
                        line.Stroke = color;

                        line.X1 = 0;
                        line.X2 = 0;
                        line.Y1 = 0;
                        line.Y2 = graphicsSize;

                        line.StrokeThickness = 2;
                        MainCanvas.Children.Add(line);
                        Canvas.SetTop(line, i * graphicsSize);
                        Canvas.SetLeft(line, j * graphicsSize);
                    }

                    /* Rectangle rect = new Rectangle();
                     rect.Stroke = Brushes.Black;
                     rect.Fill = Brushes.White;
                     rect.Width = 20;
                     rect.Height = 20;
                     MainCanvas.Children.Add(rect);
                     Canvas.SetTop(rect, i * 20);
                     Canvas.SetLeft(rect, j * 20);*/
                }
            }
            //</ show in maingrid > 
        }
    }
}
