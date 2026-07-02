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
        private List<Student> studentlist = new(); // Db Connection
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
        public Student? GetStudent(Func<Student , bool> predicate)
        {
            return studentlist.FirstOrDefault(predicate);
        }
        public List<Student> GetStudents(Func<Student , bool> predicate)
        {
            return studentlist.Where(predicate).ToList();
        }
        public Student? GetHighestGrade()
        {
            return studentlist.MaxBy(s => s.Grade); // maxby == orderbydescending + first or default
        }
        public Student? GetLowestGrade()
        {
            return studentlist.MinBy(s => s.Grade);
        }
        public double AverageGrade()
        {
            return studentlist.Average(s => s.Grade);
        }
        public int GetPassedCount()
        {
            return studentlist.Count(s => s.Grade >= 50);
        }
        public int GetFailedCount()
        {
            return studentlist.Count(s => s.Grade < 50);
        }
        public void AddStudent(Student student)
        {
            studentlist.Add(student);
        }
        public bool DeleteStudentById(int id)
        {
            var student = GetStudent(s => s.Id == id);
            if (student == null)
            {
                return false;
            }
            studentlist.Remove(student);
            return true;
        }
        public void DeleteStudentByName(string name)
        {
            var student = GetStudent(s =>s.Name.Equals(name , StringComparison.OrdinalIgnoreCase));
            if (student != null)
            studentlist.Remove(student);
        }
        public List<Student> ListStudents(Func<IEnumerable<Student> , IEnumerable<Student>> sorter , Func<Student , bool>? filter = null)
        {
            IEnumerable<Student> students = studentlist;

            if (filter != null)
                students = students.Where(filter);

            students = sorter(students);

            return students.ToList();
        }
        
    }
}
