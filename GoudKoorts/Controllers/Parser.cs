using GoudKoorts.Models;
using GoudKoorts.Models.Squares.Standable;
using GoudKoorts.Models.Squares.Static;
using GoudKoorts.Models.Standable;
using GoudKoorts.Models.Static;
using System;
using System.IO;

namespace GoudKoorts.Controllers
{
    class Parser
    {
        private string _file;
        private FileSelector _fileSelector;
        private int _switches = 1;

        public Parser()
        {
            _file = GuessPath();
        }

        public Game Parse()
        {
            Game game = new Game();
            Square north = null, west = null;

            foreach (string line in File.ReadAllLines(_file))
            {
                foreach (char c in line)
                {
                    Square square = CharToSquare(c);

                    if (square is SwitchableSquare)
                    {
                        game.Switches.Add(_switches, square as SwitchableSquare);
                        _switches++;
                    }
                    else if (square is StartingSquare)
                    {
                        game.StartingPoints.Add(square as StartingSquare);
                    }

                    SetNeighbours(square, north, west);

                    if (game.Square == null)
                    {
                        game.Square = square;
                    }

                    west = square;
                    north = GetNextNorth(north);
                }

                north = GetOuterWest(west);
                west = null;
            }

            return game;
        }

        private void SetNeighbours(Square square, Square north, Square west)
        {
            if (north != null)
            {
                square.NeighbourNorth = north;
                north.NeighbourSouth = square;
            }

            if (west != null)
            {
                square.NeighbourWest = west;
                west.NeighbourEast = square;
            }
        }

        private Square GetOuterWest(Square square)
        {
            while (square.NeighbourWest != null)
            {
                square = square.NeighbourWest;
            }

            return square;
        }

        private Square GetNextNorth(Square north)
        {
            if (north == null)
            {
                return null;
            }

            return north.NeighbourEast;
        }

        private Square CharToSquare(char c)
        {
            switch(c)
            {
                case '~':
                    return new WaterSquare();
                case '.':
                    return new EmptySquare();
                case '-':
                    return new LineSquare(Alignment.Horizontal);
                case '|':
                    return new LineSquare(Alignment.Vertical);
                case 'A':
                case 'B':
                case 'C':
                    return new StartingSquare(c);
                case '{':
                case '}':
                    return SwitchableSquare();
                case '┐':
                    return new CornerSquare(Direction.West, Direction.South);
                case '┌':
                    return new CornerSquare(Direction.South, Direction.East);
                case '┘':
                    return new CornerSquare(Direction.West, Direction.North);
                case '└':
                    return new CornerSquare(Direction.North, Direction.East);
                case 'k':
                    return new DockSquare();
                case '=':
                    return new QueueableSquare(Alignment.Horizontal);
                default:
                    throw new Exception("Character not recoginized");
            }
        }

        private SwitchableSquare SwitchableSquare()
        {
            switch (_switches)
            {
                case 1:
                    return new SwitchableSquare(Direction.North, Direction.East);
                case 2:
                    return new SwitchableSquare(Direction.West, Direction.North);
                case 3:
                    return new SwitchableSquare(Direction.North, Direction.East);
                case 4:
                    return new SwitchableSquare(Direction.South, Direction.East);
                case 5:
                    return new SwitchableSquare(Direction.West, Direction.South);
                default:
                    return new SwitchableSquare(Direction.Empty, Direction.Empty);
            }
        }

        private string GuessPath()
        {
            if (File.Exists("map.txt"))
            {
                return "map.txt";
            }
            else if (File.Exists("..\\..\\map.txt"))
            {
                return "..\\..\\map.txt";
            }

            Console.WriteLine("We couldn't find the default map.txt file. Please press enter to select it manually.");
            Console.ReadLine();

            _fileSelector = new FileSelector();

            return _fileSelector.Get();
        }
    }
}