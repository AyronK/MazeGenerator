using MazeGenerator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MazeTest
{
    [TestClass]
    class CellTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            Cell cell = new Cell();
            Assert.IsTrue(cell.ContainsWall(Direction.North));
            Assert.IsTrue(cell.ContainsWall(Direction.East));
            Assert.IsTrue(cell.ContainsWall(Direction.South));
            Assert.IsTrue(cell.ContainsWall(Direction.West));
        }

        [TestMethod]
        public void RemoveWallTest()
        {
            Cell cell = new Cell();
            Assert.IsTrue(cell.ContainsWall(Direction.North));
            cell.RemoveWall(Direction.North);
            Assert.IsFalse(cell.ContainsWall(Direction.North));
        }
    }
}
