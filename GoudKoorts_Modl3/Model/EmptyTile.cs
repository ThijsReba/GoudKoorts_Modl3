using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class EmptyTile : Tile
    {
        override public bool IsEmpty()
        {
            return false;
        }

        public override bool Place(MovableObject objectToBePlaced)
        {
            return false;
        }


        public override char ToChar()
        {
            return ' ';
        }

        public override void Remove()
        {
            this.Content = null;
        }
    }
}
