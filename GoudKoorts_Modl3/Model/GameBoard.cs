using System;
using System.Collections.Generic;
using System.Linq;
using Controller;

namespace Model
{
    public class GameBoard
    {
        private List<Cart> _CartList;
        private List<Ship> _ShipList;
        private Boolean _Collision;
        private int _TotalScore = 0;
        private Controller.Controller controller;

        public GameBoard(Controller.Controller controller)
        {
            _CartList = new List<Cart>();
            _ShipList = new List<Ship>();
            this.controller = controller;
        }

        public virtual IEnumerable<Tile> Tile
        {
            get;
            set;
        }

        public Tile Origin
        {
            get;
            set;
        }

        public Tile PointA
        {
            get;
            set;
        }

        public Tile PointB
        {
            get;
            set;
        }

        public Tile PointC
        {
            get;
            set;
        }

        public Switch Switch1
        {
            get;
            set;
        }

        public Switch Switch2
        {
            get;
            set;
        }
        public Switch Switch3
        {
            get;
            set;
        }

        public Switch Switch4
        {
            get;
            set;
        }
        public Switch Switch5
        {
            get;
            set;
        }

        public Water ShipStart
        {
            get;
            set;
        }

        public Water ShipEnd
        {
            get;
            set;
        }

        public int Width
        {
            get;
            set;
        }
        public int Height
        {
            get;
            set;
        }
        public bool Collision
        {
            get
            {
                return _Collision;
            }
            set
            {
                _Collision = value;
            }
        }

        public int TotalScore
        {
            get
            {
                return _TotalScore;
            }
            set
            {
                _TotalScore = value;
                controller.IncrementNewCartSpeed();
            }
        }


        public bool ChangeSwitchStands(int switchNumber)
        {
            switch (switchNumber)
            {
                case 1:
                    Switch1.ChangeSwitchState();
                    return true;
                case 2:
                    Switch2.ChangeSwitchState();
                    return true;
                case 3:
                    Switch3.ChangeSwitchState();
                    return true;
                case 4:
                    Switch4.ChangeSwitchState();
                    return true;
                case 5:
                    Switch5.ChangeSwitchState();
                    return true;
            }
            return false;
        }

        public void ThrowNewCart()
        {
            Tile newStartTile = GenerateStartTile();
            Cart newCart = new Cart(newStartTile);
            _CartList.Add(newCart);
            newStartTile.Place(newCart);
        }

        public void ThrowNewShip()
        {
            Ship newShip = new Ship(ShipStart);
            _ShipList.Add(newShip);
            ShipStart.Place(newShip);
        }

        public void MoveShip()
        {
            foreach (Ship ship in _ShipList.ToList())
            {
                Direction richting = ship.WayToMove();
                if (richting != Direction.Still)
                    if (richting.Equals(Direction.Remove))
                    {
                        _ShipList.Remove(ship);
                        ShipEnd.ToChar1 = '█';
                        Water Tile1 = (Water)ShipEnd.TileToRight;
                        Tile1.ToChar1 = '█';
                        Tile1.Remove();
                        Water Tile2 = (Water)ShipEnd.TileToRight.TileToRight;
                        Tile2.ToChar1 = '█';
                        Tile2.Remove();
                        Water Tile3 = (Water)ShipEnd.TileToRight.TileToRight.TileToRight;
                        Tile3.ToChar1 = '█';
                        Tile3.Remove();
                        TotalScore += 10;
                    }
                    else
                        ship.MakeMove(richting);
            }
        }

        public int CheckScoreShip()
        {
            if (_ShipList.Count != 0)
                return _ShipList.First().CountOfLoads;

            return 0;
        }

        public int NumberOfItemsIn_ShipList()
        {
            return _ShipList.Count;
        }
        public void MoveCarts()
        {
            foreach (Cart cart in _CartList.ToList())
            {
                if (cart.checkGameOver())
                    Collision = true;

                Direction richting = cart.WayToMove();
                if (richting != Direction.Still)
                    if (richting == Direction.Remove)
                    {
                        _CartList.Remove(cart);
                        ShipEnd.TileBelow.TileBelow.Remove();
                    }
                    else
                        cart.MakeMove(richting);

                if (cart.checkScore())
                {
                    if (_ShipList.Count != 0)
                    {
                        _ShipList.First().CountOfLoads++;
                        TotalScore++;
                    }
                }
            }
        }

        private Tile GenerateStartTile()
        {
            List<Tile> startPointList = new List<Tile>
            {
                PointA,
                PointB,
                PointC
            };
            Random rnd = new Random();
            int r = rnd.Next(startPointList.Count);
            return (Tile)startPointList[r];
        }
    }
}
