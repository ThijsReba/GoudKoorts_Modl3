using System;
using System.Collections.Generic;
using System.IO;
using Model;    

namespace Controller
{
    class LevelParser
    {
        private string _pathName = "";
        private FileStream _inputStream;
        private StreamReader _streamReader;
        private GameBoard _gameBoard;
        private Controller _controller;

        public Model.GameBoard GameBoard
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public GameBoard LoadGameBoard(Controller _controller)
        {
            this._controller = _controller;
            _gameBoard = new GameBoard(_controller);
            _pathName = String.Concat("..\\..\\Map\\Map", ".txt");

            List<Tile> previousLine = null;
            int lineCounter = 0;
            try
            {
                DetectDimensions();
                _inputStream = new FileStream(_pathName, FileMode.Open, FileAccess.Read);
                _streamReader = new StreamReader(_inputStream);
                string lineString = _streamReader.ReadLine();
                do
                {
                    if (lineString != null)
                    {
                        List<Tile> currentLine;
                        currentLine = ProcesLine(lineString, lineCounter);
                        if (previousLine != null)
                        {
                            LinkLines(previousLine, currentLine);
                        }
                        else
                        {
                            _gameBoard.Origin = currentLine[0];
                        }
                        previousLine = currentLine;
                        lineCounter++;
                        lineString = _streamReader.ReadLine();
                    }
                    else
                    {
                        _streamReader.Close();
                        _inputStream.Close();
                    }
                }
                while (lineString != null);

                return _gameBoard;
            }
            catch (Exception)
            {
                throw new FileLoadException();
            }
        }

        private List<Tile> ProcesLine(string lineString, int y)
        {
            List<Tile> TileLine = new List<Tile>();
            Tile previousTile = null;
            for (int x = 0; x < lineString.Length; x++)
            {
                Tile tile;

                switch (lineString[x])
                {
                    case 'W':
                        tile = new Water();
                        break;
                    case 'D':
                        tile = new Dock();
                        break;
                    case 'R':
                        tile = new Track('-');
                        break;
                    case 'U':
                        tile = new Track('|');
                        break;
                    case '1':
                        tile = new Switch('S');
                        _gameBoard.Switch1 = (Switch)tile;
                        _gameBoard.Switch1.SwitchDirection = SwitchDirection.MIDDLE;
                        break;
                    case '2':
                        tile = new Switch('S');
                        _gameBoard.Switch2 = (Switch)tile;
                        _gameBoard.Switch2.SwitchDirection = SwitchDirection.MIDDLE;
                        break;
                    case '3':
                        tile = new Switch('S');
                        _gameBoard.Switch3 = (Switch)tile;
                        _gameBoard.Switch3.SwitchDirection = SwitchDirection.MIDDLE;
                        break;
                    case '4':
                        tile = new Switch('S');
                        _gameBoard.Switch4 = (Switch)tile;
                        _gameBoard.Switch4.SwitchDirection = SwitchDirection.MIDDLE;
                        break;
                    case '5':
                        tile = new Switch('S');
                        _gameBoard.Switch5 = (Switch)tile;
                        _gameBoard.Switch5.SwitchDirection = SwitchDirection.MIDDLE;
                        break;
                    case '8':
                        tile = new Water();
                        _gameBoard.ShipEnd = (Water)tile;
                        break;
                    case '9':
                        tile = new Water();
                        _gameBoard.ShipStart = (Water)tile;
                        break;
                    case 'A':
                        tile = new Start('A');
                        _gameBoard.PointA = tile;
                        break;
                    case 'B':
                        tile = new Start('B');
                        _gameBoard.PointB = tile;
                        break;
                    case 'C':
                        tile = new Start('C');
                        _gameBoard.PointC = tile;
                        break;
                    case 'O':
                        tile = new Warehouse();
                        break;
                    case '.':
                        tile = new EmptyTile();
                        break;
                    default:
                        tile = new EmptyTile();
                        break;
                }

                
                if (previousTile != null)
                {
                    tile.TileToLeft = previousTile;
                    previousTile.TileToRight = tile;
                }
                previousTile = tile;
                TileLine.Add(tile);
            }
            return TileLine;
        }

        private void LinkLines(List<Tile> previousLine, List<Tile> currentLine)
        {
            for (int n = 0; n < currentLine.Count; n++)
            {
                if ((previousLine[n] != null) && (currentLine[n] != null))
                {
                    currentLine[n].TileAbove = previousLine[n];
                    previousLine[n].TileBelow = currentLine[n];
                }
            }
        }

        private void DetectDimensions()
        {
            _inputStream = new FileStream(_pathName, FileMode.Open, FileAccess.Read);
            _streamReader = new StreamReader(_inputStream);
            int x = 0;
            int y = 0;

            try
            {
                string lineString = _streamReader.ReadLine();
                do
                {
                    if (lineString != null)
                    {
                        if (lineString.Length > x)
                        {
                            x = lineString.Length;
                        }
                        y++;
                        lineString = _streamReader.ReadLine();
                    }
                    else
                    {
                        _streamReader.Close();
                        _inputStream.Close();
                    }
                }
                while (lineString != null);
                _gameBoard.Width = x;
                _gameBoard.Height = y;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

    }
}
