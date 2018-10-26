using System;

namespace View
{
    public class InputView
    {
        public int AskForSwitch()
        {
            int returnValue = 0;
            bool proceed = true;
            ConsoleKeyInfo input;
            ConsoleKey k = ConsoleKey.Escape;
            while (proceed)
            {
                input = Console.ReadKey();
                k = input.Key;
                if (k != ConsoleKey.D1 && k != ConsoleKey.D2 && k != ConsoleKey.D3 && k != ConsoleKey.D4 && k != ConsoleKey.D5
                    && k != ConsoleKey.NumPad1 && k != ConsoleKey.NumPad2 && k != ConsoleKey.NumPad3 && k != ConsoleKey.NumPad4
                    && k != ConsoleKey.NumPad5 && k != ConsoleKey.S && k != ConsoleKey.R)
                {
                    Console.WriteLine("> ?");
                }
                else
                {
                    proceed = false;
                }
            }

            switch (k)
            {
                case ConsoleKey.D1:
                    returnValue = 1;
                    break;

                case ConsoleKey.D2:
                    returnValue = 2;
                    break;

                case ConsoleKey.D3:
                    returnValue = 3;
                    break;

                case ConsoleKey.D4:
                    returnValue = 4;
                    break;

                case ConsoleKey.D5:
                    returnValue = 5;
                    break;

                case ConsoleKey.NumPad1:
                    returnValue = 1;
                    break;

                case ConsoleKey.NumPad2:
                    returnValue = 2;
                    break;

                case ConsoleKey.NumPad3:
                    returnValue = 3;
                    break;

                case ConsoleKey.NumPad4:
                    returnValue = 4;
                    break;

                case ConsoleKey.NumPad5:
                    returnValue = 5;
                    break;

                default:
                    throw new NotImplementedException();

            }
            return returnValue;

        }
    }
}