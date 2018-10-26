using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class Warehouse : Tile
    {
        override public bool IsEmpty()
        {
            return this.Content == null;
        }

        public override bool Place(MovableObject objectToBePlaced)
        {
            if (this.IsEmpty())
            {
                this.Content = objectToBePlaced;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override char ToChar()
        {
            if (this.Content != null)
                return this.Content.ToChar();
            else
                return '.';
        }

        public override void Remove()
        {
            this.Content = null;
        }

    }
}
