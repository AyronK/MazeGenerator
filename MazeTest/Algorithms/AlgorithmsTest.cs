using MazeGenerator;
using MazeGenerator.Algorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MazeGeneratorTest.Algorithms
{
    [TestClass]
    public class AlgorithmsTest
    {
        [TestMethod]
        public void RecursiveBacktrackerGenerateTest()
        {
            int rowCount = 10;
            int columnCount = 10;
            IMazeFactory rb = new RecursiveBacktracker();
            Maze maze = rb.generate(rowCount, columnCount);

            Assert.IsInstanceOfType(maze, typeof(Maze));
            Assert.IsNotNull(maze.Entrance); ;
            Assert.IsNotNull(maze.Exit);
            Assert.AreEqual(rowCount, maze.RowsCount);
            Assert.AreEqual(columnCount, maze.ColumnsCount);
        }

        [TestMethod]
        public void PrimsAlgorithmGenerateTest()
        {
            int rowCount = 10;
            int columnCount = 10;
            IMazeFactory pa = new PrimsAlgorithm();
            Maze maze = pa.generate(rowCount, columnCount);

            Assert.IsInstanceOfType(maze, typeof(Maze));
            Assert.IsNotNull(maze.Entrance); ;
            Assert.IsNotNull(maze.Exit);
            Assert.AreEqual(rowCount, maze.RowsCount);
            Assert.AreEqual(columnCount, maze.ColumnsCount);
        }
    }
}

