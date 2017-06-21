using MazeGenerator;
using MazeGenerator.Algorithms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MazeGeneratorWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Maze maze;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            maze = new RecursiveBacktracker().generate(20);
            DrawMaze();
        }

        private void DrawMaze()
        {
            MainCanvas.Children.Clear();
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    Cell cell = maze[i, j];

                    Brush color = Brushes.DarkSlateBlue;

                    if(cell == maze.Entrance)
                    {
                        Rectangle rect = new Rectangle();
                        rect.Width = 20;
                        rect.Height = 20;
                        rect.Fill = Brushes.LightGreen;
                        MainCanvas.Children.Add(rect);
                        Canvas.SetTop(rect, i * 20);
                        Canvas.SetLeft(rect, j * 20);
                    }
                    else if (cell == maze.Exit)
                    {
                        Rectangle rect = new Rectangle();
                        rect.Width = 20;
                        rect.Height = 20;
                        rect.Fill = Brushes.LightPink;
                        MainCanvas.Children.Add(rect);
                        Canvas.SetTop(rect, i * 20);
                        Canvas.SetLeft(rect, j * 20);
                    }

                    if (cell.ContainsWall(Direction.North))
                    {
                        Line line = new Line();
                        line.Stroke = color;

                        line.X1 = 0;
                        line.X2 = 20;
                        line.Y1 = 20;
                        line.Y2 = 20;

                        line.StrokeThickness = 2;
                        MainCanvas.Children.Add(line);
                        Canvas.SetTop(line, i * 20);
                        Canvas.SetLeft(line, j * 20);
                    }
                    if (cell.ContainsWall(Direction.East))
                    {
                        Line line = new Line();
                        line.Stroke = color;

                        line.X1 = 20;
                        line.X2 = 20;
                        line.Y1 = 0;
                        line.Y2 = 20;

                        line.StrokeThickness = 2;
                        MainCanvas.Children.Add(line);
                        Canvas.SetTop(line, i * 20);
                        Canvas.SetLeft(line, j * 20);
                    }
                    if (cell.ContainsWall(Direction.South))
                    {
                        Line line = new Line();
                        line.Stroke = color;

                        line.X1 = 0;
                        line.X2 = 20;
                        line.Y1 = 0;
                        line.Y2 = 0;

                        line.StrokeThickness = 2;
                        MainCanvas.Children.Add(line);
                        Canvas.SetTop(line, i * 20);
                        Canvas.SetLeft(line, j * 20);
                    }
                    if (cell.ContainsWall(Direction.West))
                    {
                        Line line = new Line();
                        line.Stroke = color;

                        line.X1 = 0;
                        line.X2 = 0;
                        line.Y1 = 0;
                        line.Y2 = 20;

                        line.StrokeThickness = 2;
                        MainCanvas.Children.Add(line);
                        Canvas.SetTop(line, i * 20);
                        Canvas.SetLeft(line, j * 20);
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
