using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public abstract class MovableObject
    {
        public Tile Spot;
        public abstract Direction WayToMove();
        public abstract void MakeMove(Direction richting);
        public abstract char ToChar();

    }
}
