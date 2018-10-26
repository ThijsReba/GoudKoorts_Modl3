using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class Ship : MovableObject
    {
        private int _countOfLoads;

        public int CountOfLoads
        {
            get
            {
                return _countOfLoads;
            }
            set
            {
                _countOfLoads = value;
            }
        }

        public Ship(Tile initialTile)
        {
            this.Spot = initialTile;
            _countOfLoads = 0;
        }

        public override Direction WayToMove()
        {
            if (Spot.TileToLeft.TileToLeft.TileToLeft == null)
            {
                return Direction.Remove;
            }

            if (CountOfLoads.Equals(8))
            {
                return Direction.Left;
            }

            if (!Spot.TileBelow.GetType().Equals(typeof(Dock)))
            {
                return Direction.Left;
            }

            return Direction.Still;
        }

        public override void MakeMove(Direction richting)
        {
            Tile targetTile = Spot.NeighbourInDirection(richting);

            //right
            Tile targetTile2 = Spot.NeighbourInDirection(richting).TileToRight;
            Tile targetTile3 = Spot.NeighbourInDirection(richting).TileToRight.TileToRight;

            //left
            Tile targetTile4 = Spot.NeighbourInDirection(richting).TileToLeft;
            Tile targetTile5 = Spot.NeighbourInDirection(richting).TileToLeft.TileToLeft;

            var Tile = (Tile)targetTile;
            //right
            var Tile2 = (Water)targetTile2;
            var Tile3 = (Water)targetTile3;
            //left
            var Tile4 = (Water)targetTile4;
            var Tile5 = (Water)targetTile5;


            Tile.Place(this);
            if (Tile2 != null)
                Tile2.ToChar1 = TocharRight();
            if (Tile4 != null)
                Tile4.ToChar1 = ToCharLeft();

            //reset            
            Spot.Remove();
            if (Spot.TileToRight != null)
                Tile3.ToChar1 = '█';

            Spot = Tile;
            Spot.TileToRight = Tile2;
            Spot.TileToLeft = Tile4;
        }

        public override char ToChar()
        {
            if (CountOfLoads >= 5)
            {
                return '=';
            }
            return '_';
        }

        public char TocharRight()
        {
            if (CountOfLoads >= 8)
            {
                return '=';
            }
            return '_';
        }

        public char ToCharLeft()
        {
            if (CountOfLoads >= 4)
            {
                return '=';
            }
            return '_';
        }

    }
}
