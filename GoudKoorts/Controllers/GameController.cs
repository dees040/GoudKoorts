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
        private OutputView _outputView = new OutputView();
        private InputView _inputView = new InputView();

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

            _timer = new System.Timers.Timer { Interval = 200 };
            _timer.Elapsed += new ElapsedEventHandler(Play);
            _timer.Start();
        }

        public void Stop(object sender, EventArgs args)
        {
            _thread.Abort();
        }

        private void Play(object sender, ElapsedEventArgs e)
        {
            _countDown--;

            _outputView.Print(_game.GetNorthWestSquare(), _game.Points);

            if (_countDown != 0)
            {
                return;
            }

            _game.MoveAndCreateObjects(Stop);
            SetGameSpeed();

            if (_game.HasEnded())
            {
                _outputView.PrintFinalMessage(_game.Points);
                _timer.Stop();
            }

            _countDown = 4;
        }

        private void HandleInput()
        {
            while (true)
            {
                int option = _inputView.GetOption();

                if (option == 0)
                {
                    _timer.Stop();

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
            int startingTime = 200;

            startingTime = Math.Max(100, startingTime - _game.Points);

            return startingTime < 40 ? 40 : startingTime;
        }
    }
}
