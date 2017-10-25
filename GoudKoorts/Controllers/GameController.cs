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

        public GameController()
        {
            _game = _parser.Parse();
        }

        public void Start()
        {
            Thread timerThread = new Thread(new ThreadStart(RunTimer));
            timerThread.Start();

            Play();
        }

        public void Stop(object sender, EventArgs args)
        {
            Console.WriteLine("Game stopped!");
        }

        private void Play()
        {
            _outputView.Print(_game.GetNorthWestSquare());

            HandleInput(_inputView.GetOption());
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
            Task.Delay(CalculateTime()).ContinueWith(t => HandleCarts());
        }

        private void HandleCarts()
        {
            _game.MoveCarts();

            _game.CreateCarts(Stop);

            _outputView.Print(_game.GetNorthWestSquare());

            RunTimer();
        }

        private int CalculateTime()
        {
            int startingTime = 2000;

            startingTime = startingTime - (_game.Level * 50);

            return startingTime < 500 ? 500 : startingTime;
        }

        private void Quit()
        {
            Console.WriteLine("Exiting..");
        }
    }
}
