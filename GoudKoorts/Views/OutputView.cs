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

        public void Print(Square square)
        {
            Clear();

            while (square != null)
            {
                Square westSquare = square;
                String line = "";

                while (westSquare != null)
                {
                    line += SquareToChar(westSquare);

                    westSquare = westSquare.NeighbourEast;
                }

                square = square.NeighbourSouth;
                Console.WriteLine(line);
            }
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

        public void Clear()
        {
            Console.Clear();
            Console.WriteLine("");
        }
    }
}
