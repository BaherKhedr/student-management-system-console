namespace ManagerApplicationSystem.Models
{
    internal class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public double Grade { get; set; }
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
    }
}
