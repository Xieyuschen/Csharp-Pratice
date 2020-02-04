using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Delegate_net_pra
{
    public class Student
    {
        public string name { get; set; }
        public int score { get; }
        public Student(string _name, int _score)
        {
            score = _score;
            name = _name;
        }
    }

    public class ClassCollection : IEnumerable<Student>
    {
        public Student[] students;
        public ClassCollection(Student[] _students)
        {
            students = new Student[_students.Length];
            for (int i = 0; i < students.Length; i++)
            {
                students[i] = _students[i];
            }
        }

        //IEnumerable<T>需要实现的
        IEnumerator<Student> IEnumerable<Student>.GetEnumerator()
        {
            return new ClassCollectionEnumerator(students);
        }

        //IEnumerable需要实现的
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator1();
        }
        public ClassCollectionEnumerator GetEnumerator1()
        {
            return new ClassCollectionEnumerator(students);
        }
    }

    public class ClassCollectionEnumerator : IEnumerator<Student>
    {
        public int position = -1;
        public Student[] students;

        //IEnumerator<T>需要实现的
        public Student Current
        {
            get
            {
                try
                {
                    return students[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }


        //IEnumerator需要实现的
        private object Current1
        {
            get { return students[position]; }
        }
        object IEnumerator.Current
        {
            get
            {
                return Current1;
            }
        }
        
        public ClassCollectionEnumerator(Student[] _students)
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
        void IDisposable.Dispose() { }
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
            foreach (var item in classCollection)
            {
                Console.WriteLine($"{item.name} is {item.score}");
            }

        }
    }
}
