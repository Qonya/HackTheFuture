using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackTheFuture.A3
{
    public class Tile
    {
        public int Id { get; set; }
        public int Direction { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public override string ToString()
        {
            return $"ID: {Id}\nDirection: {Direction}\nX: {X}\nY: {Y}";
        }
    }
}
