
using ManagerApplicationSystem.Models;

namespace StudentManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var students = new StudentService();
            students.PrintMenu();
        }
    }
}