using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGenerator
{
    internal static class ListRandomExtension
    {
        private static Random random = new Random();
        internal static T RandomElement<T>(this List<T> list)
        {
            int r = random.Next(0, list.Count);
            return list[r];
        }
    }

}
