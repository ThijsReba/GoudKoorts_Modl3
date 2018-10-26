using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class Start : Tile
    {
        private readonly char _char;

        public Start(char _char)
        {
            this._char = _char;
        }

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
            if (!this.IsEmpty())
            {
                return Content.ToChar();
            }
            else
            {
                return this._char;
            }
        }

        public override void Remove()
        {
            this.Content = null;
        }
    }
}
