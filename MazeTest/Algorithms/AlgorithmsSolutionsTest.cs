using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MazeGenerator;
using MazeGenerator.Algorithms;

namespace MazeTest.Algorithms
{
    [TestClass]
    public class AlgorithmsSolutionsTest
    {
        /// <summary>
        /// Amount of each algorithm's mazes generated per test.
        /// </summary>
        private const int TEST_SET_SIZE = 100;
        /// <summary>
        /// Amount of rows in each maze for all tests.
        /// </summary>
        private const int ROWS_COUNT = 10;
        /// <summary>
        /// Amount of columns in each maze for all tests.
        /// </summary>
        private const int COLUMNSCOUNT = 10;

        [TestMethod]
        public void RecursiveBacktrackerSolvableTest()
        {
            for (int setNumber = 0; setNumber < TEST_SET_SIZE; setNumber++)
            {
                RecursiveBacktracker rb = new RecursiveBacktracker();
                IMazeFactory factory = rb;
                ISolver solver = rb;

                Maze recursiveBacktracerMaze = factory.generate(ROWS_COUNT, COLUMNSCOUNT);
                Assert.IsTrue(solver.IsSolvable(recursiveBacktracerMaze));
            }
        }

        [TestMethod]
        public void PrimsSolvableTest()
        {
            for (int setNumber = 0; setNumber < TEST_SET_SIZE; setNumber++)
            {
                PrimsAlgorithm pa = new PrimsAlgorithm();
                IMazeFactory factory = pa;
                ISolver solver = new RecursiveBacktracker();

                Maze prismMaze = factory.generate(ROWS_COUNT, COLUMNSCOUNT);
                Assert.IsTrue(solver.IsSolvable(prismMaze));
            }
        }
    }
}
