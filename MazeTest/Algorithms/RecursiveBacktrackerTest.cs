using MazeGenerator;
using MazeGenerator.Algorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MazeGeneratorTest.Algorithms
{
    [TestClass]
    public class RecursiveBacktrackerTest
    {
        [TestMethod]
        public void GenerateTest()
        {
            int rowCount = 10;
            int columnCount = 10;
            MazeFactory rb = new RecursiveBacktracker();
            Maze maze = rb.generate(rowCount, columnCount);

            Assert.IsInstanceOfType(maze, typeof(Maze));
            Assert.IsNotNull(maze.Entrance); ;
            Assert.IsNull(maze.Exit);
            Assert.AreEqual(rowCount, maze.RowsCount);
            Assert.AreEqual(columnCount, maze.ColumnsCount);
        }
    }
}