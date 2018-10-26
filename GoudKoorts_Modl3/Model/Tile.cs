using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public abstract class Tile
    {
        public Tile TileToRight
        {
            get;
            set;
        }
        public Tile TileBelow
        {
            get;
            set;
        }
        public Tile TileToLeft
        {
            get;
            set;
        }
        public Tile TileAbove
        {
            get;
            set;
        }

        public MovableObject Content
        {
            get;
            set;
        }

        public MovableObject MovableObject
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public abstract bool IsEmpty();
        public abstract char ToChar();
        public abstract bool Place(MovableObject objectToBePlaced);
        public abstract void Remove();

        public Tile NeighbourInDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    return TileToLeft;
                case Direction.Right:
                    return TileToRight;
                case Direction.Up:
                    return TileAbove;
                case Direction.Down:
                    return TileBelow;
                default:
                    return null;
            }
        }

    }
}
