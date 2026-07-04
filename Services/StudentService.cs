using ManagerApplicationSystem.Data;
using ManagerApplicationSystem.Helpers;
using ManagerApplicationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ManagerApplicationSystem.Services
{
    class StudentService
    {
        private readonly AppDbContext _context;

        public StudentService(AppDbContext context)
        {
            _context = context;
        }
        private List<Student> studentlist = new(); // Db Connection
        public bool IsEmpty()
        {
            return !_context.Students.Any();
        }
        public int Count()
        {
            return _context.Students.Count();
        }
        public bool IdExists(int id)
        {
            return _context.Students.Any(s => s.Id == id);
        }
        public Student? GetStudent(Func<Student, bool> predicate)
        {
            return _context.Students.FirstOrDefault(predicate);
        }
        public List<Student> GetStudents(Func<Student, bool> predicate)
        {
            return _context.Students.Where(predicate).ToList();
        }
        public Student? GetHighestGrade()
        {
            return _context.Students.OrderByDescending(s => s.Grade).FirstOrDefault(); // maxby == orderbydescending + first or default
        }
        public Student? GetLowestGrade()
        {
            return _context.Students.OrderBy(s => s.Grade).FirstOrDefault();
        }
        public double AverageGrade()
        {
            return _context.Students.Average(s => s.Grade);
        }
        public int GetPassedCount()
        {
            return _context.Students.Count(s => s.Grade >= 50);
        }
        public int GetFailedCount()
        {
            return _context.Students.Count(s => s.Grade < 50);
        }
        public void AddStudent(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }
        public bool DeleteStudentById(int id)
        {
            var student = GetStudent(s => s.Id == id);
            if (student == null)
                return false;
            _context.Students.Remove(student);
            _context.SaveChanges();
            return true;
        }
        public void DeleteStudentByName(string name)
        {
            var student = GetStudent(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
                
        }
        public List<Student> ListStudents(Func<IEnumerable<Student>, IEnumerable<Student>> sorter, Func<Student, bool>? filter = null)
        {
            IEnumerable<Student> students = _context.Students;

            if (filter != null)
                students = students.Where(filter);

            students = sorter(students);

            return students.ToList();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
