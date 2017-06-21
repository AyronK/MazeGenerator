using MazeGenerator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MazeGeneratorTest
{
    [TestClass]
    public class MazeTest
    {
        [TestMethod]
        public void OneParamConstructorTest()
        {
            Maze grid = new Maze(1);
            Assert.AreEqual(1, grid.RowsCount);
            Assert.AreEqual(1, grid.ColumnsCount);
        }

        [TestMethod]
        public void TwoParamConstructorTest()
        {
            Maze grid = new Maze(1, 2);
            Assert.AreEqual(1, grid.RowsCount);
            Assert.AreEqual(2, grid.ColumnsCount);
        }

        [TestMethod]
        public void GetValueTest()
        {
            Maze grid = new Maze(1, 1);
            Assert.IsInstanceOfType(grid[0, 0], typeof(Cell));
        }

        [TestMethod]
        public void RowOfColumnOfTest()
        {
            Maze grid = new Maze(5, 10);
            Assert.AreEqual(1, grid.RowOf(grid[1, 7]));
            Assert.AreEqual(7, grid.ColumnOf(grid[1, 7]));
        }

        [TestMethod]
        public void ContainsTest()
        {
            Maze grid = new Maze(1, 1);
            Assert.IsTrue(grid.Contains(grid[0, 0]));
        }

    }
}
