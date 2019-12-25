using System;
namespace 常见接口练习
{
    class Program
    {
        
        class Student : IComparable<Student>
        {
            public Student(string _name,uint _score) { score = _score;name = _name; }
            public string name { get; set; }
            public uint score { get; set; }
            public void Show() { Console.WriteLine($"the score is {score}"); }

            //if the student's score is smaller than the back,return 1,else return 0.
            public int CompareTo(Student? stu) 
            {
                if (this.score < stu.score)
                {
                    return 1;
                }
                return 0;
            }
        }

        static void Main(string[] args)
        {
            Student student1 = new Student("xieyuchen", 90);
            Student student2 = new Student("Mich", 91);
            Console.WriteLine(student1.CompareTo(student2));
        }
    }
}
