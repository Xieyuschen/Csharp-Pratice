using System;
using System.Collections;
using System.Text;

namespace Delegate_.net_pra
{
    public class Student
    {
        public string name { get; set; }
        public int score { get; }
        public Student(string _name,int _score)
        {
            score = _score;
            name = _name;
        }
    }
    public class ClassCollection : IEnumerable
    {
        public Student[] students;
        public ClassCollection(Student[] _students)
        {
            students = new Student[_students.Length];
            for(int i = 0; i < students.Length; i++)
            {
                students[i] = _students[i];
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerat();
        }

        public ClassCollectionEnum GetEnumerat()
        {
            return new ClassCollectionEnum(students);
        }
    }

    public class ClassCollectionEnum :IEnumerator
    {
        public int position = -1;
        public Student[] students;
        object IEnumerator.Current { get
            {
                return Current;
            } }
        public Student Current { get
            {
                try
                {
                    return students[position];
                }
                catch (IndexOutOfRangeException)
            {
                throw new InvalidOperationException();
            }
            } }
        public ClassCollectionEnum(Student[] _students)
        {
            students = new Student[_students.Length];
            for (int i = 0; i < students.Length; i++)
            {
                students[i] = _students[i];
            }
        }
        public bool MoveNext()
        {
            position += 1;
            return (position < students.Length);
        }
        public void Reset()
        {
            position = -1;
        }
    }
    public class main
    {
        public static void Main()
        {
            Student[] students = new Student[3]
            {
                new Student("Yuchen",100),
                new Student("Derlinst",80),
                new Student("Grillen",70),
            };
            ClassCollection classCollection = new ClassCollection(students);
            foreach(var item in classCollection)
            {
                Console.WriteLine($"{item.name} is {item.score}");
            }
            
        }
    }
}
