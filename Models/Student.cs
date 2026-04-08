namespace ManagerApplicationSystem.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public double Grade { get; set; }
        public Student()
        {
            Id = 0;
            Name = "UNKNOWN";
            Age = 0;
            Grade = 0;
        }
        public Student(int id, string name, int age, double grade)
        {
            Id = id;
            Name = name;
            Age = age;
            Grade = grade;
        }
        public void PrintInfo()
        {
            Console.WriteLine($"[{Id}] Name: {Name} , Age: {Age} , Grade: {Grade} Status: {(IsPassed() ? "Passed" : "Failed")}");
        }
        public bool IsPassed()
        {
            return Grade >= 50;
        }
        public static void ValidateAge(int age)
        {
            if (age < 5 || age > 35)
            {
                throw new Exception("Age must be between 5 an 35");
            }
        }
        public static void ValidateGrade(double grade)
        {
            if (grade < 0 || grade > 100)
            {
                throw new Exception("Grade must be between 0 and 100");
            }
        }
    }
}