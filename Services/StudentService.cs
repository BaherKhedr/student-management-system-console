using ManagerApplicationSystem.Helpers;
using ManagerApplicationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ManagerApplicationSystem.Services
{
    class StudentService
    {
        private List<Student> studentlist = new();
        public bool IsEmpty()
        {
            return !studentlist.Any();
        }
        public int Count()
        {
            return studentlist.Count();
        }
        public bool IdExists(int id)
        {
            return studentlist.Any(s => s.Id == id);
        }
        public Student? GetStudentById(int id)
        {
            return studentlist.FirstOrDefault(s => s.Id == id);
        }
        public List<Student> GetStudentByName(string name)
        {
            return studentlist.Where(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        public List<Student> GetStudentByGrade(Func<Student, bool> predicate)
        {
            return studentlist.Where(predicate).ToList();
        }
        public Student? GetHighestGrade()
        {
            return studentlist.OrderByDescending(s => s.Grade).FirstOrDefault();
        }
        public Student? GetLowestGrade()
        {
            return studentlist.OrderBy(s => s.Grade).FirstOrDefault();
        }
        public double AverageGrade()
        {
            return studentlist.Average(s => s.Grade);
        }
        public int GetPassedCount()
        {
            return studentlist.Where(s => s.Grade >= 50).Count();
        }
        public int GetFailedCount()
        {
            return studentlist.Where(s => s.Grade < 50).Count();
        }
        public void AddStudent(Student student)
        {
            studentlist.Add(student);
        }
        public bool DeleteStudentById(int id)
        {
            var student = studentlist.FirstOrDefault(s => s.Id == id);
            if (student != null)
            {
                studentlist.Remove(student);
                return true;
            }
            else
            {
                return false;
            }
        }
        public void DeleteStudentByName(string name)
        {
            var students = studentlist.Where(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            studentlist.Remove(students.First());
        }
        public List<Student> ListAllStudentsAccordingToId()
        {
            return studentlist.ToList();
        }
        public List<Student> ListAllStudentsAccordingToIdFilteredByAge(int Age)
        {
            return studentlist.Where(s => s.Age == Age).ToList();
        }
        public List<Student> ListAllStudentsAccordingToGrade()
        {
            return studentlist.OrderByDescending(s => s.Grade).ToList();
        }
        public List<Student> ListAllStudentsAccordingToGradeFilteredByAge(int Age)
        {
            return studentlist.OrderByDescending(s => s.Grade).Where(s => s.Age == Age).ToList();
        }
    }
}
