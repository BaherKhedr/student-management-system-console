using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem
{
    internal class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public double Grade { get; set; }
        public Student(string name, int age, double grade)
        {
            Name = name;
            Age = age;
            Grade = grade;
        }
        public void PrintInfo()
        {
            Console.WriteLine($"Name: {Name} , Age: {Age} , Grade: {Grade}");
        }
    }
}
