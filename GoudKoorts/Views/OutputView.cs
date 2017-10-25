using GoudKoorts.Models;
using GoudKoorts.Models.Squares.Standable;
using GoudKoorts.Models.Squares.Static;
using GoudKoorts.Models.Standable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoudKoorts.Views
{
    public class OutputView
    {
        private Dictionary<Type, int> types = new Dictionary<Type, int>
        {
            {typeof(WaterSquare), 0},
            {typeof(EmptySquare), 1},
            {typeof(LineSquare), 2},
            {typeof(StartingSquare), 3},
            {typeof(SwitchableSquare), 4},
            {typeof(CornerSquare), 5},
            {typeof(DockSquare), 6},
            {typeof(QueueableSquare), 7},
        };

        private List<ConsoleColor> _colors = new List<ConsoleColor>() {
            ConsoleColor.Red,
            ConsoleColor.Yellow,
            ConsoleColor.Green,
            ConsoleColor.Blue,
            ConsoleColor.Magenta
        };

        private int _color = 0;

        public void Print(Square square)
        {
            Clear();
            _color = 0;

            while (square != null)
            {
                Square westSquare = square;

                while (westSquare != null)
                {
                    char c = SquareToChar(westSquare);

                    PrintChar(c, westSquare);

                    westSquare = westSquare.NeighbourEast;
                }

                Console.Write("\n");
                square = square.NeighbourSouth;
            }

            PrintColorLine();
        }

        public void PrintColorLine()
        {
            Console.Write("\nWhich switch do you want to open? (press ");

            for (int i = 0; i < _colors.Count; i++)
            {
                Console.ForegroundColor = _colors[i];
                Console.Write(i + 1);
                Console.ForegroundColor = ConsoleColor.White;

                if (i != _colors.Count - 1)
                {
                    Console.Write(", ");
                }
            }

            Console.Write("): ");
        }

        private void PrintChar(char c, Square square)
        {
            int type = types[square.GetType()];

            if (type == 4)
            {
                Console.ForegroundColor = _colors[_color];
                _color++;
            }
            else if (type == 7)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
            }

            Console.Write(c);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public char SquareToChar(Square square)
        {
            char c;

            switch (types[square.GetType()])
            {
                case 0:
                    c = '~';
                    break;
                case 1:
                    c = '.';
                    break;
                case 2:
                case 7:
                    LineSquare lineSquare = square as LineSquare;

                    c = lineSquare.Alignment == Alignment.Horizontal ? '-' : '|';
                    break;
                case 3:
                    StartingSquare startingSquare = square as StartingSquare;

                    c = startingSquare.Index;
                    break;
                case 4:
                case 5:
                    CornerSquare cornerSquare = square as CornerSquare;

                    if (cornerSquare.Beginning == Direction.West)
                    {
                        if (cornerSquare.Ending == Direction.South)
                        {
                            c = '┐';
                        }
                        else
                        {
                            c = '┘';
                        }
                    }
                    else if (cornerSquare.Beginning == Direction.North)
                    {
                        c = '└';
                    }
                    else
                    {
                        c = '┌';
                    }

                    break;
                case 6:
                    c = 'K';
                    break;
                default:
                    c = '.';
                    break;
            }

            if (square is StandableSquare)
            {
                StandableSquare standableSquare = square as StandableSquare;

                if (standableSquare.Cart != null)
                {
                    c = '#';
                }
            }

            return c;
        }

        public void PrintFinalMessage(int points)
        {
            Console.WriteLine("\n\nThe carts have collision, game over.. You ended with " + points + " points.");
            Console.WriteLine("Press q to exit.");
            Console.ReadKey();
        }

        public void Clear()
        {
            Console.Clear();
            Console.WriteLine("");
        }
    }
}
