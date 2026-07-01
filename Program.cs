using ManagerApplicationSystem.Enums;
using ManagerApplicationSystem.Helpers;
using ManagerApplicationSystem.Models;
using ManagerApplicationSystem.Services;

namespace StudentManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var studentServices = new StudentService();

            while (true)
            {

                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("===============================");
                Console.WriteLine("StudentManagementSystem");
                Console.WriteLine("===============================");
                Console.ResetColor();

                foreach (MenuOptions option in Enum.GetValues(typeof(MenuOptions)))
                {
                    Console.WriteLine($"Enter [{(int)option}] to {option}");
                }
                Console.Write("input:");

                if (int.TryParse(Console.ReadLine(), out int input))
                {
                    if (Enum.IsDefined(typeof(MenuOptions), input))
                    {
                        MenuOptions options = (MenuOptions)input;

                        switch (options)
                        {
                            case MenuOptions.Add:

                                Console.Clear();

                                int Id;

                                while (true)
                                {
                                    Id = InputHelper.ReadInt("Please enter student's Id:");
                                    if (studentServices.IdExists(Id))
                                    {
                                        ConsoleHelper.ErrorMessage("Id already exists");
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }

                                string? Name = InputHelper.ReadString("Please enter student's name:");

                                int Age;

                                while (true)
                                {
                                    Age = InputHelper.ReadInt("Please enter student's Age:");
                                    try
                                    {
                                        Student.ValidateAge(Age);
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        ConsoleHelper.ErrorMessage(ex.Message);
                                    }
                                }

                                double Grade;
                                while (true)
                                {
                                    Grade = InputHelper.ReadDouble("Please enter student's Grade:");
                                    try
                                    {
                                        Student.ValidateGrade(Grade);
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        ConsoleHelper.ErrorMessage(ex.Message);
                                    }
                                }

                                var newStudent = new Student(Id, Name, Age, Grade);

                                studentServices.AddStudent(newStudent);

                                ConsoleHelper.PrintStudent(newStudent);

                                ConsoleHelper.PressAnyKeyToContinue();

                                continue;

                            case MenuOptions.Search:

                                Console.Clear();

                                if (studentServices.IsEmpty())
                                {
                                    ConsoleHelper.ErrorMessage("The list is empty. No students to search for.");

                                    ConsoleHelper.PressAnyKeyToContinue();

                                    continue;
                                }
                                Console.WriteLine("Select an option:");
                                Console.WriteLine("[1] Search By Id.");
                                Console.WriteLine("[2] Search By Name.");
                                Console.WriteLine("[3] Search By Grade.");
                                Console.Write("input:");

                                int value = InputHelper.ReadInt("");
                                switch (value)
                                {
                                    case 1:

                                        Console.Clear();

                                        int id = InputHelper.ReadInt("Please enter the Id:");
                                        var searchedId = studentServices.GetStudentById(id);
                                        if (searchedId != null)
                                        {
                                            ConsoleHelper.PrintStudent(searchedId);
                                        }
                                        else
                                        {
                                            ConsoleHelper.ErrorMessage("Student with this Id wasn't found");
                                        }
                                        break;

                                    case 2:

                                        Console.Clear();

                                        string SearchedName = InputHelper.ReadString("Please enter the Name:");
                                        var studentsSearchedByName = studentServices.GetStudentByName(SearchedName);
                                        if (studentsSearchedByName.Any())
                                        {
                                            ConsoleHelper.PrintStudents(studentsSearchedByName);
                                        }
                                        else
                                            ConsoleHelper.ErrorMessage("Not found");

                                        break;
                                    case 3:

                                        Console.Clear();

                                        Console.WriteLine("Select:");
                                        Console.WriteLine("[1] Show students with grade HIGHER than your input.");
                                        Console.WriteLine("[2] Show students with grade LOWER than your input.");
                                        Console.WriteLine("[3] Show students with grade EQUAL to your input.");
                                        Console.Write("Input:");
                                        var selectinput = InputHelper.ReadInt("");
                                        switch (selectinput)
                                        {
                                            case 1:

                                                Console.Clear();

                                                var SearchedGrade1 = InputHelper.ReadDouble("Please enter the Grade:");
                                                var GradeHigherThan = studentServices.GetStudentByGrade(b => b.Grade > SearchedGrade1);
                                                if (GradeHigherThan.Any())
                                                {
                                                    ConsoleHelper.PrintStudents(GradeHigherThan);

                                                }
                                                else
                                                    ConsoleHelper.ErrorMessage("Nothing was found.");
                                                break;
                                            case 2:

                                                Console.Clear();

                                                var SearchedGrade2 = InputHelper.ReadDouble("Please enter the Grade:");
                                                var GradeLowerThan = studentServices.GetStudentByGrade(b => b.Grade < SearchedGrade2);
                                                if (GradeLowerThan.Any())
                                                {
                                                    ConsoleHelper.PrintStudents(GradeLowerThan);
                                                }
                                                else
                                                    ConsoleHelper.ErrorMessage("Nothing was found.");
                                                break;
                                            case 3:

                                                Console.Clear();

                                                var SearchedGrade3 = InputHelper.ReadDouble("Please enter the Grade:");
                                                var GradeEqualTo = studentServices.GetStudentByGrade(b => b.Grade == SearchedGrade3);
                                                if (GradeEqualTo.Any())
                                                {
                                                    ConsoleHelper.PrintStudents(GradeEqualTo);
                                                }
                                                else
                                                    ConsoleHelper.ErrorMessage("Nothing was found.");
                                                break;
                                        }
                                        break;
                                    case 4:
                                        ConsoleHelper.ErrorMessage();
                                        break;
                                }


                                ConsoleHelper.PressAnyKeyToContinue();

                                continue;

                            case MenuOptions.Delete:

                                Console.Clear();

                                if (studentServices.IsEmpty())
                                {
                                    ConsoleHelper.ErrorMessage("List is empty... No students to delete.");

                                    ConsoleHelper.PressAnyKeyToContinue();

                                    continue;
                                }

                                Console.WriteLine("Select");
                                Console.WriteLine("[1] Delete student using Id.");
                                Console.WriteLine("[2] Delete student using Name.");
                                value = InputHelper.ReadInt("Input:");
                                switch (value)
                                {
                                    case 1:
                                        Id = InputHelper.ReadInt("Please enter the Id:");

                                        var deletedStudentUsingId = studentServices.GetStudentById(Id);
                                        if (deletedStudentUsingId != null)
                                        {
                                            if (deletedStudentUsingId != null)
                                            {
                                                Console.ForegroundColor = ConsoleColor.Green;
                                                Console.WriteLine("Student was found.");
                                                Console.ResetColor();
                                                ConsoleHelper.PrintStudent(deletedStudentUsingId);

                                                Console.WriteLine("Are you sure? (y/n)");
                                                var confirm = InputHelper.ReadString("Input:");
                                                switch (confirm.ToLower())
                                                {
                                                    case "y":
                                                        Console.ForegroundColor = ConsoleColor.Green;
                                                        Console.WriteLine("Student deleted successfully.");
                                                        Console.ResetColor();
                                                        studentServices.DeleteStudentById(Id);
                                                        break;
                                                    case "n":

                                                        Console.ForegroundColor = ConsoleColor.Green;
                                                        Console.WriteLine("Operation Cancelled.");
                                                        Console.ResetColor();
                                                        break;
                                                    default:

                                                        ConsoleHelper.ErrorMessage();
                                                        break;
                                                }
                                                ConsoleHelper.PressAnyKeyToContinue();
                                            }
                                        }
                                        else
                                        {
                                            ConsoleHelper.ErrorMessage("Student with this Id was not found.");

                                            ConsoleHelper.PressAnyKeyToContinue();
                                        }
                                        break;
                                    case 2:
                                        var name = InputHelper.ReadString("Please enter his name:");
                                        var deletedStudentUsingName = studentServices.GetStudentByName(name);
                                        if (deletedStudentUsingName.Any())
                                        {
                                            if (deletedStudentUsingName.Count() == 1)
                                            {
                                                Console.WriteLine("Are you sure? (y/n)");
                                                Console.Write("input:");
                                                var confirm = InputHelper.ReadString("");
                                                switch (confirm.ToLower())
                                                {
                                                    case "y":
                                                        Console.ForegroundColor = ConsoleColor.Green;
                                                        Console.WriteLine("Student deleted successfully.");
                                                        Console.ResetColor();
                                                        studentServices.DeleteStudentByName(name);
                                                        break;
                                                    case "n":

                                                        Console.ForegroundColor = ConsoleColor.Green;
                                                        Console.WriteLine("Operation Cancelled.");
                                                        Console.ResetColor();
                                                        break;
                                                    default:

                                                        ConsoleHelper.ErrorMessage();
                                                        break;
                                                }

                                                ConsoleHelper.PressAnyKeyToContinue();
                                            }
                                            else
                                            {
                                                ConsoleHelper.ErrorMessage("Student wasn't found or it's duplicated... Use the Id method instead.");
                                                ConsoleHelper.PressAnyKeyToContinue();
                                            }
                                        }
                                        else
                                        {
                                            ConsoleHelper.ErrorMessage("Student wasn't found or it's duplicated... Use the Id method instead.");
                                            ConsoleHelper.PressAnyKeyToContinue();
                                        }
                                        break;
                                }
                                continue;

                            case MenuOptions.List:
                                if (studentServices.IsEmpty())
                                {
                                    ConsoleHelper.ErrorMessage("List is empty... No students to delete.");

                                    ConsoleHelper.PressAnyKeyToContinue();

                                    continue;
                                }

                                Console.Clear();

                                Console.WriteLine("Select:");
                                Console.WriteLine("[1] sort according to Id.");
                                Console.WriteLine("[2] sort according to Grade.");
                                Console.Write("Input:");

                                input = InputHelper.ReadInt("");

                                switch (input)
                                {
                                    case 1:
                                        Console.WriteLine("Do you want to Filter students by Age ? (y / n)");
                                        var confirm = InputHelper.ReadString("Input:");
                                        switch (confirm.ToLower())
                                        {
                                            case "y":
                                                Console.WriteLine("Enter Age.");
                                                Age = InputHelper.ReadInt("Input:");
                                                if (studentServices.ListAllStudentsAccordingToIdFilteredByAge(Age).Count() == 0)
                                                {
                                                    ConsoleHelper.ErrorMessage("No students with this Age were found.");
                                                    break;
                                                }
                                                ConsoleHelper.PrintStudents(studentServices.ListAllStudentsAccordingToIdFilteredByAge(Age));
                                                break;
                                            case "n":
                                                ConsoleHelper.PrintStudents(studentServices.ListAllStudentsAccordingToId());
                                                break;
                                            default:
                                                ConsoleHelper.ErrorMessage();
                                                break;
                                        }
                                        break;
                                    case 2:
                                        Console.WriteLine("Do you want to Filter students by Age ? (y / n)");
                                        confirm = InputHelper.ReadString("Input:");
                                        switch (confirm.ToLower())
                                        {
                                            case "y":
                                                Console.WriteLine("Enter Age.");
                                                Age = InputHelper.ReadInt("Input:");
                                                if (studentServices.ListAllStudentsAccordingToGradeFilteredByAge(Age).Count() == 0)
                                                {
                                                    ConsoleHelper.ErrorMessage("No students with this Age were found.");
                                                    break;
                                                }
                                                ConsoleHelper.PrintStudents(studentServices.ListAllStudentsAccordingToGradeFilteredByAge(Age));
                                                break;
                                            case "n":
                                                ConsoleHelper.PrintStudents(studentServices.ListAllStudentsAccordingToGrade());
                                                break;
                                            default:
                                                ConsoleHelper.ErrorMessage();
                                                break;
                                        }
                                        break;
                                    default:
                                        ConsoleHelper.ErrorMessage();
                                        break;

                                }

                                ConsoleHelper.PressAnyKeyToContinue();

                                continue;

                            case MenuOptions.Update:
                                if (studentServices.IsEmpty())
                                {
                                    ConsoleHelper.ErrorMessage("List is empty... No students to update.");
                                    ConsoleHelper.PressAnyKeyToContinue();
                                    continue;
                                }

                                Console.Clear();

                                Id = InputHelper.ReadInt("Please enter the Id for the student you wish to update:");
                                var studenToUpdate = studentServices.GetStudentById(Id);
                                if (studenToUpdate != null)
                                {
                                    Console.WriteLine("Select:");
                                    Console.WriteLine("[1] Only update student's Name.");
                                    Console.WriteLine("[2] Only update student's Age.");
                                    Console.WriteLine("[3] Only update student's Grade.");
                                    Console.WriteLine("[4] Update both his Age and Grade.");
                                    input = InputHelper.ReadInt("Input:");
                                    switch (input)
                                    {
                                        case 1:
                                            var updatedStudentName = InputHelper.ReadString("Please enter his new Name:");
                                            studenToUpdate.Name = updatedStudentName;
                                            ConsoleHelper.PrintStudent(studenToUpdate);
                                            break;
                                        case 2:
                                            int updatedStudentAge;
                                            while (true)
                                            {
                                                updatedStudentAge = InputHelper.ReadInt("Please enter his new Age:");
                                                try
                                                {
                                                    Student.ValidateAge(updatedStudentAge);
                                                    break;
                                                }
                                                catch (Exception ex)
                                                {
                                                    ConsoleHelper.ErrorMessage(ex.Message);
                                                }
                                            }
                                            studenToUpdate.Age = updatedStudentAge;
                                            ConsoleHelper.PrintStudent(studenToUpdate);
                                            break;
                                        case 3:
                                            double updatedStudentGrade;
                                            while (true)
                                            {
                                                updatedStudentGrade = InputHelper.ReadDouble("Please enter his new Grade:");
                                                try
                                                {
                                                    Student.ValidateGrade(updatedStudentGrade);
                                                    break;
                                                }
                                                catch (Exception ex)
                                                {
                                                    ConsoleHelper.ErrorMessage(ex.Message);
                                                }
                                            }
                                            studenToUpdate.Grade = updatedStudentGrade;
                                            ConsoleHelper.PrintStudent(studenToUpdate);
                                            break;
                                        case 4:
                                            while (true)
                                            {
                                                updatedStudentAge = InputHelper.ReadInt("Please enter his new Age:");
                                                try
                                                {
                                                    Student.ValidateAge(updatedStudentAge);
                                                    break;
                                                }
                                                catch (Exception ex)
                                                {
                                                    ConsoleHelper.ErrorMessage(ex.Message);
                                                }
                                            }
                                            while (true)
                                            {
                                                updatedStudentGrade = InputHelper.ReadDouble("Please enter his new Grade:");
                                                try
                                                {
                                                    Student.ValidateGrade(updatedStudentGrade);
                                                    break;
                                                }
                                                catch (Exception ex)
                                                {
                                                    ConsoleHelper.ErrorMessage(ex.Message);
                                                }
                                            }
                                            studenToUpdate.Age = updatedStudentAge;
                                            studenToUpdate.Grade = updatedStudentGrade;

                                            ConsoleHelper.PrintStudent(studenToUpdate);
                                            break;
                                        default:
                                            ConsoleHelper.ErrorMessage();
                                            break;
                                    }
                                    ConsoleHelper.PressAnyKeyToContinue();
                                }
                                else
                                {
                                    ConsoleHelper.ErrorMessage("Student with this Id was not found.");
                                    ConsoleHelper.PressAnyKeyToContinue();
                                }
                                continue;

                            case MenuOptions.Stats:

                                Console.Clear();

                                if (studentServices.IsEmpty())
                                {
                                    ConsoleHelper.ErrorMessage("The list is empty. No students to search for.");

                                    ConsoleHelper.PressAnyKeyToContinue();

                                    continue;
                                }

                                Console.WriteLine("Student with Highest Grade is:");

                                var highestGrade = studentServices.GetHighestGrade();

                                if (highestGrade != null)
                                {
                                    ConsoleHelper.PrintStudent(highestGrade);
                                }
                                Console.WriteLine("================================");
                                Console.WriteLine("Student with Lowest Grade is:");

                                var lowestGrade = studentServices.GetLowestGrade();

                                if (lowestGrade != null)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    ConsoleHelper.PrintStudent(lowestGrade);
                                    Console.ResetColor();
                                }
                                Console.WriteLine("================================");

                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine($"Average --> {studentServices.AverageGrade()}");
                                Console.ResetColor();

                                Console.WriteLine("================================");

                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine($"Number of students in list --> {studentServices.Count()}");
                                Console.ResetColor();

                                Console.WriteLine("================================");

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"Number of Passed Students --> {studentServices.GetPassedCount()}");
                                Console.ResetColor();

                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Number of Failed Students --> {studentServices.GetFailedCount()}");
                                Console.ResetColor();

                                ConsoleHelper.PressAnyKeyToContinue();
                                continue;

                            case MenuOptions.Exit:
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Application Stopped.");
                                Console.ResetColor();
                                return;
                        }
                    }
                }
            }
        }
    }
}