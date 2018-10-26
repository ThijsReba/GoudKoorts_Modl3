using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Switch : Tile
    {
        private char _char;
        private SwitchDirection _switchDirection;

        public SwitchDirection SwitchDirection
        {
            get
            {
                return _switchDirection;
            }
            set
            {
                _switchDirection = value;
            }
        }


        public Switch(char _char)
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

        public void ChangeSwitchState()
        {
            if (SwitchDirection == SwitchDirection.MIDDLE)
            {
                SwitchDirection = SwitchDirection.UP;
                this._char = '/';
            }
            else if (SwitchDirection == SwitchDirection.UP)
            {
                SwitchDirection = SwitchDirection.DOWN;
                this._char = '\\';
            }
            else if (SwitchDirection == SwitchDirection.DOWN)
            {
                SwitchDirection = SwitchDirection.UP;
                this._char = '/';
            }
        }

        public SwitchDirection ToDirection()
        {
            return this.SwitchDirection;
        }

        public override char ToChar()
        {
            if (this.Content != null)
                return this.Content.ToChar();
            else
                return this._char;
        }

        public override void Remove()
        {
            this.Content = null;
        }

    }
}
