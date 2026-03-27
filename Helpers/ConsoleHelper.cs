using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerApplicationSystem.Helpers
{
    public static class ConsoleHelper
    {
        public static void ErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n-----------------------------------");
            Console.WriteLine("ERROR: " + message);
            Console.WriteLine("-----------------------------------");
            Console.ResetColor();
        }
    }
}
