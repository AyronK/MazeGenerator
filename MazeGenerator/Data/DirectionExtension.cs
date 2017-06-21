using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGenerator
{
    public enum Direction
    {
        North,
        East,
        South,
        West
    }
    
    static class DirectionExtension
    {
        /// <summary>
        /// Reverses direction
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static Direction Opposite(this Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    return Direction.South;
                case Direction.South:
                    return Direction.North;
                case Direction.West:
                    return Direction.East;
                case Direction.East:
                    return Direction.West;
                default:
                    throw new ArgumentOutOfRangeException("Unknown direction");
            }
        }
       
    }
}
