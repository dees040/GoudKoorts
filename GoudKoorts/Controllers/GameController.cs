using GoudKoorts.Models;
using GoudKoorts.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoudKoorts.Controllers
{
    public class GameController
    {
        private Parser _parser = new Parser();
        private OutputView _outputView = new OutputView();
        private InputView _inputView = new InputView();
        private Game _game;
        private Thread _timer;

        public GameController()
        {
            _game = _parser.Parse();
        }

        public void Start()
        {
            _timer = new Thread(new ThreadStart(RunTimer));
            _timer.Start();

            Play();
        }

        public void Stop(object sender, EventArgs args)
        {
            _timer.Abort();
        }

        private void Play()
        {
            if (_game.HasEnded())
            {
                _outputView.Print(_game.GetNorthWestSquare(), _game.Points);

                _outputView.PrintFinalMessage(_game.Points);
            }
            else
            {
                _outputView.Print(_game.GetNorthWestSquare(), _game.Points);

                HandleInput(_inputView.GetOption());
            }
        }

        private void HandleInput(int option)
        {
            if (option == 0)
            {
                Quit();

                return;
            }

            _game.PrepareSwitchToggle(option);

            Play();
        }

        private void RunTimer()
        {
            if (_game.HasEnded())
            {
                _outputView.Print(_game.GetNorthWestSquare(), _game.Points);

                _outputView.PrintFinalMessage(_game.Points);
            }
            else
            {
                Task.Delay(CalculateTime()).ContinueWith(t => HandleCartsAndShip());
            }
        }

        private void HandleCartsAndShip()
        {
            _game.MoveShip();

            _game.MoveCarts();

            _game.CreateShip();

            _game.CreateCarts(Stop);

            _outputView.Print(_game.GetNorthWestSquare(), _game.Points);

            RunTimer();
        }

        private int CalculateTime()
        {
            int startingTime = 2000;

            startingTime = startingTime - _game.Points * 20;

            return startingTime < 500 ? 500 : startingTime;
        }

        private void Quit()
        {
            Console.WriteLine("Exiting..");
        }
    }
}
