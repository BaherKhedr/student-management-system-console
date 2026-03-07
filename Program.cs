
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
            Console.Write("Please enter the student's name:");
            string? name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name))
            {
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
                        else
                            break;
                    else
                    {
                        ErrorMessage("invalid grade input. Please try again");
                        continue;
                    }
                }
                var student = new Student(name, age, grade);
                studentlist.Add(student);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("student added successfully");
                Console.ResetColor();
            }
            else
            {
                ErrorMessage("Invalid input");
            }

        }
        private static void SearchStudent(List<Student> studentlist)
        {
            if (studentlist.Count() == 0)
            {
                ErrorMessage("No students are available to search for");
                return;
            }
            Console.Write("please enter the name of the student you want to search for:");
            string? name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name))
            {
                if (studentlist.Any(n => n.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("student was found successfully");
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
            Console.Write("pleae enter the student's name:");
            string? name = Console.ReadLine();
            var studenttodelete = studentlist.FirstOrDefault(n => n.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (studenttodelete != null)
            {
                studentlist.Remove(studenttodelete);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("student deleted succesfully");
                Console.ResetColor();
            }
            else
            {
                ErrorMessage("Name not found");
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
            Console.Write("please enter the name of the student you want to apply changes on:");
            string? name = Console.ReadLine();
            var studenttoupdate = studentlist.FirstOrDefault(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (studenttoupdate == null)
            {
                ErrorMessage("Student not found");
                return;
            }
            Console.WriteLine("please select an option:\n[1] only change his name\n[2] only change his age\n[3] only change his grade\n[4]change both his age and his grade");
            if (int.TryParse(Console.ReadLine(), out int Value))
            {
                if (Value == 1)
                {
                    Console.Write("please enter his new name");
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
        private static void ShowStats(List<Student> studentlist)
        {
            if (studentlist.Count() == 0)
            {
                ErrorMessage("No students are available to show their stats");
                return;
            }

            var highest = studentlist.OrderByDescending(g => g.Grade).First();
            var lowest = studentlist.OrderBy(g => g.Grade).First();
            var average = studentlist.Average(g => g.Grade);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("======================================================");
            Console.WriteLine($"The highest grade is: {highest.Name} --> {highest.Grade}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"The lowest grade is: {lowest.Name} --> {lowest.Grade}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Average:{average}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("======================================================");
            Console.ResetColor();

        }
        static void Main(string[] args)
        {
            PrintMenu();
        }
    }
}