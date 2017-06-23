using MazeGenerator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MazeGeneratorTest
{
    [TestClass]
    public class MazeTest
    {
        [TestMethod]
        public void OneParamConstructorTest()
        {
            Maze maze = new Maze(1);
            Assert.AreEqual(1, maze.RowsCount);
            Assert.AreEqual(1, maze.ColumnsCount);
        }

        [TestMethod]
        public void TwoParamConstructorTest()
        {
            Maze maze = new Maze(1, 2);
            Assert.AreEqual(1, maze.RowsCount);
            Assert.AreEqual(2, maze.ColumnsCount);
        }

        [TestMethod]
        public void GetValueTest()
        {
            Maze maze = new Maze(1, 1);
            Assert.IsInstanceOfType(maze[0, 0], typeof(Cell));
        }

        [TestMethod]
        public void RowOfColumnOfTest()
        {
            Maze maze = new Maze(5, 10);
            Assert.AreEqual(1, maze.RowOf(maze[1, 7]));
            Assert.AreEqual(7, maze.ColumnOf(maze[1, 7]));
        }

        [TestMethod]
        public void ContainsTest()
        {
            Maze maze = new Maze(1, 1);
            Assert.IsTrue(maze.Contains(maze[0, 0]));
        }

        [TestMethod]
        public void IsNeighbourTest()
        {
            Maze maze = new Maze(2);
            Cell cell = maze[0, 0];
            Cell neighbour = maze[0, 1];
            Assert.IsTrue(maze.IsNeighbour(cell, neighbour));
        }

        [TestMethod]
        public void IsNotNeighbourTest()
        {
            Maze maze = new Maze(2);
            Cell cell = maze[0, 0];
            Cell neighbour = maze[1, 1];
            Assert.IsFalse(maze.IsNeighbour(cell, neighbour));
        }

        [TestMethod]
        public void IsWallBetweenNeighboursTest()
        {
            Maze maze = new Maze(2);
            Cell cell = maze[0, 0];
            Cell neighbour = maze[0, 1];
            Assert.IsTrue(maze.IsWallBetween(cell, neighbour));
        }

        [TestMethod]
        public void IsNotWallBetweenNeighboursAfterRemoveTest()
        {
            Maze maze = new Maze(2);
            Cell cell = maze[0, 0];
            Cell neighbour = maze[0, 1];
            maze.RemoveWallsBetween(cell, neighbour);
            Assert.IsFalse(maze.IsWallBetween(cell, neighbour));
        }

        [TestMethod]
        public void IsWallBetweenNotNeighboursTest()
        {
            Maze maze = new Maze(2);
            Cell cell = maze[0, 0];
            Cell notNeighbour = maze[1, 1];
            Assert.IsFalse(maze.IsWallBetween(cell, notNeighbour));
        }

        [TestMethod]
        public void GetNeighboursCornerTest()
        {
            Maze maze = new Maze(2);
            Cell cell = maze[0, 0];
            List<Cell> neighbours = new List<Cell>(maze.GetNeighbours(cell));
            Assert.AreEqual(2, neighbours.Count);
            Assert.IsTrue(neighbours.Contains(maze[1, 0]));
            Assert.IsTrue(neighbours.Contains(maze[0, 1]));
        }

        [TestMethod]
        public void GetNeighboursCenterrTest()
        {
            Maze maze = new Maze(3);
            Cell cell = maze[1, 1];
            List<Cell> neighbours = new List<Cell>(maze.GetNeighbours(cell));
            Assert.AreEqual(4, neighbours.Count);
            Assert.IsTrue(neighbours.Contains(maze[1, 0]));
            Assert.IsTrue(neighbours.Contains(maze[1, 2]));
            Assert.IsTrue(neighbours.Contains(maze[2, 1]));
            Assert.IsTrue(neighbours.Contains(maze[0, 1]));
        }

    }
}
