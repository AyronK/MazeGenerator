using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MazeGenerator;
using MazeGeneratorMVVM.ViewModel;

namespace MazeGeneratorMVVMTest
{
    [TestClass]
    public class CellItemTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            Cell cell = new Cell();
            CellItem cellItem = new CellItem(cell)
            {
                TopLocation = 0,
                LeftLocation = 0,
                Width = 0
            };

            Assert.AreEqual(cell.ContainsWall(Direction.North), cellItem.NorthWall);
            Assert.AreEqual(cell.ContainsWall(Direction.West), cellItem.WestWall);
            Assert.AreEqual(cell.ContainsWall(Direction.East), cellItem.EastWall);
            Assert.AreEqual(cell.ContainsWall(Direction.South), cellItem.SouthWall);
            Assert.AreEqual(0, cellItem.TopLocation);
            Assert.AreEqual(0, cellItem.LeftLocation);
            Assert.AreEqual(0, cellItem.Width);
            Assert.IsNull(cellItem.Background);
        }

        [TestMethod]
        public void ConstructorFromChangedCellTest()
        {
            Cell cell = new Cell();
            cell.RemoveWall(Direction.North);
            cell.RemoveWall(Direction.East);
            CellItem cellItem = new CellItem(cell);
            Assert.AreEqual(cell.ContainsWall(Direction.North), cellItem.NorthWall);
            Assert.AreEqual(cell.ContainsWall(Direction.West), cellItem.WestWall);
            Assert.AreEqual(cell.ContainsWall(Direction.East), cellItem.EastWall);
            Assert.AreEqual(cell.ContainsWall(Direction.South), cellItem.SouthWall);
        }
    }
}
