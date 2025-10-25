namespace MVCDemoLabpart1.Models
{
    public class Student
    {

        public static List<Student> students = new List<Student>()
        {
            new Student() {Id=1 , Name = "Tamer1" , Age = 20},
            new Student() {Id=2 , Name = "Tamer2" , Age = 30},
            new Student() {Id=3 , Name = "Tamer3" , Age = 40},
            new Student() {Id=4 , Name = "Tamer4" , Age = 50},
            new Student() {Id=5 , Name = "Tamer5" , Age = 25}
        };
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }
    }
}
