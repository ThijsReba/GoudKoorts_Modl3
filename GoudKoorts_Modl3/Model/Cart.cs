using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class Cart : MovableObject
    {
        private char _toChar;

        public Cart(Tile initialTile)
        {
            this.Spot = initialTile;
            _toChar = 'F';
        }

        public override Direction WayToMove()
        {
            if (Spot.GetType().Equals(typeof(Start)))
            {
                return Direction.Right;
            }

            if (Spot.GetType().Equals(typeof(Warehouse)))
            {
                if (Spot.TileToLeft.GetType().Equals(typeof(EmptyTile)))
                    return Direction.Still;

                if (Spot.TileToLeft.IsEmpty())
                    return Direction.Left;
            }

            if (Spot.GetType().Equals(typeof(Track)))
            {
                if (Spot.TileToLeft == null)
                {
                    return Direction.Remove;
                }

                if (Spot.TileToLeft.GetType().Equals(typeof(Warehouse)) && Spot.TileToLeft.Content != null)
                    return Direction.Still;

                if (Spot.TileAbove.GetType().Equals(typeof(Water)) || Spot.TileAbove.GetType().Equals(typeof(Dock)))
                    return Direction.Left;

                if (Spot.TileAbove.GetType().Equals(typeof(Track)) && Spot.TileToLeft.TileToLeft.TileToLeft.GetType().Equals(typeof(Warehouse)))
                    return Direction.Left;

                if (Spot.TileAbove.GetType().Equals(typeof(Track)) && Spot.TileBelow.GetType().Equals(typeof(Switch))
                    && Spot.TileToRight.GetType().Equals(typeof(EmptyTile)) && Spot.TileToRight.TileToRight.GetType().Equals(typeof(EmptyTile)))
                    return Direction.Up;

                if (Spot.TileToRight.GetType().Equals(typeof(Track)))
                {
                    Tile targetTile = Spot.NeighbourInDirection(Direction.Right);
                    if (!targetTile.IsEmpty())
                    {
                        return Direction.Still;
                    }
                    if (targetTile.IsEmpty() && !targetTile.GetType().Equals(typeof(EmptyTile)))
                    {
                        return Direction.Right;
                    }
                    else
                        return Direction.Still;
                }
                if (Spot.TileAbove.GetType().Equals(typeof(Switch)))
                {
                    Tile targetTile = Spot.NeighbourInDirection(Direction.Up);

                    if (targetTile.ToChar().Equals('\\'))
                        return Direction.Still; ;
                    if (targetTile.ToChar().Equals('/'))
                    {
                        return Direction.Up;
                    }
                    else
                        return Direction.Still;
                }
                if (Spot.TileBelow.GetType().Equals(typeof(Switch)))
                {
                    Tile targetTile = Spot.NeighbourInDirection(Direction.Down);

                    if (targetTile.ToChar().Equals('\\'))
                    {
                        return Direction.Down;
                    }
                    if (targetTile.ToChar().Equals('/'))
                        return Direction.Still;
                    else
                        return Direction.Still;
                }
                if (Spot.TileToRight.GetType().Equals(typeof(Switch)))
                {
                    Tile targetTile = Spot.NeighbourInDirection(Direction.Right);

                    if (targetTile.ToChar().Equals('S'))
                    {
                        return Direction.Still;
                    }
                    else
                        return Direction.Right;
                }

                if (Spot.TileToRight.GetType().Equals(typeof(EmptyTile)) && Spot.TileBelow.GetType().Equals(typeof(EmptyTile))
                    && !Spot.TileToLeft.TileToLeft.TileToLeft.GetType().Equals(typeof(Warehouse)))
                    return Direction.Up;

                if (Spot.TileToRight.GetType().Equals(typeof(EmptyTile)) && Spot.TileToLeft.GetType().Equals(typeof(EmptyTile)))
                    return Direction.Up;

                if (Spot.TileBelow.GetType().Equals(typeof(Track)) && Spot.TileAbove.GetType().Equals(typeof(EmptyTile)))
                    return Direction.Down;
            }

            if (Spot.GetType().Equals(typeof(Switch)))
            {
                Tile targetTile = Spot.NeighbourInDirection(Direction.Down);
                if (Spot.TileToRight.GetType().Equals(typeof(Track)))
                {
                    return Direction.Right;
                }

                Switch Tile = (Switch)Spot;
                if (Tile.ToDirection().Equals(SwitchDirection.UP))
                    return Direction.Up;
                if (Tile.ToDirection().Equals(SwitchDirection.DOWN))
                    return Direction.Down;
            }
            return Direction.Still;
        }

        public bool checkGameOver()
        {
            if (Spot.GetType().Equals(typeof(Start)))
            {
                if (Spot.TileToRight.Content != null)
                {
                    if (Spot.TileToRight.Content.ToChar().Equals('W'))
                        return true;
                }
            }
            else if (!Spot.GetType().Equals(typeof(Warehouse)))
            {
                if (Spot.TileToLeft == null)
                    return false;
                if (Spot.TileToRight.Content != null)
                {
                    if (Spot.TileToRight.Content.ToChar().Equals('W'))
                        return true;
                }
                if (Spot.TileToLeft.Content != null)
                {
                    if (Spot.TileToLeft.Content.ToChar().Equals('W'))
                        return true;
                }
            }
            return false;
        }

        public override void MakeMove(Direction richting)
        {
            Tile targetTile = Spot.NeighbourInDirection(richting);
            var Tile = (Tile)targetTile;
            Tile.Place(this);
            Spot.Remove();
            Spot = Tile;
        }

        public bool checkScore()
        {
            if (Spot.TileAbove.GetType().Equals(typeof(Dock)) && Spot.TileAbove.TileAbove.Content != null)
            {
                _toChar = 'E';
                return true;
            }
            return false;
        }

        public override char ToChar()
        {
            return _toChar;
        }
    }
}