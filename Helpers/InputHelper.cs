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
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out inputValue) && inputValue > 0)
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
                Console.Write(message);
                if (double.TryParse(Console.ReadLine(), out inputValue) && inputValue >= 0)
                {
                    return inputValue;
                }
                else
                    ConsoleHelper.ErrorMessage("Invalid input");
            }
        }
        public static string ReadString(string message)
        {
            string? inputValue;
            while (true)
            {
                Console.Write(message);
                inputValue = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(inputValue))
                {
                    return inputValue;
                }
                else
                {
                    ConsoleHelper.ErrorMessage("Invalid input.");
                }
            }
        }
    }
}
