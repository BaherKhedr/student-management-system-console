using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerApplicationSystem.Helpers
{
    public static class InputHelper
    {
        public static int ReadInt(string message)
        {
            int inputValue;
            while (true)
            {
                Console.WriteLine(message);
                if (int.TryParse(Console.ReadLine(), out inputValue))
                {
                    return inputValue;
                }
                else
                    ConsoleHelper.ErrorMessage("Invalid input");
            }
        }
        public static double ReadDouble(string message)
        {
            double inputValue;
            while (true)
            {
                Console.WriteLine(message);
                if (double.TryParse(Console.ReadLine(), out inputValue))
                {
                    return inputValue;
                }
                else
                    ConsoleHelper.ErrorMessage("Invalid input");
            }
        }
    }
}
