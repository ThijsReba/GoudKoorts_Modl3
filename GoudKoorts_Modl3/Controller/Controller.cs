using View;
using Model;
using System.Timers;
using System;

namespace Controller
{
    public class Controller
    {
        private InputView _inputView;
        private OutputView _outputView;
        private GameBoard _gameBoard;
        private LevelParser _parser;
        private Timer _gameTimer;
        private Timer _cartTimer;
        private Timer _shipTimer;

        public Controller()
        {
            _inputView = new InputView();
            _outputView = new OutputView();

            InitializeTimers();
            Go();
        }


        public void Go()
        {
            _parser = new LevelParser();
            _outputView.ShowStart();
            
            _gameBoard = _parser.LoadGameBoard(this);
            _gameBoard.Collision = false;
            
            _outputView.UpdateBoard(_gameBoard);
            
            _gameTimer.Start();
            _cartTimer.Start();
            _shipTimer.Start();

            _gameBoard.ThrowNewShip();
            
            while (!_gameBoard.Collision)
            {
                if (_gameBoard.ChangeSwitchStands(_inputView.AskForSwitch()))
                {
                    _outputView.UpdateBoard(_gameBoard);
                }
            }
            
            _gameTimer.Stop();
            _cartTimer.Stop();
            _shipTimer.Stop();
            
            _outputView.ShowGameOver(_gameBoard);
        }
        
        private void OnTimedEventGame(object source, ElapsedEventArgs e)
        {
            if (!_gameBoard.Collision)
            {
                _gameBoard.MoveCarts();
                _outputView.UpdateBoard(_gameBoard);
            }
        }
        
        private void OnTimedEventCart(object source, ElapsedEventArgs e)
        {
            if (!_gameBoard.Collision)
            {
                _gameBoard.ThrowNewCart();
            }
        }

        private void OnTimedEventShip(object source, ElapsedEventArgs e)
        {
            if (_gameBoard.CheckScoreShip() == 0 && _gameBoard.NumberOfItemsIn_ShipList() == 0)
            {
                _gameBoard.ThrowNewShip();
            }
            _gameBoard.MoveShip();
        }

        public void IncrementNewCartSpeed()
        {
            _cartTimer.Interval = _cartTimer.Interval - TimeSpan.FromMilliseconds(250).TotalMilliseconds;
            _gameTimer.Interval = _gameTimer.Interval - TimeSpan.FromMilliseconds(10).TotalMilliseconds;
        }

        private void InitializeTimers()
        {
            _gameTimer = new Timer();
            _gameTimer.Elapsed += new ElapsedEventHandler(OnTimedEventGame);
            _gameTimer.Interval = 1500;

            _cartTimer = new Timer();
            _cartTimer.Elapsed += new ElapsedEventHandler(OnTimedEventCart);
            _cartTimer.Interval = 8000;

            _shipTimer = new Timer();
            _shipTimer.Elapsed += new ElapsedEventHandler(OnTimedEventShip);
            _shipTimer.Interval = 1500;
        }
    }
}