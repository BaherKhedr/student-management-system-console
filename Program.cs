using ManagerApplicationSystem.Services;

namespace StudentManagementSystem
{
    internal class Program
    {
       
        static void Main(string[] args)
        {
            var studentService = new StudentService();
            studentService.PrintMenu();
        }
    }
}