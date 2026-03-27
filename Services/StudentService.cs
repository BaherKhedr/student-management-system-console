using ManagerApplicationSystem.Enums;
using ManagerApplicationSystem.Helpers;
using ManagerApplicationSystem.Models;

namespace ManagerApplicationSystem.Services
{
    public class StudentService
    {
        private List<Student> studentlist = new();
        public void PrintMenu()
        {
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
                    if (Enum.IsDefined(typeof(MenuOptions), input))
                    {
                        MenuOptions options = (MenuOptions)input;

                        Console.Clear();

                        switch (options)
                        {
                            case MenuOptions.Add:
                                AddStudent();
                                break;
                            case MenuOptions.Search:
                                SearchStudent();
                                break;
                            case MenuOptions.Delete:
                                DeleteStudent();
                                break;
                            case MenuOptions.List:
                                ListStudents();
                                break;
                            case MenuOptions.Update:
                                UpdateStudent();
                                break;
                            case MenuOptions.Stats:
                                ShowStats();
                                break;
                            case MenuOptions.Exit:
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Application stopped successfully");
                                Console.ResetColor();
                                return;
                            default:
                                ConsoleHelper.ErrorMessage("Invalid input, please select a number between 1 and 7");
                                break;
                        }
                    }
                    else
                    {
                        ConsoleHelper.ErrorMessage("Invalid option number.");
                    }
                }
                else
                {
                    ConsoleHelper.ErrorMessage("Invalid input, please select a number between 1 and 7");
                }
                Console.WriteLine("Please type any key to continue...");
                Console.ReadKey();
            }
        }
        public void AddStudent()
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
                            ConsoleHelper.ErrorMessage("There is already a student with that Id, Please enter a new one.");
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        ConsoleHelper.ErrorMessage("Id can't be lower than or equal to Zero. please try again.");
                        continue;
                    }
                }
                else
                {
                    ConsoleHelper.ErrorMessage("Invalid Id input. Please try again.");
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
                    ConsoleHelper.ErrorMessage("Invalid input");
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
                        ConsoleHelper.ErrorMessage("Age must be higher than 5 years old and less than 40 years old. Please try again.");
                        continue;
                    }
                    else
                        break;
                }
                else
                {
                    ConsoleHelper.ErrorMessage("invalid age input. Please try again");
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
                        ConsoleHelper.ErrorMessage("How can a grade be less than zero? Please try again.");
                        continue;
                    }
                    else if (grade > 100)
                    {
                        ConsoleHelper.ErrorMessage("Please enter a grade that is lower than or equal to 100");
                    }
                    else
                        break;
                else
                {
                    ConsoleHelper.ErrorMessage("invalid grade input. Please try again");
                    continue;
                }
            }
            var student = new Student(id, name, age, grade);
            studentlist.Add(student);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("student added successfully");
            Console.ResetColor();
        }
        public void SearchStudent()
        {
            if (studentlist.Count() == 0)
            {
                ConsoleHelper.ErrorMessage("The list is empty, No students are available to search for");
                return;
            }
            Console.WriteLine("Select an option:");
            Console.WriteLine("[1] Search By Id.");
            Console.WriteLine("[2] Search By Name.");
            Console.WriteLine("[3] Search By Grade.");
            Console.Write("input:");
            if (int.TryParse(Console.ReadLine(), out int value))
            {
                if (value == 1)
                {
                    Console.Write("please enter the Id of the student you want to search for:");
                    if (int.TryParse(Console.ReadLine(), out int id))
                    {
                        var studenttosearch = studentlist.FirstOrDefault(x => x.Id == id);
                        if (studenttosearch != null)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("=========================");
                            studenttosearch.PrintInfo();
                            Console.WriteLine("=========================");
                            Console.ResetColor();
                        }
                        else
                        {
                            ConsoleHelper.ErrorMessage("Student not found");
                        }
                    }
                    else
                    {
                        ConsoleHelper.ErrorMessage("Invalid Input");
                    }
                }
                else if (value == 2)
                {
                    Console.Write("please enter the name of the student you want to search for:");
                    string? name = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        var searchedstudent = studentlist.Where(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).ToList();
                        if (searchedstudent.Any())
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"students was found successfully:");
                            Console.WriteLine("----------------------------------");
                            foreach (var student in searchedstudent)
                            {
                                student.PrintInfo();
                            }
                            Console.ResetColor();
                        }
                        else
                        {
                            ConsoleHelper.ErrorMessage("Student not found");
                            return;
                        }
                    }
                    else
                    {
                        ConsoleHelper.ErrorMessage("Please enter a VALID name");
                    }
                }
                else if (value == 3)
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
                                    ConsoleHelper.ErrorMessage("No matching grades");
                                }
                            }
                            else
                            {
                                ConsoleHelper.ErrorMessage("Invalid Grade input.");
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
                                    ConsoleHelper.ErrorMessage("Invalid Grade input.");
                                }
                            }
                            else
                            {
                                ConsoleHelper.ErrorMessage("Invalid Grade input.");
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
                                    ConsoleHelper.ErrorMessage("No matching grades");
                                }
                            }
                            else
                            {
                                ConsoleHelper.ErrorMessage("Invalid Grade input.");
                            }
                        }
                        else
                        {
                            ConsoleHelper.ErrorMessage("Please type a VALID number.");
                        }
                    }
                    else
                    {
                        ConsoleHelper.ErrorMessage("Invalid input");
                    }
                }
            }
            else
            {
                ConsoleHelper.ErrorMessage("invalid input");
            }
        }
        public void DeleteStudent()
        {
            if (studentlist.Count() == 0)
            {
                ConsoleHelper.ErrorMessage("No students are available to delete");
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
                            ConsoleHelper.ErrorMessage("invalid id input. please try again.");
                        }
                    }
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
                    else
                    {
                        ConsoleHelper.ErrorMessage("Student not found.");
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
                            ConsoleHelper.ErrorMessage("Invalid input.");
                        }
                    }
                    if (studentlist.Count(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase)) > 1)
                    {
                        ConsoleHelper.ErrorMessage("There is more than one student with the same name that you entered... Please consider using his Id in this case.");
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
                                ConsoleHelper.ErrorMessage("Invalid input. Please type y or n");
                            }

                        }
                        else
                        {
                            ConsoleHelper.ErrorMessage("Name not found");
                        }
                    }
                    else
                    {
                        ConsoleHelper.ErrorMessage("Student not found");
                    }
                }
                else
                {
                    ConsoleHelper.ErrorMessage("Please select a number [1 or 2]");
                }
            }
            else
            {
                ConsoleHelper.ErrorMessage("Invalid input.");
            }
        }
        public void ListStudents()
        {
            if (studentlist.Count() == 0)
            {
                ConsoleHelper.ErrorMessage("No students are available to list");
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
                    ConsoleHelper.ErrorMessage("invalid input");

                }
            }
            else
            {
                ConsoleHelper.ErrorMessage("invalid input");
            }
        }
        public void UpdateStudent()
        {
            if (studentlist.Count() == 0)
            {
                ConsoleHelper.ErrorMessage("No students are available to update");
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
                                ConsoleHelper.ErrorMessage("invalid new age input");
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
                                ConsoleHelper.ErrorMessage("invalid new grade input");
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
                                    ConsoleHelper.ErrorMessage("invalid new grade input");
                                }
                            }
                            else
                            {
                                ConsoleHelper.ErrorMessage("invalid new age input");
                            }
                        }
                    }
                    else
                    {
                        ConsoleHelper.ErrorMessage("invalid input");

                    }
                }
                else
                {
                    ConsoleHelper.ErrorMessage("Student not found.");
                }
            }

            else
            {
                ConsoleHelper.ErrorMessage("invalid input.");
            }
        }
        public void ShowStats()
        {
            if (studentlist.Count() == 0)
            {
                ConsoleHelper.ErrorMessage("The list is empty, No students are available to show their stats");
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

            Console.ForegroundColor = ConsoleColor.Green;
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
            Console.WriteLine($"({NumberOfPassedStudents}) Passed!");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"({NumberOfFailedStudents}) Failed :(");
            Console.ResetColor();

        }
    }
}
