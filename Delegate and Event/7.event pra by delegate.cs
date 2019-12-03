using System;
using System.Collections.Generic;
using System.Text;

namespace delegate_pra_with_data_member
{
    class Class1
    {
        public delegate void Attention(int x);
        public class Operation
        {
            public event Attention Reminder;
            public void Show(int x)
            {
                Reminder(x);
            }

        }
        public class Student{
            public string Name { get; set; }
            public List<int> Score { get; set; }
            public State Nowstate { get; set; }
            public Student(string _name = null, int _score = 0, State state = State.outschool)
            {
                Name = _name;
                Score = new List<int>();
                Score.Add(_score);
                Nowstate = state;
            }
            public void ChangeState()
            {
                if (this.Nowstate==State.outschool)
                {
                    this.Nowstate = State.inschool;
                }
                else this.Nowstate = State.outschool;
            }
            public void AddScore(int x)
            {
                Score.Add(x);
            }
            public void ShowScores()
            {
                Console.WriteLine($"{Name}'s scores are: ");
                foreach(var a in Score)
                {
                    Console.WriteLine(a);
                }
            }
            public void AttentionFunc(int x)
            {
                Console.WriteLine($"You have add a score in {Name} score list: {x}");
            }
        }
        public enum State { inschool,outschool }

        
        static void Main()
        {
            Student student = new Student("Yuchen",90,State.inschool);
            Attention attention = new Attention(student.AttentionFunc);
            Operation operation = new Operation();
            operation.Reminder += attention;
            operation.Reminder += student.AddScore;
            operation.Show(90);
            
        }
    }
}
