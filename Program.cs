
using ManagerApplicationSystem.Models;
using System.Security.Cryptography;

namespace StudentManagementSystem
{
    internal class Program
    {
        private static void PrintMenu()
        {
            var studentlist = new List<Student>();
            int input;
            while (true)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("===============================");
                Console.WriteLine("StudentManagementSystem");
                Console.WriteLine("===============================");
                Console.ResetColor();

                Console.WriteLine("Please select an option:\n[1]Add a student \n[2]Search for a student \n[3]Delete a student \n[4]List all students \n[5]Update a student's information \n[6]Show stats \n[7]Stop application");
                Console.Write("Input:");

                if (int.TryParse(Console.ReadLine(), out input))
                {
                    Console.Clear();

                    switch (input)
                    {
                        case 1:
                            AddStudent(studentlist);
                            break;
                        case 2:
                            SearchStudent(studentlist);
                            break;
                        case 3:
                            DeleteStudent(studentlist);
                            break;
                        case 4:
                            ListStudents(studentlist);
                            break;
                        case 5:
                            UpdateStudent(studentlist);
                            break;
                        case 6:
                            ShowStats(studentlist);
                            break;
                        case 7:
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Application stopped successfully");
                            Console.ResetColor();
                            return;
                        default:
                            ErrorMessage("Invalid input, please select a number between 1 and 7");
                            break;


                    }
                }
                else
                {
                    ErrorMessage("Invalid input, please select a number between 1 and 7");
                }
                Console.WriteLine("Please type any key to continue...");
                Console.ReadKey();
            }

        }
        private static void ErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n-----------------------------------");
            Console.WriteLine("ERROR: " + message);
            Console.WriteLine("-----------------------------------");
            Console.ResetColor();
        }
        private static void AddStudent(List<Student> studentlist)
        {
            int id;
            while (true)
            {
                Console.Write("Please enter the student's Id:");
                if (int.TryParse(Console.ReadLine(), out id))
                {
                    if (id > 0)
                    {
                        if (studentlist.Any(x => x.Id == id))
                        {
                            ErrorMessage("There is already a student with that Id, Please enter a new one.");
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        ErrorMessage("Id can't be lower than or equal to Zero. please try again.");
                        continue;
                    }
                }
                else
                {
                    ErrorMessage("Invalid Id input. Please try again.");
                }
            }
            string? name;
            while (true)
            {
                Console.Write("Please enter the student's name:");
                name = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(name))
                {
                    break;
                }
                else
                {
                    ErrorMessage("Invalid input");
                }
            }
            int age;
            while (true)
            {
                Console.Write("Please enter his/her Age:");
                if (int.TryParse(Console.ReadLine(), out age))
                {
                    if (age <= 5 || age >= 40)
                    {
                        ErrorMessage("Age must be higher than 5 years old and less than 40 years old. Please try again.");
                        continue;
                    }
                    else
                        break;
                }
                else
                {
                    ErrorMessage("invalid age input. Please try again");
                    continue;
                }
            }
            double grade;
            while (true)
            {

                Console.Write("Please enter his/her grade:");
                if (double.TryParse(Console.ReadLine(), out grade))
                    if (grade < 0)
                    {
                        ErrorMessage("How can a grade be less than zero? Please try again.");
                        continue;
                    }
                    else if (grade > 100)
                    {
                        ErrorMessage("Please enter a grade that is lower than or equal to 100");
                    }
                    else
                        break;
                else
                {
                    ErrorMessage("invalid grade input. Please try again");
                    continue;
                }
            }
            var student = new Student(id, name, age, grade);
            studentlist.Add(student);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("student added successfully");
            Console.ResetColor();
        }
        private static void SearchStudent(List<Student> studentlist)
        {
            if (studentlist.Count() == 0)
            {
                ErrorMessage("The list is empty, No students are available to search for");
                return;
            }
            Console.WriteLine("Select an option:");
            Console.WriteLine("[1] Search By Name.");
            Console.WriteLine("[2] Search By Grade.");
            Console.Write("input:");
            if (int.TryParse(Console.ReadLine(), out int value))
            {
                if (value == 1)
                {
                    Console.Write("please enter the name of the student you want to search for:");
                    string? name = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        var searchedstudent = studentlist.FirstOrDefault(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
                        if (searchedstudent != null)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"student was found successfully:");
                            Console.WriteLine("----------------------------------");
                            searchedstudent.PrintInfo();
                            Console.ResetColor();
                        }
                        else
                        {
                            ErrorMessage("Student not found");
                            return;
                        }
                    }
                    else
                    {
                        ErrorMessage("Please enter a VALID name");
                    }
                }
                else if (value == 2)
                {
                    Console.WriteLine("Select:");
                    Console.WriteLine("[1] Show students with grade HIGHER than your input.");
                    Console.WriteLine("[2] Show students with grade LOWER than your input.");
                    Console.WriteLine("[3] Show students with grade EQUAL to your input.");
                    Console.Write("Input:");
                    if (int.TryParse(Console.ReadLine(), out int Value))
                    {
                        if (Value == 1)
                        {
                            Console.Write("Please enter the grade:");
                            if (int.TryParse(Console.ReadLine(), out int Hgrade))
                            {
                                if (studentlist.Any(s => s.Grade > Hgrade))
                                {
                                    var studentsWithGradesHigherThan = studentlist.Where(x => x.Grade > Hgrade);
                                    foreach (var student in studentsWithGradesHigherThan)
                                    {
                                        student.PrintInfo();
                                    }
                                }
                                else
                                {
                                    ErrorMessage("No matching grades");
                                }
                            }
                            else
                            {
                                ErrorMessage("Invalid Grade input.");
                            }
                        }
                        else if (Value == 2)
                        {
                            Console.Write("Please enter the grade:");
                            if (int.TryParse(Console.ReadLine(), out int Lgrade))
                            {
                                if (studentlist.Any(s => s.Grade <= Lgrade))
                                {
                                    var studentsWithGradesLowerThan = studentlist.Where(x => x.Grade < Lgrade);
                                    foreach (var student in studentsWithGradesLowerThan)
                                    {
                                        student.PrintInfo();
                                    }
                                }
                                else
                                {
                                    ErrorMessage("Invalid Grade input.");
                                }
                            }
                            else
                            {
                                ErrorMessage("Invalid Grade input.");
                            }
                        }
                        else if (Value == 3)
                        {
                            Console.WriteLine("Please enter the grade:");
                            if (int.TryParse(Console.ReadLine(), out int Egrade))
                            {
                                if (studentlist.Any(s => s.Grade == Egrade))
                                {
                                    var studentsWithGradesEqualTo = studentlist.Where(x => x.Grade == Egrade);
                                    foreach (var student in studentsWithGradesEqualTo)
                                    {
                                        student.PrintInfo();
                                    }
                                }
                                else
                                {
                                    ErrorMessage("No matching grades");
                                }
                            }
                            else
                            {
                                ErrorMessage("Invalid Grade input.");
                            }
                        }
                        else
                        {
                            ErrorMessage("Please type a VALID number.");
                        }
                    }
                    else
                    {
                        ErrorMessage("Invalid input");
                    }
                }
            }
            else
            {
                ErrorMessage("invalid input");
            }
        }
        private static void DeleteStudent(List<Student> studentlist)
        {
            if (studentlist.Count() == 0)
            {
                ErrorMessage("No students are available to delete");
                return;
            }
            Console.WriteLine("Select an option:");
            Console.WriteLine("[1] Delete using student's Id.");
            Console.WriteLine("[2] Delete using student's Name.");
            Console.Write("Input:");
            if (int.TryParse(Console.ReadLine(), out int number))
            {
                if (number == 1)
                {
                    int id;
                    while (true)
                    {
                        Console.Write("Please enter his Id:");
                        if (int.TryParse(Console.ReadLine(), out id))
                        {
                            break;
                        }
                        else
                        {
                            ErrorMessage("invalid id input. please try again.");
                        }
                    }
                    if (studentlist.Any(x => x.Id == id))
                    {
                        var studenttodelete = studentlist.FirstOrDefault(x => x.Id == id);
                        if (studenttodelete != null)
                        {
                            Console.WriteLine("Are you sure? \nif yes type : y \nif not type anything else.");
                            Console.Write("Input:");
                            string? value = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(value))
                            {
                                if (value.ToLowerInvariant() == "y")
                                {
                                    studentlist.Remove(studenttodelete);
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Student Deleted successfully.");
                                    Console.ResetColor();
                                }
                                else
                                {
                                    Console.WriteLine("Deletion reverted");
                                }
                            }
                        }
                    }
                    else
                    {
                        ErrorMessage("Student not found.");
                    }

                }
                else if (number == 2)
                {
                    string? name;
                    while (true)
                    {
                        Console.Write("Please enter the student's name:");
                        name = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(name))
                        {
                            break;
                        }
                        else
                        {
                            ErrorMessage("Invalid input.");
                        }
                    }
                    if (studentlist.Where(x => x.Name.Equals( name , StringComparison.OrdinalIgnoreCase)).ToList().Count() > 1)
                    {
                        ErrorMessage("There is more than one student with the same name that you entered... Please consider using his Id in this case.");
                    }
                    else if (studentlist.Where(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).ToList().Count() == 1)
                    {
                        var studenttodelete = studentlist.FirstOrDefault(n => n.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
                        if (studenttodelete != null)
                        {
                            Console.WriteLine("Are you sure? \nif yes type : y \nif not type anything else.");
                            Console.Write("Input:");
                            string? value = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(value))
                            {
                                if (value.ToLowerInvariant() == "y")
                                {
                                    studentlist.Remove(studenttodelete);
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("student deleted succesfully");
                                    Console.ResetColor();
                                }
                                else
                                {
                                    Console.WriteLine("Deletion reverted");
                                }
                            }
                            else
                            {
                                ErrorMessage("Invalid input. Please type y or n");
                            }

                        }
                        else
                        {
                            ErrorMessage("Name not found");
                        }
                    }
                    else
                    {
                        ErrorMessage("Student not found");
                    }
                }
                else
                {
                    ErrorMessage("Please select a number [1 or 2]");
                }
            }
            else
            {
                ErrorMessage("Invalid input.");
            }
        }
        private static void ListStudents(List<Student> studentlist)
        {
            if (studentlist.Count() == 0)
            {
                ErrorMessage("No students are available to list");
                return;
            }
            Console.WriteLine("please select an option:\n[1] List all students.\n[2] Sort according to the highest grade.");
            string? value = Console.ReadLine();
            if (int.TryParse(value, out int Value))
            {
                if (Value == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("========================");
                    Console.WriteLine("students list [not sorted]:");
                    Console.WriteLine("========================");
                    foreach (Student student in studentlist)
                        student.PrintInfo();
                    Console.ResetColor();
                }
                else if (Value == 2)
                {
                    var sortedlist = studentlist.OrderByDescending(n => n.Grade);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("========================");
                    Console.WriteLine("students list [sorted]:");
                    Console.WriteLine("========================");
                    foreach (Student student in sortedlist)
                        student.PrintInfo();
                    Console.ResetColor();
                }
                else
                {
                    ErrorMessage("invalid input");

                }
            }
            else
            {
                ErrorMessage("invalid input");
            }
        }
        private static void UpdateStudent(List<Student> studentlist)
        {
            if (studentlist.Count() == 0)
            {
                ErrorMessage("No students are available to update");
                return;
            }
            Console.Write("please enter the Id of the student you want to apply changes on:");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var studenttoupdate = studentlist.FirstOrDefault(s => s.Id == id);
                if (studenttoupdate != null)
                {
                    Console.WriteLine("please select an option:\n[1] only change his name\n[2] only change his age\n[3] only change his grade\n[4]change both his age and his grade");
                    if (int.TryParse(Console.ReadLine(), out int Value))
                    {
                        if (Value == 1)
                        {
                            Console.Write("please enter his new name:");
                            string? newname = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(newname))
                            {
                                studenttoupdate.Name = newname;
                            }
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("updates applied successfully");
                            Console.ResetColor();
                        }
                        else if (Value == 2)
                        {
                            Console.Write("please enter his new age:");
                            string? newage = Console.ReadLine();
                            if (int.TryParse(newage, out int age))
                            {
                                studenttoupdate.Age = age;
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("updates applied successfully");
                                Console.ResetColor();
                            }
                            else
                            {
                                ErrorMessage("invalid new age input");
                            }
                        }
                        else if (Value == 3)
                        {
                            Console.Write("please enter his new grade:");
                            string? newgrade = Console.ReadLine();
                            if (double.TryParse(newgrade, out double grade))
                            {
                                studenttoupdate.Grade = grade;
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("updates applied successfully");
                                Console.ResetColor();
                            }
                            else
                            {
                                ErrorMessage("invalid new grade input");
                            }
                        }
                        else if (Value == 4)
                        {
                            Console.Write("please enter his new age:");
                            string? newage = Console.ReadLine();
                            if (int.TryParse(newage, out int age))
                            {
                                studenttoupdate.Age = age;
                                Console.Write("please enter his new grade:");
                                string? newgrade = Console.ReadLine();
                                if (double.TryParse(newgrade, out double grade))
                                {
                                    studenttoupdate.Grade = grade;
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("updates applied successfully");
                                    Console.ResetColor();
                                }
                                else
                                {
                                    ErrorMessage("invalid new grade input");
                                }
                            }
                            else
                            {
                                ErrorMessage("invalid new age input");
                            }
                        }
                    }
                    else
                    {
                        ErrorMessage("invalid input");

                    }
                }
                else
                {
                    ErrorMessage("Student not found.");
                }
            }

            else
            {
                ErrorMessage("invalid input.");
            }
        }
        private static void ShowStats(List<Student> studentlist)
        {
            if (studentlist.Count() == 0)
            {
                ErrorMessage("The list is empty, No students are available to show their stats");
                return;
            }

            var highest = studentlist.OrderByDescending(g => g.Grade).First();
            var lowest = studentlist.OrderBy(g => g.Grade).First();
            var average = studentlist.Average(g => g.Grade);

            var NumberOfPassedStudents = studentlist.Where(x => x.Grade >= 50).Count();
            var NumberOfFailedStudents = studentlist.Where(x => x.Grade < 50).Count();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("======================================================");
            Console.ResetColor();
            Console.WriteLine($"The highest grade is: {highest.Name} --> {highest.Grade}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"The lowest grade is: {lowest.Name} --> {lowest.Grade}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Average:{average}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("======================================================");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"No. of students in list: {studentlist.Count()}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{NumberOfPassedStudents} Passed!");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{NumberOfFailedStudents} Failed :(");
            Console.ResetColor();

        }
        static void Main(string[] args)
        {
            PrintMenu();
        }
    }
}