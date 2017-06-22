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
            MazeFactory rb = new RecursiveBacktracker();
            Maze maze = rb.generate(rowCount, columnCount);

            Assert.IsInstanceOfType(maze, typeof(Maze));
            Assert.IsNotNull(maze.Entrance); ;
            Assert.IsNotNull(maze.Exit);
            Assert.AreEqual(rowCount, maze.RowsCount);
            Assert.AreEqual(columnCount, maze.ColumnsCount);
        }

        [TestMethod]
        public void PrismsAlgorithmGenerateTest()
        {
            int rowCount = 10;
            int columnCount = 10;
            MazeFactory pa = new PrimsAlgorithm();
            Maze maze = pa.generate(rowCount, columnCount);

            Assert.IsInstanceOfType(maze, typeof(Maze));
            Assert.IsNotNull(maze.Entrance); ;
            Assert.IsNotNull(maze.Exit);
            Assert.AreEqual(rowCount, maze.RowsCount);
            Assert.AreEqual(columnCount, maze.ColumnsCount);
        }

        /// <summary>
        /// Amount of each algorithm's mazes generated per test.
        /// </summary>
        private const int TEST_SET_SIZE = 10;
        /// <summary>
        /// Amount of rows in each maze for all tests.
        /// </summary>
        private const int ROWS_COUNT = 25;
        /// <summary>
        /// Amount of columns in each maze for all tests.
        /// </summary>
        private const int COLUMNSCOUNT = 25;

        [TestMethod]
        public void AlgorithmsSolvableTest()
        {
            for (int setNumber = 0; setNumber < TEST_SET_SIZE; setNumber++)
            {
                MazeFactory pa = new PrimsAlgorithm();
                MazeFactory rb = new RecursiveBacktracker();

                Maze prismMaze = pa.generate(ROWS_COUNT, COLUMNSCOUNT);
                Maze recursiveBacktracerMaze = rb.generate(ROWS_COUNT, COLUMNSCOUNT);

                Assert.IsTrue(MazeSolver.IsSolvable(prismMaze));
                Assert.IsTrue(MazeSolver.IsSolvable(recursiveBacktracerMaze));
            }
        }
    }
}

