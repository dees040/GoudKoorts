using GoudKoorts.Models;
using GoudKoorts.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace GoudKoorts.Controllers
{
    public class GameController
    {
        private Game _game;
        private Parser _parser = new Parser();
        private InputView _inputView = new InputView();
        private OutputView _outputView = new OutputView();

        private Thread _thread;
        private int _countDown = 4;
        private System.Timers.Timer _timer;

        public GameController()
        {
            _game = _parser.Parse();
        }

        public void Start()
        {
            _thread = new Thread(new ThreadStart(HandleInput));
            _thread.Start();

            _timer = new System.Timers.Timer { Interval = 250 };
            _timer.Elapsed += new ElapsedEventHandler(Play);
            _timer.Start();
        }

        private void Play(object sender, ElapsedEventArgs e)
        {
            _countDown--;

            _outputView.Print(_game.Square, _game.Points);

            if (_countDown != 0)
            {
                return;
            }

            _game.MoveAndCreateVehicles();
            SetGameSpeed();

            if (_game.HasEnded())
            {
                _timer.Enabled = false;
                _outputView.PrintFinalMessage(_game.Square, _game.Points);
            }

            _countDown = 4;
        }

        private void HandleInput()
        {
            while (!_game.HasEnded())
            {
                int option = _inputView.GetOption();

                if (option == 0)
                {
                    _timer.Enabled = false;

                    return;
                }

                _game.PrepareSwitchToggle(option);
            }
        }

        private void SetGameSpeed()
        {
            _timer.Interval = CalculateTime();
        }

        private int CalculateTime()
        {
            int startingTime = 250;

            startingTime = Math.Max(100, startingTime - _game.Points);

            return startingTime < 40 ? 40 : startingTime;
        }
    }
}
