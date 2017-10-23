using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoudKoorts.Views
{
    class InputView
    {
        public int GetOption()
        {
            Console.WriteLine("\nWhich switch do you want to open? (press: 1, 2, 3, 4, 5):");

            char input = Console.ReadKey().KeyChar;

            if (IncorrectInput(input))
            {
                Console.WriteLine("Please provide a number between 1 and 5 or press q to exit.");

                return GetOption();
            }

            return CharToInt(input.ToString());
        }

        private bool IncorrectInput(char input)
        {
            int givenInt = CharToInt(input.ToString());

            return givenInt < 0 || givenInt > 5;
        }

        private int CharToInt(string input)
        {
            if (input.ToLower() == "q")
            {
                return 0;
            }

            try
            {
                return Int32.Parse(input);
            }
            catch
            {
                return -1;
            }
        }
    }
}
