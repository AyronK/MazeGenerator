using System;
using System.Collections.Generic;

namespace MazeGenerator
{
    public class Cell
    {
        private List<Direction> walls = new List<Direction>();

        public Cell()
        {
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                walls.Add(direction);
            }
        }
        
        public bool ContainsWall(Direction direction)
        {
            return walls.Contains(direction);
        }
        
        public void RemoveWall(Direction direction)
        {
            walls.Remove(direction);
        }
    }
}
