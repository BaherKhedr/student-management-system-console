using ManagerApplicationSystem.Models;
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
        public static void ErrorMessage()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n-----------------------------------");
            Console.WriteLine("ERROR: " + "Invalid Input.");
            Console.WriteLine("-----------------------------------");
            Console.ResetColor();
        }
        public static void PrintStudents(List<Student> students)
        {
            Console.WriteLine("===========================");
            Console.ForegroundColor = ConsoleColor.Green;

            foreach (var s in students)
                s.PrintInfo();

            Console.ResetColor();
            Console.WriteLine("===========================");
        }
        public static void PrintStudent(Student student)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("-----------------------------------");
            student.PrintInfo();
            Console.WriteLine("-----------------------------------");
            Console.ResetColor();
        }
        public static void PressAnyKeyToContinue()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Press any key to continue.");
            Console.ResetColor();

            Console.ReadKey();
        }
    }
}